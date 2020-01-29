$(document).ready(function() {
    
    $("#age").mask('00');
    $("#cellphone").mask('(00) 0 0000-0000');
    $("#create").hide();
});

function validacaoEmail(field) {
    usuario = field.value.substring(0, field.value.indexOf("@"));
    dominio = field.value.substring(field.value.indexOf("@")+ 1, field.value.length);
    var ok = false;
     
    if ((usuario.length >=1) &&
        (dominio.length >=3) && 
        (usuario.search("@")==-1) && 
        (dominio.search("@")==-1) &&
        (usuario.search(" ")==-1) && 
        (dominio.search(" ")==-1) &&
        (dominio.search(".")!=-1) &&      
        (dominio.indexOf(".") >=1)&& 
        (dominio.lastIndexOf(".") < dominio.length - 1)) {
            document.getElementById("resultemail").innerHTML="E-mail válido";   
             ok = true;
    }
    else if(ok == false) {
        document.getElementById("resultemail").innerHTML="<font color='red'>E-mail inválido </font>";
    }

    if (field.value.length == 0 && ok == false){
        document.getElementById("resultemail").style.display = 'none';
    }
}

$("#btnadduser").click(function showform() {
    $("#create").show();
    $("#list").hide();
    
    window.scroll({       
        top: document
      .querySelector( '#create' )
        .offsetTop,       
        left: 0,
        behavior: 'smooth'
     });
});

$("#btncreate").click(function create() {

    model = {
        name : $("#name").val(),
        username : $("#username").val(),
        age : $("#age").val(),
        cellphone : $("#cellphone").val(),
        address : $("#address").val(),
        email : $("#email").val(),
        obs : $("#obs").val()
    }

    $.ajax({
        url: "Home/Create",
        data: model,
        type: 'POST',
        success: function(jsonResult){
        alert("Dados enviados!")
      },
    error: function (jsonResult) {
        console.log(jsonResult);
    }
});

    console.log(model);
});

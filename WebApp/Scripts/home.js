$(document).ready(function() {
    
    $("#age").mask('00');
    $("#cellphone").mask('(00) 0 0000-0000');
    $("#create").hide();
});

$("#btnadduser").click(function showform() {
    $("#create").fadeIn(1000);
    $("#list").hide();    
});

$("#comelist").click(function showlist() {
    $("#list").fadeIn(1000);
    $("#create").hide();    
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
        alert(jsonResult.message);
        console.log(jsonResult);
      },
    error: function (jsonResult) {
        console.log(jsonResult);
        }
    });

    console.log(model);
});

function validacaoEmail(text) {
    usuario = text.value.substring(0, text.value.indexOf("@"));
    dominio = text.value.substring(text.value.indexOf("@")+ 1, text.value.length);
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

    if (text.value.length == 0 && ok == false){
        document.getElementById("resultemail").style.display = 'none';
    }
}

function remove() {
    var userid = parseInt($("#txtuserid")[0].innerHTML);
    
    model = {
        id : userid
    }

    $.ajax({
        url: "Home/Delete",
        data: model,
        type: 'POST',
        success: function(jsonResult){
            alert(jsonResult.Message);
      },
    error: function (jsonResult) {
        alert(jsonResult.Message);
        console.log(jsonResult);
        }
    });
}

function edit() {
    
    model = {
        name : $("#txtuserid")[0].innerHTML,
        username : $("#txtusername")[0].innerHTML,
        age : $("#txtage")[0].innerHTML,
        cellphone : $("#txtcellphone")[0].innerHTML,
        address : $("#txtaddress")[0].innerHTML,
        email : $("#txtemail")[0].innerHTML,
        obs : $("#txtobs")[0].innerHTML
    }

    $.ajax({
        url: "Home/Edit",
        data: model,
        type: 'POST',
        success: function(jsonResult){
            alert(jsonResult.Message);
      },
    error: function (jsonResult) {
        alert(jsonResult.Message);
        console.log(jsonResult);
        }
    });
}



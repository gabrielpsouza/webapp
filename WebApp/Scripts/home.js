$(document).ready(function() {
    
    $("#age").mask('00');
    $("#cellphone").mask('(00) 0 0000-0000');

});

$("#btncreate").click(function create() {

    model = {
        name : $("#name").val(),
        username : $("#username").val(),
        age : $("#age").val(),
        cellphone : $("#cellphone").val(),
        adress : $("#address").val(),
        email : $("#email").val(),
        obs : $("#obs").val()
    }

    $.ajax({
        url: "Home/Create",
        data: model,
        typw: 'POST',
        success: function(jsonResult){
        alert("Dados enviados!")
      },
    error: function (jsonResult) {
        console.log(jsonResult);
    }
});

    console.log(model);
});

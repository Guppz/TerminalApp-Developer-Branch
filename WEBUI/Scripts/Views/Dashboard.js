$(document).ready(function () {
    var user = JSON.parse(sessionStorage.getItem("user"));
    $("#lblAdmin")[0].textContent = "Bienvenido " + user.Name + "!";
   

    
});


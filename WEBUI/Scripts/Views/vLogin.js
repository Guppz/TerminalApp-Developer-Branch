function vLogin() {
    this.service = 'User/Login';
    this.viewName = vLogin
    this.ctrlActions = new ControlActions();

    var loginSchema = {
        Email: function (value) {
            var regularEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!regularEx.test(value.toLowerCase())) {
                return "Por favor digite su correo electronico con el formato correcto";
            }
            return "";
        },
        Password: function (value) {
            if (parseInt(value.toString().length) < 8) {
                return "La contraseña debe de ser de almenos 8 caracteres";
            }
            return "";
        }, 
    };

    this.Login = function () {
        var userLogin = {};
        let isValid
        userLogin = this.ctrlActions.GetDataForm('frmLogin');
        isValid = this.ctrlActions.validateData(userLogin, loginSchema);
        

        if (isValid) {
            userLogin = this.getUser(this.service, userLogin);
            var inter = setInterval(function () {
                if ($('.alert').css("display") === "block") {
                    $('#Loading').modal("hide");
                    clearInterval(inter);

                } else {
                    if ($("#Loading").css("display") !== "block") {
                        $('#Loading').modal("toggle");
                    }
                }
            }, 1000);
        }
    };
    this.setModify = function () {

        $("#btnLogin").hide();
        $("#setModify").hide();
        $("#textPassword").hide();
        $("label[for='textPassword']").hide();
        $("#btnModify").show();
        $("#setLogin").show(); 
    };
    this.setLogin = function () {
        $("#btnLogin").show();
        $("#setModify").show();
        $("#textPassword").show();
        $("label[for='textPassword']").show();
        $("#btnModify").hide();
        $("#setLogin").hide(); 

    }

    this.Modify = function () {
        validateEmail = loginSchema["Email"];
        result = validateEmail($("#txtEmail").val());
        if (result === "") {
            window.location.replace(window.location.origin + "/Home/vModifyPassword?email=" + $("#txtEmail").val());
        } else {
            $("#txtEmail").addClass("is-invalid");
            $("div[columndataname = " + '"' + "Email" + '"' + "]")[0].textContent = result;

        }
    }
    this.getUser =  function (service, data) {
        var jqxhr = $.get(this.ctrlActions.GetUrlApiService(service) + "?parameters=" + JSON.stringify(data), data, function (response) {
            var ctrlActions = new ControlActions();
            
            sessionStorage.setItem('user', JSON.stringify(response.Data));
            ctrlActions.cleanForm(loginSchema);

            if (IsComapanyRole(response.Data)) {
                getCompany(response.Data, response.Data.ViewList[0].ViewName)
            } else {
                ctrlActions.ShowMessage('I', response.Message);
                window.location.replace(window.location.origin + "/Home/" + "HomeDashboard");
            }
            return response.Data;      
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
                return {};
            })
    };
    var getCompany = function (data , view) {
        this.ctrlActions = new ControlActions();
        var jqxhr = $.get(this.ctrlActions.GetUrlApiService("Company") +"?id="+ data.IdUser, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
            sessionStorage.setItem('company', JSON.stringify(response.Data));
            
            window.location.replace(window.location.origin + "/Home/" + "HomeDashboard");
           
         
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
                return {};
            })
    };

   

    

    var IsComapanyRole = function (user) {
        var role = user.Roleslist;
        for (var i = 0; i < role.length; i++) {
            if (role[i].IdRole === 38) {
                return true;
            }
        }
        return false;
    };
}


$(document).ready(function () {
    $("#btnModify").hide();
    $("#setLogin").hide();
  
    
   
});

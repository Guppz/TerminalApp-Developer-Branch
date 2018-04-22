function vModifyPassword() {
    this.service = 'User/ModifyPassword';
    this.ctrlActions = new ControlActions();
    this.email = null
    this.modifyPasswordSchema = {
        
        Password: function (value) {
            if (parseInt(value.toString().length) < 8) {
                return "La contraseña debe de ser de almenos 8 caracteres";
            }
            return "";
        },
        Name: function (value) {
            if (parseInt(value.toString().length) < 8) {
                return "La contraseña debe de ser de almenos 8 caracteres";
            }
            return "";
        },
        Identification: function (value) {

            if (value !== $("#textNewPass").val()) {
                return "Las contraseñas no son las mismas";
            }
            return "";
        }, 

    }


    this.Modify = function () {
        var userLogin = {};
        var email = this.urlParam("email");
        let isValid
        userLogin = this.ctrlActions.GetDataForm('frmLogin');
        userLogin.Email = email;
        
        isValid = this.ctrlActions.validateData(userLogin, this.modifyPasswordSchema);
        
        if (isValid) {
            userLogin.Identification = userLogin.Password;
            this.ctrlActions.PostToAPI(this.service, userLogin);


        }
    }
    this.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return decodeURI(results[1]) || 0;
        }
    }


}
$(document).ready(function () {
    var vMPass = new vModifyPassword();
    vMPass.email = vMPass.urlParam("email");
    if (vMPass.email === null) {
        window.location.replace(window.location.origin + "/Home/vLogin");
    }
});

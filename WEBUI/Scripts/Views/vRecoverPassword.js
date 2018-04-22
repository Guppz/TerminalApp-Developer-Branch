function vRecoverPassword() {
    this.service = 'User/RecoverPassword';
    this.ctrlActions = new ControlActions();
    this.RecoverSchema = {
        Email: function (value) {
            var regularEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!regularEx.test(value.toLowerCase())) {
                return "Por favor digite su correo electronico con el formato correcto";
            }
            return "";
        }, 
    }

    this.Recover = function () {

        var email = {};
        let isValid
        email = this.ctrlActions.GetDataForm('frmLogin');

        isValid = this.ctrlActions.validateData(email, this.RecoverSchema);
       
        if (isValid) {

            this.ctrlActions.PostToAPI(this.service, email);


        }
    } 

}
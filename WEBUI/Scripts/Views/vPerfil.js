
function vPerfil() {

    this.service = 'User';
    this.viewName = "vPerfil";
    this.ctrlActions = new ControlActions();
    this.columns = "PkIdUser,Name,LastName,Email,RoleList";

    var userSchema = {
        Identification: function (value) {
            if (parseInt(value.toString().length) < 9 || parseInt(value.toString().length) > 13) {
                return "La cedula tiene que ser de entre 9 a 13 digitos";
            }
            return "";
        },
        Name: function (value) {
            if (value === "") {
                return "Digite su nombre por favor";
            }
            return "";
        },
        LastName: function (value) {
            if (value === "") {
                return "Digite su apellido por favor";
            }
            return "";
        },
        Email: function (value) {
            var regularEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!regularEx.test(value.toLowerCase())) {
                return "Por favor digite su correo electronico con el formato correcto";
            }
            return "";
        },
        
    }




    

    this.Update = function () {

        var userData = {};
        let data = JSON.parse(sessionStorage.getItem("user"));
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');

        userData.Identification = userData.Identification.toString();
        //Hace el post al create
        isValid = this.ctrlActions.validateData(userData, userSchema)
        if (isValid) {
            data.Identification = userData.Identification;
            data.Name = userData.Name;
            data.LastName = userData.LastName;
            data.Email = userData.Email;
            this.ctrlActions.PutToAPI(this.service, data);
            sessionStorage.setItem('user', JSON.stringify(data));
            var inter = setInterval(function () {
                if ($('.alert').css("display") === "block") {
                    $('#Loading').modal("hide");
                    clearInterval(inter);
                    ctrlActions = new ControlActions();
                    ctrlActions.BindFields('frmEdition', data);

                } else {
                    if ($("#Loading").css("display") !== "block") {
                        $('#Loading').modal("toggle");
                    }
                }
            }, 1000);
        }

        //Refresca la tabla


    }

    

    this.BindFields = function () {
        let data = JSON.parse(sessionStorage.getItem("user"));



        this.ctrlActions.BindFields('frmEdition', data);

    }

 


}

//ON DOCUMENT READY
$(document).ready(function () {
    /*  let user = JSON.parse(sessionStorage.getItem("user"));
      let canEnter = false;
      let page = new vPerfil();
      let pageview = page.viewName;
      if (user === null) {
  
          window.location.replace(window.location.origin);
      }
      let views = user.ViewList;
      for (var i = 0; i < views.length; i++) {
          if (views[i].ViewName === pageview) {
              canEnter = true;
          }
  
      }
  
      if (!canEnter) {
          window.location.replace(window.location.origin);
      } else {*/
    var vuser = new vPerfil();
    vuser.BindFields();
    // }




});


function vUser() {
    this.tblUserId = 'tblUser';
    this.service = 'User';
    this.viewName = "vUser";
    this.ctrlActions = new ControlActions();
    this.columns = "PkIdUser,Name,LastName,Email,RoleList";
    self = this;

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
        Roleslist: function (value) {
            if (value.length === 0) {
                return "Por favor seleccione al menos un rol"
            } else {

                for (var i = 0; i < value.length; i++) {
                    if (value.length >= 2 && value[i].IdRole === 1) {
                        return "El usuario no puede ser mas que full Admin";
                    }
                    if (value.length >= 2  && value[i].IdRole === 36) {
                        return "El usuario no puede ser mas que cliente"
                    }
                }
               
            }

            return "";
        }, Terminal: function (value) {
            if (value === "") {
                var roles = getCheckbox();

                for (var i = 0; i < roles.length; i++) {
                    if (roles[i].IdRole === 1 || roles[i].IdRole === 36) {
                        return "";
                    }
                    
                }
                return "Por favor seleccione terminal"
            } 
            return "";
        },
        BirthDateString: function (value) {
            var birthday = new Date(value);
            var today = new Date();
            var years = today.getFullYear() - birthday.getFullYear();
            if (birthday > today) {
                return "La fecha no puede ser mayor a la de hoy";
            }
            // Reset birthday to the current year.  
            birthday.setFullYear(today.getFullYear());

            // If the user's birthday has not occurred yet this year, subtract 1.  
            if (today < birthday) {
                years--;
            } 
            
            
            if (value === '') {
                return 'Por favor ingrese la fecha de nacimiento';
            }
            
            if (years < 18) {
                return "El usuario tiene que ser mayor a 18 años";

            }
            return "";
            
        }
    };

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, false );
    };
    this.RetrieveByTerminal = function (idTerminal) {
        this.ctrlActions.FillTable(this.service + '/' + idTerminal, this.tblUserId, false)
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, true);
    };

    this.Create = function () {
        var userData = {};
        let isValid;

        userData = this.ctrlActions.GetDataForm('frmEdition');
        userData.Roleslist = getCheckbox();
        userData.Identification = userData.Identification.toString();
        isValid = this.ctrlActions.validateData(userData, userSchema);
        userData.UserTerminal = { IdTerminal: userData.Terminal };
        userData.BirthDate = new Date(userData.BirthDateString)
        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, userData);
            $('#myModal').modal("hide");
            cleanCheckbox();
            this.ctrlActions.cleanFormSchema(userSchema);
            sessionStorage.setItem('tempUser', JSON.stringify(userData));
            win.OpenModal();
            this.ReloadTable();
        }
    };
    this.SatatusAlertDesactive = function () {
        var userData = {};
        userData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea desactivar esta usuario?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Desactivar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {

                    self.Delete();
                    swal("Desactivado!", "Este usuario ha sido eliminado con exito.", "success")



                }
            });

    };
    this.SatatusAlertActive = function () {
        var userData = {};
        userData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea activar este usuario!",
            type: "warning",
            background: "#95c9d4",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Activar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    if (userData.Status == 1) {
                        $('#myModal').modal("hide");
                        swal({
                            type: 'error',
                            title: 'Oops...',
                            text: 'Este usuario ya se encuentra activada!',
                        })

                    }
                    else {
                        self.Delete();
                        swal("Activado!", "Este usuario ha sido activado con exito.", "success");
                    }
                }
            });

    }


    this.Create = function () {
        var userData = {};
        let isValid;

        userData = this.ctrlActions.GetDataForm('frmEdition');
        userData.Roleslist = getCheckbox();
        userData.Identification = userData.Identification.toString();
        isValid = this.ctrlActions.validateData(userData, userSchema);
        userData.UserTerminal = { IdTerminal: userData.Terminal };
        userData.BirthDate = new Date(userData.BirthDateString);
        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, userData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El usuario se ha registrado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
            cleanCheckbox();
            this.ctrlActions.cleanFormSchema(userSchema);
            sessionStorage.setItem('tempUser', JSON.stringify(userData));
            var win = window.open('/Home/vAddCards', '_self');
            win.OpenModal();
            this.ReloadTable();
        }
    };
    this.Update = function () {
        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        userData.Roleslist = getCheckbox();
        userData.Identification = userData.Identification.toString();
        userData.IdUser = parseInt($("#btnUpdate").attr("name"));
        isValid = this.ctrlActions.validateData(userData, userSchema)
        userData.UserTerminal = { IdTerminal: userData.Terminal };
        userData.BirthDate = new Date(userData.BirthDateString);

        if (isValid) {
            this.ctrlActions.PutToAPI(this.service, userData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El usuario se ha actualizado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
            cleanCheckbox();
            this.ctrlActions.cleanFormSchema(userSchema);
            this.ReloadTable();
        }
    };

    this.Delete = function () {
        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        userData.IdUser = parseInt($("#btnUpdate").attr("name"));
        userData.Status = parseInt($("#btnActivate").attr("name"));
        this.ctrlActions.DeleteToAPI(this.service, userData);
        
        $('#myModal').modal("hide");
        cleanCheckbox();
        this.ctrlActions.cleanForm(userSchema);
        this.ReloadTable();
    };
    this.FormatDate = function (date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    };
    this.updateButtons = function (value) {
        if (value === 1) {

            $('#btnCreate').show();
            $('#btnUpdate').hide();
            $('#btnActivate').hide();
            $('#btnDesactive').hide();

        } else {

            $('#btnCreate').hide();
            $('#btnUpdate').show();

            $('#btnActivate').show();
            $('#btnDesactive').show();

        }
    };
    this.BindFields = function (data) {
        this.ctrlActions.cleanFormSchema(userSchema);
        cleanCheckbox();
        self.updateButtons(2);

        if (data.Status == 1) {
            $('#btnActivate').hide();
            $('#btnDesactive').show();

        } else if (data.Status == 0) {
            $('#btnActivate').show();
            $('#btnDesactive').hide();


        }
        var dat = data.BirthDateString.split("/");
        var date = new Date(dat[2],dat[1]-1,dat[0]);
        var sDate = this.FormatDate(date);
        $('#myModal').modal("toggle");
        $('.modal-title').text('Editar Usuario');

        $('#txtPkIdUser').attr('disabled', 'disabled');
    
        $('#btnUpdate').attr('name', data.IdUser);
        $('#btnDesactive').attr('name', data.Status);
        $('#btnActivate').attr('name', data.Status);

        $("#terminalComboBox").find("option").first().prop("selected", true);
        $("#terminalComboBox").val("");
        $("#terminalComboBox").removeClass("is-invalid");

        
        fillCheckbox(data);

        this.ctrlActions.BindFields('frmEdition', data);
        $('#txtBDate').val(sDate);
        if (data.UserTerminal.IdTerminal.toString() === "-1") {
            $("#terminalComboBox").val("");
        } else {
            $("#terminalComboBox").val(data.UserTerminal.IdTerminal.toString());
        }
       
    };

    var fillCheckbox = function (data) {
        let checkboxes = $("#Role :input");
        for (var i = 0; i < data.Roleslist.length; i++) {
            for (var j = 0; j < checkboxes.length; j++) {
                if (data.Roleslist[i].IdRole === parseInt(checkboxes[j].value)) {
                    $(checkboxes[j]).prop("checked", true);
                }
            }
        }
    };

    var getCheckbox = function () {
        let Roles = [];
        let checkboxes = $("#Role :input");

        for (var j = 0; j < checkboxes.length; j++) {
            if ($(checkboxes[j]).prop("checked") === true) {
                let Role = {};
                Role['IdRole'] = parseInt($(checkboxes[j])[0].value);
                Roles.push(Role);
            }
        }
        return Roles;
    };

    var cleanCheckbox = function () {
        let checkboxes = $("#Role :input");

        for (var j = 0; j < checkboxes.length; j++) {
            $(checkboxes[j]).prop("checked", false);
        }
    };

    this.OpenModal = function () {
        $("#terminalComboBox").val("");
        self.updateButtons(1);

        cleanCheckbox();
        this.ctrlActions.cleanFormSchema(userSchema);
        $('#txtPkIdUser').removeAttr('disabled');
        $('#myModal').modal("toggle");
        $('.modal-title').text('Registrar Usuario');

        $("#terminalComboBox").find("option").first().prop("selected", true);
        $("#terminalComboBox").val("");
        $("#terminalComboBox").removeClass("is-invalid");


    };
}

$(document).ready(function () {
    let user = JSON.parse(sessionStorage.getItem("user"));
    let canEnter = false;
    let page = new vUser();
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
    } else {
        
        var vuser = new vUser();
        if (user.UserTerminal.IdTerminal === -1) {
            vuser.RetrieveAll();
        } else {
            vuser.RetrieveByTerminal(user.UserTerminal.IdTerminal)
        }
        
    }
});




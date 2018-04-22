window.currentTypeData;

function vRole() {

    this.tblRoleId = 'tblRole';
    this.service = 'Role';
    this.serviceUpdateActivacion = 'Role/PutActivacion';

    self = this;

    this.ctrlActions = new ControlActions();
    var RoleSchema = {

        Name: function (value) {
            if (value === "") {
                return "Digite el nombre del rol por favor";
            }
            return "";
        },

        ViewsPerRole: function (value) {
            if (value.length === 0) {
                return "Por favor seleccione al menos una vista"
            }
            return "";
        }




    }

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblRoleId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblRoleId, true);
    }

    this.Create = function () {

        var RoleData = {};
        let isValid;
        RoleData = this.ctrlActions.GetDataForm('frmEdition');
        RoleData.ViewsPerRole = getCheckbox();
        console.log(RoleData);
        isValid = this.ctrlActions.validateData(RoleData, RoleSchema);

        if (isValid) {
            RoleData.Users = { IdUsers: RoleData.Users };
            this.ctrlActions.PostToAPI(this.service, RoleData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El rol se ha registrado con exito!',
                showConfirmButton: false,
                timer: 1500
            })

            $('#myModal').modal("hide");
            cleanCheckbox();

            this.ReloadTable();
        }
    }

    this.Update = function () {

        var RoleData = {};
        let isValid;
        RoleData = this.ctrlActions.GetDataForm('frmEdition');
        RoleData.ViewsPerRole = getCheckbox();
        RoleData.IdRole = parseInt($("#btnUpdate").attr("name"));
        isValid = this.ctrlActions.validateData(RoleData, RoleSchema);
        if (isValid) {
            this.ctrlActions.PutToAPI(this.service, RoleData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El rol se ha actualizado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(RoleSchema);
            cleanCheckbox();

        }
        this.ReloadTable();
    }
    this.SatatusAlertActive = function () {
        var RoleData = {};
        RoleData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea activar este rol!",
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
                    if (RoleData.Status == 1) {
                        $('#myModal').modal("hide");
                        swal({
                            type: 'error',
                            title: 'Oops...',
                            text: 'Este rol ya se encuentra activado!',
                        })

                    }
                    else {
                        self.Activate();
                        swal("Activado!", "Este rol ha sido activado con exito.", "success");
                    }
                }
            });

    }

    this.SatatusAlertDesactive = function () {
        var RoleData = {};
        RoleData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea desactivar este rol!",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Desactivar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    if (RoleData.Status == 0) {
                        $('#myModal').modal("hide");
                        swal({
                            background: "#95c9d4",
                            type: 'error',
                            title: 'Oops...',
                            text: 'Este rol ya se encuentra desactivo!',
                        })

                    }
                    else {
                        self.Desactivate();
                        swal("Desactivado!", "Este rol ha sido desactivado con exito.", "success");
                    }
                }
            });

    }


    this.Activate = function () {


        var RoleData = {};
        let isValid;
        RoleData = this.ctrlActions.GetDataForm('frmEdition');
        isValid = true;
        if (isValid) {

            this.ctrlActions.PutToAPI(this.serviceUpdateActivacion, RoleData);
            RoleData.IdRole = parseInt($("#btnActivate").attr("name"));

            $('#myModal').modal("hide");
            this.ReloadTable();

        }
    }

    this.Desactivate = function () {


        var RoleData = {};
        let isValid;
        RoleData = this.ctrlActions.GetDataForm('frmEdition');
        isValid = true;
        if (isValid) {
            this.ctrlActions.DeleteToAPI(this.service, RoleData);
            RoleData.IdRole = parseInt($("#btnDesactive").attr("name"));


            $('#myModal').modal("hide");
        }
        this.ReloadTable();
    }

    this.BindFields = function (data) {

        $('#txtIdRole').parent('div').hide();
        currentTypeData = data;
        cleanCheckbox();
        this.ctrlActions.cleanForm(RoleSchema);
        self.updateButtons(2);

        if (data.Status == 1) {
            $('#btnActivate').hide();
            $('#btnDesactive').show();

        } else if (data.Status == 0) {
            $('#btnActivate').show();
            $('#btnDesactive').hide();


        }

        $('.modal-title').text('Editar Rol');
        $('#txtIdRole').attr('disabled', 'disabled');
        $('#btnUpdate').attr('name', data.IdRole);
        $('#Activate').attr('name', data.IdRole);
        $('#Desactivate').attr('name', data.IdRole);

        $('#myModal').modal("toggle");
        this.ctrlActions.BindFields('frmEdition', data);
        fillCheckbox(data);
    }


    this.delete = function () {
        self.deleteType();
    }

    this.OpenModal = function () {
        this.ctrlActions.cleanForm(RoleSchema);
        $('#frmEdition')[0].reset();
        $('#txtIdRole').attr('disabled', 'disabled');

        $('#txtIdRole').parent('div').hide();
        $('.modal-title').text('Registrar Rol');
        $('#myModal').modal("toggle");
        self.updateButtons(1);

        cleanCheckbox();

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



}



var fillCheckbox = function (data) {
    let checkboxes = $("#View :input");
    for (var i = 0; i < data.ViewsPerRole.length; i++) {
        for (var j = 0; j < checkboxes.length; j++) {
            if (data.ViewsPerRole[i].IdView === parseInt(checkboxes[j].value)) {
                $(checkboxes[j]).prop("checked", true);
            }
        }
    }
}

var getCheckbox = function () {
    let Views = [];
    let checkboxes = $("#View :input");

    for (var j = 0; j < checkboxes.length; j++) {
        if ($(checkboxes[j]).prop("checked") === true) {
            let View = {};
            View['IdView'] = parseInt($(checkboxes[j])[0].value);
            Views.push(View);
        }
    }
    return Views;
}

var cleanCheckbox = function () {
    let checkboxes = $("#View :input");

    for (var j = 0; j < checkboxes.length; j++) {
        $(checkboxes[j]).prop("checked", false);
    }
}


//ON DOCUMENT READY
$(document).ready(function () {

    var vrole = new vRole();
    vrole.RetrieveAll();

});


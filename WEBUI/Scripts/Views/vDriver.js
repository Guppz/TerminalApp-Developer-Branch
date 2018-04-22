window.currentTypeData;

function vDriver() {

    this.tblDriverId = 'tblDriver';
    this.service = 'Driver';
    this.serviceUpdateActivacion = 'Driver/PutActivacion';
    self = this;

    this.ctrlActions = new ControlActions();
    var driverSchema = {

        CardNumber: function (value) {
            if (parseInt(value.toString().length) < 9 || parseInt(value.toString().length) > 13) {
                return "La cedula tiene que ser de entre 9 a 13 digitos";
            }
            return "";
        },
        Name: function (value) {
            if (value === "") {
                return "Digite un nombre  por favor";
            }
            return "";
        },

        Company: function (value) {
            if (value === "") {
                return "Por favor selccione una compañia";
            }
            return "";
        }


    }

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblDriverId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblDriverId, true);
    }

    this.Create = function () {
        var driverData = {};
        let isValid;
        driverData = this.ctrlActions.GetDataForm('frmEdition');
        isValid = this.ctrlActions.validateData(driverData, driverSchema);

        if (isValid) {
            driverData.Company = { IdCompany: driverData.Company };
            this.ctrlActions.PostToAPI(this.service, driverData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El chofer se ha registrado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
            $('#companyComboBox').val("");
            $('#txtIdDriver').val("");
            $('#txtName').val("");
            this.ctrlActions.cleanForm(driverSchema);
         
            $(this).delay(1500).queue(function () {
                this.ctrlActions.cleanForm('frmEdition', driverSchema);
            });
        }
        this.ReloadTable();
    }

    this.Update = function () {

        var driverData = {};
        let isValid;
        driverData = this.ctrlActions.GetDataForm('frmEdition');
        driverData.CardNumber = parseInt($("#btnUpdate").attr("name"));
        isValid = this.ctrlActions.validateData(driverData, driverSchema);

        if (isValid) {
            driverData.Company = { IdCompany: driverData.Company };
            this.ctrlActions.PutToAPI(this.service, driverData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El chofer se ha actualizado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
            $('#companyComboBox').val("");
            $('#txtIdDriver').val("");
            $('#txtName').val("");
            this.ctrlActions.cleanForm(driverSchema);
            $(this).delay(1500).queue(function () {
                this.ctrlActions.cleanForm('frmEdition', driverSchema);
            });
        }
        this.ReloadTable();
    }

    this.Activate = function () {

        var driverData = {};
        let isValid;
        driverData = this.ctrlActions.GetDataForm('frmEdition');
        driverData.CardNumber = parseInt($("#btnActivate").attr("name"));

        isValid = true;
        if (isValid) {
            this.ctrlActions.PutToAPI(this.serviceUpdateActivacion, driverData);
            $('#myModal').modal("hide");
        }
        this.ReloadTable();

    }
    this.Desactivate = function () {

        var driverData = {};
        let isValid;
        driverData = this.ctrlActions.GetDataForm('frmEdition');
        driverData.CardNumber = parseInt($("#btnDesactive").attr("name"));
        this.ctrlActions.DeleteToAPI(this.service, driverData);
        $('#myModal').modal("hide");
        //Refresca la tabla
        this.ReloadTable();

    }

    this.SatatusAlertActive = function () {
        var driverData = {};
        driverData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea activar este chofer!",
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
                    if (driverData.Status == 1) {
                        $('#myModal').modal("hide");
                        swal({
                            type: 'error',
                            title: 'Oops...',
                            text: 'Este chofer ya se encuentra activado!',
                        })

                    }
                    else {
                        self.Activate();
                        swal("Activado!", "Este chofer ha sido activado con exito.", "success");
                    }
                }
            });

    }
    this.SatatusAlertDesactive = function () {
        var driverData = {};
        driverData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea desactivar este chofer!",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Desactivar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    if (driverData.Status == 0) {
                        $('#myModal').modal("hide");
                        swal({
                            background: "#95c9d4",
                            type: 'error',
                            title: 'Oops...',
                            text: 'Este chofer ya se encuentra desactivo!',
                        })

                    }
                    else {
                        self.Desactivate();
                        swal("Desactivado!", "Este chofer ha sido desactivado con exito.", "success");
                    }
                }
            });

    }
    this.BindFields = function (data) {
        currentTypeData = data;
        this.ctrlActions.cleanForm(driverSchema);
        self.updateButtons(2);
        if (data.Status == 1) {
            $('#btnActivate').hide();
            $('#btnDesactive').show();

        } else if (data.Status == 0) {
            $('#btnActivate').show();
            $('#btnDesactive').hide();
        }
        $('.modal-title').text('Editar Chofer');
        $('#myModal').modal("toggle");
        $('#txtIdentidad').attr('disabled', 'disabled');
        $('#btnCreate').attr('disabled', 'disabled');
        $('#btnUpdate').removeAttr('disabled');
        $('#btnActivate').removeAttr('disabled');
        $('#btnDesactive').removeAttr('disabled');
        $('#btnUpdate').attr('name', data.CardNumber);
        $('#btnCreate').attr('name', data.CardNumber);
        $('#btnActivate').attr('name', data.CardNumber);
        $('#btnDesactive').attr('name', data.CardNumber);
        this.ctrlActions.BindFields('frmEdition', data);
        $('#companyComboBox').val(data.Company.IdCompany);
    

    }
    this.OpenModal = function () {
        this.ctrlActions.cleanForm(driverSchema);
        self.updateButtons(1);

        $('#txtIdentidad').removeAttr('disabled', 'disabled');
        $('#txtIdentidad').val("");
        $('#myModal').modal("toggle");
        $('#btnUpdate').attr('disabled', 'disabled');
        $('#btnDesactive').attr('disabled', 'disabled');
        $('#btnActivate').attr('disabled', 'disabled');
        $('#btnActivate').attr('disabled', 'disabled');
        $('#btnCreate').removeAttr('disabled');
        $('#companyComboBox').val("");
        $('#txtIdDriver').val("");
        $('#txtName').val("");
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

//ON DOCUMENT READY
$(document).ready(function () {

    var vdriver = new vDriver();
    vdriver.RetrieveAll();

});


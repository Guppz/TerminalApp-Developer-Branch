window.currentTypeData;

function vFine() {

    this.URL_API = "http://localhost:61693/api/";
    this.tblFineId = 'tblFine';
    this.service = 'Fine';
    this.ctrlActions = new ControlActions();
    self = this;

    var fineSchema = {

        FineDescription: function (value) {
            if (value === "") {
                return "Digite una descripción por favor";
            }
            return "";
        },


        Company: function (value) {
            if (value === "") {
                return "Por favor selccione una compañia";
            }
            return "";
        },
        Terminal: function (value) {
            if (value === "") {
                return "Por favor selccione una terminal";
            }
            return "";
        },
        FType: function (value) {
            if (value === "") {
                return "Se debe de registrar primeramente un tipo de multa o por favor selccione un tipo de multa, si ya se encuentran registradas en el sitema";
            }
            return "";
        }
    }

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblFineId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblFineId, true);
    }

    this.getUrlApiService = function (service) {
        return this.URL_API + service;
    };

    this.Create = function () {
        var fineData = {};
        let isValid;
        fineData = this.ctrlActions.GetDataForm('frmEdition');
        isValid = this.ctrlActions.validateData(fineData, fineSchema);

        if (isValid) {
            fineData.Company = { IdCompany: fineData.Company };
            fineData.Terminal = { IdTerminal: fineData.Terminal };
            fineData.FType = { IdType: fineData.FType };
            this.ctrlActions.PostToAPI(this.service, fineData);

            $('#myModal').modal("hide");
            $('#txtDescription').val("");
            $('#companyComboBox').val("");
            $('#terminalComboBox').val("");
            $('#fineTypeComboBox').val("");
            $(this).delay(1500).queue(function () {
                this.ctrlActions.cleanForm('frmEdition', fineSchema);
                this.ReloadTable();

            });
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'La multa se ha registrado con exito!',
                showConfirmButton: false,
                timer: 1500
            })          
        }
    };

    this.Update = function () {

        var fineData = {};
        let isValid;
        fineData = this.ctrlActions.GetDataForm('frmEdition');
        fineData.IdFine = parseInt($("#btnUpdate").attr("name"));
        isValid = this.ctrlActions.validateData(fineData, fineSchema);

        if (isValid) {
            fineData.Company = { IdCompany: fineData.Company };
            fineData.Terminal = { IdTerminal: fineData.Terminal };
            fineData.FType = { IdType: fineData.FType };
            this.ctrlActions.PutToAPI(this.service, fineData);

            $('#myModal').modal("hide");
            $('#txtDescription').val("");
            $('#companyComboBox').val("");
            $('#terminalComboBox').val("");
            $('#fineTypeComboBox').val("");
            $(this).delay(1500).queue(function () {
                this.ctrlActions.cleanForm('frmEdition', fineSchema);
                this.ReloadTable();

                swal({
                    type: 'success',
                    background: "#95c9d4",
                    title: 'La multa se ha actualizado con exito!',
                    showConfirmButton: false,
                    timer: 1500
                })
            });
        }
    }

    this.Delete = function () {

        var fineData = {};
        let isValid;
        fineData.IdFine = parseInt($("#btnDelete").attr("name"));

        this.ctrlActions.DeleteToAPI(this.service, fineData);
        $('#myModal').modal("hide");
        $('#txtDescription').val("");
        $('#companyComboBox').val("");
        $('#terminalComboBox').val("");
        $('#fineTypeComboBox').val("");
        this.ctrlActions.cleanForm(fineSchema);
        this.ReloadTable();

    };
    this.SatatusAlertDesactive = function () {
        var fineData = {};
        fineData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar esta multa?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Eliminar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    self.Delete();
                    swal("Eliminado!", "Esta multa se ha  eliminado con exito.", "success");
                }
            });
    }

    this.BindFields = function (data) {
        $('#txtIdFine').parent('div').hide();
        currentTypeData = data;
        this.ctrlActions.cleanForm(fineSchema);

        self.updateButtons(2);
        $('.modal-title').text('Editar Multa');
        $('#txtIdFine').attr('disabled', 'disabled');
        $('#btnUpdate').attr('name', data.IdFine);
        $('#btnDelete').attr('name', data.IdFine);
        $('#myModal').modal("toggle");

        this.ctrlActions.BindFields('frmEdition', data);
        $('#companyComboBox').val(data.Company.IdCompany);
        $('#terminalComboBox').val(data.Terminal.IdTerminal);
        $('#fineTypeComboBox').val(data.FType.IdType);
    }

    this.OpenModal = function () {
        this.ctrlActions.cleanForm(fineSchema);
        $('#frmEdition')[0].reset();
        $('#txtIdFine').attr('disabled', 'disabled');

        $('#txtIdFine').parent('div').hide();
        $('.modal-title').text('Registrar Multa');
        $('#myModal').modal("toggle");
        self.updateButtons(1);

        $('#companyComboBox').val("");
        $('#terminalComboBox').val("");
        $('#fineTypeComboBox').val("");
    };

    this.updateButtons = function (value) {
        if (value === 1) {
            $('#btnCreate').show();
            $('#btnUpdate').hide();
            $('#btnDelete').hide();
        } else {
            $('#btnCreate').hide();
            $('#btnUpdate').show();
            $('#btnDelete').show();
        }
    };
}


$(document).ready(function () {
    var vfine = new vFine();
    vfine.RetrieveAll();

});




window.currentTypeData;

function VSanctionType() {

    this.tblComplaintId = 'tblSanctionsTypes';
    this.service = 'sanction';
    this.ctrlActions = new ControlActions();
    this.columns = "IdType~Name~Description~Cost";
    self = this;

    var typeSchema = {
        Name: function (value) {
            if (value === "") {
                return "Digite el nombre del tipo por favor";
            }
            return "";
        },
        Description: function (value) {
            if (value === "") {
                return "Digite la descripción por favor";
            }
            return "";
        },
        Cost: function (value) {
            if (!/^(?=\d*[1-9])\d+$/.test(parseInt(value))) {
                return "Por favor digite un valor númerico mayor a cero";
            }
            return "";
        }
    };

    this.retrieveAll = function () {
        this.ctrlActions.FillTable(this.service + '/RetrieveSanctionsTypes', this.tblComplaintId, false, );
    };

    this.reloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblComplaintId, true);
    };

    this.create = function () {
        var typeData = this.ctrlActions.GetDataForm('frmEdition');
        var isValid = this.ctrlActions.validateData(typeData, typeSchema);

        if (isValid) {
            this.ctrlActions.PostToAPI(this.service + '/CreateSanctionType', typeData);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(typeSchema);
            this.reloadTable();
            self.showAlertModal('El tipo de sanción se ha registrado con exito!');
        }
    };

    this.update = function () {
        var typeData = this.ctrlActions.GetDataForm('frmEdition');
        var isValid = this.ctrlActions.validateData(typeData, typeSchema);
        var notDuplicatedInfo;

        if (isValid) {
            notDuplicatedInfo = this.verifyIsNotDuplicatedInfo(typeData);
        }

        if (isValid && notDuplicatedInfo) {
            this.ctrlActions.PutToAPI(this.service + '/UpdateSanctionType', typeData);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(typeSchema);
            this.reloadTable();
            self.showAlertModal('El tipo de sanción se ha actualizado con exito!');
        }
    };

    this.delete = function () {
        self.deleteType();
    };

    this.deleteType = function () {
        var typeData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.DeleteToAPI(this.service + '/DeleteSanctionType', typeData);
        self.cancelProcess();
        this.reloadTable();
        self.showAlertModal('El tipo de sanción se ha eliminado con exito!');
    };

    this.cancelProcess = function () {
        $('#myModal').modal('hide');
        $('#alertBox').modal('hide');
    };

    this.verifyIfHasDependencies = function () {
        var typeData = this.ctrlActions.GetDataForm('frmEdition');
        var dependencies = this.ctrlActions.getSanctionsByType(this.service, '/RetrieveSanctionsByType', typeData.IdType);

        if (dependencies.length > 0) {
            $('#myModal').hide();
            $('#alertBoxHeader').text('Dependencias de Información');
            $('#alertBox').modal("toggle");
            self.cancelProcess();
        } else {
            self.deleteType();
        }
    };

    this.verifyIsNotDuplicatedInfo = function (typeData) {
        var isValid = true;
        var object;

        var description = currentTypeData.Description;
        var name = currentTypeData.Name;

        if (name != typeData.Name) {
            object = this.ctrlActions.verifyIsNotDuplicatedSanctionType(this.service, '/RetrieveSanctionTypeByName', typeData.Name);
            if (object != null) {
                $('#myModal').modal("hide");
                this.ctrlActions.ShowMessage('E', 'El nombre del tipo ya existe');
                isValid = false;
            }
        }

        if (description != typeData.Description) {
            object = this.ctrlActions.verifyIsNotDuplicatedSanctionType(this.service, '/RetrieveSanctionTypeByDescription', typeData.Description);
            if (object != null) {
                $('#myModal').modal("hide");
                this.ctrlActions.ShowMessage('E', 'La descripción del tipo ya existe');
                isValid = false;
            }
        }

        return isValid;
    };

    this.bindFields = function (data) {
        currentTypeData = data;
        self.cleanFields();
        $('#txtPkIdType').parent('div').hide();
        this.ctrlActions.cleanForm(typeSchema);
        self.updateButtons(2);
        $('.modal-title').text('Editar Tipo de Sanción');
        $('#myModal').modal("toggle");
        this.ctrlActions.BindFields('frmEdition', data);
    };

    this.OpenModal = function () {
        self.cleanFields();
        this.ctrlActions.cleanForm(typeSchema);
        $('#txtPkIdType').parent('div').hide();
        $('.modal-title').text('Registrar Tipo de Sanción');
        $('#myModal').modal("toggle");
        self.updateButtons(1);
    };

    this.cleanFields = function () {
        $('#txtName').val('');
        $('#typeDescription').val('');
        $('#typeCost').val('');
    }

    this.updateButtons = function (value) {
        if (value === 1) {
            $('#btnCreate').show();
            $('#btnCancel').show();
            $('#btnUpdate').hide();
            $('#btnDelete').hide();
        } else {
            $('#btnCreate').hide();
            $('#btnCancel').hide();
            $('#btnUpdate').show();
            $('#btnDelete').show();
        }
    };

    self.showAlertModal = function (message) {
        var inter = setInterval(function () {
            if ($('.alert').hasClass('alert-success') && $('.alert').css("display") === "block") {
                swal({
                    type: 'success',
                    background: "#95c9d4",
                    title: message,
                    showConfirmButton: false,
                    timer: 1500
                })

                clearInterval(inter);
            }
        }, 1000);
    };

    self.alertDeactivationStatus = function () {
        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar este Tipo de Sanción?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Eliminar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    self.verifyIfHasDependencies();
                }
            });
    }
}

$(document).ready(function () {
    var vType = new VSanctionType();
    vType.retrieveAll();
});


window.currentCompanyInfo;

function VCompany() {
    this.service = 'company';
    this.ctrlActions = new ControlActions();
    this.tblCompanyId = 'tblCompanies';
    self = this;
    this.columns = "CorpIdentification~Name~OwnerName~Email";

    var companySchema = {
        CorpIdentification: function (value) {
            if (!/^(?=\d*[1-9])\d+$/.test(parseInt(value))) {
                return "Por favor digite un valor númerico mayor a cero y sin guiones";
            }
            return "";
        },
        OwnerName: function (value) {
            if (value === "") {
                return "Introduzca el nombre del propietario por favor";
            }
            return "";
        },
        Name: function (value) {
            if (value === "") {
                return "Introduzca el nombre del propietario por favor";
            }
            return "";
        },
        Email: function (value) {
            var regularEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!regularEx.test(value.toLowerCase())) {
                return "Por favor digite el correo electrónico con el formato correcto";
            }
            return "";
        }
    };

    this.retrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblCompanyId, false);
    };

    this.reloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCompanyId, true);
    };

    this.create = function () {
        var companyData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(companyData, companySchema);

        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, companyData);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(companySchema);
            this.reloadTable();
            self.showAlertModal('La compañía se ha registrado con exito!');

        }
    };

    this.update = function () {
        var companyData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(companyData, companySchema);
        let isNotDuplicated = self.validateIsNotDuplicated(companyData);
        companyData.IdCompany = currentCompanyInfo.IdCompany;

        if (isValid && isNotDuplicated) {
            this.ctrlActions.PutToAPI(this.service, companyData);
            self.showAlertModal();
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(companySchema);
            this.reloadTable();
            self.showAlertModal('La compañía se ha actualizado con exito!');
        }
    };

    this.delete = function () {
        var companyData = this.ctrlActions.GetDataForm('frmEdition');
        companyData.IdCompany = currentCompanyInfo.IdCompany;
        this.ctrlActions.DeleteToAPI(this.service, companyData);
        $('#myModal').modal("hide");
        this.ctrlActions.cleanForm(companySchema);
        this.reloadTable();
    };

    this.showAlertModal = function (message) {
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
    this.SatatusAlertDesactive = function () {
        var fineData = {};
        fineData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar esta compañía?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Eliminar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {

                    self.delete();
                    swal("Eliminado!", "Esta compañía  ha sido eliminada con exito.", "success")



                }
            });

    }

    this.validateIsNotDuplicated = function (companyData) {
        var isNotDuplicated = true;
        var company = this.ctrlActions.getCompanyByInfo(this.service, '/RetrieveByInfo', companyData);

        if (currentCompanyInfo.CorpIdentification != companyData.CorpIdentification) {
            if (company != null && companyData.CorpIdentification === company.CorpIdentification) {
                self.ctrlActions.ShowMessage('E', 'La cédula jurídica ingresada ya existe en el sistema');
                isNotDuplicated = false;
                $('#myModal').modal("hide");
            }
        }

        if (currentCompanyInfo.Name != companyData.Name) {
            if (company != null && companyData.Name === company.Name) {
                self.ctrlActions.ShowMessage('E', 'El nombre ingresado ya existe en el sistema');
                isNotDuplicated = false;
                $('#myModal').modal("hide");
            }
        }

        if (currentCompanyInfo.Email != companyData.Email) {
            if (company != null && companyData.Email === company.Email) {
                self.ctrlActions.ShowMessage('E', 'El email ingresado ya existe en el sistema');
                isNotDuplicated = false;
                $('#myModal').modal("hide");
            }
        }

        return isNotDuplicated;
    };

    this.SatatusAlertDesactive = function () {
        var agreementData = {};
        agreementData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar esta compañia?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Eliminar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {

                    self.delete();
                    swal("Eliminado!", "Esta compañia ha sido eliminada con exito.", "success")
                }
            });
    }

    this.verifyIsValid = function () {
        var companyData = this.ctrlActions.GetDataForm('frmEdition');

        if (companyData.CorpIdentification !== '') {
            var company = this.ctrlActions.getCompanyByCorpId(this.service, '/RetrieveByCorpId', companyData.CorpIdentification);
            this.addToTerminal(company);
        } else {
            $('#myModal').modal("hide");
            this.ctrlActions.ShowMessage('E', 'Por favor ingrese una cédula jurídica primero');
        }
    };

    this.cleanFormFields = function () {
        $('#frmEdition input').each(function () {
            $(this).val('');
        });
    };

    this.bindFields = function (data) {
        currentCompanyInfo = data;
        self.cleanFormFields();
        this.ctrlActions.cleanForm(companySchema);
        self.updateButtons(2);
        $('.modal-title').text('Editar Compañía');
        $('#myModal').modal("toggle");
        $('#addMessage').hide();
        this.ctrlActions.BindFields('frmEdition', data);
    };

    this.openModal = function () {
        self.cleanFormFields();
        this.ctrlActions.cleanForm(companySchema);
        $('#addMessage').show();
        $('#frmEdition')[0].reset();
        $('.modal-title').text('Registrar Compañía');
        $('#myModal').modal("toggle");
        self.updateButtons(1);
    };

    this.updateButtons = function (value) {
        if (value === 1) {
            $('#btnAdd').show();
            $('#btnCreate').show();
            $('#btnCancel').show();
            $('#btnUpdate').hide();
            $('#btnDelete').hide();
        } else {
            $('#btnAdd').hide();
            $('#btnCreate').hide();
            $('#btnCancel').hide();
            $('#btnUpdate').show();
            $('#btnDelete').show();
        }
    };
}

$(document).ready(function () {
    var vCompany = new VCompany();
    vCompany.retrieveAll();
});




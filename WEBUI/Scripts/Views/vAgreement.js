function vAgreement() {
    var self = this;

    var myModal = $("#agreementModal");

    var btnCreate = $("#btnCreate");
    var btnUpdate = $("#btnUpdate");
    var btnDelete = $("#btnDelete");

    var fsAgreementTerminalID = "fsAgreementTerminal";
    var fsAgreementTypeID = "fsAgreementType";
    var fsAgreementID = "fsAgreement";

    var selectTerminal = $("#selectTerminal");
    var selectAgreementType = $("#selectAgreementType");

    var txtAgreementID = $("#txtAgreementID");
    var txtInstituteName = $("#txtInstituteName");
    var txtInstituteEmail = $("#txtInstituteEmail");

    self.user = JSON.parse(sessionStorage.getItem("user"));

    self.tblAgreementId = "tblAgreement";
    var tblAgreement = $("#" + self.tblAgreementId);
    self.service = "Agreement";
    self.viewName = "vAgreement";
    self.ctrlActions = new ControlActions();
    self.columns = "IdAgreement,InstituteName,InstituteEmail,AgreementType,Terminal";

    var terminalSchema = {
        IdTerminal: function (value) {
            if (value === "") {
                return "Por favor seleccione una Terminal";
            }
            return "";
        }
    };

    var agreementSchema = {
        IdAgreement: function (value) {
            return "";
        },
        InstituteName: function (value) {
            if (value === "") {
                return "Digite el nombre de la institución nombre por favor";
            }
            return "";
        },
        InstituteEmail: function (value) {
            var regularEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!regularEx.test(value.toLowerCase())) {
                return "Por favor digite su correo electrónico con el formato correcto";
            }
            return "";
        }
    };

    var agreementTypeSchema = {
        IdAgreementType: function (value) {
            if (value === "") {
                return "Por favor seleccione un tipo de convenio";
            }
            return "";
        }
    };

    self.RetrieveAll = function () {
        self.ctrlActions.FillTable(self.service, self.tblAgreementId, false, );
    };

    self.ReloadTable = function () {
        self.ctrlActions.FillTable(self.service, self.tblAgreementId, true);
    };

    self.Create = function () {
        var terminalData = {};
        var agreementData = {};
        var agreementTypeData = {};

        let isValid;

        terminalData = self.ctrlActions.GetDataForm(fsAgreementTerminalID);
        agreementData = self.ctrlActions.GetDataForm(fsAgreementID);
        agreementTypeData = self.ctrlActions.GetDataForm(fsAgreementTypeID);

        isValid = self.ctrlActions.validateData(agreementData, agreementSchema);
        isValid = self.ctrlActions.validateData(terminalData, terminalSchema) && isValid;
        isValid = self.ctrlActions.validateData(agreementTypeData, agreementTypeSchema) && isValid;

        if (isValid) {
            agreementData.Terminal = terminalData;
            agreementData.AgreementType = agreementTypeData;

            self.ctrlActions.PostToAPI(self.service, agreementData);
            self.ReloadTable();
            myModal.modal("hide");

            self.ctrlActions.cleanFormSchema(terminalSchema);
            self.ctrlActions.cleanFormSchema(agreementSchema);
            self.ctrlActions.cleanFormSchema(agreementTypeSchema);

            self.showAlertModal('El Convenio se ha registrado con exito!');
        }
    };

    self.Update = function () {
        var terminalData = {};
        var agreementData = {};
        var agreementTypeData = {};

        let isValid;

        terminalData = self.ctrlActions.GetDataForm(fsAgreementTerminalID);
        agreementData = self.ctrlActions.GetDataForm(fsAgreementID);
        agreementTypeData = self.ctrlActions.GetDataForm(fsAgreementTypeID);

        isValid = self.ctrlActions.validateData(agreementData, agreementSchema);
        isValid = self.ctrlActions.validateData(terminalData, terminalSchema) && isValid;
        isValid = self.ctrlActions.validateData(agreementTypeData, agreementTypeSchema) && isValid;

        if (isValid) {
            agreementData.Terminal = terminalData;
            agreementData.AgreementType = agreementTypeData;

            self.ctrlActions.PutToAPI(self.service, agreementData);
            self.ReloadTable();
            myModal.modal("hide");

            self.ctrlActions.cleanFormSchema(terminalSchema);
            self.ctrlActions.cleanFormSchema(agreementSchema);
            self.ctrlActions.cleanFormSchema(agreementTypeSchema);
            self.showAlertModal('El Convenio se ha actualizado con exito!');
        }
    };

    self.Delete = function () {
        var agreementData = {};
        let isValid;
        agreementData.IdAgreement = parseInt(txtAgreementID.val())

        self.ctrlActions.DeleteToAPI(self.service, agreementData);

        self.ReloadTable();
        myModal.modal("hide");

        self.ctrlActions.cleanFormSchema(terminalSchema);
        self.ctrlActions.cleanFormSchema(agreementSchema);
        self.ctrlActions.cleanFormSchema(agreementTypeSchema);
        self.showAlertModal('El Convenio se ha eliminado con exito!');
    };

    this.SatatusAlertDesactive = function () {
        var agreementData = {};
        agreementData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar este convenio?",
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
                        swal("Eliminado!", "Este convenio ha sido eliminado con exito.", "success")
                }
            });

    }


    self.BindFields = function (data) {
        CleanModal();

        //selectTerminal.removeAttr("disabled");
        btnUpdate.removeAttr("disabled").show();
        btnDelete.removeAttr("disabled").show();
        
        this.ctrlActions.BindFields(fsAgreementID, data);
        this.ctrlActions.BindFields(fsAgreementTerminalID, data.Terminal);
        this.ctrlActions.BindFields(fsAgreementTypeID, data.AgreementType);

        myModal.modal("toggle");
    };

    self.OpenModal = function () {
        CleanModal();

        if (self.user.UserTerminal.IdTerminal > 0) {
            selectTerminal.val(self.user.UserTerminal.IdTerminal);
        }
        
        selectTerminal.removeAttr("disabled");
        btnCreate.removeAttr("disabled").show();

        myModal.modal("toggle");
    };

    self.init = function () {
        self.RetrieveAll();
        filterTableByTerminal();
        tblAgreement.DataTable().column(5).visible(false);
    }

    function filterTableByTerminal() {
        if (self.user.UserTerminal.IdTerminal > 0) {
            tblAgreement.dataTable.ext.search.push(function (settings, data, dataIndex) {
                return data[5] == self.user.UserTerminal.IdTerminal;
            });
            tblAgreement.DataTable().draw();
        }
    }

    function CleanModal() {
        self.ctrlActions.cleanForm(terminalSchema);
        self.ctrlActions.cleanForm(agreementSchema);
        self.ctrlActions.cleanForm(agreementTypeSchema);

        selectTerminal.attr("disabled", "disabled").removeClass("is-invalid").val("");
        selectAgreementType.removeClass("is-invalid").val("");

        txtAgreementID.attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();
        txtInstituteName.removeClass("is-invalid").val("");
        txtInstituteEmail.removeClass("is-invalid").val("");

        btnCreate.attr("disabled", "disabled").hide();
        btnUpdate.attr("disabled", "disabled").hide();
        btnDelete.attr("disabled", "disabled").hide();

        if (self.user.UserTerminal.IdTerminal > 0) {
            $("#" + fsAgreementTerminalID).hide();
        }
    }

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
            text: "Desea eliminar esta Rampa?",
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
                }
            });
    }
}

$(document).ready(function () {
    let user = JSON.parse(sessionStorage.getItem("user"));
    let canEnter = false;
    let page = new vAgreement();

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
        var vuser = new vAgreement();
        vuser.init();
    }
});


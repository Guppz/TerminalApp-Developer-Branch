function vAgreementCards() {
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
    self.serviceUser = "User/PostUserConvenio";
    self.viewName = "vAgreementCards";
    self.ctrlActions = new ControlActions();
    self.columns = "IdAgreement,InstituteName,InstituteEmail,AgreementType,Terminal";

    var fileSchema = {
        files: function (value) {
            let fileName = JSON.parse(sessionStorage.getItem("fileName"));
            if (fileName !== null){
                var split = fileName.split(".");
                if (split[1]!=="xlsx"){
                    return "Formato del archivo incorrecto";
                }
            }
            if (fileName === null) {
                return "Suba un archivo porfavor o un formato correcto";
            }
            return "";
        },
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
        var newUser = {};
        let isValid;
        terminalData = self.ctrlActions.GetDataForm(fsAgreementTerminalID);
        agreementData = self.ctrlActions.GetDataForm(fsAgreementID);
        agreementTypeData = self.ctrlActions.GetDataForm(fsAgreementTypeID);
        let file = JSON.parse(sessionStorage.getItem("file"));
        if (file !== undefined){
            var length = Object.keys(file).length;
        }
        sessionStorage.removeItem("file");
        for (var i = 0; i < length; i++){
            newUser[i] = {
                IdUser: file[i]["IdUser"] = agreementData.IdAgreement,
                Name: file[i]["Nombre"],
                LastName: file[i]["Apellido"],
                Email: file[i]["Email"],
                Identification: file[i]["Identificación"],
                idConvenio: file[i]["idConvenio"] = agreementTypeData.IdAgreementType
            }
            newUser[i].UserTerminal = { IdTerminal: terminalData.IdTerminal };
            newUser[i].Roleslist = [{ IdRole:  36}];
        }
        isValid = this.ctrlActions.validateData(newUser, fileSchema);
        if (isValid) {
            sessionStorage.removeItem("fileName");
            sessionStorage.removeItem("file");
            for (i = 0; i < length; i++) {
            self.ctrlActions.PostToAPI(self.serviceUser, newUser[i]);
            }
            $("#textFile").replaceWith($("#textFile").val('').clone(true));
            self.ReloadTable();
            myModal.modal("hide");
        }
    };

    this.excelToJson = function (e) {
        var fileName = e.target.files[0].name;
        var files = e.target.files;
        var i, f;
        for (i = 0, f = files[i]; i !== files.length; ++i) {
            var reader = new FileReader();
            var name = f.name;
            reader.onload = function (e) {
                var data = e.target.result;
                var result;
                var workbook = XLSX.read(data, { type: 'binary' });
                var sheet_name_list = workbook.SheetNames;
                sheet_name_list.forEach(function (y) {
                    var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    if (roa.length > 0) {
                        result = roa;
                    }
                });
                sessionStorage.setItem('file', JSON.stringify(result));
                sessionStorage.setItem('fileName', JSON.stringify(fileName));
            };
            reader.readAsArrayBuffer(f);
        }
    }

    self.BindFields = function (data) {
        CleanModal();
        btnCreate.show();
        //selectTerminal.removeAttr("disabled");
        //btnUpdate.removeAttr("disabled").show();
        //btnDelete.removeAttr("disabled").show();
        this.ctrlActions.BindFields(fsAgreementID, data);
        this.ctrlActions.BindFields(fsAgreementTerminalID, data.Terminal);
        this.ctrlActions.BindFields(fsAgreementTypeID, data.AgreementType);
        myModal.modal("toggle");
    };

    function CleanModal() {
        selectTerminal.attr("disabled", "disabled").removeClass("is-invalid").val("");
        selectAgreementType.removeClass("is-invalid").val("");
        txtAgreementID.attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();
        txtInstituteName.removeClass("is-invalid").val("");
        txtInstituteEmail.removeClass("is-invalid").val("");
        btnUpdate.attr("disabled", "disabled").hide();
        btnDelete.attr("disabled", "disabled").hide();


        if (self.user.UserTerminal.IdTerminal > 0) {
            $("#" + fsAgreementTerminalID).hide();
        }
    }

    self.init = function () {
        self.RetrieveAll();
        filterTableByTerminal();
        tblAgreement.DataTable().column(5).visible(false);
    }

    function filterTableByTerminal() {
        if (self.user.UserTerminal.IdTerminal > 0) {
            tblAgreement.dataTable.ext.search.push(function (settings, data, dataIndex) {
                return data[5] === self.user.UserTerminal.IdTerminal;
            });
            tblAgreement.DataTable().draw();
        }
    }
}

$(document).ready(function () {
 
        var vuser = new vAgreementCards();
        vuser.init();
        $('#textFile').attr("accept", "xlsx");
        $("#textFile").change(function (File) {
            var vuser = new vAgreementCards();
            vuser.excelToJson(File);
        });
    
});

$(document).ready(function () {
 $('#textFile').attr("accept", "xlsx");
        $("#textFile").change(function (File) {
            var vuser = new vAgreementCards();
            vuser.excelToJson(File);
        });
});

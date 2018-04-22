function vSystemParam() {
    var self = this;
    this.tblSystemParamId = 'tblSystemParam';
    this.service = 'SystemParam';
    this.ctrlActions = new ControlActions();
    this.columns = "IdSystemParam,Name,Value";

    var systemParamSchema = {
        Name: function (value) {
            if (value === "") {
                return "Por favor ingrese el nombre del parámetro";
            } else {
                if (/^[0-9]*$/.test(value)) {
                    return "El nombre del parámetro debe ser textual";
                }
            }
            return "";
        },
        Value: function (value) {
            if (value === "") {
                return "Por favor ingrese el valor del parámetro";
            }
            return "";
        },
        ParamType: function (value) {
            if (value === "") {
                return "Por favor ingrese el tipo del parámetro";
            }
            return "";
        }
    };

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblSystemParamId, false, );
    };

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblSystemParamId, true);
    };

    this.Create = function () {
        var systemParamData = this.ctrlActions.GetDataForm('fsEditSystemParam');
        let validSystemParam = this.ctrlActions.validateData(systemParamData, systemParamSchema);

        if (validSystemParam) {
            systemParamData.ParamType = { IdParamType: systemParamData.ParamType };
            this.ctrlActions.PostToAPI(this.service, systemParamData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El párametro se ha registrado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#modalSystemParam').modal("hide");
            this.ctrlActions.cleanForm(systemParamSchema);
            this.ReloadTable();
        }
    };

    this.Update = function () {
        var systemParamData = this.ctrlActions.GetDataForm('fsEditSystemParam');
        let validSystemParam = this.ctrlActions.validateData(systemParamData, systemParamSchema);

        if (validSystemParam) {
            systemParamData.ParamType = { IdParamType: systemParamData.ParamType };
            this.ctrlActions.PutToAPI(this.service, systemParamData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El párametro se ha actualizado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#modalSystemParam').modal("hide");
            this.ctrlActions.cleanForm(systemParamSchema);
            $("#fsEditSystemParam").find('option').attr('selected', false);
            $('#fsEditSystemParam')[0].reset();
            this.ReloadTable();
        }
    };

    this.Delete = function () {
        var systemParamData = {};
        systemParamData.IdSystemParam = parseInt($("#txtSystemParamId").val());
        this.ctrlActions.DeleteToAPI(this.service, systemParamData);
        swal({
            type: 'success',
            background: "#95c9d4",
            title: 'El párametro se ha eliminado con exito!',
            showConfirmButton: false,
            timer: 1500
        })
        $('#modalSystemParam').modal("hide");
        this.ctrlActions.cleanForm(systemParamSchema);
        this.ReloadTable();
    };

    this.BindFields = function (data) {
        this.ctrlActions.cleanForm(systemParamSchema);
        $('#txtSystemParamId').parent('div').hide();
        $("#fsEditSystemParam").find('option').attr('selected', false);
        self.updateButtons(2);
        this.ctrlActions.BindFields('fsEditSystemParam', data);
        $('#txtSystemParamId').attr('disabled', true);
        $('#txtSystemParamName').attr('disabled', true);
        self.fillEditionFormFields(data);
        $('#paramTypeComboBox').attr('disabled', true);
        ToggleModal();
    };

    this.fillEditionFormFields = function (data) {
        $('#paramTypeComboBox').val(data.ParamType.IdParamType);
    };

    this.OpenModal = function () {
        self.updateButtons(1);
        $('#txtSystemParamName').attr('disabled', false);
        $("#fsEditSystemParam").find('option').attr('selected', false);
        this.ctrlActions.cleanForm(systemParamSchema);
        $('#fsEditSystemParam')[0].reset();
        $('#txtSystemParamId').parent('div').hide();
        $('#paramTypeComboBox').attr('disabled', false);
        ToggleModal();
    };

    var ToggleModal = function () {
        $('#modalSystemParam').modal("toggle");
    };

    this.updateButtons = function (value) {
        $('#btnDelete').hide();
        if (value === 1) {
            $('#btnAdd').show();
            $('#btnCreate').show();
            $('#btnCancel').show();
            $('#btnUpdate').hide();
        } else {
            $('#btnAdd').hide();
            $('#btnCreate').hide();
            $('#btnCancel').hide();
            $('#btnUpdate').show();
        }
    };
}

$(document).ready(function () {
    var vsystemParam = new vSystemParam();
    vsystemParam.RetrieveAll();
});
function vBus() {
    var Login = JSON.parse(sessionStorage.user);
    this.tblUserId = 'tblBus';
    this.serviceNormal = 'Bus';
    this.service = 'Bus/RetrieveBusesByCompanyUser?CorpIdentification=' + Login.Identification;
    this.viewName = "vBus";
    var selectComeanyName = "CompanyComboBox";
    var selectCompany = $("#" + selectComeanyName)
    self = this;

    this.ctrlActions = new ControlActions();

    var busSchema = {
        Plate: function (value) {
            var regularEx = /^[0-9]*$/;
            if (value === "" || !regularEx.test(value) || value.length < 4) {
                return "Formato incorrecto";
            }
            return "";
        },
        Model: function (value) {
            if (value === "") {
                return "Ingrese un modelo de carro";
            }
            return "";
        },
        Year: function (value) {
            if (value === "" || value == 0 || value < 2000 || value.length < 4 || value.length > 4) {
                if (value.length > 4){
                    return "Fecha incorrecta";
                }
                return "El año del carro debe ser mayor a 2000";
            }
            return "";
        },
        Brand: function (value) {
            if (value === "") {
                return "Ingrese la marca de su carro";
            }
            return "";
        },
        ExpiryMarchamo: function (value) {
            var year = value.split('-');
            year = parseInt(year);
            if (value === "" || year > 2019 || year == 2018) {
                return "Ingrese un marchamo correcto";
            }
            return "";
        },
        ExpiryReteve: function (value) {
           
            if (value === "") {
                return "Ingrese su reteve";
            }
            return "";
        },
        ExpirySeguro: function (value) {

            if (value =="") {
                return "Ingrese su seguro";
            }
            return "";
        },
        Seats: function (value) {
            var regularEx = /^[+]?\d+([.]\d+)?$/;
            if (!regularEx.test(value) || value == 0) {
                return "No numeros negativos";
            }
            return "";
        },
        Standing: function (value) {
            var regularEx = /^[+]?\d+([.]\d+)?$/;
            if (!regularEx.test(value) || value == 0 ) {
                return "No numeros negativos";
            }
            return "";
        }
    };

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, false, );
    };

    this.RetrieveAllAdmin = function () {
        this.ctrlActions.FillTable(this.serviceNormal, this.tblUserId, false, );
    };

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, true);
    };



    this.Create = function () {
        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        userData.RequirementsPerBus =
            [{ "IdRequirement": "1", "Name": "Marchamo", "Expiry": userData.ExpiryMarchamo },
            { "IdRequirement": "2", "Name": "Reteve", "Expiry": userData.ExpiryReteve },
            { "IdRequirement": "3", "Name": "Seguro", "Expiry": userData.ExpirySeguro }];
        var idCompany = $('#CompanyComboBox option:selected').val();
        userData.Company = { IdCompany: idCompany};
        isValid = this.ctrlActions.validateData(userData, busSchema);
        if (isValid) {
            this.ctrlActions.PostToAPI(this.serviceNormal, userData);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanFormSchema(busSchema);
            this.ReloadTable();

            self.showAlertModal('El bus  se ha registrado con exito!');

        }
    };



    this.Update = function () {
        var busData = {};
        let isValid;
        var bus = JSON.parse(sessionStorage.Bus);
        busData = this.ctrlActions.GetDataForm('frmEdition');
        busData.RequirementsPerBus = [{ "IdRequirement": bus.RequirementsPerBus[0].IdRequirement, "Name": "Marchamo", "Expiry": busData.ExpiryMarchamo},
            { "IdRequirement": bus.RequirementsPerBus[1].IdRequirement, "Name": "Reteve", "Expiry": busData.ExpiryReteve },
            { "IdRequirement": bus.RequirementsPerBus[2].IdRequirement, "Name": "Seguro", "Expiry": busData.ExpirySeguro }];
        isValid = this.ctrlActions.validateData(busData, busSchema);
        if (isValid) {
            this.ctrlActions.PutToAPI(this.serviceNormal, busData);
            $('#myModal').modal("hide");
            this.ReloadTable();
            self.showAlertModal('El bus  se ha actualizado con exito!');

        }
    };


    this.Delete = function () {
        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.DeleteToAPI(this.serviceNormal, userData);
        $('#myModal').modal("hide");
        this.ctrlActions.cleanForm(busSchema);
        this.ReloadTable();
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

    this.BindFields = function (data) {
        $('#txtIdBus').parent('div').hide();

        this.ctrlActions.cleanFormSchema(busSchema);
        this.ctrlActions.BindFields('frmEdition', data);
        sessionStorage.removeItem('Bus');
        sessionStorage.setItem('Bus', JSON.stringify(data));
        var bus = JSON.parse(sessionStorage.Bus);
        var dateMarchamo = new Date(bus.RequirementsPerBus[0].Expiry);
        dateMarchamo = this.DateFormmat(dateMarchamo);
        var dateReteve = new Date(bus.RequirementsPerBus[1].Expiry);
        dateReteve = this.DateFormmat(dateReteve);
        var dateSeguro = new Date(bus.RequirementsPerBus[2].Expiry);
        dateSeguro = this.DateFormmat(dateSeguro);
        $('#myModal').modal("toggle");
        $('#txtMarchamo').val(dateMarchamo);
        $('#txtReteve').val(dateReteve);
        $('#txtSeguro').val(dateSeguro);
        $('#txtIdBus').attr('disabled', 'disabled');

        $('#btnCreate').hide();
        $('#btnUpdate').show();
        $('#btnUpdate').attr('name', data.IdUser);
        $('#btnDelete').show();
        $('#btnDelete').attr('name', data.IdUser);
    };


    this.DateFormmat = function (data) {
        var result = moment(data).format('YYYY-MM-DD');
        return result;
    };
    this.SatatusAlertDesactive = function () {
        var agreementData = {};
        agreementData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar este bus del sistema?",
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
                    swal("Eliminado!", "Este bus ha sido eliminado con exito.", "success")



                }
            });

    }


    this.OpenModal = function () {
        $('#txtIdBus').parent('div').hide();

        this.ctrlActions.cleanFormSchema(busSchema);
        $("#terminalComboBox").val("");
        $('#txtIdBus').val("");
        $('#myModal').modal("toggle");
        $('#btnUpdate').hide();
        $('#btnDelete').hide();
        $('#btnCreate').show();
    };

    this.removeEmptyCompanyOption = function () {
        if (selectCompany.find('option').length > 0) {
            selectCompany.find('option').first().remove();
        }
    }

    this.filterTableByCompanySelect = function () {
        $("#"+this.tblUserId).dataTable.ext.search.push(function (settings, data, dataIndex) {
            return data[7] == $('#CompanyComboBox :selected').text();
        });
        $("#"+this.tblUserId).DataTable().draw();
    }

    this.AdminFillTable = function () {
        this.removeEmptyCompanyOption()
        this.RetrieveAllAdmin();
        this.filterTableByCompanySelect();
        $('#CompanyComboBox').change(function () {
            var vbus = new vBus();
            vbus.filterTableByCompanySelect();
        });
    };
}

$(document).ready(function () {
    $('#txtPlaca').attr('maxlength', '6');
    var user = JSON.parse(sessionStorage.user);
    var vbus = new vBus();
    if (user.Roleslist[0].Name == "FullAdministrator") {
        vbus.AdminFillTable();
    } else {
        $('#CompanyComboBox').hide();
        var target = 'CompanyComboBox'; 
        $('label[for="' + target + '"]').hide();
        vbus.RetrieveAll();
        var length = $('#CompanyComboBox > option').length;
        for (var i = 0; i < length; i++){
            $("#CompanyComboBox")[0].selectedIndex = i;
            if (user.Name === $("#CompanyComboBox :selected ").text()) {
                $("#CompanyComboBox")[0].selectedIndex = i;
                break;
            }
        }
    }
});





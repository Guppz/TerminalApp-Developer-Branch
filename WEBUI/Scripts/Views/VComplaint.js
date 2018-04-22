window.buses = [];
window.drivers = [];
window.companiesClicked = [];
var currentComplaintInfo;

function VComplaint() {
    this.service = 'complaint';
    this.ctrlActions = new ControlActions();
    this.tblComplaintId = 'tblComplaints';
    self = this;

    $(document).ready(function () {
        var dropdown = $('#companyComboBox');

        dropdown.change(function () {
            self.getCompanyDriversAndBuses($(this).find(':selected').val());
            self.fillDriversNames($(this).find(':selected').val());
            self.fillPlates($(this).find(':selected').val());
        });
    });

    var complaintSchema = {
        Terminal: function (value) {
            if (value === "") {
                return "Seleccione una terminal por favor";
            }
            return "";
        },
        Company: function (value) {
            if (value === "") {
                return "Selecione una compañía por favor";
            }
            return "";
        },
        Description: function (value) {
            if (value === "") {
                return "Ingrese el motivo de su queja por favor";
            }
            return "";
        }
    };

    this.fillDriversNames = function (companySelected) {
        $('#driverComboBox option:not(:first)').remove();

        $.each(drivers, function (key, value) {
            if (drivers[key].idCompany === parseInt(companySelected)) {
                $('#driverComboBox').append($('<option />').val(drivers[key].idDriver).text(drivers[key].name));
            }
        });
    };

    this.fillPlates = function (companySelected) {
        $('#plateComboBox option:not(:first)').remove();

        $.each(buses, function (key, value) {
            if (buses[key].idCompany === parseInt(companySelected)) {
                $('#plateComboBox').append($('<option />').val(buses[key].idBus).text(buses[key].plate));
            }
        });
    };

    this.retrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblComplaintId, false);
    };

    this.reloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblComplaintId, true);
    };

    this.verifyHasNotBeenSelected = function (companyId) {
        var isSelected = false;

        if (companiesClicked.length > 0) {
            for (var i = 0; i < companiesClicked.length; ++i) {
                if (companiesClicked[i] === parseInt(companyId)) {
                    isSelected = true;
                }
            }
        } else {
            companiesClicked.push(parseInt(companyId));
        }

        if (!isSelected) {
            companiesClicked.push(parseInt(companyId));
        }

        return isSelected;
    };

    this.getCompanyDriversAndBuses = function (idCompany) {
        var hasNotBeenSelected = this.verifyHasNotBeenSelected(idCompany);

        if (!hasNotBeenSelected) {
            var driversList = this.ctrlActions.getDriversByCompany('driver', '/RetrieveDriversByCompany', parseInt(idCompany));
            var busesList = this.ctrlActions.getBusesByCompany('bus', '/RetrieveBusesByCompany', parseInt(idCompany));
            this.storeDriversInformation(driversList);
            this.storeBusesInformation(busesList);
        }
    };

    this.storeDriversInformation = function (companyDrivers) {
        $.each(companyDrivers, function (index, value) {
            var driver = {
                idDriver: value.CardNumber,
                name: value.Name,
                idCompany: value.Company.IdCompany
            }

            drivers.push(driver);
        });
    };

    this.storeBusesInformation = function (companyBuses) {
        $.each(companyBuses, function (index, value) {
            var bus = {
                idBus: value.IdBus,
                plate: value.Plate,
                idCompany: value.Company.IdCompany
            }

            buses.push(bus);
        });
    };

    this.fillEditionFormFields = function (formId, data) {
        var selectsList = $('#' + formId + ' select');

        $('#driverComboBox option:not(:first)').remove();
        $('#plateComboBox option:not(:first)').remove();
        $('#' + selectsList[1].id).prop('selectedIndex', $('#' + selectsList[1].id + ' option[value=' + data.idTerminal + ']').index());
        $('#' + selectsList[2].id).prop('selectedIndex', $('#' + selectsList[2].id + ' option[value=' + data.idCompany + ']').index());
        self.loadDriverCombobox(data, formId);
        self.loadBusCombobox(data, formId);
        $('#complaintDescription').val(data.description);
    };

    this.loadDriverCombobox = function (data, formId) {
        self.getCompanyDriversAndBuses(data.idCompany);
        var selectsList = $('#' + formId + ' select');
        self.fillDriversNames(data.idCompany);

        if (data.idDriver !== '') {
            $('#' + selectsList[3].id).prop('selectedIndex', $('#' + selectsList[3].id + ' option[value=' + data.idDriver + ']').index());
        }
    };

    this.loadBusCombobox = function (data, formId) {
        var selectsList = $('#' + formId + ' select');
        self.fillPlates(data.idCompany);

        if (data.idBus !== '') {
            $('#' + selectsList[4].id).prop('selectedIndex', $('#' + selectsList[4].id + ' option[value=' + data.idBus + ']').index());
        }
    };

    this.create = function () {
        var complaintData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(complaintData, complaintSchema);

        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, this.prepareInfoToSend(complaintData));
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(complaintSchema);
            self.cleanFormFields();
            this.reloadTable();
            self.showAlertModal('La queja se ha registrado con exito!');
        }
    };

    this.update = function () {
        var complaintData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(complaintData, complaintSchema);

        if (isValid) {
            complaintData = this.prepareInfoToSend(complaintData);
            complaintData.IdComplaint = currentComplaintInfo.IdComplaint;
            this.ctrlActions.PutToAPI(this.service, complaintData);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(complaintSchema);
            self.cleanFormFields();
            this.reloadTable();
            self.showAlertModal('La queja se ha actualizado con exito!');
        }
    };

    this.delete = function () {
        var complaintData = this.ctrlActions.GetDataForm('frmEdition');
        complaintData = this.prepareInfoToSend(complaintData);
        complaintData.IdComplaint = currentComplaintInfo.IdComplaint;
        this.ctrlActions.DeleteToAPI(this.service, complaintData);
        $('#myModal').modal("hide");
        this.ctrlActions.cleanForm(complaintSchema);
        self.cleanFormFields();
        this.reloadTable();
        self.showAlertModal('La queja se ha eliminado con exito!');
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

    this.cleanFormFields = function () {
        $('#driverComboBox option:not(:first)').remove();
        $('#plateComboBox option:not(:first)').remove();

        $('#frmEdition select').each(function () {
            $(this).attr('selected', false);
            $(this).prop('selectedIndex', 0);
            $(this).attr('selected', false);
        });

        $('#complaintDescription').val('');
    };

    this.prepareInfoForEdition = function (complaintData) {
        var sanctionInfo = {
            idTerminal: complaintData.Terminal.IdTerminal,
            description: complaintData.Description,
            idCompany: complaintData.Company.IdCompany,
            idComplaint: complaintData.IdComplaint,
            idDriver: (complaintData.Driver !== null) ? complaintData.Driver.CardNumber : '',
            idBus: (complaintData.Bus !== null) ? complaintData.Bus.IdBus : ''
        };

        return sanctionInfo;
    };

    this.getDriver = function () {
        return $("#driverComboBox option:selected").val();
    };

    this.getBus = function () {
        return $("#plateComboBox option:selected").val();
    };

    this.prepareInfoToSend = function (complaintData) {
        complaintData.Date = this.ctrlActions.getDateFormatted();
        complaintData.Company = { IdCompany: complaintData.Company };
        complaintData.Driver = { CardNumber: self.getDriver() };
        complaintData.Bus = { IdBus: self.getBus() };
        complaintData.Terminal = { IdTerminal: complaintData.Terminal };
        complaintData.Description = complaintData.Description;
        complaintData.User = { IdUser: self.getUserId() };

        return complaintData;
    };

    this.getUserId = function () {
        var user = JSON.parse(sessionStorage.getItem("user"));;
        return user.IdUser;
    }

    this.bindFields = function (data) {
        currentComplaintInfo = data;
        this.ctrlActions.cleanForm(complaintSchema);
        self.cleanFormFields();
        self.updateButtons(2);
        var rowData = self.prepareInfoForEdition(data);
        $('#txtPkIdComplaint').parent('div').hide();
        $('.modal-title').text('Editar Quejas');
        $('#myModal').modal("toggle");
        self.fillEditionFormFields('frmEdition', rowData);
    };

    this.openModal = function () {
        this.ctrlActions.cleanForm(complaintSchema);
        self.cleanFormFields();
        $('.modal-title').text('Envío de Quejas');
        $('#txtPkIdComplaint').parent('div').hide();
        self.updateButtons(1);
        $('#myModal').modal("toggle");
    };

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
}

$(document).ready(function () {
    var vComplaint = new VComplaint();
    vComplaint.retrieveAll();
});




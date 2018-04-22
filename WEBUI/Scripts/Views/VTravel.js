window.buses = [];
window.companiesClicked = [];
window.currentTravelInfo;

function VTravel() {
    this.service = 'travel';
    this.ctrlActions = new ControlActions();
    this.tblComplaintId = 'tblTravels';
    self = this;

    $(document).ready(function () {
        self.setHourInputsAttributes();
        var dropdown = $('#companyComboBox');

        dropdown.change(function () {
            self.fillRoutesByCompany($(this).find(':selected').val());
            self.getCompanyBuses($(this).find(':selected').val());
            self.fillPlates($(this).find(':selected').val());
        });
    });

    var travelSchema = {
        Company: function (value) {
            if (value === "") {
                return "Seleccione una compañía por favor";
            }
            return "";
        },
        Route: function (value) {
            if (value === "") {
                return "Selecione una ruta por favor";
            }
            return "";
        },
        Day: function (value) {
            if (value === "") {
                return "Seleccione un día por favor";
            }
            return "";
        },
        Schedule: function (value) {
            if (value === "") {
                return "Seleccione un horario por favor";
            }
            return "";
        },
        Bus: function (value) {
            if (value === "") {
                return "Seleccione un autobus por favor";
            }
            return "";
        },
        DepartHour: function (value) {
            if (value === "") {
                return "Seleccione una hora por favor";
            }
            return "";
        }
    };

    this.retrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblComplaintId, false);
    };

    this.reloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblComplaintId, true);
    };

    this.setHourInputsAttributes = function () {
        $('#departHour').attr('step', '1800');
        $('#departHour').attr('onkeydown', 'return false');
    };

    this.fillRoutesByCompany = function (idCompany) {
        $('#routeComboBox option:not(:first)').remove();
        var routesList = this.ctrlActions.getRoutesByCompany('route', '/RetrieveRoutesByCompany', parseInt(idCompany));
        var dropdown = $('#routeComboBox');

        $.each(routesList, function (key, value) {
            dropdown.append($('<option />').val(value.IdRoute).text(value.Name));
        });
    };


    this.fillPlates = function (companySelected) {
        self.getCompanyBuses(companySelected);
        $('#busComboBox option:not(:first)').remove();

        $.each(buses, function (key, value) {
            if (buses[key].idCompany === parseInt(companySelected)) {
                $('#busComboBox').append($('<option />').val(buses[key].idBus).text(buses[key].plate));
            }
        });
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

    this.getCompanyBuses = function (idCompany) {
        var hasNotBeenSelected = this.verifyHasNotBeenSelected(idCompany);

        if (!hasNotBeenSelected) {
            var busesList = this.ctrlActions.getBusesByCompany('bus', '/RetrieveBusesByCompany', parseInt(idCompany));
            this.storeBusesInformation(busesList);
        }
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
        $('#' + formId)[0].reset();
        $('#routeComboBox option:not(:first)').remove();
        $('#busComboBox option:not(:first)').remove();
        self.fillRoutesByCompany(data.idCompany);
        $('#txtPkIdTravel').val(data.idTravel).attr('disabled', 'disabled');
        $('#' + selectsList[1].id).prop('selectedIndex', $('#' + selectsList[1].id + ' option[value=' + data.idCompany + ']').index());
        $('#' + selectsList[2].id).prop('selectedIndex', $('#' + selectsList[2].id + ' option[value=' + data.idRoute + ']').index());
        $('#' + selectsList[3].id + ' option:contains(' + data.day + ')').attr('selected', 'selected')
        self.loadBusCombobox(data, formId);
        $('#departHour').val(data.hour);
    };

    this.loadBusCombobox = function (data, formId) {
        var selectsList = $('#' + formId + ' select');
        self.fillPlates(data.idCompany);

        if (data.idBus !== '') {
            $('#' + selectsList[4].id).prop('selectedIndex', $('#' + selectsList[4].id + ' option[value=' + data.idBus + ']').index());
        }
    };

    this.create = function () {
        var travelData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(travelData, travelSchema);

        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, this.prepareInfoToSend(travelData));
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(travelSchema);
            self.cleanFormFields();
            this.reloadTable();
            self.showAlertModal('El viaje se ha registrado con exito!');
        }
    };

    this.update = function () {
        var travelData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(travelData, travelSchema);

        if (isValid) {
            travelData = this.prepareInfoToSend(travelData)
            travelData.Schedule.IdSchedule = currentTravelInfo.Schedule.IdSchedule;
            this.ctrlActions.PutToAPI(this.service, travelData);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(travelSchema);
            self.cleanFormFields();
            this.reloadTable();
            self.showAlertModal('El viaje se ha actualizado con exito!');
        }
    };

    this.delete = function () {
        var travelData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.DeleteToAPI(this.service, this.prepareInfoToSend(travelData));
        $('#myModal').modal("hide");
        this.ctrlActions.cleanForm(travelSchema);
        self.cleanFormFields();
        this.reloadTable();
        self.showAlertModal('El viaje se ha eliminado con exito!');
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
        var travelData = {};
        travelData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar este viaje?",
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
                    swal("Eliminado!", "Este viaje ha sido eliminado con exito.", "success")



                }
            });

    }


    this.cleanFormFields = function () {
        this.ctrlActions.cleanForm(travelSchema);
        $('#busComboBox option:not(:first)').remove();
        $('#routeComboBox option:not(:first)').remove();
        $("#frmEdition").find('option').attr('selected', false);
        $('#frmEdition')[0].reset();
    };

    this.prepareInfoForEdition = function (travelData) {
        var travelInfo = {
            idTravel: travelData.IdTravel,
            idCompany: travelData.Route.RouteCompany.IdCompany,
            idRoute: travelData.Route.IdRoute,
            day: travelData.Schedule.Day,
            idBus: travelData.Bus.IdBus,
            hour: travelData.Schedule.DepartHour
        };

        return travelInfo;
    };

    this.prepareInfoToSend = function (travelData) {
        travelData.Schedule = {DepartHour: travelData.DepartHour, Day: travelData.Day };
        travelData.Bus = { IdBus: travelData.Bus };
        travelData.Route = { IdRoute: travelData.Route };

        return travelData;
    };

    this.bindFields = function (data) {
        currentTravelInfo = data;
        this.ctrlActions.cleanForm(travelSchema);
        self.cleanFormFields();
        self.updateButtons(2);
        var rowData = self.prepareInfoForEdition(data);
        $('#txtPkIdTravel').parent('div').show();
        $('.modal-title').text('Editar Viajes');
        $('#myModal').modal("toggle");
        self.fillEditionFormFields('frmEdition', rowData);
    };

    this.openModal = function () {
        self.cleanFormFields();
        $('.modal-title').text('Registro de Viajes');
        $('#txtPkIdTravel').parent('div').hide();
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
    var vTravel = new VTravel();
    vTravel.retrieveAll();
});



function VSchedule() {
    var self = this;
    var fsCompanyID = "fsCompany";
    var fsRouteID = "fsRoute";
    var fsScheduleID = "fsSchedule";
    self = this;
    var txtScheduleID = $("#txtScheduleID");
    var selectCompany = $("#companyComboBox");
    var selectRoute = $("#routeComboBox");
    var selectDay = $("#dayComboBox");
    var txtDepartHour = $("#departHour");
    var txtArriveHour = $("#arriveHour");

    this.URL_API = "http://localhost:61693/api/";

    this.service = 'schedule';
    this.ctrlActions = new ControlActions();
    this.tblScheduleId = 'tblSchedules';

    this.columns = "Terminal~Company~Route~Day~DeparHour~ArriveHour";

    $(document).ready(function () {
        self.setHourInputsAttributes();
        var dropdown = $('#companyComboBox');

        dropdown.change(function () {
            self.fillRoutesByCompany($(this).find(':selected').val());
        });
    });

    var companySchema = {
        IdCompany: function (value) {
            if (value === "") {
                return "Seleccione una compañía por favor";
            }
            return "";
        }
    }

    var routeSchema = {
        IdRoute: function (value) {
            if (value === "") {
                return "Seleccione una compañía por favor";
            }
            return "";
        }
    }

    var scheduleSchema = {
        Day: function (value) {
            if (value === "") {
                return "Seleccione un día por favor";
            }
            return "";
        },
        DepartHour: function (value) {
            if (value === "") {
                return "Ingrese la hora de salida por favor";
            }
            return "";
        },
        ArriveHour: function (value) {
            if (value === "") {
                return "Ingrese la hora de llegada por favor";
            }
            return "";
        }
    };

    this.getUrlApiService = function (service) {
        return this.URL_API + service;
    };

    this.retrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblScheduleId, false);
    };

    this.reloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblScheduleId, true);
    };

    this.setHourInputsAttributes = function () {
        $('#departHour').attr('step', '1800');
        $('#departHour').attr('onkeydown', 'return false');
        $('#arriveHour').attr('step', '1800');
        $('#arriveHour').attr('onkeydown', 'return false');
    }

    this.fillRoutesByCompany = function (idCompany) {
        $('#routeComboBox option:not(:first)').remove();
        var routesList = $.getRoutesByCompany(self.getUrlApiService('route') + '/RetrieveRoutesByCompany', parseInt(idCompany));
        var dropdown = $('#routeComboBox');

        $.each(routesList, function (key, value) {
            dropdown.append($('<option />').val(value.IdRoute).text(value.Name));
        });
    };

    this.create = function () {
        var companyData = this.ctrlActions.GetDataForm(fsCompanyID);
        var routeData = this.ctrlActions.GetDataForm(fsRouteID);
        var scheduleData = this.ctrlActions.GetDataForm(fsScheduleID);

        let isValid = this.ctrlActions.validateData(companyData, companySchema);
        isValid = this.ctrlActions.validateData(routeData, routeSchema) && isValid;
        isValid = this.ctrlActions.validateData(scheduleData, scheduleSchema) && isValid;

        if (isValid) {
            scheduleData.Company = companyData;
            scheduleData.Route = routeData;

            if (self.validateIsNotDuplicated(scheduleData)) {
                this.ctrlActions.PostToAPI(this.service, scheduleData);
                $('#myModal').modal("hide");
                this.ctrlActions.cleanForm(companySchema);
                this.ctrlActions.cleanForm(routeSchema);
                this.ctrlActions.cleanForm(scheduleSchema);
                this.reloadTable();
                self.showAlertModal('El horario se ha registrado con exito!');

            }
        }
    };

    this.update = function () {
        var companyData = this.ctrlActions.GetDataForm(fsCompanyID);
        var routeData = this.ctrlActions.GetDataForm(fsRouteID);
        var scheduleData = this.ctrlActions.GetDataForm(fsScheduleID);

        let isValid = this.ctrlActions.validateData(companyData, companySchema);
        isValid = this.ctrlActions.validateData(routeData, routeSchema) && isValid;
        isValid = this.ctrlActions.validateData(scheduleData, scheduleSchema) && isValid;

        if (isValid) {
            scheduleData.Company = companyData;
            scheduleData.Route = routeData;
            scheduleData.IdSchedule = $('#txtScheduleID').val();

            if (self.validateIsNotDuplicated(scheduleData)) {
                this.ctrlActions.PutToAPI(this.service, scheduleData);
                $('#myModal').modal("hide");
                this.ctrlActions.cleanForm(companySchema);
                this.ctrlActions.cleanForm(routeSchema);
                this.ctrlActions.cleanForm(scheduleSchema);
                this.reloadTable();
                self.showAlertModal('El horario se ha actualizado con exito!');
            }
        }
    };

    this.delete = function () {
        var scheduleData = this.ctrlActions.GetDataForm(fsScheduleID);
        this.ctrlActions.DeleteToAPI(this.service, scheduleData);

        $('#myModal').modal("hide");
        this.ctrlActions.cleanForm(companySchema);
        this.ctrlActions.cleanForm(routeSchema);
        this.ctrlActions.cleanForm(scheduleSchema);
        this.reloadTable();
    };
    this.SatatusAlertDesactive = function () {
        var fineData = {};
        fineData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar este horario?",
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
                    swal("Eliminado!", "Este horario se ha  eliminado con exito.", "success")



                }
            });

    }

    this.showAlertModal = function (message) {
        var inter = setInterval(function () {
            if ($('.alert').hasClass('alert-success') && $('.alert').css("display") === "block") {
                swal({
                    type: 'success',
                    background: "#95c9d4",
                    title: message,
                    showConfirmButton: false,
                    timer: 3000
                })

                clearInterval(inter);
            }
        }, 1000);
    };

    this.cleanFormFields = function () {
        this.ctrlActions.cleanForm(companySchema);
        this.ctrlActions.cleanForm(routeSchema);
        this.ctrlActions.cleanForm(scheduleSchema);

        $('#routeComboBox option:not(:first)').remove();

        $('#frmEdition select').each(function () {
            $(this).attr('selected', false);
            $(this).prop('selectedIndex', 0);
            $(this).attr('selected', false);
        });

        $('#frmEdition input').each(function () {
            $(this).val('');
        });
    };

    this.validateIsNotDuplicated = function (scheduleData) {
        var isNotDuplicated = true;
        var schedule = $.getScheduleByHour(self.getUrlApiService(this.service) + '/RetrieveScheduleByDayHour', scheduleData);

        if (schedule !== null) {
            self.ctrlActions.ShowMessage('E', 'El horario ingresado ya ha sido reservado');
            isNotDuplicated = false;
            $('#myModal').modal("hide");
        }

        return isNotDuplicated;
    };

    this.fillEditionFormFields = function (data) {
        var selectsList = $('#frmEdition select');

        $('#' + selectsList[1].id).prop('selectedIndex', $('#' + selectsList[1].id + ' option[value=' + data.Route.RouteCompany.IdCompany + ']').index());
        $('#' + selectsList[2].id).prop('selectedIndex', $('#' + selectsList[2].id + ' option[value=' + data.Route.IdRoute + ']').index());
        selectDay.val(encodeDay(data.Day));
        txtDepartHour.val(data.DepartHour);
        $('#txtScheduleID').val(data.IdSchedule)
    };

    this.bindFields = function (data) {
        self.cleanFormFields();
        self.fillRoutesByCompany(data.Route.RouteCompany.IdCompany);
        self.updateButtons(2);
        $('.modal-title').text('Editar Horarios');
        $('#myModal').modal("toggle");
        $('#addMessage').hide();

        self.fillEditionFormFields(data);
    };

    this.openModal = function () {
        self.cleanFormFields();
        self.updateButtons(1);
        $('#addMessage').show();
        $('#frmEdition')[0].reset();
        $('.modal-title').text('Registrar Horarios');
        $('#myModal').modal("toggle");
    };

    this.updateButtons = function (value) {
        txtScheduleID.parent().hide();
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

    function encodeDay(pDay) {
        var dayNumber = -1;

        switch (pDay) {
            case "Domingo":
                dayNumber = 1;
                break;
            case "Lunes":
                dayNumber = 2;
                break;
            case "Martes":
                dayNumber = 3;
                break;
            case "Miércoles":
                dayNumber = 4;
                break;
            case "Jueves":
                dayNumber = 5;
                break;
            case "Viernes":
                dayNumber = 6;
                break;
            case "Sábado":
                dayNumber = 7;
                break;
            default:
                dayNumber = -1;
        }
        return dayNumber;
    }
};

$(document).ready(function () {
    var vSchedule = new VSchedule();
    vSchedule.retrieveAll();
});

$.getScheduleByHour = function (url, scheduleData) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: {
            'day': scheduleData.Day,
            'departHour': scheduleData.DepartHour,
            'idRoute': scheduleData.Route.IdRoute
        },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};

$.getRoutesByCompany = function (url, companyId) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "idCompany": companyId },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};




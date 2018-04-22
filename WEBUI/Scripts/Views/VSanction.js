function VSanction() {
    this.service = 'sanction';
    this.ctrlActions = new ControlActions();
    this.tblSanctionId = 'tblSanctions';
    self = this;
    this.columns = "IdSanction~Terminal~Company~Route~Type~Cost~Date";

    $(document).ready(function () {
        var dropdown = $('#companyComboBox');

        dropdown.change(function () {
            self.fillRoutesByCompany($(this).find(':selected').val());
        });
    });

    var sanctionSchema = {
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
        Route: function (value) {
            if (value === "") {
                return "Selecione una ruta por favor";
            }
            return "";
        },
        Type: function (value) {
            if (value === "") {
                return "Selecione un tipo de sanción por favor";
            }
            return "";
        },
        Description: function (value) {
            if (value === "") {
                return "Ingrese la razón de la sanción por favor";
            }
            return "";
        }
    };

    this.retrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblSanctionId, false);
    };

    this.reloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblSanctionId, true);
    };

    this.fillSanctionsTypesCombobox = function () {
        self.fillTypes(this.service);
    };

    this.fillTypes = function (service) {
        var typesList = this.ctrlActions.getSanctionsTypes(this.service, '/RetrieveSanctionsTypes');
        var dropdown = $('#sanctionTypeComboBox');
        $('#sanctionTypeCost').attr('disabled', true);

        $.each(typesList, function (key, value) {
            dropdown.append($('<option />').val(value.IdType).text(value.Name));
        });

        dropdown.change(function () {
            self.setTypeDescription($(this).find(':selected').val());
        });
    };

    this.setTypeDescription = function (typeSelected) {
        var sanctionType = this.ctrlActions.getSanctionTypeById(this.service, '/RetrieveSanctionTypeById', typeSelected);

        if (sanctionType != null) {
            $('#sanctionDescription').val(sanctionType.Description);
            $('#sanctionCost').val(sanctionType.Cost);
        } else {
            $('#sanctionDescription').val('');
            $('#sanctionCost').val('');
        }
    };

    this.fillRoutesByCompany = function (idCompany) {
        $('#routeComboBox option:not(:first)').remove();
        var routesList = this.ctrlActions.getRoutesByCompany('route', '/RetrieveRoutesByCompany', parseInt(idCompany));
        var dropdown = $('#routeComboBox');

        $.each(routesList, function (key, value) {
            dropdown.append($('<option />').val(value.IdRoute).text(value.Name));
        });
    };

    this.fillEditionFormFields = function (formId, data) {
        var selectsList = $('#' + formId + ' select');

        $('#txtPkIdSanction').val(data.idSanction).attr('disabled', 'disabled');
        $('#' + selectsList[1].id).prop('selectedIndex', $('#' + selectsList[1].id + ' option[value=' + data.idTerminal + ']').index());
        $('#' + selectsList[2].id).prop('selectedIndex', $('#' + selectsList[2].id + ' option[value=' + data.idCompany + ']').index());
        $('#' + selectsList[4].id).prop('selectedIndex', $('#' + selectsList[4].id + ' option[value=' + data.idSanctionType + ']').index());
        self.fillRoutesByCompany($('#' + selectsList[2].id + ' option:selected').val());
        $('#' + selectsList[3].id).prop('selectedIndex', $('#' + selectsList[3].id + ' option[value=' + data.idRoute + ']').index());
        $('#sanctionDescription').val(data.description);
        $('#sanctionCost').val(data.cost).attr('disabled', true);
    };

    this.create = function () {
        var sanctionData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(sanctionData, sanctionSchema);

        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, this.prepareInfoToSend(sanctionData));
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(sanctionSchema);
            self.showAlertModal('La sanción se ha registrado con exito!');
            self.cleanForm();
        }

        this.reloadTable();
    };

    this.update = function () {
        var sanctionData = this.ctrlActions.GetDataForm('frmEdition');
        let isValid = this.ctrlActions.validateData(sanctionData, sanctionSchema);

        if (isValid) {
            this.ctrlActions.PutToAPI(this.service, this.prepareInfoToSend(sanctionData));
            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(sanctionSchema);
            self.showAlertModal('La sanción se ha actualizado con exito!');
        }

        this.reloadTable();
    };

    this.delete = function () {
        this.ctrlActions.DeleteToAPI(this.service, this.ctrlActions.GetDataForm('frmEdition'));
        $('#myModal').modal("hide");
        this.ctrlActions.cleanForm(sanctionSchema);
        this.reloadTable();
        self.showAlertModal('La sanción se ha eliminado con exito!');
    };

    this.cleanForm = function () {
        this.ctrlActions.cleanForm(sanctionSchema);
        $('#routeComboBox option:not(:first)').remove();

        $('#frmEdition select').each(function () {
            $(this).attr('selected', false);
            $(this).prop('selectedIndex', 0);
            $(this).attr('selected', false);
        });

        $('#sanctionDescription').val('');
        $('#sanctionCost').val('');
    };

    this.prepareInfoForEdition = function (sanctionData) {
        var sanctionInfo = {
            idTerminal: sanctionData.Terminal.IdTerminal,
            idCompany: sanctionData.Company.IdCompany,
            idRoute: sanctionData.Route.IdRoute,
            idSanctionType: sanctionData.Type.IdType,
            description: sanctionData.Description,
            idSanction: sanctionData.IdSanction,
            cost: sanctionData.Type.Cost
        };

        return sanctionInfo;
    };

    this.prepareInfoToSend = function (sanctionData) {
        sanctionData.Date = this.ctrlActions.getDateFormatted();
        sanctionData.Company = { IdCompany: sanctionData.Company };
        sanctionData.Terminal = { IdTerminal: sanctionData.Terminal };
        sanctionData.Type = { IdType: sanctionData.Type };
        sanctionData.Route = { IdRoute: sanctionData.Route };
        sanctionData.Description = sanctionData.Description;

        return sanctionData;
    };

    this.bindFields = function (data) {
        self.cleanForm();
        self.updateButtons(2);
        var rowData = self.prepareInfoForEdition(data);
        $('#txtPkIdSanction').parent('div').hide();
        $('.modal-title').text('Editar Sanciones');
        $('#myModal').modal("toggle");
        self.fillEditionFormFields('frmEdition', rowData);
    };

    this.openModal = function () {
        self.cleanForm();
        $('#txtPkIdSanction').parent('div').hide();
        $('.modal-title').text('Registrar Sanciones');
        $('#myModal').modal("toggle");
        self.updateButtons(1);
        $('#sanctionCost').attr('disabled', true);
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
            text: "Desea eliminar esta Sanción?",
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
                }
            });
    }
}

$(document).ready(function () {
    var vSanction = new VSanction();
    vSanction.fillSanctionsTypesCombobox();
    vSanction.retrieveAll();
});




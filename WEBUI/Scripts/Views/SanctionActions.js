var santionTypesDescriptions = {};

function SanctionActions() {

    this.urlApi = "http://localhost:61693/api/";
    self = this;

    $(document).ready(function () {
        var dropdown = $('#companyComboBox');

        dropdown.change(function () {
            self.fillRoutesByCompany($(this).find(':selected').val());
        });
    })

    this.getUrlApiService = function (service) {
        return this.urlApi + service;
    }

    this.fillTypes = function (service) {
        var typesList = $.getSanctionsTypes(this.getUrlApiService(service) + '/RetrieveSanctionsTypes');
        var dropdown = $('#sanctionTypeComboBox');
        $('#sanctionTypeCost').attr('disabled', true);

        $.each(typesList, function (key, value) {
            dropdown.append($('<option />').val(value.IdType).text(value.Name));
            santionTypesDescriptions.push({ 'IdType': value.IdType, 'Description': value.Description, 'Cost': value.Cost });
        });

        dropdown.change(function () {
            self.setTypeDescription($(this).find(':selected').val());
        });
    }

    this.setTypeDescription = function (typeSelected) {
        var descriptionField = $('#sanctionDescription');
        var sanctionTypeCost = $('#sanctionTypeCost');
        sanctionTypeCost.val('');

        $.each(santionTypesDescriptions, function (key, value) {
            if (santionTypesDescriptions[key]['IdType'] === parseInt(typeSelected)) {
                descriptionField.val(santionTypesDescriptions[key]['Description']);
                sanctionTypeCost.val(santionTypesDescriptions[key]['Cost']);

            }
        });
    }

    this.companiesComboboxAddEvent = function () {
        var dropdown = $('#companyComboBox');

        dropdown.change(function () {
            this.fillRoutesByCompany($(this).find(':selected').val());
        });
    }

    this.fillRoutesByCompany = function (idCompany) {
        $('#routeComboBox option:not(:first)').remove();
        var routesList = $.getRoutesByCompany((this.getUrlApiService('route') + '/RetrieveRoutesByCompany'), parseInt(idCompany));
        var dropdown = $('#routeComboBox');

        $.each(routesList, function (key, value) {
            dropdown.append($('<option />').val(value.IdRoute).text(value.Name));
        });
    }

    this.getFormData = function (formId) {
        var data = {};

        $('#' + formId + ' *').filter(':input').each(function (input) {
            var columnDataName = $(this).attr('columndataname');
            data[columnDataName] = this.value;
        });

        return data;
    }

    this.validateData = function (validateObject, formSchema) {
        let validData = true;

        Object.keys(formSchema).map(function (property) {
            let validator = formSchema[property];
            let result = validator(validateObject[property]);
            var tagName = self.getElemetTag(property);

            var targetedElement = $(tagName + "[columndataname = " + '"' + property + '"' + "]");
            var divTextContent = $("div[columndataname = " + '"' + property + '"' + "]")[0];

            if (result !== "") {
                validData = false;

                $(targetedElement.addClass("is-invalid"));
                divTextContent.textContent = result;
            } else {
                self.cleanFormErrors(targetedElement, divTextContent);
            }
        });

        return validData;
    }

    this.getElemetTag = function (property) {
        var tagName;

        $('#frmSanction *').filter(':input').each(function (input) {
            var columnDataName = $(this).attr('columndataname');
            if (columnDataName === property) {
                tagName = $(this).prop("tagName");
            }

        });

        return tagName.toLowerCase();
    }

    this.cleanFormErrors = function (tagTargeted, elementTargeted) {
        if (tagTargeted.hasClass('is-invalid')) {
            $(tagTargeted.removeClass('is-invalid'));
            $(elementTargeted.textContent = '');
        }
    }

    this.cleanForm = function (idForm, formSchema) {
        Object.keys(formSchema).map(function (property) {
            var tagName = self.getElemetTag(property);
            var targetedElement = $(tagName + "[columndataname = " + '"' + property + '"' + "]");
            var divTextContent = $("div[columndataname = " + '"' + property + '"' + "]")[0];
            self.cleanFormErrors(targetedElement, divTextContent);
        });

        $('#' + idForm)[0].reset();
        $('.alert').hide();
    }

    this.postToApi = function (service, sanctionData) {
        var jqxhr = $.post(this.getUrlApiService(service), sanctionData, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            })
    }

    this.showMessage = function (type, message) {
        if (type === 'E') {
            $("#alert_container").removeClass("alert alert-success alert-dismissable")
            $("#alert_container").addClass("alert alert-danger alert-dismissable");
            $("#alert_message").text(message);
        } else if (type === 'I') {
            $("#alert_container").removeClass("alert alert-danger alert-dismissable")
            $("#alert_container").addClass("alert alert-success alert-dismissable");
            $("#alert_message").text(message);
        }
        $('.alert').show();
    }
}

$.getRoutesByCompany = function (url, companyId) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "IdCompany": companyId },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
}

$.getSanctionsTypes = function (url) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: {},
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
}

$.post = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }

    return $.ajax({
        url: url,
        async: false,
        type: 'POST',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}


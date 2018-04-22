function ControlActions() {
    this.URL_API = "http://localhost:61693/api/";
    var self = this;

    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    };

    this.GetTableColumsDataName = function (tableId) {
        var val = $('#' + tableId).attr("ColumnsDataName");
        return val;
    };

    this.FillTable = function (service, tableId, refresh) {
        if (!refresh) {
            columns = this.GetTableColumsDataName(tableId).split('~');
            var arrayColumnsData = [];

            $.each(columns, function (index, value) {
                var obj = {};
                obj.data = value;
                arrayColumnsData.push(obj);
            });

            $('#' + tableId).DataTable({
                "processing": true,
                'language': {
                    'url': '/../Scripts/Views/DatatableLanguage.json'
                },
                columnDefs: [
                    {
                        targets: '_all',
                        defaultContent: ''
                    }
                ],
                "ajax": {
                    "url": this.GetUrlApiService(service),
                    dataSrc: 'Data'
                },
                "columns": arrayColumnsData
            });
        } else {
            var inter = setInterval(function () {
                if ($('.alert').css("display") === "block") {
                    $('#Loading').modal("hide");
                    clearInterval(inter);
                    $('#' + tableId).DataTable().ajax.reload();
                } else {
                    if ($("#Loading").css("display") !== "block") {
                        $('#Loading').modal("toggle");
                    }
                }
            }, 1000);
        }
    };

    this.GetSelectedRow = function () {
        var data = sessionStorage.getItem(tableId + '_selected');
        return data;
    };

    this.BindFields = function (formId, data) {
        $('#' + formId + ' *').filter(':input').each(function (input) {
            if ($(this)[0].type !== "checkbox") {
                var columnDataName = $(this).attr("ColumnDataName");
                this.value = data[columnDataName];
            }
        });
    };

    this.GetDataForm = function (formId) {
        var data = {};

        $('#' + formId + ' *').filter(':input').each(function (input) {
            if ($(this)[0].type !== "checkbox") {
                var columnDataName = $(this).attr("ColumnDataName");
                data[columnDataName] = this.value;
            }
        });

        return data;
    };

    this.ShowMessage = function (type, message) {
        if (type === 'E') {
            $("#alert_container").removeClass("alert alert-success alert-dismissable");
            $("#alert_container").addClass("alert alert-danger alert-dismissable");
            $("#alert_message").text(message);
        } else if (type === 'I') {
            $("#alert_container").removeClass("alert alert-danger alert-dismissable");
            $("#alert_container").addClass("alert alert-success alert-dismissable");
            $("#alert_message").text(message);
        }

        $(window).scrollTop(0);
        $('.alert').show();
        self.hideAlertMessage();
    };

    this.GetToAPI = function (service, data) {
        var jqxhr = $.get(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
            return response.data;
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                return {};
            });
    };

    this.PostToAPI = function (service, data) {
        var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            });
    };

    this.PutToAPI = function (service, data) {
        var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            });
    };

    this.DeleteToAPI = function (service, data) {
        var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            });
    };

    this.hideAlertMessage = function () {
        var inter = setTimeout(function () {
            $('#alert_container').hide();
            $('.alert').removeClass('alert-success');
            $('.alert').removeClass('alert-danger');
        }, 3000);
    };

    //Si el formulario tiene varios "ColumnDataName" con el mismo nombre, la validación falla. Se ocupa pasarle el Id del Form/Fieldset.
    this.validateDataByForm = function (validateObject, objectSchema, formID) {
        var form = $("#" + formID);
        let isValid = true;
        Object.keys(objectSchema).map(function (property) {
            let validator = objectSchema[property];
            let result = validator(validateObject[property]);
            if (result !== "") {
                isValid = false;
                form.find(" input[columnDataName = " + '"' + property + '"' + "], select[columnDataName = " + '"' + property + '"' + "]").addClass("is-invalid");
                form.find(" div[columnDataName = " + '"' + property + '"' + "]").empty();
                form.find(" div[columnDataName = " + '"' + property + '"' + "]").append("<p>" + result + "</p>");
            } else {
                form.find(" input[columnDataName = " + '"' + property + '"' + "]").removeClass("is-invalid");
                form.find(" div[columnDataName = " + '"' + property + '"' + "]").empty();
            }
        });

        return isValid;
    };
    //Si el formulario tiene varios "ColumnDataName" con el mismo nombre, la limpieza del formulario falla. Se ocupa pasarle el Id del Form/Fieldset.
    this.cleanFormById = function (objectSchema, formID) {
        var form = $("#" + formID);
        Object.keys(objectSchema).map(function (property) {
            var tagName = self.getTagName(property);
            var targetedElement = form.find(tagName + "[columndataname = " + '"' + property + '"' + "]");
            var divTextContent = form.find(" div[columnDataName = " + '"' + property + '"' + "]")[0];
            self.cleanFormErrors(targetedElement, divTextContent);
        });
    };

    this.validateData = function (validateObject, formSchema) {
        let validData = true;
        var tagName = '';
        Object.keys(formSchema).map(function (property) {
            let validator = formSchema[property];
            let result = validator(validateObject[property]);
            var tagName = self.getTagName(property);

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
    };

    this.getTagName = function (property) {
        var tagName = '';

        $('#frmEdition *').filter(':input').each(function (input) {
            var columnDataName = $(this).attr('columndataname');
            if (columnDataName === property) {
                tagName = $(this).prop("tagName");
            }
        });

        return tagName.toLowerCase();
    };

    this.cleanForm = function (objectSchema) {
        Object.keys(objectSchema).map(function (property) {
            var tagName = self.getTagName(property);
            var targetedElement = $(tagName + "[columndataname = " + '"' + property + '"' + "]");
            var divTextContent = $("div[columndataname = " + '"' + property + '"' + "]")[0];
            self.cleanFormErrors(targetedElement, divTextContent);
        });
    };

    this.cleanFormSchema = function (objectSchema) {
        Object.keys(objectSchema).map(function (property) {
            $($(" input[columnDataName = " + '"' + property + '"' + "]")[0]).removeClass("is-invalid").val("");
            $(" div[columnDataName = " + '"' + property + '"' + "]")[0].textContent = "";
        });
    };

    this.cleanFormErrors = function (tagTargeted, elementTargeted) {
        if (tagTargeted.hasClass('is-invalid')) {
            $(tagTargeted.removeClass('is-invalid'));
            $(elementTargeted.textContent = '');
        }
    };

    this.getDateFormatted = function () {
        var date = new Date();
        var currentDate = date.getDate();
        var currentMonth = date.getMonth() + 1;
        var currentYear = date.getFullYear();

        return currentYear + "/" + currentMonth + "/" + currentDate;
    };

    this.getSanctionsTypes = function (service, route) {
        return $.getSanctionsTypes(this.GetUrlApiService(service) + route);
    };

    this.getRoutesByCompany = function (service, route, idCompany) {
        return $.getRoutesByCompany(this.GetUrlApiService(service) + route, idCompany);
    };

    this.getSanctionTypeById = function (service, route, idType) {
        return $.getSanctionType(this.GetUrlApiService(service) + route, idType);
    };

    this.verifyIsNotDuplicatedSanctionType = function (service, route, data) {
        return $.verifyNotExistingSanctionType(this.GetUrlApiService(service) + route, data);
    };

    this.getDriversByCompany = function (service, route, idCompany) {
        return $.getDriversByCompany(this.GetUrlApiService(service) + route, idCompany);
    };

    this.getBusesByCompany = function (service, route, idCompany) {
        return $.getBusesByCompany(this.GetUrlApiService(service) + route, idCompany);
    };

    this.getSanctionsByType = function (service, route, idType) {
        return $.getSanctionsByType(this.GetUrlApiService(service) + route, idType);
    };
    this.getFineByType = function (service, route, idType) {
        return $.getFineByType(this.GetUrlApiService(service) + route, idType);
    };

    this.getCompanyByCorpId = function (service, route, corpId) {
        return $.getCompanyByCorpId(this.GetUrlApiService(service) + route, corpId);
    };

    this.getCompanyByInfo = function (service, route, company) {
        return $.getCompanyByInfo(this.GetUrlApiService(service) + route, company);
    };
}

$.post = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'POST',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
};

$.put = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {};
    }
    return $.ajax({
        url: url,
        type: 'PUT',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
};

$.delete = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {};
    }
    return $.ajax({
        url: url,
        type: 'DELETE',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
};

$.getDriversByCompany = function (url, companyId) {
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

$.getCompanyByCorpId = function (url, corpId) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "corpId": corpId },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};

$.getSanctionsByType = function (url, idType) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "idType": idType },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};

$.getFineByType = function (url, idType) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "idType": idType },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};

$.getBusesByCompany = function (url, companyId) {
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

$.getCompanyByInfo = function (url, company) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: {
            "name": company.Name,
            "corpIdentification": company.CorpIdentification,
            "email": company.Email
        },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};

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
};

$.getSanctionType = function (url, idType) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "idType": idType },
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

$.verifyNotExistingSanctionType = function (url, value) {
    var info;

    $.ajax({
        type: 'GET',
        async: false,
        url: url,
        dataType: 'json',
        data: { "value": value },
        error: function () {
        },
        success: function (result) {
            info = result.Data;
        }
    });

    return info;
};


$.getCredidCard = function (url) {
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
};


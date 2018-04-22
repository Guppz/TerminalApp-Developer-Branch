function vExitRamp() {

    var self = this;

    var txtExitRampId = $("#txtExitRampId");

    var selectTerminal = $("#selectTerminal");
    var selectCompany = $("#selectCompany");
    var selectRoute = $("#selectRoute");

    var modalID = "modalExitRamp";
    var myModal = $("#" + modalID);

    var fsEditExitRampId = "fsEditExitRamp";

    self.tblExitRampId = "tblExitRamp";
    self.serviceTerminal = "terminal";
    self.serviceExitRamp = "exitRamp";
    self.serviceCompany = "company/RetrieveCompaniesByTerminal";
    self.serviceBus = "bus";
    self.serviceRoute = "route";
    self.ctrlActions = new ControlActions();
    self.columns = "IdExitRamp,Name,Location";

    self.user = JSON.parse(sessionStorage.getItem("user"));

    var tblExitRamp = $("#" + self.tblExitRampId);    

    var exitRampSchema = {
        IdExitRamp: function (value) {
            return "";
        },
        Name: function (value) {
            if (value === "") {
                return "Por favor ingrese el nombre de la Rampa de Salida";
            }
            return "";
        }
    };
    var routeSchema = {
        IdRoute: function (value) {
            return "";
        }
    }

    self.RetrieveAll = function () {

        self.ctrlActions.FillTable(self.serviceExitRamp, self.tblExitRampId, false, );
    };

    self.ReloadTable = function () {
        self.ctrlActions.FillTable(self.serviceExitRamp, self.tblExitRampId, true);
    };

    self.Create = function () {
        var exitRampData = self.ctrlActions.GetDataForm("fsEditExitRamp");
        var routeData = { IdRoute: selectRoute.val() };
        var terminalData = { IdTerminal: selectTerminal.val() };

        let validExitRamp = self.ctrlActions.validateData(exitRampData, exitRampSchema, "fsEditExitRamp");
        let validRoute = self.ctrlActions.validateData(routeData, routeSchema);

        if (validExitRamp) {
            if (validRoute) {
                //Hace el POST
                exitRampData.Terminal = terminalData;
                exitRampData.Route = routeData;
                self.ctrlActions.PostToAPI(self.serviceExitRamp, exitRampData);

                myModal.modal("hide");
                CleanModal(exitRampSchema);
                self.showAlertModal('La Rampa se ha creado con exito!');
            }
        }
        //Refresca la tabla
        self.ReloadTable();
    };

    self.Update = function () {
        var exitRampData = self.ctrlActions.GetDataForm("fsEditExitRamp");
        var routeData = { IdRoute: selectRoute.val() };

        let validExitRamp = self.ctrlActions.validateData(exitRampData, exitRampSchema);
        let validRoute = self.ctrlActions.validateData(routeData, routeSchema);

        if (validExitRamp) {
            if (validRoute) {
                exitRampData.Route = routeData;
                //Hace el PUT a ExitRamp
                self.ctrlActions.PutToAPI(self.serviceExitRamp, exitRampData);

                myModal.modal("hide");
                CleanModal(exitRampSchema);
                self.showAlertModal('La Rampa se ha creado con exito!');
            }
        }
        //Refresca la tabla
        self.ReloadTable();
    };

    self.Delete = function () {
        var exitRampData = {};
        exitRampData.IdExitRamp = parseInt($("#txtExitRampId").val());

        //Hace el DELETE
        self.ctrlActions.DeleteToAPI(self.serviceExitRamp, exitRampData);

        myModal.modal("hide");
        CleanModal();

        //Refresca la tabla
        self.ReloadTable();
        self.showAlertModal('La Rampa se ha eliminado con exito!');
    };

    self.BindFields = function (data) {
        CleanModal();

        $("#btnDelete").removeAttr("disabled").show();
        $("#btnUpdate").removeAttr("disabled").show();

        self.ctrlActions.BindFields(fsEditExitRampId, data);

        selectModalSelects(data);

        ToggleModal();
    };

    self.OpenModal = function () {
        CleanModal();
        $("#btnCreate").removeAttr("disabled").show();

        ToggleModal();
    };

    var ToggleModal = function () {
        myModal.modal("toggle");
    };

    var CleanModal = function () {
        self.ctrlActions.cleanForm(exitRampSchema);
        txtExitRampId.attr("disabled", "disabled").removeClass("is-invalid").val("");
        txtExitRampId.parent('div').hide();
        
        $("#txtExitRampName").removeClass("is-invalid").val("");

        $("#btnCreate").attr("disabled", "disabled").hide();
        $("#btnUpdate").attr("disabled", "disabled").hide();
        $("#btnDelete").attr("disabled", "disabled").hide();

        filterRouteByTerminalSelect();
    };

    function removeEmptyTerminalOption() {
        if (selectTerminal.find('option').length > 0) {
            selectTerminal.find('option').first().remove();
        }
    }

    function filterTableByTerminalSelect() {
        tblExitRamp.dataTable.ext.search.push(function (settings, data, dataIndex) {
            return data[0] == selectTerminal.val();
        });
        tblExitRamp.DataTable().draw();
    }

    function filterRouteByTerminalSelect() {
        var options = selectRoute.find("option");
        options.hide();
        options.first().show().prop("selected", true);
        options = selectRoute.find("option[data-terminalid='" + selectTerminal.val() + "']");
        options.show();
    }

    function fillModalSelects() {
        $.get(self.ctrlActions.GetUrlApiService(self.serviceRoute), {}, fillRouteSelect, "json");
    }

    function fillRouteSelect(response) {
        var routes = response.Data;
        selectRoute.empty();
        selectRoute.append("<option value=''>Seleccione una Ruta</option>");
        for (var i = 0; i < routes.length; i++) {
            selectRoute.append("<option value=" + routes[i].IdRoute + " data-CompanyId=" + routes[i].RouteCompany.IdCompany + " data-TerminalId=" + routes[i].RouteTerminal.IdTerminal + ">" + routes[i].Name + "</option>");
        }
    }

    function selectModalSelects(pExitRamp) {
        if (pExitRamp.Route.IdRoute > 0) {
            selectRoute.val("" + pExitRamp.Route.IdRoute).prop('selected', true);
            if (pExitRamp.Route.RouteCompany.IdCompany > 0) {
                selectCompany.val("" + pExitRamp.Route.RouteCompany.IdCompany).prop('selected', true);
            }
        }
    }

    function filterByTerminal() {
        filterTableByTerminalSelect();
    }

    self.init = function () {
        if (self.user.UserTerminal.IdTerminal > 0) {
            selectTerminal.parent().parent().hide();
        }
        removeEmptyTerminalOption();
        self.RetrieveAll();

        if (self.user.UserTerminal.IdTerminal > 0) {
            selectTerminal.val(self.user.UserTerminal.IdTerminal);
        } else {
            selectTerminal.find("option").first().prop("selected", true);
            selectTerminal.change(filterByTerminal);
        }

        tblExitRamp.DataTable().column(0).visible(false);

        fillModalSelects();
        filterTableByTerminalSelect();
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

//ON DOCUMENT READY
$(document).ready(function () {
    var vexitRamp = new vExitRamp();
    vexitRamp.init();
});
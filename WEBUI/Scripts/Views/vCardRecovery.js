
function vCardRecovery() {
    var self = this;
    var Login = JSON.parse(sessionStorage.user);
    var myModal = $("#modalCardRecovery");
    var inputGIN = $("#txtGIN");
    var selectRecoveryType = $("#selectRecoveryType");

    self.viewName = "vCardRecovery";
    self.tblCard = "tblCardRecovery";
    self.service = "Card/RetrieveCardRecovery?idUser=4";

    self.viewName = "vCardRecovery";
    self.service = "card";
    self.serviceTerminal = "Card/RetrieveByTerminal?idTerminal=" + Login.UserTerminal.IdTerminal;
    self.serviceRecovery = "card/recovery";
    self.ctrlActions = new ControlActions();
    self.columns = "IdCard,Status,CrType.CardName,Terminal.Name,User.Identification";

    self.user = JSON.parse(sessionStorage.getItem("user"));

    var CardSchema = {
        GIN: function (value) {
            return "";
        }
    };

    self.RetrieveAll = function () {
        if (self.user.UserTerminal.IdTerminal > 0) {
            self.ctrlActions.FillTable(self.serviceTerminal, self.tblCard, false);
        } else {
            self.ctrlActions.FillTable(self.service, self.tblCard, false);
        }
    };

    self.ReloadTable = function () {
        if (self.user.UserTerminal.IdTerminal > 0) {
            self.ctrlActions.FillTable(self.serviceTerminal, self.tblCard, true);
        } else {
            self.ctrlActions.FillTable(self.service, self.tblCard, true);
        }
    };

    self.ReIssueCard = function () {

        var recoveryData = {};
        recoveryData.type = parseInt(selectRecoveryType.val());
        recoveryData.card = self.ctrlActions.GetDataForm("fsCard");

        // POST to Card/cardRecovery
        self.ctrlActions.PostToAPI(self.serviceRecovery, recoveryData);
        myModal.modal("hide");
        CleanModal();

        //Refresca la tabla
        self.ReloadTable();
        self.showAlertModal('La Tarjeta se ha reexpedido con exito!');
    };

    self.Delete = function () {
        var cardData = {};
        cardData = self.ctrlActions.GetDataForm("fsCard");

        //Hace el DELETE
        self.ctrlActions.DeleteToAPI(self.service, cardData);
        myModal.modal("hide");
        CleanModal();

        //Refresca la tabla
        self.ReloadTable();
        self.showAlertModal('La Tarjeta se ha desactivado con exito!');
    };

    self.Cancel = function () {
        //Refresca la tabla
        self.ReloadTable();
        myModal.modal("hide");

    };

    self.BindFields = function (data) {
        CleanModal();

        self.ctrlActions.BindFields("fsCard", data);

        myModal.modal("toggle");
    };

    self.OpenModal = function () {
        CleanModal();
        myModal.modal("toggle");
    };

    self.initModal = function () {
        inputGIN.attr("disabled", "disabled");
    };

    function CleanModal() {
        inputGIN.val("");
        selectRecoveryType.val(1);
    }

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
            text: "Desea eliminar esta Tarjeta?",
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

    self.alertReIssue = function () {
        swal({
            title: "Se encuentra seguro?",
            text: "Desea re-expedir esta Tarjeta?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Re-Expedir!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    self.ReIssueCard();
                }
            });
    }

    return self;
}

//ON DOCUMENT READY
//$(document).ready(function () {
//    var vcardRecovery = new vCardRecovery();
//    vcardRecovery.initModal();
//    vcardRecovery.RetrieveAll();
//});

$(document).ready(function () {
    let user = JSON.parse(sessionStorage.getItem("user"));
    let canEnter = false;
    let page = new vCardRecovery();
    let pageview = page.viewName;
    if (user === null) {

        window.location.replace(window.location.origin);
    }
    let views = user.ViewList;
    for (var i = 0; i < views.length; i++) {
        if (views[i].ViewName === pageview) {
            canEnter = true;
        }

    }
    if (!canEnter) {
        window.location.replace(window.location.origin);
    } else {
        var vcardRecovery = new vCardRecovery();
        vcardRecovery.initModal();
        vcardRecovery.RetrieveAll();
    }
});
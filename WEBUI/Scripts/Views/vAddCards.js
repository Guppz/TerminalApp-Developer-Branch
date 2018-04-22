
function vAddCards() {
    this.viewName = "vAddCards";
    this.tblUserId = 'tblUser';
    this.service = 'User';
    this.serviceCards = 'card';
    this.serviceCardsType = 'CardType';
    this.ctrlActions = new ControlActions();
    this.columns = "PkIdUser,Name,LastName,Email";
    self = this;

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, false,);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, true);
    }

    var CardSchema = {
        DaysForNotification: function (value) {
            var regularEx = /^[+]?\d+([.]\d+)?$/;
            if (!regularEx.test(value)) {
                return "No numeros negativos";
            }
            if (value > 30) {
                return "No mas de 30 dias";
            }
            return "";
        },
        CardType: function (value) {
            if (value === "") {
                return "Selecione un tipo de tarjeta";
            }
            return "";
        },
        Terminal: function (value) {
            if (value.IdTerminal === undefined) {
                return "Selecione un tipo de Terminal";
            }
            return "";
        }
    }

    this.Create = function () {
        var userData = {};
        let isValid = true;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        userData.IdUser = parseInt($("#btnCreate").attr("name"));
        userData.IdTerminal = $('#TerminalComboBox option:selected').val()
        userData.Card = { DaysForNotification: userData.DaysForNotification, Balance: userData.Balance, DaysForNotification: userData.DaysForNotification };
        userData.Terminal = { IdTerminal: userData.IdTerminal };
        userData.User = { IdUser: userData.IdUser };
        userData.CrType = { IdCardType: userData.CardType };

        isValid = this.ctrlActions.validateData(userData, CardSchema);
        if (isValid) {
            this.ctrlActions.PostToAPI(this.serviceCards, userData);
            $('#myModal').modal("hide");
            $('#frmEdition')[0].reset();
            this.ReloadTable();
            self.showAlertModal('Se ha registrado con exito!');

        }
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
        $('#frmEdition')[0].reset();
        this.ctrlActions.cleanForm(CardSchema);
        $('#myModal').modal("toggle");
        $('#txtPkIdUser').attr('disabled', 'disabled');
        $('#btnCreate').attr('name', data.IdUser);
        this.ctrlActions.BindFields('frmEdition', data);
        this.comboBoxTerminalSelector();
    }

    this.OpenModal = function (data) {
        $('#frmEdition')[0].reset();
        this.ctrlActions.cleanForm(CardSchema);
        $('#txtPkIdUser').attr('disabled', 'disabled');
        $('#myModal').modal("toggle");
    }
    this.filterTableByClientSelect = function () {
        $("#" + this.tblUserId).dataTable.ext.search.push(function (settings, data, dataIndex) {
            return data[4] == ('Cliente');
        });
        $("#" + this.tblUserId).DataTable().draw();
    }
    this.comboBoxTerminalSelector = function () {
        var user = JSON.parse(sessionStorage.user);
        if (user.Roleslist[0].Name == "FullAdministrator") {
        } else {
            var target = 'TerminalComboBox';
            $('label[for="' + target + '"]').hide();
            $('#TerminalComboBox').hide();
            var length = $('#TerminalComboBox > option').length;
            for (var i = 0; i < length; i++) {
                $("#TerminalComboBox")[0].selectedIndex = i;
                if (user.UserTerminal.Name === $("#TerminalComboBox option:selected ").text()) {
                    $("#TerminalComboBox")[0].selectedIndex = i;
                    break;
                }
            }
        }
    }
}
//ON DOCUMENT READY
$(document).ready(function () {
        var vaddCards = new vAddCards();
        vaddCards.RetrieveAll();
        vaddCards.filterTableByClientSelect();
});


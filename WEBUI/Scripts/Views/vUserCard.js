
function vUserCards() {
    var Login = JSON.parse(sessionStorage.user);
    this.tblCardId = 'tblUserCards';
    this.service = 'Card/RetrieveUserCards?idUser=' + Login.IdUser;
    this.serviceCC = 'http://localhost:61693/api/CredidCard/GetCredidCard?idUser=' + Login.IdUser;
    var stripeData = {};
    this.serviceUpdate = 'Card';
    this.serviceCredidCard = 'CredidCard';
    this.viewName = "vUserCards";
    this.serviceUpdateActivacion = 'card/PutActivacion';
    this.serviceUpdateNotifacion = 'card/PutNotification';
    this.serviceUpdatePayment = 'card/PutNewBalance';
    this.ctrlActions = new ControlActions();
    this.columns = "IdCard,Balance,ExpiryDate,Status,Terminal.Name,CrType.CardName";
    self = this;

    var CardPaymentSchema = {
        CredidCard: function (value) {
            var regularEx = /^((4\d{3})|(5[1-5]\d{2})|(6011)|(34\d{1})|(37\d{1}))-?\s?\d{4}-?\s?\d{4}-?\s?\d{4}|3[4,7][\d\s-]{15}$/;
            if ($('#txtTarjetas:checked').val() == "on") {
                return "";
            }else
            if (!regularEx.test(value)) {

                return "Formato incorrecto";
            }
            return "";
        },
        newBalance: function (value) {
            var regularEx = /^[+]?\d+([.]\d+)?$/;
            if (!regularEx.test(value)) {
                return "No numeros negativos";
            }
            return "";
        },
        CredidCardBox: function (value) {
            if ($('#txtTarjetas:checked').val() == "on") {
                if (value == ""){
                    return "Selecione una opcion";
                }
            }
            return "";
        },
        cvc: function (value) {
            var regularEx = /^[0-9]{3,4}$/;
            if (!regularEx.test(value)) {
                return "Cvc incorrecto";
            }
            return "";
        },
        
        expiry: function (value) {
            var today = new Date();
            var date = value.split("/");
            var day = date[0];
            var year = "20" + date[1];
            var todayDay = today.getMonth() + 1;
            var todayYear = today.getFullYear();
            todayDay = "0" + todayDay;
            if (year =="20undefined") {
                return "Formato incorrecto";
            }
            if (year < todayYear) {
                return "Fecha expirada";
            }
            if (year == todayYear){
                if (day <= todayDay){
                    return "Fecha expirada";
                }
            }

            return "";
        }
    };

    var NotificacionSchema = {
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
    };


    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblCardId, false);
    };

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCardId, true);
    };

    this.fillCredidCardCombobox = function () {
        this.fillTypes(this.serviceCC);
    };

    this.UpdateSaldo = function () {
        var userData = {};
        var cards = JSON.parse(sessionStorage.card);
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        Stripe.setPublishableKey('pk_test_M4kDH63BeaaDl6O8B3fWXjLM');
        var balanceInt = parseInt(userData.Balance);
        var NuevoSaldoInt = parseInt(userData.newBalance);
        userData.Balance = balanceInt + NuevoSaldoInt;
        userData.User = { IdUser: Login.IdUser };
        userData.Terminal = { IdTerminal: cards.Terminal.IdTerminal };
        userData.agreement = { IdAgreement: 0 };

        stripeData = userData;
        isValid = this.ctrlActions.validateData(userData, CardPaymentSchema);
        if (isValid){
            this.ctrlActions.PutToAPI(this.serviceUpdatePayment, userData);
            Stripe.createToken({
                number: userData.CredidCard,
                cvc: userData.cvc,
                exp_month: '12',
                exp_year: '30'
            }, StripeResponseHandler);
            if ($('#txtYes:checked').val() == 'on') {
                var credidCard = {};
                userData.CredidCard = userData.CredidCard.replace(/ /g, '');
                credidCard.credidCardNum = userData.CredidCard
                credidCard.User = Login;
                console.log(credidCard);
                this.ctrlActions.PostToAPI(this.serviceCredidCard, credidCard);
            }

            $('#myModal').modal("hide");
            this.ctrlActions.cleanForm(CardPaymentSchema);
            this.ReloadTable();

            self.showAlertModal('Se ha regirecargado el saldo con exito!');

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



    function StripeResponseHandler(status, response) {
        var dollars = stripeData.newBalance / 567;
        var cents = Math.round(dollars * 100);
        console.log(cents);
        console.log(Login.Email);
        $.ajax({
            type: 'POST',
            url: 'https://api.stripe.com/v1/charges',
            headers: {
                Authorization: 'Bearer sk_test_w5GD9ZUSlIra2c06fc3CK9iH'
            },
            data: {
                amount: cents,
                currency: 'USD',
                source: response.id,
                description: "Charge for " + Login.Email
            },
            success: (response) => {
                console.log('successful payment: ', response);
            },
            error: (response) => {
                console.log('error payment: ', response);
            }
        })
    }

    this.fillTypes = function (serviceCC) {
        //var typesList = this.ctrlActions.getCredidCard(serviceCC);
        var info;
        $.ajax({
            type: 'GET',
            async: false,
            url: serviceCC,
            dataType: 'json',
            data: {},
            error: function () {
            },
            success: function (result) {
                info = result.Data;
            }
        });

        var dropdown = $('#CredidCardBox');
        $.each(info, function (key, value) {
            var replace = value.credidCardNum.replace(/\d(?=\d{4})/g, "*");
            dropdown.append($('<option />').val(value.credidCardNum).text(replace));
        });
    };


    this.UpdateNoti = function () {

        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        isValid = this.ctrlActions.validateData(userData, NotificacionSchema);
        if (isValid) {
            this.ctrlActions.PutToAPI(this.serviceUpdateNotifacion, userData);
            $('#myModal').modal("hide");
        }

        //Refresca la tabla

        this.ReloadTable();
    };

    this.Delete = function () {

        var userData = {};
        let isValid;
        userData.IdUser = parseInt($("#btnDelete").attr("name"));
        //Hace el post al create
        this.ctrlActions.DeleteToAPI(this.service, userData);
        $('#myModal').modal("hide");
        cleanCheckbox();
        this.ctrlActions.cleanForm(userSchema);

        //Refresca la tabla
        this.ReloadTable();

    

    };

    this.BindFields = function (data) {
        this.ctrlActions.cleanForm(CardPaymentSchema);
        this.ctrlActions.cleanForm(NotificacionSchema);
        this.ctrlActions.BindFields('frmEdition', data);
        sessionStorage.setItem('card', JSON.stringify(data));
        $('#myModal').modal("toggle");
        $('#txtGin').attr('disabled', 'disabled');
        $('#txtBalance').attr('disabled', 'disabled');
        $('#txtCredidCard').val('');
        $('#cc-exp').val('');
        $('#cc-cvc').val('');
        $('#mc').css("color", "");
        $('#visa').css("color", "");
        $('#amex').css("color", "");
        $('#dis').css("color", "");
    };

    this.OpenModal = function () {
        this.ctrlActions.cleanForm(CardPaymentSchema);
        $('#txtGin').removeAttr('disabled');
        $('#myModal').modal("toggle");
    };
}

//ON DOCUMENT READY
$(document).ready(function () {
    $('#txtYes').css('form-check-input');
    $('#txtNo').css('form-check-input');
    $('#CredidCardBox').css({ "display": "none" });
    $('#txtCredidCard').addClass("input-credit-card");
    $('#cc-exp').addClass("input-date-b");
    $('#cc-cvc').attr('maxlength', '4');
    var vuserCards = new vUserCards();
    vuserCards.fillCredidCardCombobox();
    vuserCards.RetrieveAll();
});

$(document).ready(function () {
    var $checkbox = $('#txtTarjetas');
    $('#txtYes').css('text-align', 'center');
    $('#txtTarjetas').css('text-align', 'center');
    $checkbox.change(function (){
        if ($('#txtTarjetas:checked').val() == "on") {
            $('#CredidCardBox').css({ "display": "" });
            $('#txtCredidCard').css({ "display": "none" });
            //$('#txtCredidCard').hide();
            $('#txtYes').attr('disabled', 'disabled');
            $('#txtYes').prop('checked', false);
        }
        if ($('#txtTarjetas:checked').val() == undefined) {
            $('#CredidCardBox').css({ "display": "none" });
            $('#txtCredidCard').css({ "display": "" });
            $('#txtYes').attr('disabled', false);
            $('#txtCredidCard').val("");

        }    
    });

    $('#CredidCardBox').change(function () {
        var value = $('#CredidCardBox').val();
        $('#txtCredidCard').val(value);
    });

    var cleave = new Cleave('.input-credit-card', {
        creditCard: true,
        onCreditCardTypeChanged: function (type) {
            console.log(type);
            if (type =='visa'){
                $('#visa').css("color", "dodgerblue");
                $('#mc').css("color", "");
                $('#amex').css("color", "");
                $('#dis').css("color", "");

            }
            if (type == 'mastercard') {
                $('#mc').css("color", "dodgerblue");
                $('#visa').css("color", "");
                $('#amex').css("color", "");
                $('#dis').css("color", "");
            }

            if (type == 'amex') {
                $('#mc').css("color", "");
                $('#visa').css("color", "");
                $('#amex').css("color", "dodgerblue");
                $('#dis').css("color", "");
            }
            if (type == 'discover') {
                $('#mc').css("color", "");
                $('#visa').css("color", "");
                $('#amex').css("color", "");
                $('#dis').css("color", "dodgerblue");
            }
            if (type == 'unknown') {
                $('#mc').css("color", "");
                $('#visa').css("color", "");
                $('#amex').css("color", "");
                $('#dis').css("color", "");
            }
            
        }
    });

    var cleaveDate = new Cleave('.input-date-b', {
        date: true,
        datePattern: ['m', 'y']
    });
});





    



function vStudiantCards() {
    var Login = JSON.parse(sessionStorage.user);
    this.tblCardId = 'tblStudiantCards';
    this.service = 'Card/RetrieveByStudiant?IdTerminal=1';  /*Login.UserTerminal.IdTerminal*/
    this.serviceDelete = 'Card';
    this.viewName = "vStudiantCards";
    this.ctrlActions = new ControlActions();
    this.columns = "IdCard,Balance,ExpiryDate,Status,Terminal.Name,CrType.CardName";
    

   this.RetrieveAll = function () {
       this.ctrlActions.FillTable(this.service, this.tblCardId, false);
    };

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCardId, true);
    };

    this.openActiveWindow = function () {
        window.open('/Home/vStudiantCardsActive', '_self');
    };



    this.UpdateActivation = function () {

        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        console.log(userData);
        //Hace el post al create
        isValid = true;
        if (isValid) {
            this.ctrlActions.PutToAPI(this.serviceUpdateActivacion, userData);
            $('#myModal').modal("hide");
            this.ReloadTable();
        }

        //Refresca la tabla

        this.ReloadTable();
    };

    this.Delete = function () {

        var userData = {};
        let isValid;
        console.log(userData);
        userData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create
        this.ctrlActions.DeleteToAPI(this.serviceDelete, userData);
        $('#myModal').modal("hide");
        //Refresca la tabla
        this.ReloadTable();

    };

    this.BindFields = function (data) {
        $('#myModal').modal("toggle");
        $('#txtGin').attr('disabled', 'disabled');
        this.ctrlActions.BindFields('frmEdition', data);

    };

    this.OpenModal = function () {
        this.ctrlActions.cleanForm(cardSchema);
        $('#txtGin').removeAttr('disabled');
        $('#myModal').modal("toggle");
    };
}

//ON DOCUMENT READY
$(document).ready(function () {
    let user = JSON.parse(sessionStorage.getItem("user"));
    let canEnter = false;
    let page = new vStudiantCards();
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
        var vstudiantCards = new vStudiantCards();
        vstudiantCards.RetrieveAll();
    }
});




    



function vStudiantCardsActive() {
    var Login = JSON.parse(sessionStorage.user);
    this.tblCardId = 'tblStudiantCards';
    this.service = 'Card/RetrieveByStudiant';
    this.serviceDelete = 'Card';
    this.serviceDisabled = 'Card/RetrieveByStudiantCardDisables?idTerminal='+Login.UserTerminal.IdTerminal;
    this.viewName = "vStudiantCardsActive";
    this.ctrlActions = new ControlActions();
    this.columns = "IdCard,Balance,ExpiryDate,Status,Terminal.Name,CrType.CardName";
    this.serviceUpdateActivacion = 'card/PutActivacion';

   


    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.serviceDisabled, this.tblCardId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.serviceDisabled, this.tblCardId, true);
    }

    this.openActiveWindow = function () {
        window.open('/Home/vStudiantCards', '_self');
    }



    this.UpdateActivation = function () {

        var userData = {};
        let isValid;
        userData = this.ctrlActions.GetDataForm('frmEdition');
        var card = JSON.parse(sessionStorage.getItem("Data"));
        userData.Notification = card.Notification
        //Hace el post al create
        isValid = true;
        if (isValid) {
            this.ctrlActions.PutToAPI(this.serviceUpdateActivacion, userData);
            $('#myModal').modal("hide");
        }
        
        //Refresca la tabla
       
        this.ReloadTable();
    }

    this.Delete = function () {

        var userData = {};
        let isValid;
        console.log(userData);
        userData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create
        this.ctrlActions.DeleteToAPI(this.serviceDelete, userData);
            $('#myModal').modal("hide");
            cleanCheckbox();
        //Refresca la tabla
        this.ReloadTable();

    }

    this.BindFields = function (data) {
        sessionStorage.setItem('Data', JSON.stringify(data));
        $('#myModal').modal("toggle");
        $('#txtGin').attr('disabled', 'disabled');
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.OpenModal = function () {
        this.ctrlActions.cleanForm(cardSchema);
        $('#txtGin').removeAttr('disabled');
        $('#myModal').modal("toggle");
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    let user = JSON.parse(sessionStorage.getItem("user"));
    let canEnter = false;
    let page = new vStudiantCardsActive();
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
        var vstudiantCardsActive = new vStudiantCardsActive();
        vstudiantCardsActive.RetrieveAll();
    }
});




    


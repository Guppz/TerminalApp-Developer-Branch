function UiExample() {
    this.tblCustomersId = 'tblCustomers';
    this.service = 'customer';
    this.ctrlActions = new ControlActions();
    this.columns = "Id,Name,LastName,Age";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, true);
    }

    this.Create = function () {
        var customerData = {};
        customerData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.PostToAPI(this.service, customerData);
        this.ReloadTable();
    }

    this.Update = function () {
        var customerData = {};
        customerData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.PutToAPI(this.service, customerData);
        this.ReloadTable();
    }

    this.Delete = function () {
        var customerData = {};
        customerData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.DeleteToAPI(this.service, customerData);
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        $('#myModal').modal("toggle");
        $('#btnCreate').attr('disabled', 'disabled');
        $('#btnUpdate').removeAttr('disabled');
        $('#btnDelete').removeAttr('disabled');
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.OpenModal = function () {
        $('#myModal').modal("toggle");
        $('#btnUpdate').attr('disabled', 'disabled');
        $('#btnDelete').attr('disabled', 'disabled');
        $('#btnCreate').removeAttr('disabled');
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var uiexample = new UiExample();
    uiexample.RetrieveAll();

});


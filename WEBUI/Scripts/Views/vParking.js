
function vParking() {
    var self = this;

    this.tblUserId = 'tblParking';
    this.service = 'Parking';
    this.viewName = "vParking";
    this.ctrlActions = new ControlActions();


    var parkingSchema = {
        Terminal: function (value) {
            if (value === "") {
                return "Por favor seleccione terminal"
            }
            return "";
        },
        ParkingType: function (value) {
            if (value === "") {
                return "Por favor seleccione tipo de parqueo"
            }
            return "";
        },
        AvailableSpaces: function (value) {
            if (value <= 0 || $("#txtOcupados").val() > value) {
                return "La cantidad de campos no es suficiente";
            }
            return "";
        },
        OccupiedSpces: function (value) {
            if (value < 0 || $("#txtEspacios").val() < value || value == "") {
                return "La cantidad de campos no es la correcta";
            }
            return "";
        },
        RentalCost: function (value) {
            if (value <= 0 ) {
                return "La cantidad a cobrar tiene que se mayor a 0";
            }
            return "";
        }
    }



    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, false, );
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblUserId, true);
    }

    this.Create = function () {
        var parkingdata = {};
        let isValid;

        parkingdata = this.ctrlActions.GetDataForm('frmEdition');

        isValid = this.ctrlActions.validateData(parkingdata, parkingSchema);
        parkingdata.Terminal = { IdTerminal: parkingdata.Terminal };

        if (isValid) {
            this.ctrlActions.PostToAPI(this.service, parkingdata);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanFormSchema(parkingSchema);
            this.ReloadTable();
            self.showAlertModal('El parqueo se ha registrado con exito!');

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

    this.Update = function () {

        var parkingdata = {};
        let isValid;
        parkingdata = this.ctrlActions.GetDataForm('frmEdition');

        parkingdata.IdParking = parseInt($("#btnUpdate").attr("name"));
        isValid = this.ctrlActions.validateData(parkingdata, parkingSchema)
        parkingdata.Terminal = { IdTerminal: parkingdata.Terminal };
        if (isValid) {
            this.ctrlActions.PutToAPI(this.service, parkingdata);
            $('#myModal').modal("hide");
            this.ctrlActions.cleanFormSchema(parkingSchema);
            this.ReloadTable();
            self.showAlertModal('El parqueo se ha actualizado con exito!');

        }



    };
    this.SatatusAlertDesactive = function () {
        var agreementData = {};
        agreementData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea eliminar este parqueo?",
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
                    swal("Eliminado!", "Este parqueo ha sido eliminado con exito.", "success")



                }
            });

    }

    this.Delete = function () {

        var parkingdata = {};
        let isValid;
        parkingdata.IdParking = parseInt($("#btnDelete").attr("name"));

        //Hace el post al create

        this.ctrlActions.DeleteToAPI(this.service, parkingdata);
   
        $('#myModal').modal("hide");
      
        this.ctrlActions.cleanFormSchema(parkingSchema);

        //Refresca la tabla
        this.ReloadTable();

    }

    this.BindFields = function (data) {
        this.ctrlActions.cleanFormSchema(parkingSchema);
        $('#myModal').modal("toggle");
        $('.modal-title').text('Editar Parqueo');
        $("#terminalComboBox").find("option").first().prop("selected", true);
        $("#typeComboBox").find("option").first().prop("selected", true);

        $('#txtPkIdUser').attr('disabled', 'disabled').hide();
        $('#btnCreate').attr('disabled', 'disabled').hide();
        $('#btnUpdate').removeAttr('disabled');
        $('#btnUpdate').attr('name', data.IdParking);
        $('#btnDelete').removeAttr('disabled');
        $('#btnDelete').attr('name', data.IdParking);
        $('#terminalComboBox').attr('disabled', 'disabled');
        $('#typeComboBox').attr('disabled', 'disabled');
        $("#terminalComboBox").removeClass("is-invalid");
        $("#typeComboBox").removeClass("is-invalid");
        $("label[for='textPassword']").show();
        $("label[for='textPassword']").show();
        $('#btnUpdate').show();
        $('#btnDelete').show();
        this.ctrlActions.BindFields('frmEdition', data);
        $("#terminalComboBox").val(data.Terminal.IdTerminal);
    }




    this.OpenModal = function () {
        this.ctrlActions.cleanFormSchema(parkingSchema);
        $("#terminalComboBox").val("");
        $("#typeComboBox").val("");
        $('#typeComboBox').removeAttr('disabled').show();
        $('#terminalComboBox').removeAttr('disabled').show();
        $('#txtPkIdUser').removeAttr('disabled').show();
        $('#myModal').modal("toggle");
        $('.modal-title').text('Registrar Parqueo');
        $("#terminalComboBox").find("option").first().prop("selected", true);
        $("#typeComboBox").find("option").first().prop("selected", true);
        $("#terminalComboBox").removeClass("is-invalid");
        $("#typeComboBox").removeClass("is-invalid");

        $('#btnUpdate').attr('disabled', 'disabled').hide();
        $('#btnDelete').attr('disabled', 'disabled').hide();
        $('#btnCreate').removeAttr('disabled').show();

    }


}

//ON DOCUMENT READY
$(document).ready(function () {
  /*  let user = JSON.parse(sessionStorage.getItem("user"));
    let canEnter = false;
    let page = new vUser();
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
    } else {*/
        var vparking = new vParking();
        vparking.RetrieveAll();
    //}




});


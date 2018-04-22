window.currentTypeData;

function vFineType() {

    this.tblFineId = 'tblFineType';
    this.service = 'FineType';
    this.ctrlActions = new ControlActions();
    self = this;
    var fineSchema = {
        TypeDescription: function (value) {
            if (value === "") {
                return "Digite una descripción por favor";
            }
            return "";
        },
        Cost: function (value) {
            if (value === "") {
                return "Digite un costo por favor";
            }
            return "";
        },


        TypeName: function (value) {
            if (value === "") {
                return "Digite un nombre por favor";
            }
            return "";
        }
    }

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblFineId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblFineId, true);
    }

    this.Create = function () {
        var fineData = {};
        let isValid;
        fineData = this.ctrlActions.GetDataForm('frmEdition');

        isValid = this.ctrlActions.validateData(fineData, fineSchema);

        if (isValid) {
            //Hace el post al create
            this.ctrlActions.PostToAPI(this.service, fineData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El tipo de  multa se ha registrado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
    
            this.ctrlActions.cleanForm(fineSchema);

            //Refresca la tabla
        }


        this.ReloadTable();
    }

    this.Update = function () {

        var fineData = {};
        let isValid;
        fineData = this.ctrlActions.GetDataForm('frmEdition');
        fineData.IdType = parseInt($("#btnUpdate").attr("name"));
        isValid = this.ctrlActions.validateData(fineData, fineSchema)
        if (isValid) {
            this.ctrlActions.PutToAPI(this.service, fineData);
            swal({
                type: 'success',
                background: "#95c9d4",
                title: 'El tipo de  multa se ha actualizado con exito!',
                showConfirmButton: false,
                timer: 1500
            })
            $('#myModal').modal("hide");
            $('#txtPkIdFineType').val("");
            $('#txtName').val("");
            $('#txtDescription').val("");
            $('#txtCost').val("");
            this.ctrlActions.cleanForm(fineSchema);
        }

        this.ReloadTable();
    }
    this.Delete = function () {

        var fineData = {};
        let isValid;
        fineData.IdType = parseInt($("#btnDelete").attr("name"));

        //Hace el post al create

        this.ctrlActions.DeleteToAPI(this.service, fineData);
   
        $('#myModal').modal("hide");
        $('#txtPkIdFineType').val("");
        $('#txtName').val("");
        $('#txtDescription').val("");
        $('#txtCost').val("");
        this.ctrlActions.cleanForm(fineSchema);

        //Refresca la tabla
        this.ReloadTable();

    }

    this.delete = function () {
        self.deleteType();
    }

    this.cancelProcess = function () {
        $('#myModal').modal('hide');
        $('#alertBox').modal('hide');
    }

    this.deleteType = function () {
        var typeData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.DeleteToAPI(this.service + '/DeleteFineType', typeData);
        swal({
            type: 'success',
            background: "#95c9d4",
            title: 'El tipo de multa y la multa asociada se han eliminado con exito!',
            showConfirmButton: false,
            timer: 1500
        })
        self.cancelProcess();
        this.ctrlActions.cleanForm(fineSchema);
        this.ReloadTable();
    }


    this.verifyIfHasDependencies = function () {
        var typeData = this.ctrlActions.GetDataForm('frmEdition');
        var dependencies = this.ctrlActions.getFineByType(this.service, '/RetrieveFineTypeById', typeData.IdType);

        if (dependencies.length > 0) {
            $('#myModal').hide();
            $('#alertBoxHeader').text('Dependencias de Información');
            $('#alertBox').modal("toggle");
            self.cancelProcess();
        } else {
            self.deleteType();
        }
    }


    this.BindFields = function (data) {
        $('#txtPkIdFineType').parent('div').hide();
        currentTypeData = data;
        this.ctrlActions.cleanForm(fineSchema);
        self.updateButtons(2);
        $('.modal-title').text('Editar Tipo de multa');
        $('#txtPkIdFineType').attr('disabled', 'disabled');

        $('#btnUpdate').attr('name', data.IdType);
        $('#btnDelete').attr('name', data.IdType);
        $('#myModal').modal("toggle");
        this.ctrlActions.BindFields('frmEdition', data);
    }



    this.OpenModal = function () {
        this.ctrlActions.cleanForm(fineSchema);
        $('#frmEdition')[0].reset();
        $('#txtPkIdFineType').attr('disabled', 'disabled');

        $('#txtPkIdFineType').parent('div').hide();
        $('.modal-title').text('Registrar Tipo de multa');
        $('#myModal').modal("toggle");
        self.updateButtons(1);
 
    };
    this.updateButtons = function (value) {
        if (value === 1) {

            $('#btnCreate').show();
            $('#btnUpdate').hide();
            $('#btnDelete').hide();
        } else {

            $('#btnCreate').hide();
            $('#btnUpdate').show();
            $('#btnDelete').show();


        }
    };
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vfineType = new vFineType();
    vfineType.RetrieveAll();

});


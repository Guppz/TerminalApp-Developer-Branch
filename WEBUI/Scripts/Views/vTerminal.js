var mapModule = (function (me) {
    var self = me || {};
    var defaultState = {
        mapDivID: "",
        mapSearchInputID: "",
        coordinates: { lat: 9.9281, lng: -84.0907 },
        callback: function (lat, lng) { console.log("Callback(lat, lng) not defined") }
    };

    var geocoder = new google.maps.Geocoder;
    var coordinates = self.coordinates || defaultState.coordinates;
    var mapDiv = null;
    var map = null;
    var marker = null;

    var searchInput = null;
    var autocomplete = null;

    self.callback = self.callback || defaultState.callback;

    self.updateMarker = function (lat, lng) {
        var coordinates = {
            lat: lat,
            lng: lng
        };

        marker.setPosition(coordinates);

        updateSearchBox();

        self.callback(marker.getPosition().lat(), marker.getPosition().lng())
    };

    self.panToMarker = function () {
        map.panTo(marker.getPosition());
    }

    self.initMap = function (pNewMe) {
        if (pNewMe != null) {
            updateModule(pNewMe);
        }

        mapDiv = $("#" + self.mapDivID);
        searchInput = $("#" + self.mapSearchInputID);

        buildMap();
    };

    self.repositionAutocomplete = function () {
        if (searchInput != null) {
            newTop = searchInput.offset().top + searchInput.outerHeight();
            $(".pac-container").css("top", newTop + "px");
        }
    }

    function buildMap() {
        if (mapDiv != null) {
            map = new google.maps.Map(mapDiv[0], {
                zoom: 17,
                center: coordinates
            });

            marker = new google.maps.Marker({
                animation: google.maps.Animation.BOUNCE,
                draggable: false,
                map: map,
                position: coordinates,
                title: "Click to zoom"
            });

            map.addListener("click", function (event) {
                self.updateMarker(event.latLng.lat(), event.latLng.lng());
            });

            marker.addListener("click", function () {
                map.setZoom(8);
                map.setCenter(marker.getPosition());
            });

            buildSearchInput();
        }
    };

    function buildSearchInput() {
        var options = {

            componentRestrictions: { country: "cr" }
        };
        if (searchInput != null) {
            autocomplete = new google.maps.places.Autocomplete(searchInput[0], options);

            autocomplete.addListener("place_changed", function () {
                var place = autocomplete.getPlace();
                if (!place.geometry) {
                    window.alert("No details available for input: '" + place.name + "'");
                    return;
                }

                if (place.geometry.viewport) {
                    self.updateMarker(place.geometry.location.lat(), place.geometry.location.lng());
                    self.panToMarker();
                    map.setZoom(17);
                }
            });
        }
    };

    function updateModule(pNewMe) {
        if (pNewMe != null) {
            coordinates = pNewMe.coordinates || self.coordinates;
            autocomplete = pNewMe.mapSearchInputID || self.mapSearchInputID;
            self.mapDivID = pNewMe.mapDivId || self.mapDivID;
            self.callback = pNewMe.callback || self.callback;
        }
    };

    function updateSearchBox() {
        if (searchInput != null) {
            geocoder.geocode({
                location: marker.getPosition(),
                region: "cr"
            }, function (results, status) {
                console.log(results);
                console.log(status);
                searchInput.val(results[0].formatted_address);
            });
        }
    }

    return self;
})({ mapDivID: "mapTerminal", coordinates: { lat: 9.9281, lng: -84.0907 }, mapSearchInputID: "txtTerminalLocName" });

function vTerminal() {

    var self = this;
    var myLatLng = {
        lat: 9.9281,
        lng: -84.0907
    };
    var modalID = "modalTerminal";
    var myModal = $("#modalTerminal");

    this.tblTerminalId = "tblTerminal";
    this.service = "Terminal";
    this.serviceLoc = "Location";
    this.ctrlActions = new ControlActions();
    this.columns = "IdTerminal,Name,Location";

    var terminalSchema = {
        IdTerminal: function (value) {
            return "";
        },
        Name: function (value) {
            if (value === "") {
                return "Por favor ingrese el nombre de la Terminal";
            }
            return "";
        }
    };
    var locSchema = {
        IdLocation: function (value) {
            return "";
        },
        Name: function (value) {
            if (value === "") {
                return "Por favor ingrese el nombre de la Ubicacion";
            }
            return "";
        },
        Latitude: function (value) {
            return "";
        },
        Longitude: function (value) {
            return "";
        }
    }

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, true);
    }

    this.Create = function () {
        var terminalData = this.ctrlActions.GetDataForm("fsEditTerminal");
        var locData = this.ctrlActions.GetDataForm("fsEditTerminalLoc");

        let validTerminal = this.ctrlActions.validateDataByForm(terminalData, terminalSchema, "fsEditTerminal");
        let validLoc = this.ctrlActions.validateDataByForm(locData, locSchema, "fsEditTerminalLoc");

        if (validTerminal) {
            if (validLoc) {
                //Hace el POST
                terminalData.Location = locData;
                this.ctrlActions.PostToAPI(this.service, terminalData);
               
                myModal.modal("hide");
                CleanModal(terminalSchema);
                self.showAlertModal('La Terminal se ha registrado con exito!');
            }
        }
        //Refresca la tabla
        this.ReloadTable();
    }

    this.Update = function () {
        var terminalData = this.ctrlActions.GetDataForm("fsEditTerminal");
        var locData = this.ctrlActions.GetDataForm("fsEditTerminalLoc");

        let validTerminal = this.ctrlActions.validateDataByForm(terminalData, terminalSchema, "fsEditTerminal");
        let validLoc = this.ctrlActions.validateDataByForm(locData, locSchema, "fsEditTerminalLoc");

        if (validTerminal) {
            if (validLoc) {
                terminalData.Location = locData;
                //Hace el PUT a Location
                this.ctrlActions.PutToAPI(this.serviceLoc, locData);
                //Hace el PUT a Terminal
                this.ctrlActions.PutToAPI(this.service, terminalData);
            
                myModal.modal("hide");
                CleanModal(terminalSchema);
                self.showAlertModal('La Terminal se ha actualizado con exito!');
            }
        }
        //Refresca la tabla
        this.ReloadTable();
    }

    this.Delete = function () {
        var terminalData = {};
        terminalData.IdTerminal = parseInt($("#txtTerminalId").val())

        //Hace el DELETE
        this.ctrlActions.DeleteToAPI(this.service, terminalData);

        myModal.modal("hide");
        CleanModal();

        //Refresca la tabla
        this.ReloadTable();
        self.showAlertModal('La Terminal se ha eliminado con exito!');
    }

    this.BindFields = function (data) {
        CleanModal();

        $("#btnDelete").removeAttr("disabled").show();
        $("#btnUpdate").removeAttr("disabled").show();
        $('.modal-title').text('Editar Terminal');

        this.ctrlActions.BindFields("fsEditTerminal", data);
        this.ctrlActions.BindFields("fsEditTerminalLoc", data.Location);

        mapModule.updateMarker(parseFloat(data.Location.Latitude), parseFloat(data.Location.Longitude));
        mapModule.panToMarker();
        ToggleModal();
    }

    this.OpenModal = function () {
        CleanModal();
        $("#btnCreate").removeAttr("disabled").show();
        $('.modal-title').text('Registrar Terminal');

        mapModule.updateMarker(myLatLng.lat, myLatLng.lng);
        mapModule.panToMarker();
        ToggleModal();
    }

    var ToggleModal = function () {
        myModal.modal("toggle");
    }

    var CleanModal = function () {
        self.ctrlActions.cleanFormById(terminalSchema, "fsEditTerminal");
        self.ctrlActions.cleanFormById(locSchema, "fsEditTerminalLoc");

        $("#txtTerminalId").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();
        $("#txtTerminalName").removeClass("is-invalid").val("");

        $("#txtTerminalLocId").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();
        $("#txtTerminalLocName").removeClass("is-invalid").val("");
        $("#txtTerminalLocLat").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();
        $("#txtTerminalLocLong").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();

        $("#btnCreate").attr("disabled", "disabled").hide();
        $("#btnUpdate").attr("disabled", "disabled").hide();
        $("#btnDelete").attr("disabled", "disabled").hide();
    }

    function updateLatLngInputs(pLat, pLong) {
        $("#txtTerminalLocLat").val(pLat);
        $("#txtTerminalLocLong").val(pLong);
    }

    this.initMap = function () {
        mapModule.initMap({
            callback: updateLatLngInputs,
            coordinates: { lat: 9.9281, lng: -84.0907 }
        });

        myModal.scroll(mapModule.repositionAutocomplete);
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
            text: "Desea eliminar esta Terminal?",
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
    var vterminal = new vTerminal();
    vterminal.RetrieveAll();
    vterminal.initMap();
});
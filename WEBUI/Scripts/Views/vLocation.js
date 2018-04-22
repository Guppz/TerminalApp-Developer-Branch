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
        if (searchInput != null) {
            autocomplete = new google.maps.places.Autocomplete(searchInput[0]);

            autocomplete.addListener("place_changed", function () {
                var place = autocomplete.getPlace();
                if (!place.geometry) {
                    window.alert("No details available for input: '" + place.name + "'");
                    return;
                }

                if (place.geometry.viewport) {
                    self.updateMarker(place.geometry.location.lat(), place.geometry.location.lng());
                    $("#txtLocLat").val(place.geometry.location.lat());
                    $("#txtLocLong").val(place.geometry.location.lng());
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
                $("#txtLocLat").val(marker.getPosition().lat());
                $("#txtLocLong").val(marker.getPosition().lng());
                searchInput.val(results[0].formatted_address);
            });
        }
    }

    return self;
})({ mapDivID: "mapLugar", coordinates: { lat: 9.9281, lng: -84.0907 }, mapSearchInputID: "txtLocName" });

function vLocation() {

    var self = this;
    var myLatLng = {
        lat: 9.9281,
        lng: -84.0907
    };
    var modalID = "modalLugar";
    var myModal = $("#modalLugar");

    this.tblTerminalId = "tblLocation";
    this.service = "Location";
    this.serviceLoc = "Location";
    this.ctrlActions = new ControlActions();
    this.columns = "IdTerminal,Name,Location";


   
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
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, false, );
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, true);
    }

    this.Create = function () {
       
        var locData = this.ctrlActions.GetDataForm("fsEditTerminalLoc");

        let validLoc = this.ctrlActions.validateData(locData, locSchema);
    
            if (validLoc) {
                //Hace el POST
             
                this.ctrlActions.PostToAPI(this.service, locData);
                swal({
                    type: 'success',
                    background: "#95c9d4",
                    title: 'La locación se ha registrado con exito!',
                    showConfirmButton: false,
                    timer: 1500
                })
                myModal.modal("hide");
                CleanModal();
                this.ReloadTable();
            }
        
        //Refresca la tabla
        
    }

    this.Update = function () {
       
        var locData = this.ctrlActions.GetDataForm("fsEditTerminalLoc");

     
        let validLoc = this.ctrlActions.validateData(locData, locSchema);

     
            if (validLoc) {
          
                //Hace el PUT a Location
                this.ctrlActions.PutToAPI(this.serviceLoc, locData);
                swal({
                    type: 'success',
                    background: "#95c9d4",
                    title: 'La locación se ha actualizado con exito!',
                    showConfirmButton: false,
                    timer: 1500
                })
                //Hace el PUT a Terminal
               
                myModal.modal("hide");
                CleanModal();
                this.ReloadTable();
            }
        
        //Refresca la tabla
       
    }

    this.Delete = function () {
        var locData = {};
        var locData = this.ctrlActions.GetDataForm("fsEditTerminalLoc");
        locData.IdLocation = parseInt($("#txtTerminalLocId").val())

        //Hace el DELETE
        this.ctrlActions.DeleteToAPI(this.service, locData);
        swal({
            type: 'success',
            background: "#95c9d4",
            title: 'La locación se ha eliminado con exito!',
            showConfirmButton: false,
            timer: 1500
        })
        myModal.modal("hide");
        CleanModal();

        //Refresca la tabla
        this.ReloadTable();
    }

    this.BindFields = function (data) {
        CleanModal();

        $("#btnDelete").removeAttr("disabled").show();
        $("#btnUpdate").removeAttr("disabled").show();

    
        this.ctrlActions.BindFields("fsEditTerminalLoc", data);

        mapModule.updateMarker(parseFloat(data.Latitude), parseFloat(data.Longitude));
        mapModule.panToMarker();
        ToggleModal();
    }

    this.OpenModal = function () {
        CleanModal();
        $("#btnCreate").removeAttr("disabled").show();
        $("#txtLocLat").val(myLatLng.lat);
        $("#txtLocLong").val(myLatLng.lng);
        mapModule.updateMarker(myLatLng.lat, myLatLng.lng);
        mapModule.panToMarker();
        ToggleModal();
    }

    var ToggleModal = function () {
        myModal.modal("toggle");
    }

    var CleanModal = function () {
        $("#txtTerminalLocId").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();

       
        $("#txtLocLat").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();
        $("#txtLocLong").attr("disabled", "disabled").removeClass("is-invalid").val("").parent().hide();

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
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vlocation = new vLocation();
    vlocation.RetrieveAll();
    vlocation.initMap();
});
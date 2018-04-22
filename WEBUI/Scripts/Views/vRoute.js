var myLatLng = {
    lat: 9.9281,
    lng: -84.0907
};

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
                self.callback(event.latLng.lat(), event.latLng.lng())
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
                    self.callback(place.geometry.location.lat(), place.geometry.location.lng())
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
                self.callback(marker.getPosition().lat(), marker.getPosition().lng())
                searchInput.val(results[0].formatted_address);
            });
        }
    }

    self.getEstimatedDistanceAndTime = function (locations) {
        var directionsService = new google.maps.DirectionsService();
        var distanceAndTime;
        var waypoints = [];

        for (var i = 0; i < locations.length; i++) {
            var address = locations[i];
            if (address !== "") {
                waypoints.push({
                    location: address,
                    stopover: true
                });
            }
        };

        var request = {
            origin: locations[0],
            destination: waypoints[waypoints.length-1].location,
            waypoints: waypoints,
            optimizeWaypoints: false,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };

        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                var distance = 0;
                var minute = 0.00;
                response.routes[0].legs.forEach(function (item, index) {
                    if (index < response.routes[0].legs.length - 1) {
                        distance = distance + parseInt(item.distance.text);
                        minute = parseFloat(minute) + parseFloat(item.duration.value / 60);
                    }
                });
            }

            distanceAndTime = {
                'distance': distance,
                'minutes': minute
            }
        });//GUARDAR EL CAT DE PROCEDIMIENTOS

        return distanceAndTime;
    }

    return self;
})({ mapDivID: "mapLugar", coordinates: { lat: 9.9281, lng: -84.0907 }, mapSearchInputID: "txtLocation" });

function vRoute() {
    var self = this;

    var modalID = "myModal";
    var myModal = $("#myModal");

    this.tblTerminalId = "tblRoute";
    this.service = "Route";

    this.ctrlActions = new ControlActions();
    this.columns = "IdTerminal,Name,Location";

    var RouteSchema = {

        Name: function (value) {
            if (value === "") {
                return "Por favor ingrese el nombre de la Ubicacion";
            }
            return "";
        },
        RouteTerminal: function (value) {
            if (value === "") {
                return "Por favor seleccione la terminal"
            }
            return "";
        },
        RouteCompany: function (value) {
            if (value === "") {
                return "Por favor seleccione alguna compañia"
            }
            return "";
        }
    };

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, false, );
    };
    this.RetrieveAllByCompany = function (idCompany) {
        this.ctrlActions.FillTable(this.service + "/RetrieveRoutesByCompany?idCompany=" + idCompany, this.tblTerminalId, false, );
    };
    this.RetrieveAllByTerminal = function (idTerminal) {
        this.ctrlActions.FillTable(this.service + "/RetrieveRoutesByTerminal?idTerminal=" + idTerminal, this.tblTerminalId, false, );
    };

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, true);
    };

    this.Create = function () {

        var RouteData = {};
        RouteData = this.ctrlActions.GetDataForm("frmEdition");

        let valid = this.ctrlActions.validateData(RouteData, RouteSchema);
        RouteData.BusStops = this.GetBusStops();
        RouteData.RouteCompany = { IdCompany: RouteData.RouteCompany };
        RouteData.RouteTerminal = { IdTerminal: RouteData.RouteTerminal };

        if (valid && RouteData.BusStops !== null) {
            this.ctrlActions.PostToAPI(this.service, RouteData);
            myModal.modal("hide");
            $('#frmEdition')[0].reset();
            CleanModal();
            this.ReloadTable();
            self.showAlertModal('La ruta se ha registrado con exito!');


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
        var RouteData = {};
        RouteData = this.ctrlActions.GetDataForm("frmEdition");
        let valid = this.ctrlActions.validateData(RouteData, RouteSchema);
        RouteData.BusStops = this.GetBusStops();
        RouteData.RouteCompany = { IdCompany: RouteData.RouteCompany };
        RouteData.RouteTerminal = { IdTerminal: RouteData.RouteTerminal };
        RouteData.IdRoute = parseInt($("#btnUpdate").attr("name"));
        if (valid && RouteData.BusStops !== null) {
            this.ctrlActions.PutToAPI(this.service, RouteData);
            myModal.modal("hide");
            CleanModal();
            this.ReloadTable();
            self.showAlertModal('La ruta se ha actualizado con exito!');

        }
    };
    this.SatatusAlertActive = function () {
        var RouteData = {};
        RouteData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea activar esta ruta!",
            type: "warning",
            background: "#95c9d4",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Activar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    if (RouteData.Status == 1) {
                        $('#myModal').modal("hide");
                        swal({
                            type: 'error',
                            title: 'Oops...',
                            text: 'Esta ruta ya se encuentra activada!',
                        })

                    }
                    else {
                        self.Delete();
                        swal("Activado!", "Esta ruta ha sido activada con exito.", "success");
                    }
                }
            });

    }

    this.SatatusAlertDesactive = function () {
        var RouteData = {};
        RouteData = this.ctrlActions.GetDataForm('frmEdition');

        swal({
            title: "Se encuentra seguro?",
            text: "Desea desactivar esta ruta?",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-warning",
            confirmButtonClass: "btn-success",
            confirmButtonText: "Desactivar!",
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    if (RouteData.Status == 0) {
                        $('#myModal').modal("hide");
                        swal({
                            background: "#95c9d4",
                            type: 'error',
                            title: 'Oops...',
                            text: 'Esta ruta ya se encuentra desactiva!',
                        })

                    }
                    else {
                        self.Delete();
                        swal("Desactivado!", "Esta ruta ha sido desactivada con exito.", "success");
                    }
                }
            });

    }

    this.Delete = function () {
        var RouteData = {};
        RouteData = this.ctrlActions.GetDataForm("frmEdition");
        RouteData.IdRoute = parseInt($("#btnUpdate").attr("name"));
        RouteData.Status = parseInt($("#btnActivate").attr("name"));
  
        this.ctrlActions.DeleteToAPI(this.service, RouteData);
       

        myModal.modal("hide");
        CleanModal();
        this.ReloadTable();
    };

    this.GetBusStops = function () {
        var LstLocations = []
        BusStops = $(".DeleteBusStop");
        var LocationsNames = []

        if ($(BusStops).length === 0) {
            return null;
        } else {
            for (var i = 0; i < $(BusStops).length; i++) {
                var Location = {}
                var li = $($(BusStops)[i]).parent();
                Location.Latitude = $(li).attr("lat");
                Location.Longitude = $(li).attr("long");
                Location.Name = $(li)[0].textContent;
                Location.Name = Location.Name.replace('×', '');
                LstLocations.push(Location);
                LocationsNames.push(Location.Name);
            }

            //var distanceAndTime = mapModule.getEstimatedDistanceAndTime(LocationsNames);

            return LstLocations;
        }
    };

    this.BindFields = function (data) {
        this.ctrlActions.cleanFormSchema(RouteSchema);
        CleanModal();
        var BusStops = data.BusStops
        $("#btnUpdate").removeAttr("disabled").show();
        self.updateButtons(2);

        if (data.Status === 1) {
            $("#btnActivate").removeAttr("disabled").hide();
            $("#btnDesactive").removeAttr("disabled").show();
        } else {
            $("#btnActivate").removeAttr("disabled").show();
            $("#btnDesactive").removeAttr("disabled").hide();
        }

        $('.modal-title').text('Editar Ruta');

        $('#btnUpdate').attr('name', data.IdRoute);
        $('#btnActivate').attr('name', data.Status);
        $('#btnDesactive').attr('name', data.Status);
        this.ctrlActions.BindFields("frmEdition", data);
        $("#companyComboBox").val(data.RouteCompany.IdCompany);
        $("#terminalComboBox").val(data.RouteTerminal.IdTerminal);
      

        for (var i = 1; i < BusStops.length; i++) {
            $("#txtLocation").val(BusStops[i].Name);
            myLatLng.lat = BusStops[i].Latitude;
            myLatLng.lng = BusStops[i].Longitude;
            this.AddBusStop();
            mapModule.updateMarker(parseFloat(myLatLng.lat), parseFloat(myLatLng.lng));
            mapModule.panToMarker();
        }

        ToggleModal();
    };

    this.OpenModal = function () {
        this.ctrlActions.cleanFormSchema(RouteSchema);
        CleanModal();
        $('.modal-title').text('Registrar Ruta');

        $("#btnCreate").removeAttr("disabled").show();
        self.updateButtons(1);

        mapModule.updateMarker(myLatLng.lat, myLatLng.lng);
        mapModule.panToMarker();
        ToggleModal();
    };

    this.updateButtons = function (value) {
        if (value === 1) {

            $('#btnCreate').show();
            $('#btnUpdate').hide();
            $('#btnActivate').hide();
            $('#btnDesactive').hide();
        } else {

            $('#btnCreate').hide();
            $('#btnUpdate').show();
            $('#btnActivate').show();
            $('#btnDesactive').show();

        }
    };


    var ToggleModal = function () {
        myModal.modal("toggle");
    };

    var CleanModal = function () {
        $("#companyComboBox").removeClass("is-invalid");
        $("#companyComboBox").find("option").first().prop("selected", true);
        $("#terminalComboBox").removeClass("is-invalid");
        $("#terminalComboBox").find("option").first().prop("selected", true);

        $("#btnCreate").attr("disabled", "disabled").hide();
        $("#btnUpdate").attr("disabled", "disabled").hide();
        $("#btnActivate").attr("disabled", "disabled").hide();

        $("#btnDeactivate").attr("disabled", "disabled").hide();
        var busstop = $(".DeleteBusStop");
        for (var i = 0; i < busstop.length; i++) {
            li = $(busstop[i]).parent();
            $(li).remove();
        }
        $("#BusStop p").remove("");
        $("#BusStop ").append(' <p>No hay ninguna parada incluida todavia. Para agregarlas por favor escriba en el espacio de abajo o bien busque en el mapa, y luego presione el boton de agregar paradas que' +
            ' esta mas abajo</p>');
        
    };

    function updateLatLngInputs(pLat, pLong) {
        myLatLng.lat = pLat;
        myLatLng.lng = pLong
    };

    this.AddBusStop = function () {
        $("#BusStop p").remove("");
        if (this.IsStopAlreadyAdded()) {
            $(" div[columnDataName = " + '"' + "SearchLocation" + '"' + "]")[0].textContent = "Ya esa parada ha sido agregada";
        }
        else {
            $("#BusStop ul").append(
                ' <li class="busStop" lat="' + myLatLng.lat + '" long="' + myLatLng.lng + '">' +
                '<button  class="btn btn-link DeleteBusStop">×</button>' +
                $("#txtLocation").val() +
                '</li >'
            );
            $(" div[columnDataName = " + '"' + "SearchLocation" + '"' + "]")[0].textContent = "";
        }

        $(".DeleteBusStop").click(function () {
            li = $(this).parent();
            $(li).remove();
            if ($(".DeleteBusStop").length === 0) {
                $("#BusStop p").remove("");
                $("#BusStop ").append(' <p>No hay ninguna parada incluida todavia. Para agregarlas por favor escriba en el espacio de abajo o bien busque en el mapa, y luego presione el boton de agregar paradas que' +
                    ' esta mas abajo</p>');
            }

        });
    };

    this.IsStopAlreadyAdded = function () {
        BusStops = $(".DeleteBusStop");

        for (var i = 0; i < $(BusStops).length; i++) {
            var li = $($(BusStops)[i]).parent();
            var Latitude = parseFloat($(li).attr("lat"));
            var Longitude = parseFloat($(li).attr("long"));
            if (myLatLng.lat === Latitude && myLatLng.lng === Longitude) {
                return true;
            }
        }
        return false;;
    };

    this.initMap = function () {
        mapModule.initMap({
            callback: updateLatLngInputs,
            coordinates: { lat: 9.9281, lng: -84.0907 }
        });

        myModal.scroll(mapModule.repositionAutocomplete);
    };
}

$(document).ready(function () {
    let company = JSON.parse(sessionStorage.getItem("company"));
    let user = JSON.parse(sessionStorage.getItem("user"));
    var vroute = new vRoute();
    if (company !== null) {
        $("#companyComboBox").val(company.IdCompany.toString()).attr("disabled", "disabled");
        vroute.RetrieveAllByCompany(company.IdCompany);
    }
    else if (user !== null && user.UserTerminal.IdTerminal !== -1) {
        vroute.RetrieveAllByTerminal(user.UserTerminal.IdTerminal)
        $("#terminalComboBox").val(user.UserTerminal.IdTerminal.toString()).attr("disabled", "disabled");
    } else {
        vroute.RetrieveAll();
    }
    
    
    vroute.initMap();
});
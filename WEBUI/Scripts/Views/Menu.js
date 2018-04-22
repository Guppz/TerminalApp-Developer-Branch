

$(document).ready(function () {
    var user = JSON.parse(sessionStorage.getItem("user"));
    var canEnter = false;
    var page = window.location.pathname.split("/")
    page = page[2].split("?")[0]
    var pageview = page;

    if (user === null) {
        window.location.replace(window.location.origin);
    }

    let views = user.ViewList;

    for (var i = 0; i < views.length; i++) {
        if (views[i].ViewName === pageview) {
            canEnter = true;
        } else if (pageview === "HomeDashboard" || pageview === "vPerfil") {
            canEnter = true;
        }
    }

    if (!canEnter) {
        window.location.replace(window.location.origin);
    }

    var LoggedUser = JSON.parse(sessionStorage.getItem("user"));

    if (LoggedUser !== null) {

        let views = LoggedUser.ViewList;


        for (var i = 0; i < views.length; i++) {
            var viewGroup = views[i].ViewGroup;
            var viewName = views[i].ViewPage;
            if (viewGroup !== "") {
                if ($("#navigation #" + viewGroup).length === 0) {
                    $("#navigation").append('<li class="nav-item dropdown"> ' +
                        '<a class="nav-link dropdown-toggle" data-toggle="dropdown" href="#" id="themes">' + viewGroup + '<span class="caret"></span></a>' +
                        ' <div class="dropdown-menu" aria-labelledby="Customers" id="' + viewGroup + '">' +
                        ' </div>' +
                        ' </li>'
                    );
                }
                if (viewGroup === "Reportes") {
                    $("#" + viewGroup).append(
                        ' <a class="dropdown-item report" reportType="' + views[i].ViewName + '">' + viewName + '</a> '
                    );
                } else {
                    $("#" + viewGroup).append(
                        ' <a class="dropdown-item" href="/Home/' + views[i].ViewName + '">' + viewName + '</a> '
                    );
                }

            } else {
                $("#navigation").append(
                    ' <a class="nav-link" href="/Home/' + views[i].ViewName + '">' + viewName + '</a> '
                );
            }
        }

        $("#userInfo").append(
            ' <li class="nav-item"><a class="nav-link" href="/Home/vPerfil">' + LoggedUser.Name + " " + LoggedUser.LastName + '</a></li>' +
            '<li class="nav-item" id="logout"><a class="nav-link"  >Salir</a></li>'
        );

    }

        if (user !== null && user.UserTerminal.IdTerminal !== -1) {
            $("#ReportTerminal").val(user.UserTerminal.IdTerminal.toString());
        } else {
            $("#ReportTerminal").val("1");
        }
        $("#logout").click(function () {
            sessionStorage.clear();
            window.location.replace("http://localhost:50614/Home/vLogin");
        });

        $(".report").click(function () {
            var view = $(this).attr("reportType");
            if (view === "TotalRevenue") {
                window.location = "http://localhost:61693/api/Report/TotalRevenue";
            } if (view === "TotalRevenueByTerminal") {
                let user = JSON.parse(sessionStorage.getItem("user"));
                if (user !== null && user.UserTerminal.IdTerminal !== -1) {
                    window.location = "http://localhost:61693/api/Report/TotalRevenueByTerminal?id=" + user.UserTerminal.IdTerminal
                } else {
                    $("#SelectTerminal").modal("toggle");

                }

            }

        });

        $("#DownloadTerminal").click(function () {
            if ($("#ReportTerminal").val() === "") {

            } else {
                $("div[columndataname = " + '"' + "ReportTerminal" + '"' + "]")[0].textContent = "";
                var id = parseInt($("#ReportTerminal").val());
                window.location = "http://localhost:61693/api/Report/TotalRevenueByTerminal?id=" + id
            }

        });

}); 



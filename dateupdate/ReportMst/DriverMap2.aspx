<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriverMap2.aspx.cs" Inherits="Web.ReportMst.DriverMap2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
    <style type="text/css">
        .abc {
            font-size: 14px;
            font-weight: normal;
            color: #333;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            box-shadow: none;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            -ms-border-radius: 4px;
            -o-border-radius: 4px;
            border-radius: 4px;
        }
    </style>
    <script type='text/javascript'>
        //Load map on window start
        google.maps.event.addDomListener(window, 'load', DrawGoogleMap);
    </script>
    <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>--%>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDd-0GRWdGVwGD-nc5Jojdnr6aQO46RV_4&sensor=false&libraries=places"></script>
    <%--  --%>
    <script type="text/javascript">
        var source, destination;
        var directionsDisplay;
        var directionsService = new google.maps.DirectionsService();
        google.maps.event.addDomListener(window, 'load', function () {
            new google.maps.places.SearchBox(document.getElementById('txtSource'));
            new google.maps.places.SearchBox(document.getElementById('txtDestination'));
            directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
        });

        function GetRoute() {
            var OP = document.getElementById('txtSource');
            var mumbai = new google.maps.LatLng(OP);
            var mapOptions = {
                zoom: 7,
                center: mumbai
            };
            map = new google.maps.Map(document.getElementById('dvMap'), mapOptions);
            directionsDisplay.setMap(map);
            //directionsDisplay.setPanel(document.getElementById('dvPanel'));

            //*********DIRECTIONS AND ROUTE**********************//
            source = document.getElementById("txtSource").value;
            destination = document.getElementById("txtDestination").value;

            var request = {
                origin: source,
                destination: destination,
                travelMode: google.maps.TravelMode.DRIVING
            };
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                }
            });

            //*********DISTANCE AND DURATION**********************//
            var service = new google.maps.DistanceMatrixService();
            service.getDistanceMatrix({
                origins: [source],
                destinations: [destination],
                travelMode: google.maps.TravelMode.DRIVING,
                unitSystem: google.maps.UnitSystem.METRIC,
                avoidHighways: false,
                avoidTolls: false
            }, function (response, status) {
                if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
                    var distance = response.rows[0].elements[0].distance.text;
                    var duration = response.rows[0].elements[0].duration.text;
                    var dvDistance = document.getElementById("dvDistance");
                    dvDistance.innerHTML = "";
                    dvDistance.innerHTML += "Distance: " + distance + "<br />";
                    dvDistance.innerHTML += "Duration:" + duration;

                    var hidfield = document.getElementById('<%= GetDistHide.ClientID %>');
                    hidfield.value = distance;
                    var str = hidfield.value;
                    var res = str.replace("km", "");
                    if (res <= 3.0) {
                        document.getElementById("CheckIn").style.display = "block";
                    }
                    else {
                        document.getElementById("CheckIn").style.display = "none";
                    }
                } else {
                    alert("Unable to find the distance via road.");
                }
            });
        }
    </script>
    <%--  --%>
    <script type="text/javascript">
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (p) {
                var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
                var mapOptions = {
                    center: LatLng,
                    zoom: 13,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                var OP = p.coords.latitude + "," + p.coords.longitude;
                txtSource.value = OP;
                var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                var marker = new google.maps.Marker({
                    position: LatLng,
                    map: map,
                    title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: " + p.coords.latitude + "<br />Longitude: " + p.coords.longitude
                });
                google.maps.event.addListener(marker, "click", function (e) {
                    var infoWindow = new google.maps.InfoWindow();
                    infoWindow.setContent(marker.title);
                    infoWindow.open(map, marker);
                });
            });
        } else {
            alert('Geo Location feature is not supported in this browser.');
        }
    </script>
    <script type="text/javascript">
        function Current() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (p) {
                    var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
                    var mapOptions = {
                        center: LatLng,
                        zoom: 13,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    var OP = p.coords.latitude + "," + p.coords.longitude;
                    txtSource.value = OP;
                    var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                    var marker = new google.maps.Marker({
                        position: LatLng,
                        map: map,
                        title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: " + p.coords.latitude + "<br />Longitude: " + p.coords.longitude
                    });
                    google.maps.event.addListener(marker, "click", function (e) {
                        var infoWindow = new google.maps.InfoWindow();
                        infoWindow.setContent(marker.title);
                        infoWindow.open(map, marker);
                    });
                });
            } else {
                alert('Geo Location feature is not supported in this browser.');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <table style="border: solid 12px gray; width: 100%; vertical-align: central;">
            <tr>
                <td style="padding-left: 20px; padding-top: 20px; padding-bottom: 20px; background-color: skyblue; text-align: center; font-family: Verdana; font-size: 20pt; color: Green;">Draw Route Between Employee's Current Location & Destination
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: skyblue; text-align: center; font-family: Verdana; font-size: 14pt; color: red;">Source:
                <input type="text" id="txtSource" value="" style="width: 200px" disabled="disabled" runat="server" />
                    <%--Bandra, Mumbai, India--%>
                    &nbsp; Destination:
                <input type="text" id="txtDestination" value="Andheri, Mumbai, India" style="width: 200px;display:none;" runat="server" />
                    <asp:DropDownList ID="EmpDest" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EmpDest_SelectedIndexChanged" CssClass="abc"></asp:DropDownList>

                    <input type="button" value="Get Distance" onclick="GetRoute()" />
                    <input type="button" id="current" value="Get Current" onclick="Current()" />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="CheckIn" runat="server" Text="Check In" Style="display: none;" OnClick="CheckIn_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:HiddenField ID="GetDistHide" runat="server" />
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="dvDistance" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvMap" style="width: 1200px; height: 600px">
                    </div>
                </td>
                <%--<td>
                    <div id="dvPanel" style="width: 500px; height: 500px">
                    </div>
                </td>--%>
            </tr>
        </table>
        <br />
    </form>
</body>
</html>

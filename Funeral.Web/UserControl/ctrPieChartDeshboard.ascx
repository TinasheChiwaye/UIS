<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrPieChartDeshboard.ascx.cs" Inherits="Funeral.Web.UserControl.ctrPieChartDeshboard" %>

<script src="../Scripts/plugins/ChartJScript/Chart.bundle.js"></script>
<script src="../Scripts/plugins/ChartJScript/jquery.min.js"></script>
    
<canvas id="barcanvas" width="400" height="400"></canvas>
<script>
    window.onload = function () { };

    var data = {
        labels: [
            "Red",
            "Blue",
            "Yellow"
        ],
        datasets: [
            {
                data: [300, 50, 100],
                backgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ],
                hoverBackgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ]
            }]
    };

    function Piechart() {
        var ctx1 = document.getElementById("barcanvas").getContext("2d");
        var myPieChart = new Chart(ctx1, {
            type: 'pie',
            data: data,
            options: options
        });
    };
</script>


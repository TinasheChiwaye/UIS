<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrDashboardChart.ascx.cs" Inherits="Funeral.Web.UserControl.ctrDashboardChart" %>

<script src="../Scripts/plugins/ChartJScript/Chart.bundle.js"></script>
<script src="../Scripts/plugins/ChartJScript/jquery.min.js"></script>
<style>
    canvas {
        -moz-user-select: none;
        -webkit-user-select: none;
        -ms-user-select: none;
    }
</style>


<div class="row">
    <div class="col-lg-6 ">
        <div class="table-responsive">
            <div id="container" style="width: 100%;">
                <canvas id="canvas"></canvas>
            </div>
        </div>
    </div>
    <div class="col-lg-6 ">
        <div class="table-responsive">
            <div id="container1" style="width: 100%;">
                <canvas id="piechart"></canvas>
            </div>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-lg-12 ">
        <div class="table-responsive">
            <div id="container3" style="width: 100%;">
                <canvas id="PremiumCollectionpiechart" height="75"></canvas>
            </div>
        </div>
    </div>
</div>
<script>
    //created membets dataset
    var MONTHS = [<% =this.ChartLabels %>];

    var randomScalingFactor = function () {
        return (Math.random() > 0.5 ? 1.0 : -1.0);
    };
    var randomColorFactor = function () {
        return Math.round(Math.random() * 255);
    };
    var randomColor = function () {
        return 'rgba(' + randomColorFactor() + ')';
    };

    var barChartData = {
        labels: [<% =this.ChartLabels %>],
        datasets: [{
            label: 'New policies count for the last 6 months : <% =this.ChartTotalCount %>',
                backgroundColor: "rgba(220,220,220,0.5)",
                data: [<% =this.ChartData1 %>]
            }]

    };
        //end created membets dataset

        //poicyPremium dataset
        var policyPremiumData = {
            labels: [<% =this.PolicyPremiumPieChartLabels %>],
            datasets: [{
                label: 'Premium collected for the last 6 months : <%=this.Currency%> <% =Convert.ToString(this.PolicyPremiumPieChartTotalCount) + ".00" %>',                
                backgroundColor: "rgba(220,220,220,0.5)",
                data: [<% =this.PolicyPremiumPieChartData %>],
            }]
        };
        //end poicyPremium dataset

        // policy pichart dataset       
        var Piedata = {
            labels: [
               <% =this.PolicyPieChartLabels %>
            ],
            datasets: [
                {
                    data: [ <% =this.PolicyPieChartData1 %>],
                    backgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56",
                       "#ff0000",
                       "#00ff00",
                       "#f2f2f2",
                       "#bf8040",
                       "#6600ff"
                    ],
                    hoverBackgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56",
                        "#ff0000",
                        "#00ff00",
                        "#f2f2f2",
                        "#bf8040",
                        "#6600ff"
                    ]
                }]
        };
        //end policy pichart dataset   

        window.onload = function () {
            var ctx = document.getElementById("canvas").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    // Elements options apply to all of the options unless overridden in a dataset
                    // In this case, we are setting the border of each bar to be 2px wide and green
                    elements: {
                        rectangle: {
                            borderWidth: 2,
                            borderColor: 'rgb(255, 0, 0)',
                            borderSkipped: 'bottom'
                        }
                    },
                    responsive: true,
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'New policies for the last 6 months'
                    }
                }
            });
            var ctx1 = document.getElementById("piechart").getContext("2d");
            var myPieChart = new Chart(ctx1, {
                type: 'pie',
                data: Piedata,
                options: {
                    responsive: true,
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Total Policies : <% =this.PolicyPieChartTotalCount %>'
                    }
                }
            });
            var ctx2 = document.getElementById("PremiumCollectionpiechart").getContext("2d");
            window.myBar = new Chart(ctx2, {
                type: 'bar',
                data: policyPremiumData,
                options: {
                    // Elements options apply to all of the options unless overridden in a dataset
                    // In this case, we are setting the border of each bar to be 2px wide and green
                    elements: {
                        rectangle: {
                            borderWidth: 2,
                            borderColor: 'rgb(255, 0, 0)',
                            borderSkipped: 'bottom'
                        }
                    },
                    responsive: true,
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Premium collected for the last 6 months'
                    },
                    //tooltips: {
                    //    callbacks: {
                    //        label: { display: true, text: "hello" }
                    //    }
                    //}
                }
            });
        };

</script>







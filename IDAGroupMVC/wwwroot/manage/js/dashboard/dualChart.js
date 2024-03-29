﻿(function ($) {
    "use strict"


    /* function draw() {
    	
    } */

    var dlabSparkLine = function () {
        let draw = Chart.controllers.line.__super__.draw; //draw shadow

        var screenWidth = $(window).width();



        var lineChart3 = function () {
            //dual line chart
            if (jQuery('#lineChart_3').length > 0) {
                const lineChart_3 = document.getElementById("lineChart_3").getContext('2d');
                //generate gradient
                const lineChart_3gradientStroke1 = lineChart_3.createLinearGradient(500, 0, 100, 0);
                lineChart_3gradientStroke1.addColorStop(0, "rgba(136,108,192, 1)");
                lineChart_3gradientStroke1.addColorStop(1, "rgba(136,108,192, 0.5)");

                const lineChart_3gradientStroke2 = lineChart_3.createLinearGradient(500, 0, 100, 0);
                lineChart_3gradientStroke2.addColorStop(0, "rgba(255, 92, 0, 1)");
                lineChart_3gradientStroke2.addColorStop(1, "rgba(255, 92, 0, 1)");

                Chart.controllers.line = Chart.controllers.line.extend({
                    draw: function () {
                        draw.apply(this, arguments);
                        let nk = this.chart.chart.ctx;
                        let _stroke = nk.stroke;
                        nk.stroke = function () {
                            nk.save();
                            nk.shadowColor = 'rgba(0, 0, 0, 0)';
                            nk.shadowBlur = 10;
                            nk.shadowOffsetX = 0;
                            nk.shadowOffsetY = 10;
                            _stroke.apply(this, arguments)
                            nk.restore();
                        }
                    }
                });

                lineChart_3.height = 100;

                new Chart(lineChart_3, {
                    type: 'line',
                    data: {
                        defaultFontFamily: 'Poppins',
                        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                        datasets: [
                            {
                                label: "My First dataset",
                                data: [25, 20, 60, 41, 66, 45, 80, 50, 30, 50, 12, 24],
                                borderColor: lineChart_3gradientStroke1,
                                borderWidth: "2",
                                backgroundColor: 'transparent',
                                pointBackgroundColor: 'rgba(136,108,192, 0.5)'
                            }, {
                                label: "My First dataset",
                                data: [5, 20, 15, 41, 35, 65, 80],
                                borderColor: lineChart_3gradientStroke2,
                                borderWidth: "2",
                                backgroundColor: 'transparent',
                                pointBackgroundColor: 'rgba(254, 176, 25, 1)'
                            }
                        ]
                    },
                    options: {
                        legend: false,
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true,
                                    max: 100,
                                    min: 0,
                                    stepSize: 10,
                                    padding: 5
                                }
                            }],
                            xAxes: [{
                                ticks: {
                                    padding: 5
                                }
                            }]
                        }
                    }
                });
            }
        }






        /* Function ============ */
        return {
            init: function () {
            },


            load: function () {
                lineChart3();
                lineChart03();
            },

            resize: function () {
                // barChart1();	
                // barChart2();
                // barChart3();	
                // lineChart1();	
                // lineChart2();		
                // lineChart3();
                // lineChart03();
                // areaChart1();
                // areaChart2();
                // areaChart3();
                // radarChart();
                // pieChart();
                // doughnutChart(); 
                // polarChart(); 
            }
        }

    }();

    jQuery(document).ready(function () {
    });

    jQuery(window).on('load', function () {
        dlabSparkLine.load();
    });

    jQuery(window).on('resize', function () {
        //dlabSparkLine.resize();
        setTimeout(function () { dlabSparkLine.resize(); }, 1000);
    });

})(jQuery);
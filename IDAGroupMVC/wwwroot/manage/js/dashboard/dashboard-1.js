

(function ($) {
    /* "use strict" */

    var dlabChartlist = function () {

        var screenWidth = $(window).width();





        /* Function ============ */
        return {
            init: function () {
            },


            load: function () {
                donutChart1();
                chartBar();
                chartBar1();
                chartBar2();
                revenueMap();
                columnChart();
                NewCustomers();
                NewCustomers1();
                redial();
                emailchart();

            },

            resize: function () {
            }
        }

    }();



    jQuery(window).on('load', function () {
        setTimeout(function () {
            dlabChartlist.load();
        }, 1000);

    });



    

})(jQuery);



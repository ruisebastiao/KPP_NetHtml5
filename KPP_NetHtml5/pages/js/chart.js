$(function () {

    $(document).ready(function () {

        Highcharts.setOptions({
            lang: {
                decimalPoint: ',',
                thousandsSep: '.'
            }
        });

        var chart = new Highcharts.Chart({            
            chart: {
                animation: false,
                renderTo: 'container',
                defaultSeriesType: 'line',
                events: {
                    load: function () {                        
                        WebSocketTest();
                    }
                }
            },
            plotOptions: {
                series: {
                    animation: false
                },
                line: {
                    lineWidth: 2,
                    states: {
                        hover: {
                            lineWidth: 3
                        }
                    },
                    marker: {
                        enabled: false
                    }                    
                }

            },
            yAxis:{
             //   min: 0,
              //  max:500,
                startOnTick: false,

               // tickInterval: 0.1,
                labels: {
                    format: '{value:.2f}'
                }
            },
            tooltip: {
                pointFormat: "Value: {point.y:.2f}"
            },
            title: {
                text: 'Not connected to server...'
            },

           
            series: [{
                name: 'Torque Fx Data',
                data:[]            
            }]
        });

        function WebSocketTest() {
            if ("WebSocket" in window) {
               // alert("WebSocket is supported by your Browser!");

                // Let us open a web socket
                var ws = new WebSocket("ws://localhost:4649/DataProvider");
                var series;
                var startdata = false;
                var xpos = 0;
                ws.onopen = function () {
                    series = chart.series[0];
                    chart.setTitle({ text: "Connected - Waiting Data" });                 

                };
                var tick = 0;
                
                ws.onmessage = function (evt) {

                    if (startdata==false) {
                        startdata = true;
                        chart.setTitle({ text: "Connected - Receiving Data" });
                    }

                    
                    
                   // var received_msg = evt.data;
                    //var intval = parseInt(received_msg, 0);
                    var y = parseFloat(evt.data);
                   // var y = Math.cos(intval);

                    //chart.setTitle({ text: "Data:" + y });

                    var shift = series.data.length > 90; // shift if the series is 
                    // longer than 20

                    // add the point
                    series.addPoint(y, true, shift, false);
                    //chart.redraw();
                    //series.addPoint(, true, true);
                    
                    // alert("Message is received...");
                };

                ws.onclose = function () {
                    // websocket is closed.
                    //  alert("Connection is closed...");
                    chart.setTitle({ text: "Disconnected" });
                };
            }

            else {
                // The browser doesn't support WebSocket
              // alert("WebSocket NOT supported by your Browser!");
            }
        }
    });
});



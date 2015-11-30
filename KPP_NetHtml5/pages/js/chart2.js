$(function () {
    
    var xpoint=0
    $(document).ready(function () {
        Highcharts.setOptions({
            global: {
                useUTC: false
            }
        });

        $('#container').highcharts({
            chart: {
                
                zoomType: 'x',
                type: 'spline',
                animation: false,//Highcharts.svg, // don't animate in old IE
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second
                        var series= this.series[0];
                        setInterval(function () {
                            xpoint++;
                            var y = new Date().getSeconds();
                            
                            series.addPoint([xpoint, y], true, true);
                        }, 50);
                    }
                }
            },
            title: {
                text: 'Force Sensor Live Data'
            },
            xAxis: {
                type: 'linear',
             
            },
            yAxis: {
                title: {
                    text: 'Value'
                }
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                        Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                        Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: 'Random data',
                data: (function () {
                    // generate an array of random data
                    //var data = [],
                    //    time = (new Date()).getTime(),
                    //    i;

                    //for (i = -19; i <= 0; i += 1) {
                    //    data.push({
                    //        x: time + i * 1000,
                    //        y: Math.random()
                    //    });
                    //}
                    var data = [0,]
                    return data;
                }())
            }]
        });
    });


    function ConnectWebSocket() {
        if ("WebSocket" in window) {
            alert("WebSocket is supported by your Browser!");

            // Let us open a web socket
            var ws = new WebSocket("ws://localhost:4649/DataProvider");

            ws.onopen = function () {

                // set up the updating of the chart each second
                // var series = this.series[0];
                /*    setInterval(function () {
                        var x = (new Date()).getTime(), // current time
                            y = Math.round(Math.random() * 100);
                        series.addPoint([x, y], true, true);
                    }, 1000);*/

                // Web Socket is connected, send data using send()
                //ws.send("Message to send");
                // alert("Message is sent...");
            };

            ws.onmessage = function (evt) {
                var received_msg = evt.data;
                var x = (new Date()).getTime(), // current time
                          y = parseInt(received_msg, 0);

                series.addPoint([x, y], true, true);

                // alert("Message is received...");
            };

            ws.onclose = function () {
                // websocket is closed.
                alert("Connection is closed...");
            };
        }

        else {
            // The browser doesn't support WebSocket
            alert("WebSocket NOT supported by your Browser!");
        }
    }

});
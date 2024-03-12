function drawChart(dispositivoId) {

    $.ajax(
        {
            url: '/Home/DeviceDay',
            dataType: "json",
            data: { id: dispositivoId },
            type: "GET",
            success: function (jsonData) {
                var data = new google.visualization.DataTable(jsonData);
                var options = { chart: { title: 'Leituras nas últimas 24 horas' } };
                var chart = new google.charts.Line(document.getElementById('chart_div'));
                chart.draw(data, options);
            }
        });

    return false;
}



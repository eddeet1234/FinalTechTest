window.MixedChartWrapper = {
    charts: {},

    createChart: function (chartId, chartOptions, units) {
        // Add the formatter function in JavaScript
        // Format tooltips with unit
        if (!chartOptions.tooltip) {
            chartOptions.tooltip = {};
        }
        if (!chartOptions.tooltip.y) {
            chartOptions.tooltip.y = {
                formatter: function (value, { seriesIndex }) {
                    return `${value} ${units[seriesIndex]}`;  // Use the unit for the specific series
                }
            };
        }

        var chart = new ApexCharts(document.querySelector(`#${chartId}`), chartOptions);
        chart.render();
        this.charts[chartId] = chart;
    },

    destroyChart: function (chartId) {
        var chart = this.charts[chartId];
        if (chart) {
            chart.destroy();
            delete this.charts[chartId];
        }
    }
};
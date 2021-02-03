<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from './mixins/resize'

export default {
  mixins: [resize],
  props: {
    className: {
      type: String,
      default: 'chart'
    },
    width: {
      type: String,
      default: '100%'
    },
    height: {
      type: String,
      default: '300px'
    },
    chartData: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      chart: null
    }
  },
  watch: {
    chartData: {
      deep: true,
      handler(val) {
        this.initChart(val)
      }
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.initChart(this.chartData)
      this.chart.on(
        'click',
        (param) => {
          // this.$router.push({ path: '/eRFQ/lists' })
          this.$router.push({ path: '/Functions/eRFQ/lists', query: { WcName: param.name }})
        },
      )
    })
  },
  beforeDestroy() {
    if (!this.chart) {
      return
    }
    this.chart.dispose()
    this.chart = null
  },
  methods: {
    initChart(val) {
      this.chart = echarts.init(this.$el, 'macarons')

      var chartData = { names: ['CPS', 'HPSLJ', 'LOREAL', 'TESLA BB'], values: [1, 2, 3, 4] }

      chartData = (typeof (val) === 'undefined') ? chartData : val

      var option = {
        title: {
          text: 'RFQ-数据统计',
          subtext: '',
          textStyle: {
            color: 'black',
            fontSize: 16,
            fontWeight: 0
          }
        },
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'shadow'
          }
        },
        legend: {
          data: ['RFQ Numbers']
        },
        grid: {
          left: '3%',
          right: '4%',
          bottom: '3%',
          containLabel: true
        },
        xAxis: {
          type: 'value',
          boundaryGap: [0, 0.01]
        },
        yAxis: {
          type: 'category',
          data: chartData.names
        },
        series: [
          {
            name: 'RFQ Numbers',
            type: 'bar',
            data: chartData.values
          }
          // ,
          // {
          //   name: '2012年',
          //   type: 'bar',
          //   data: [19325, 23438, 31000, 121594, 134141, 681807]
          // }
        ]
      }

      this.chart.setOption(option)
    }
  }
}
</script>

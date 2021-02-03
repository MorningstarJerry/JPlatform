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
          // alert(param.name)
          this.$router.push({ path: '/Functions/eDfa/Dfa', query: { WcName: param.name }})
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

      var data = [{
        name: 'nodeA', // First tree
        value: 10
      },
      {
        name: 'nodeAb', // Second leaf of first tree
        value: 6
      },
      {
        name: 'nodeAc', // Second leaf of first tree
        value: 6
      }]

      data = (typeof (val) === 'undefined') ? data : val

      var option = {
        title: {
          text: 'DFA-数量统计',
          left: '80',
          top: 0,
          textAlign: 'center',
          textStyle: {
            color: 'black',
            fontSize: 16,
            fontWeight: 0
          }
        },
        tooltip: {
          trigger: 'item',
          formatter: '{b} : {c}'
        },
        series: [{
          type: 'treemap',
          data: data
        }]
      }

      this.chart.setOption(option)
    }
  }
}
</script>

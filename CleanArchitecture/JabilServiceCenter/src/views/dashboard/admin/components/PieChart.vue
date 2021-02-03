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
          this.$router.push({ path: '/Functions/Downtime/downtime-search', query: { WcName: param.name }})
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
      const PieData = [
        { value: 320, name: 'HPSLJ' },
        { value: 240, name: 'Zebra' },
        { value: 149, name: 'Mustang' },
        { value: 100, name: 'Tesla' },
        { value: 59, name: 'IROBOT' },
        { value: 259, name: 'LOREAL' }
      ]

      val = (typeof (val) === 'undefined') ? PieData : val

      var opt1 = {
        backgroundColor: '#FFFFFF',
        color: ['#EAEA26', '#906BF9', '#FE5656', '#01E17E', '#3DD1F9', '#FFAD05', '#EAEA26', '#906BF9', '#FE5656', '#01E17E', '#3DD1F9', '#FFAD05', '#EAEA26', '#906BF9', '#FE5656', '#01E17E', '#3DD1F9', '#FFAD05'],
        title: {
          text: 'Downtime-数量统计',
          left: '80',
          top: 0,
          textAlign: 'center',
          textStyle: {
            color: 'black',
            fontSize: 16,
            fontWeight: 0
          }
        },
        grid: {
          left: -100,
          top: 50,
          containLabel: true
        },
        tooltip: {
          trigger: 'item',
          formatter: '{b} : {c} ({d}%)'
        },
        legend: {
          type: 'scroll',
          orient: 'vartical',
          // x: "right",
          top: 'center',
          right: '15',
          // bottom: "0%",
          itemWidth: 16,
          itemHeight: 8,
          itemGap: 16,
          textStyle: {
            color: '#76567d',
            fontSize: 12,
            fontWeight: 0
          },

          data: ['HP',
            'IROBOT',
            'CPS',
            'HPLSJ',
            'MAKERBOT',
            'ZEBRA BB',
            'ELECTROLX',
            'EMERSON',
            'FPA',
            'GE',
            'INCUBATOR',
            'IROBOT BB',
            'IROBOT PCBA',
            'LOREAL',
            'SOLAREDGE',
            'TRAEGER',
            'WHIRLPOOL',
            'XEROX',
            'DANAHER & UTC',
            'FORD',
            'JE',
            'OSRAM',
            'ROADTRACK',
            'SHARP',
            'TESLA BB',
            'TESLA PCBA',
            'Valeo',
            'JTG',
            'UAT',
            'Littlebits',
            'TTEC',
            'Function',
            'SUMUP'
          ]
        },
        polar: {},
        angleAxis: {
          interval: 1,
          type: 'category',
          data: [],
          z: 10,
          axisLine: {
            show: false,
            lineStyle: {
              color: '#0B4A6B',
              width: 1,
              type: 'solid'
            }
          },
          axisLabel: {
            interval: 0,
            show: true,
            color: '#0B4A6B',
            margin: 8,
            fontSize: 16
          }
        },
        radiusAxis: {
          min: 40,
          max: 120,
          interval: 20,
          axisLine: {
            show: false,
            lineStyle: {
              color: '#0B3E5E',
              width: 1,
              type: 'solid'
            }
          },
          axisLabel: {
            formatter: '{value} %',
            show: false,
            padding: [0, 0, 20, 0],
            color: '#0B3E5E',
            fontSize: 16
          },
          splitLine: {
            lineStyle: {
              color: '#0B3E5E',
              width: 2,
              type: 'solid'
            }
          }
        },
        calculable: true,
        series: [{
          type: 'pie',
          radius: ['5%', '10%'],
          hoverAnimation: false,
          labelLine: {
            normal: {
              show: false,
              length: 30,
              length2: 55
            },
            emphasis: {
              show: false
            }
          },
          data: [{
            name: '',
            value: 10,
            itemStyle: {
              normal: {
                color: '#FFFFFF',
                fontSize: '16'
              }
            }
          }]
        }, {
          type: 'pie',
          radius: ['90%', '95%'],
          hoverAnimation: false,
          labelLine: {
            normal: {
              show: false,
              length: 30,
              length2: 55
            },
            emphasis: {
              show: false
            }
          },
          name: '',
          data: [{
            name: '',
            value: 0,
            itemStyle: {
              normal: {
                color: '#FFFFFF',
                fontSize: '16'
              }
            }
          }]
        }, {
          stack: 'a',
          type: 'pie',
          radius: ['20%', '80%'],
          roseType: 'area',
          zlevel: 10,
          label: {
            normal: {
              show: true,
              formatter: '{c}',
              textStyle: {
                fontSize: 12
              },
              position: 'outside'
            },
            emphasis: {
              show: true
            }
          },
          labelLine: {
            normal: {
              show: true,
              length: 20,
              length2: 55
            },
            emphasis: {
              show: false
            }
          },
          data: val
        }]
      }

      this.chart.setOption(opt1)
    }
  }
}
</script>

<template>
  <el-row :gutter="10" class="panel-group">
    <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('SMT')">
        <div class="card-panel-icon-wrapper icon-people">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            SMT
          </div>
          <count-to :start-val="0" :end-val="SMTcount" :duration="2600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('MI')">
        <div class="card-panel-icon-wrapper icon-message">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            MI
          </div>
          <count-to :start-val="0" :end-val="MIcount" :duration="3000" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('ICT')">
        <div class="card-panel-icon-wrapper icon-money">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            ICT
          </div>
          <count-to :start-val="0" :end-val="ICTcount" :duration="3200" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('FVT')">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            FVT
          </div>
          <count-to :start-val="0" :end-val="FVTcount" :duration="3600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
        <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('EOL')">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            EOL
          </div>
          <count-to :start-val="0" :end-val="EOLcount" :duration="3600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
     <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('Coating')">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            Coating
          </div>
          <count-to :start-val="0" :end-val="Coatingcount" :duration="3600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
     <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('Washing')">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            Washing
          </div>
          <count-to :start-val="0" :end-val="Washingcount" :duration="3600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('Cashing')">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            Cashing
          </div>
          <count-to :start-val="0" :end-val="Cashingcount" :duration="3600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="3" :sm="3" :lg="4" class="card-panel-col">
      <div class="card-panel" @click="handleSetPieChartData('Assembly')">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            Assembly
          </div>
          <count-to :start-val="0" :end-val="Assemblycount" :duration="3600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
  </el-row>
</template>

<script>

export default {
  props: {
    SMTcount: {
      type: Number,
      default: '600'
    },
    MIcount: {
      type: Number,
      default: '600'
    },
    ICTcount: {
      type: Number,
      default: '600'
    },
    FVTcount: {
      type: Number,
      default: '600'
    },
    EOLcount: {
      type: Number,
      default: '600'
    },
    Coatingcount: {
      type: Number,
      default: '600'
    },
    Washingcount: {
      type: Number,
      default: '600'
    },
    Cashingcount: {
      type: Number,
      default: '600'
    },
    Assemblycount: {
      type: Number,
      default: '600'
    },
    chartData: {
      type: Object,
      required: true
    }
  },
  methods: {
    handleSetPieChartData(type) {
      this.$emit('handleSetPieChartData', type)
    }
  }
}
</script>

<style lang="scss" scoped>
.panel-group {
  margin-top: 18px;

  .card-panel-col {
    margin-bottom: 32px;
  }

  .card-panel {
    height: 78px;
    cursor: pointer;
    font-size: 12px;
    position: relative;
    overflow: hidden;
    color: #666;
    background: #fff;
    box-shadow: 4px 4px 40px rgba(0, 0, 0, .05);
    border-color: rgba(0, 0, 0, .05);

    &:hover {
      .card-panel-icon-wrapper {
        color: #fff;
      }

      .icon-people {
        background: #40c9c6;
      }

      .icon-message {
        background: #36a3f7;
      }

      .icon-money {
        background: #f4516c;
      }

      .icon-shopping {
        background: #34bfa3
      }
    }

    .icon-people {
      color: #40c9c6;
    }

    .icon-message {
      color: #36a3f7;
    }

    .icon-money {
      color: #f4516c;
    }

    .icon-shopping {
      color: #34bfa3
    }

    .card-panel-icon-wrapper {
      float: left;
      margin: 1px 0 0 1px;
      padding: 3px;
      transition: all 0.38s ease-out;
      border-radius: 6px;
    }

    .card-panel-icon {
      float: left;
      font-size: 48px;
    }

    .card-panel-description {
      float: right;
      font-weight: bold;
      margin: 6px;
      margin-left: 0px;

      .card-panel-text {
        line-height: 18px;
        color: rgba(0, 0, 0, 0.45);
        font-size: 16px;
        margin-bottom: 12px;
      }

      .card-panel-num {
        font-size: 20px;
      }
    }
  }
}

@media (max-width:550px) {
  .card-panel-description {
    display: none;
  }

  .card-panel-icon-wrapper {
    float: none !important;
    width: 100%;
    height: 100%;
    margin: 0 !important;

    .svg-icon {
      display: block;
      margin: 14px auto !important;
      float: none !important;
    }
  }
}
</style>

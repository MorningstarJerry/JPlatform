/* eslint-disable no-unused-vars */
<template>
  <div class="dashboard-editor-container">
    <el-row>
       <el-col  :lg="12">
      <div  class="chart-wrapper">
          <pie-chart  :chart-data="pieChartData" ref="pie" />
       </div>
        </el-col>
      <el-col  :lg="12">
      <div  class="chart-wrapper">
          <dfa-chart  :chart-data="dfaChartData" />
       </div>
        </el-col>
    </el-row>
     <el-row>
       <el-col :xs="5" :sm="5" :lg="24">
      <div  class="chart-wrapper">
          <rfq-chart  :chart-data="rfqChartData" />
       </div>
        </el-col>
     
    </el-row>
  </div>
</template>

<script>
import GithubCorner from '@/components/GithubCorner'
import PanelGroup from './components/PanelGroup'
import LineChart from './components/LineChart'
import RaddarChart from './components/RaddarChart'
import PieChart from './components/PieChart'
import DfaChart from './components/DFAChart'
import RfqChart from './components/RFQChart'
import BarChart from './components/BarChart'
import TransactionTable from './components/TransactionTable'
import TodoList from './components/TodoList'
import BoxCard from './components/BoxCard'
// eslint-disable-next-line no-unused-vars
import { GetDowntimeStatistics } from '@/api/downtime'
import { getDfaStatistics } from '@/api/eDfa'
import { GetRfqStatistics } from '@/api/eRfq'

const lineChartData = {
  newVisitis: {
    expectedData: [100, 120, 161, 134, 105, 160, 165],
    actualData: [120, 82, 91, 154, 162, 140, 145]
  },
  messages: {
    expectedData: [200, 192, 120, 144, 160, 130, 140],
    actualData: [180, 160, 151, 106, 145, 150, 130]
  },
  purchases: {
    expectedData: [80, 100, 121, 104, 105, 90, 100],
    actualData: [120, 90, 100, 138, 142, 130, 130]
  },
  shoppings: {
    expectedData: [130, 140, 141, 142, 145, 150, 160],
    actualData: [120, 82, 91, 154, 162, 140, 130]
  }
}

export default {
  name: 'DashboardAdmin',
  components: {
    GithubCorner,
    PanelGroup,
    LineChart,
    RaddarChart,
    PieChart,
    BarChart,
    TransactionTable,
    TodoList,
    BoxCard,
    DfaChart,
    RfqChart
  },
  data() {
    return {
      DefaultCountTo: undefined,
      lineChartData: lineChartData.newVisitis,
      AllPieChartData: undefined,
      pieChartData: undefined,
      dfaChartData: undefined,
      rfqChartData: undefined
    }
  },
  created() {
    this.getStaticsData()
  },
  methods: {
    // handleSetLineChartData(type) {
    //   this.lineChartData = lineChartData[type]
    // },
    handleSetPieChartData(type) {
      this.pieChartData = this.AllPieChartData[type]
    },
    getStaticsData() {
      this.listLoading = true
      GetDowntimeStatistics().then(response => {
        this.pieChartData = response.data.Total
        getDfaStatistics().then(response => {
          this.dfaChartData = response.data
          GetRfqStatistics().then(response => {
            this.rfqChartData = response.data
          })
        })
      })
    },
    getObjectCount() {

    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard-editor-container {
  padding: 2px;
  background-color: rgb(240, 242, 245);
  position: relative;

  .github-corner {
    position: absolute;
    top: 0px;
    border: 0;
    right: 0;
  }

  .chart-wrapper {
    background: #fff;
    padding: 2px 2px 0;
    margin-bottom: 2px;
  }
}

@media (max-width:1024px) {
  .chart-wrapper {
    padding: 8px;
  }
}
</style>

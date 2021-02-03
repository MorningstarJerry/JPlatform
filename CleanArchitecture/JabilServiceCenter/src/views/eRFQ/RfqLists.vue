<template>
  <div class="app-container">
    <el-form class="form-container">
      <sticky :z-index="10" :class-name="'sub-navbar'">
        <div class="filter-container">
           <el-select
            v-model="listQuery.SiteId"
            placeholder="Site"
            clearable
            style="width: 150px"
            class="filter-item"
            size="mini"
          >
            <el-option
              v-for="item in siteOptions"
              :key="item.ID"
              :label="item.Name"
              :value="item.ID"
            />
          </el-select>
          <el-select
            v-model="listQuery.WorkcellId"
            placeholder="Workcell"
            clearable
            style="width: 150px"
            class="filter-item"
            size="mini"
          >
            <el-option
              v-for="item in workcellOptions"
              :key="item.Id"
              :label="item.Name"
              :value="item.Id"
            />
          </el-select>
        
          <el-button
            class="filter-item"
            type="success"
            icon="el-icon-search"
            @click="handleFilter"
            size="mini"
          >Search</el-button>
          <el-button
            class="filter-item"
            icon="el-icon-search"
            @click="handleAdvanceSearch"
            size="mini"
          >Advanced</el-button>
          <el-button
            class="filter-item"
            style="margin-left: 10px;"
            type="warning"
            icon="el-icon-circle-plus-outline"
            @click="handleCreate"
            size="mini"
            :disabled = "!canOperater"
          >Add</el-button>
          <el-button
            v-waves
            :loading="downloadLoading"
            class="filter-item"
            type="primary"
            icon="el-icon-download"
            @click="handleDownload"
            size="mini"
            :disabled = "!canOperater"
          >Export</el-button>
        </div>
      </sticky>
    </el-form>
    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      stripe
      highlight-current-row
      size="mini"
      @sort-change="sortChange"
      :row-style="{height:'20px'}"
      height="430px"
    >
      <el-table-column
        label="ID"
        prop="id"
        sortable="custom"
        align="center"
        width="80"
        :class-name="getSortClass('id')"
        fixed
      >
        <template slot-scope="{row}">
          <span>{{ row.Id }}</span>
        </template>
      </el-table-column>
        <el-table-column label="Product" width="180px" align="center" fixed>
        <template slot-scope="scope">
          <span>{{ scope.row.ProductId }}</span>
        </template>
      </el-table-column>

      <el-table-column label="PlantId" width="140px" align="center">
        <template slot-scope="scope">
          <span>{{ GetSiteName(scope.row.PlantId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="WorkcellId" width="140px" align="center" >
        <template slot-scope="scope">
          <span>{{ GetWCName(scope.row.WorkcellId) }}</span>
        </template>
      </el-table-column>
      
      
      <el-table-column label="Product #" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.ProductNo }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Date" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Date }}</span>
        </template>
      </el-table-column>
      
    
      <el-table-column label="Revision" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Revision}}</span>
        </template>
      </el-table-column>
      <el-table-column label="QuotedBy" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.QuotedBy }}</span>
        </template>
      </el-table-column>

      <el-table-column label="ApprovedBy" width="200px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.ApprovedBy }}</span>
        </template>
      </el-table-column>
      <el-table-column label="ShiftNums" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.ShiftNums}}</span>
        </template>
      </el-table-column>
      <el-table-column label="WorkDayNums" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.WorkDayNums }}</span>
        </template>
      </el-table-column>
      <el-table-column label="ShiftMinutes" width="100px" align="left">
        <template slot-scope="scope">
          <span>{{ scope.row.ShiftMinutes }}</span>
        </template>
      </el-table-column>
      <el-table-column label="PFDFactor" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.PFDFactor }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Efficiency" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Efficiency }}</span>
        </template>
      </el-table-column>
       
       <el-table-column label="LaborScrap" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.LaborScrap }}</span>
        </template>
      </el-table-column>
       <el-table-column label="Estimating" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Estimating }}</span>
        </template>
      </el-table-column>
       <el-table-column label="Setup" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Setup }}</span>
        </template>
      </el-table-column>
       <el-table-column label="Rework" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Rework }}</span>
        </template>
      </el-table-column>

        <el-table-column label="AdjustedCapacity" width="140px" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.AdjustedCapacity }}</span>
        </template>
      </el-table-column>
       <el-table-column label="TotalDL" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.TotalDL }}</span>
        </template>
      </el-table-column>
       <el-table-column label="UPH" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.UPH }}</span>
        </template>
      </el-table-column>
       <el-table-column label="UPPH" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.UPPH }}</span>
        </template>
      </el-table-column>

        <el-table-column label="TotalArea" width="120px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.TotalArea }}</span>
        </template>
      </el-table-column>
       <el-table-column label="TotalCapitalInvestments" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.TotalCapitalInvestments }}</span>
        </template>
      </el-table-column>
       <el-table-column label="TotalNRE" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.TotalNRE }}</span>
        </template>
      </el-table-column>
      

      <el-table-column fixed="right" label="Action" width="120">
        <template slot-scope="{row}">
          <router-link :to="'/Functions/eRFQ/edit/'+row.Id" class="link-type">
            <el-button type="text" size="small">Detail</el-button>
          </router-link>
            <el-button
            size="mini"
            type="text"
            @click="handleDelete(row)" :disabled = "!canOperater">Delete</el-button>
        </template>
      </el-table-column>
    </el-table>
         <pagination
      v-show="total>0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="getPageList"
    />
    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" width="80%">
      <el-form ref="dataForm" :model="listQueryAdv" label-position="left" label-width="120px">
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Site">
              <el-select
                v-model="listQueryAdv.SiteId"
                placeholder="Site"
                clearable
                style="width: 200px;"
                class="filter-item"
                size="mini"
              >
                <el-option
                  v-for="item in siteOptions"
                   :key="item.ID"
                  :label="item.Name"
                  :value="item.ID"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Workcell">
              <el-select
                v-model="listQueryAdv.WorkcellId"
                placeholder="Workcell"
                clearable
                style="width: 200px"
                class="filter-item"
                size="mini"
              >
                <el-option
                  v-for="item in workcellOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          
           <el-col :span="8">
                      <el-form-item
                        label-width="120px"
                        label="Product"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                           v-model="listQueryAdv.ProductId"
                        ></el-input>
                      </el-form-item>
                    </el-col>
        </el-row>

        <el-row>
          <el-col :span="8">
            <el-form-item
                        label-width="120px"
                        label="Product #"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                           v-model="listQueryAdv.ProductNo"
                        ></el-input>
                      </el-form-item>
          </el-col>
          <el-col :span="8">
              <el-form-item
                        label-width="120px"
                        label="Revision"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                           v-model="listQueryAdv.Revision"
                        ></el-input>
                      </el-form-item>
          </el-col>
          <el-col :span="8">
              <el-form-item
                        label-width="120px"
                        label="QuotedBy"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                           v-model="listQueryAdv.QuotedBy"
                        ></el-input>
                      </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="From">
              <el-date-picker
                v-model="listQueryAdv.FromDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select start date and time"
                size="mini"
                style="width: 200px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="To">
              <el-date-picker
                v-model="listQueryAdv.DueDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select end date and time"
                size="mini"
                style="width: 200px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
               <el-form-item
                        label-width="120px"
                        label="ApproveBy"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                           v-model="listQueryAdv.ApproveBy"
                        ></el-input>
                      </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Cancel</el-button>
        <el-button type="primary" @click="handleFilterAdv()">Confirm</el-button>
      </div>
    </el-dialog>

    <el-dialog :visible.sync="dialogPvVisible" title="Reading statistics">
      <el-table :data="pvData" border fit highlight-current-row style="width: 100%">
        <el-table-column prop="key" label="Channel" />
        <el-table-column prop="pv" label="Pv" />
      </el-table>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogPvVisible = false">Confirm</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
/* eslint-disable */
import {
  fetchList,
  fetchPv,
  createArticle,
  updateArticle
} from "@/api/article";
import waves from "@/directive/waves"; // waves directive
import { parseTime } from "@/utils";
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination
import Sticky from "@/components/Sticky"; // 粘性header组件
import store from "@/store";
import {getSiteOptions, getWorkcellOptions, getProcessesOptions } from "@/api/downtime";

import { getUsersByQuery } from "@/api/user";

import {
  Create,
  Update,
  
} from "@/api/eDfa";

import {
  Delete,
  getListsByQuery
} from "@/api/eRfq";

export default {
  name: "RFQ Lists",
  components: { Pagination, Sticky },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: "success",
        draft: "info",
        deleted: "danger"
      };
      return statusMap[status];
    },
    typeFilter(type) {
      return calendarTypeKeyValue[type];
    }
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        SiteId: store.getters.siteId,
        WorkcellId: store.getters.workcellId,
        ProductId: undefined,
        ProductNo: undefined,
        DueDate: undefined,
        FromDate: undefined,
        Revision: undefined,
        QuotedBy: undefined,
        ApproveBy: undefined,
        sort: "+id"
      },
      listQueryAdv: {
        page: 1,
        limit: 10,
        SiteId: store.getters.siteId,
        WorkcellId: store.getters.workcellId,
        ProductId: undefined,
        ProductNo: undefined,
        DueDate: undefined,
        FromDate: undefined,
        Revision: undefined,
        QuotedBy: undefined,
        ApproveBy: undefined,
        sort: "+id"
      },
      siteOptions:undefined,
      canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
      workcellOptions: undefined,
      productTypeOptions: undefined,
      productPhaseOptions: undefined,
      dataSourceOptions: undefined,
      processOptions: undefined,
      severityOptions: undefined,
      categorytypeOptions: undefined,
      businessOwnerOptions: undefined,
      assignOptions: undefined,
      statusOptions: undefined,
      materialTypeOptions: undefined,
      resolvedOptions: [
        { EnumName: "Yes", EnumValue: 1 },
        { EnumName: "No", EnumValue: 0 }
      ],
      verificationOptions: [
        { EnumName: "Yes", EnumValue: 1 },
        { EnumName: "No", EnumValue: 0 }
      ],
      sortOptions: [
        { label: "ID Ascending", key: "+id" },
        { label: "ID Descending", key: "-id" }
      ],
      // statusOptions: ["published", "draft", "deleted"],
      temp: {
        id: undefined,
        importance: 1,
        remark: "",
        timestamp: new Date(),
        title: "",
        type: "",
        status: "published"
      },
      dialogFormVisible: false,
      dialogStatus: "",
      textMap: {
        update: "Edit",
        create: "Create",
        Advance: "Advance-Conditions Search"
      },
      ImpactOptions: [
        { Id: 1, Name: "Station" },
        { Id: 2, Name: "Line" },
        { Id: 3, Name: "Workcell" }
      ],
      dialogPvVisible: false,
      pvData: [],
      rules: {
        type: [
          { required: true, message: "type is required", trigger: "change" }
        ],
        timestamp: [
          {
            type: "date",
            required: true,
            message: "timestamp is required",
            trigger: "change"
          }
        ],
        title: [
          { required: true, message: "title is required", trigger: "blur" }
        ]
      },
      downloadLoading: false
    };
  },
  created() {
    this.getSitesOps();
    this.getWorkcellOptions();

      if(this.$route.query.WcName)
      {
          getWorkcellOptions().then(response => {
           var targetObs = response.data.filter(p=> {
            return p.Name === this.$route.query.WcName;
          });
          this.listQuery.WorkcellId  = (targetObs[0].Id===undefined)?store.getters.workcellId: targetObs[0].Id;
          this.getList();
      })
      }

    // this.getList();
  },
  computed: {
    titleClass() {
      if (this.showReviewer) {
        return "advSearch";
      } else {
        return "advSearch";
      }
    }
  },
  methods: {
    GetSiteName(val) {
      if (val) {
        var targetObs = this.siteOptions.filter(p => {
          return p.ID == val;
        });

        return targetObs[0].Name;
      }
    },
    GetWCName(val) {
      if (val) {
        var targetObs = this.workcellOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    getList() {
      this.listLoading = true;
      getListsByQuery(this.listQuery).then(response => {
        this.list = response.data;
        this.total = response.total;

        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getListAdv() {
      this.listLoading = true;
      getListsByQuery(this.listQueryAdv).then(response => {
        this.list = response.data;
        this.total = response.total;

        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getUsersOptions(query) {
      this.listLoading = true;
      getUsersByQuery(query).then(response => {
        this.assignOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    //=================Get Options=====================
     getPageList(page) {
      this.listQuery.page = page.page;
      this.listQuery.limit = page.limit;
      this.getList();
    },
     getSitesOps() {
      this.listLoading = true;
      getSiteOptions().then(response => {
        this.siteOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    
    getWorkcellOptions() {
      this.listLoading = true;
      getWorkcellOptions().then(response => {
        this.workcellOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    
    handleFilter() {
      this.getList();
    },
    handleFilterAdv() {
      this.dialogFormVisible = false;
      this.getListAdv();
    },
    handleClearAdv() {
      this.dialogFormVisible = false;
      this.getListAdv();
    },
    handleModifyStatus(row, status) {
      this.$message({
        message: "操作Success",
        type: "success"
      });
      row.status = status;
    },
    sortChange(data) {
      const { prop, order } = data;
      if (prop === "id") {
        this.sortByID(order);
      }
    },
    sortByID(order) {
      if (order === "ascending") {
        this.listQuery.sort = "+id";
      } else {
        this.listQuery.sort = "-id";
      }
      this.handleFilter();
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        importance: 1,
        remark: "",
        timestamp: new Date(),
        title: "",
        status: "published",
        type: ""
      };
    },
    handleAdvanceSearch() {
      this.dialogStatus = "Advance";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    handleCreate() {
      this.$router.push({ path: "/Functions/eRFQ/create" });

      // let routeData = this.$router.resolve({ path: '/eDfa/create' });
      // window.open(routeData.href, '_blank');
    },
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          this.temp.id = parseInt(Math.random() * 100) + 1024; // mock a id
          this.temp.author = "vue-element-admin";
          createArticle(this.temp).then(() => {
            this.list.unshift(this.temp);
            this.dialogFormVisible = false;
            this.$notify({
              title: "Success",
              message: "Created Successfully",
              type: "success",
              duration: 2000
            });
          });
        }
      });
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row); // copy obj
      this.temp.timestamp = new Date(this.temp.timestamp);
      this.dialogStatus = "update";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    updateData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp);
          tempData.timestamp = +new Date(tempData.timestamp); // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          updateArticle(tempData).then(() => {
            for (const v of this.list) {
              if (v.id === this.temp.id) {
                const index = this.list.indexOf(v);
                this.list.splice(index, 1, this.temp);
                break;
              }
            }
            this.dialogFormVisible = false;
            this.$notify({
              title: "Success",
              message: "Update Successfully",
              type: "success",
              duration: 2000
            });
          });
        }
      });
    },
     handleDelete(row) {
      const tempData = Object.assign({}, row);

        this.$confirm('Are you sure to delete this record?')
          .then(_ => {
             Delete(tempData).then(() => {
                this.getList();
                this.$notify({
                  title: "Success",
                  message: "Delete Successfully",
                  type: "success",
                  duration: 2000
                });
              });
          })
          .catch(_ => {});
      },
    handleFetchPv(pv) {
      fetchPv(pv).then(response => {
        this.pvData = response.data.pvData;
        this.dialogPvVisible = true;
      });
    },
    handleDownload() {
      this.downloadLoading = true;
      import("@/vendor/Export2Excel").then(excel => {
        const tHeader = [
          "RFQGid",
          "PlantId",
          "WorkcellId",
          "ProductId",
          "ProductNo",
          "Date",
          "Revision",
          "QuotedBy",
          "ApprovedBy",
          "ShiftNums",
          "WorkDayNums",
          "ShiftMinutes",
          "PFDFactor",
          "RealizationFactor",
          "Efficiency",
          "LaborScrap",
          "Estimating",
          "Setup",
          "Rework",
          "AdjustedCapacity",
          "TotalDL",
          "UPH",
          "UPPH",
          "TotalArea",
          "TotalCapitalInvestments",
          "TotalNRE"
        ];
        const filterVal = [
         "RFQGid",
          "PlantId",
          "WorkcellId",
          "ProductId",
          "ProductNo",
          "Date",
          "Revision",
          "QuotedBy",
          "ApprovedBy",
          "ShiftNums",
          "WorkDayNums",
          "ShiftMinutes",
          "PFDFactor",
          "RealizationFactor",
          "Efficiency",
          "LaborScrap",
          "Estimating",
          "Setup",
          "Rework",
          "AdjustedCapacity",
          "TotalDL",
          "UPH",
          "UPPH",
          "TotalArea",
          "TotalCapitalInvestments",
          "TotalNRE"
        ];
        const data = this.formatJson(filterVal, this.list);
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: "RFQlists"
        });
        this.downloadLoading = false;
      });
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v =>
        filterVal.map(j => {
          if (j === "PlantId") {
            return this.GetSiteName(v[j]);
          } else if (j == "WorkcellId") {
            return this.GetWCName(v[j]);
          } else {
            return v[j];
          }
        })
      );
    },
    getSortClass: function(key) {
      const sort = this.listQuery.sort;
      return sort === `+${key}`
        ? "ascending"
        : sort === `-${key}`
        ? "descending"
        : "";
    },
    getSiteOptions: function() {}
  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.customWidth {
  width: 80%;
}
</style>
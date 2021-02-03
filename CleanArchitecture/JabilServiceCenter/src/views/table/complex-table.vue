<template>
  <div class="app-container">
    <el-form class="form-container">
      <sticky :z-index="10" :class-name="'sub-navbar'">
        <div class="filter-container">
          <el-select
            v-model="listQuery.SiteId"
            placeholder="Site"
            clearable
            style="width: 100px"
            class="filter-item"
            size = "mini"
          >
            <el-option
              v-for="item in siteOptions"
              :key="item.ID"
              :label="item.Name"
              :value="item.ID"
            />
          </el-select>
          <el-select
            v-model="listQuery.SectorId"
            placeholder="Sector"
            clearable
            style="width: 100px"
            class="filter-item"
            size = "mini"
          >
            <el-option
              v-for="item in sectorOptions"
              :key="item.Id"
              :label="item.Name"
              :value="item.Id"
            />
          </el-select>
          <el-select
            v-model="listQuery.workcellId"
            placeholder="Workcell"
            clearable
            class="filter-item"
            style="width: 100px"
            size = "mini"
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
            size = "mini"
          >Search</el-button>
           <el-button
            class="filter-item"
            icon="el-icon-search"
            @click="handleAdvanceSearch"
            size = "mini"
          >Advanced</el-button>
          <el-button
            class="filter-item"
            style="margin-left: 10px;"
            type="warning"
            icon="el-icon-circle-plus-outline"
            @click="handleCreate"
            size = "mini"
            :disabled = "!canOperater"
          >Add</el-button>
           <el-button
            class="filter-item"
            icon="el-icon-upload"
            @click="handleUpload"
            size = "mini"
            :disabled = "!canOperater"
          >Upload</el-button>
          <el-button
            v-waves
            :loading="downloadLoading"
            class="filter-item"
            type="primary"
            icon="el-icon-download"
            @click="handleDownload"
            size = "mini"
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
      @sort-change="sortChange"
      size="mini"
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
            <span>{{ row.ID }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Date" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.DownDate | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="Site" min-width="120px" fixed>
        <template slot-scope="{row}">
          <span class="link-type">{{ GetSiteName(row.SiteId) }}</span>
        </template>
      </el-table-column> -->
      <!-- <el-table-column  label="Sector" width="80px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Id }}</span>
        </template>
      </el-table-column> -->
        <el-table-column label="Equipment" width="120px" align="center" fixed>
        <template slot-scope="scope">
          <span>{{ scope.row.EquipmentId }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Workcell" width="140px" align="center" fixed>
        <template slot-scope="scope">
          <span>{{ GetWCName(scope.row.WorkcellId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Line" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetLineName(scope.row.LineId) }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="Process" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetProcessName(scope.row.ProcessId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Downtime type" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ GetDowntypeName(scope.row.Downtimetype) }}</span>
        </template>
      </el-table-column>
    
      <el-table-column  label="DowntimeDescription" width="200px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.DowntimeDescription }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="StartDate" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.StartDate | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="EndDate" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.EndDate | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="DurationMins" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.DurationMins }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="RootCause" width="300px" align="left">
        <template slot-scope="scope">
          <span>{{ scope.row.RootCause }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="Impact" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetImpactName(scope.row.Impact) }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="ImpactWorkingHours" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.ImpactWorkingHours }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="ContainmentSolution" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.ContainmentSolution }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="LongTermSolution" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.LongTermSolution }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Department" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetDeptName(scope.row.DepartmentId) }}</span>
        </template>
      </el-table-column>
      <el-table-column  label="PIC" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.PIC }}</span>
        </template>
      </el-table-column>
        <el-table-column
      fixed="right"
      label="Action"
      width="120">
      <template slot-scope="{row}">
         <router-link :to="'/Functions/Downtime/edit/'+row.ID" class="link-type">
          <el-button  type="text" size="small">Detail</el-button>
         </router-link>
          <el-button
            size="mini"
            type="text"
            @click="handleDelete(row)" :disabled = "!canOperater">Delete</el-button>
      </template>
    </el-table-column>
      <!-- <el-table-column  label="Remark" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Remark }}</span>
        </template>
      </el-table-column> -->
      <!-- <el-table-column
        label="Actions"
        align="center"
        width="230"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{row}">
          <el-button
            v-if="row.status!='deleted'"
            size="mini"
            type="danger"
            @click="handleModifyStatus(row,'deleted')"
          >Delete</el-button>
        </template>
      </el-table-column>-->
    </el-table>

    <pagination
      v-show="total>0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="getPageList"
    />
    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" width="70%" label-position="left">
      <el-form ref="dataForm" :model="listQueryAdv" label-position="left" label-width="120px">
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Site">
              <el-select
                v-model="listQueryAdv.SiteId"
                placeholder="Site"
                clearable
                style="width: 120px"
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
            <el-form-item class="postInfo-container-item" label="Sector">
              <el-select
                v-model="listQueryAdv.SectorId"
                placeholder="Sector"
                clearable
                style="width: 120px;"
                class="filter-item"
                size="mini"
              >
                <el-option
                  v-for="item in sectorOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Workcell">
              <el-select
                v-model="listQueryAdv.workcellId"
                placeholder="Workcell"
                clearable
                class="filter-item"
                style="width: 120px"
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
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Line">
              <el-select
                v-model="listQueryAdv.lineId"
                placeholder="Line"
                clearable
                class="filter-item"
                style="width: 120px"
                :remote-method="getLineOptionsByQuery"
                filterable
                default-first-option
                remote
                size="mini"
              >
                <el-option
                  v-for="item in lineOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Process">
              <el-select
                v-model="listQueryAdv.processId"
                placeholder="Process"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in processOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="DownType">
              <el-select
                v-model="listQueryAdv.downtimetype"
                placeholder="DownType"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in downtimetypeOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Department">
              <el-select
                v-model="listQueryAdv.departmentId"
                placeholder="Department"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in departmentOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Type">
              <el-select
                v-model="listQueryAdv.equipmentTypeId"
                placeholder="equipmentType"
                clearable
                class="filter-item"
                style="width: 120px"
                :remote-method="getEquipmentTypeOptionsByQuery"
                default-first-option
                remote
                size="mini"
              >
                <el-option
                  v-for="item in equipmentTypeOptions"
                  :key="item.Id"
                  :label="item.Name"
                  :value="item.Id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Equipment">
              <el-select
                v-model="listQueryAdv.equipmentId"
                placeholder="Equipment"
                clearable
                class="filter-item"
                style="width: 120px"
                :remote-method="getEquipmentOptionsByQuery"
                filterable
                default-first-option
                remote
                size="mini"
              >
                <el-option
                  v-for="item in equipmentOptions"
                  :key="item.Id"
                  :label="item.Code+'-'+item.Name"
                  :value="item.Code"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="From">
              <el-date-picker
                v-model="listQueryAdv.startDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select start date and time"
                size="mini"
                 style="width: 120px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="To">
              <el-date-picker
                v-model="listQueryAdv.endDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select end date and time"
                size="mini"
                 style="width: 120px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8"></el-col>
        </el-row>

        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Pic">
              <el-input
                v-model="listQueryAdv.PIC"
                placeholder="Pic"
                style="width: 120px;"
                class="filter-item"
                @keyup.enter.native="handleFilter"
                size="mini"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8"></el-col>
          <el-col :span="8"></el-col>
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
import {
  Delete,
  getLineOptions,
  getSiteOptions,
  getWorkcellOptions,
  getsectorsOptions,
  getProcessesOptions,
  getDowntimeTypesOptions,
  getDepartmentsOptions,
  getEquipmentOptions,
  getEquipmentOptionsByQuery,
  getEquipmentTypeOptionsByQuery,
  getLineOptionsByQuery
} from "@/api/downtime";
const calendarTypeOptions = [
  { key: "CN", display_name: "China" },
  { key: "US", display_name: "USA" },
  { key: "JP", display_name: "Japan" },
  { key: "EU", display_name: "Eurozone" }
];

// arr to obj, such as { CN : "China", US : "USA" }
const calendarTypeKeyValue = calendarTypeOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name;
  return acc;
}, {});

export default {
  name: "ComplexTable",
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
      showReviewer: false,
      showDetails: false,
      canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
      listQuery: {
        page: 1,
        limit: 10,
        SiteId: store.getters.siteId,
        SectorId: store.getters.sectorId,
        workcellId:  store.getters.workcellId,
        lineId: undefined,
        processId: undefined,
        downtimetype: undefined,
        startDate: undefined,
        endDate: undefined,
        departmentId: store.getters.DepartmentId,
        equipmentId: undefined,
        equipmentTypeId: undefined,
        PIC: undefined,
        sort: "+id"
      },
       listQueryAdv: {
        page: 1,
        limit: 10,
        SiteId: store.getters.siteId,
        SectorId: store.getters.sectorId,
        workcellId: undefined,
        lineId: undefined,
        processId: undefined,
        downtimetype: undefined,
        startDate: undefined,
        endDate: undefined,
        departmentId: store.getters.DepartmentId,
        equipmentId: undefined,
        equipmentTypeId: undefined,
        PIC: undefined,
        sort: "+id"
      },
      siteOptions: undefined,
      sectorOptions: undefined,
      workcellOptions: undefined,
      lineOptions: undefined,
      processOptions: undefined,
      downtimetypeOptions: undefined,
      departmentOptions: undefined,
      equipmentOptions: undefined,
      equipmentTypeOptions: undefined,
      calendarTypeOptions,
      sortOptions: [
        { label: "ID Ascending", key: "+id" },
        { label: "ID Descending", key: "-id" }
      ],
      statusOptions: ["published", "draft", "deleted"],
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
        Advance:"Advance-Conditions Search"
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
 
    this.getWorkcellOptions();
    this.getSitesOps();
    this.getsectorsOptions();
    this.getLineOptions();
    this.getProcessesOptions();
    this.getDowntimeTypesOptions();
    this.getDepartmentsOptions();
  
      if(this.$route.query.WcName)
      {
          getWorkcellOptions().then(response => {
           var targetObs = response.data.filter(p=> {
            return p.Name === this.$route.query.WcName;
          });
          this.listQuery.workcellId  = (targetObs[0].Id===undefined)?store.getters.workcellId: targetObs[0].Id;
          this.getList();
      })
      }
     
    this.getEquipmentOptions();
    this.getEquipmentTypeOptionsByQuery("");
   
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
     GetWCId(val) {
      if (val) {
        var targetObs = this.workcellOptions.filter(function(p) {
          return p.Name === val;
        });

        return targetObs[0].Id;
      }
    },
    GetLineName(val) {
      if (val) {
        var targetObs = this.lineOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetProcessName(val) {
      if (val) {
        var targetObs = this.processOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetImpactName(val) {
     if (val) {
        var targetObs = this.ImpactOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetDowntypeName(val) {
      if (val) {
        var targetObs = this.downtimetypeOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetDeptName(val) {
      if (val) {
        var targetObs = this.departmentOptions.filter(p => {
          return p.Id == val;
        });
        return targetObs[0].Name;
      }
    },
    getPageList(page) {
      this.listQuery.page = page.page;
      this.listQuery.limit = page.limit;
      this.getList();
    },
    getList() {
      this.listLoading = true;
     
      debugger
      fetchList(this.listQuery).then(response => {
        this.list = response.data;
        this.total = response.total;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 3.5 * 1000);
      });
    },
     getListAdv() {
      this.listLoading = true;
      fetchList(this.listQueryAdv).then(response => {
        this.list = response.data;
        this.total = response.total;

        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
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
    getsectorsOptions() {
      this.listLoading = true;
      getsectorsOptions().then(response => {
        this.sectorOptions = response.data;
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
    getLineOptions() {
      this.listLoading = true;
      getLineOptions().then(response => {
        this.lineOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getLineOptionsByQuery(query) {
      this.listLoading = true;
      getLineOptionsByQuery(query).then(response => {
        this.lineOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getProcessesOptions() {
      this.listLoading = true;
      getProcessesOptions().then(response => {
        this.processOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getDowntimeTypesOptions() {
      this.listLoading = true;
      getDowntimeTypesOptions().then(response => {
        this.downtimetypeOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getDepartmentsOptions() {
      this.listLoading = true;
      getDepartmentsOptions().then(response => {
        this.departmentOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getEquipmentOptions() {
      this.listLoading = true;
      getEquipmentOptions().then(response => {
        this.equipmentOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getEquipmentOptionsByQuery(query) {
      this.listLoading = true;
      getEquipmentOptionsByQuery(query).then(response => {
        this.equipmentOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getEquipmentTypeOptionsByQuery(query) {
      this.listLoading = true;
      getEquipmentTypeOptionsByQuery(query).then(response => {
        debugger;
        this.equipmentTypeOptions = response.data;
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
    handleUpload() {
      this.$router.push({ path: "/Functions/Downtime/ExcelUpload" });
    },
    handleCreate() {
      this.$router.push({ path: "/Functions/Downtime/create" });
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
          "DownDate",
          "SiteId",
          "WorkcellId",
          "LineId",
          "ProcessId",
          "Downtimetype",
          "EquipmentId",
          "DowntimeDescription",
          "StartDate",
          "EndDate",
          "DurationMins",
          "RootCause",
          "Impact",
          "ImpactWorkingHours",
          "ContainmentSolution",
          "LongTermSolution",
          "DepartmentId",
          "PIC"
        ];
        const filterVal = [
          "DownDate",
          "SiteId",
          "WorkcellId",
          "LineId",
          "ProcessId",
          "Downtimetype",
          "EquipmentId",
          "DowntimeDescription",
          "StartDate",
          "EndDate",
          "DurationMins",
          "RootCause",
          "Impact",
          "ImpactWorkingHours",
          "ContainmentSolution",
          "LongTermSolution",
          "DepartmentId",
          "PIC"
        ];
        const data = this.formatJson(filterVal, this.list);
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: "Downtimelists"
        });
        this.downloadLoading = false;
      });
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v =>
        filterVal.map(j => {
          if (j === "SiteId") {
            return this.GetSiteName(v[j]);
          } else if (j == "WorkcellId") {
            return this.GetWCName(v[j]);
          } else if (j == "LineId") {
            return this.GetLineName(v[j]);
          } else if (j == "ProcessId") {
            return this.GetProcessName(v[j]);
          } else if (j == "Downtimetype") {
            return this.GetDowntypeName(v[j]);
          } else if (j == "DepartmentId") {
            return this.GetDeptName(v[j]);
          } else if (j == "Impact") {
            return this.GetImpactName(v[j]);
          } 
          else {
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
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
</style>
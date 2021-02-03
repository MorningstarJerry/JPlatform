<template>
  <div class="app-container">
    <el-form class="form-container">
      <sticky :z-index="10" :class-name="'sub-navbar'">
        <div class="filter-container">
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
          <el-select
            v-model="listQuery.ProducttypeId"
            placeholder="ProductType"
            clearable
            style="width: 150px"
            class="filter-item"
            size="mini"
          >
            <el-option
              v-for="item in productTypeOptions"
              :key="item.EnumValue"
              :label="item.EnumName"
              :value="item.EnumValue"
            />
          </el-select>
          <el-select
            v-model="listQuery.Status"
            placeholder="status"
            clearable
            class="filter-item"
            style="width: 150px"
            size="mini"
          >
            <el-option
              v-for="item in statusOptions"
              :key="item.EnumValue"
              :label="item.EnumName"
              :value="item.EnumValue"
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
      <el-table-column label="WorkcellId" width="140px" align="center" fixed>
        <template slot-scope="scope">
          <span>{{ GetWCName(scope.row.WorkcellId) }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="StartDate" width="180px" align="center" fixed>
        <template slot-scope="scope">
          <span>{{ scope.row.StartDate | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DueDate" width="180px" align="center" fixed>
        <template slot-scope="scope">
          <span>{{ scope.row.DueDate | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column> -->
       <el-table-column label="Description" width="250px" align="left">
        <template slot-scope="scope">
          <span>{{scope.row.Suggestion }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Solution" width="250px" align="left">
        <template slot-scope="scope">
          <span>{{ scope.row.Solution}}</span>
        </template>
      </el-table-column>
      <el-table-column label="ProducttypeId" width="120px" align="center">
        <template slot-scope="scope">
          <span>{{ GetProductTypeName(scope.row.ProducttypeId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="ProductPhase" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetProductPhaseName(scope.row.ProductPhaseId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DataSource" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetDataSourceName(scope.row.DataSourceId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Process" width="80px" align="center">
        <template slot-scope="scope">
          <span>{{ GetProcessName(scope.row.ProcessId) }}</span>
        </template>
      </el-table-column>

      <el-table-column label="AffectedNo" width="100px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.AffectedNo }}</span>
        </template>
      </el-table-column>
      <el-table-column label="MaterialType" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ GetMaterialTypeName(scope.row.MaterialTypeId)}}</span>
        </template>
      </el-table-column>
      <el-table-column label="Severity" width="150px" align="center">
        <template slot-scope="scope">
          <span>{{ GetServerityName(scope.row.Severity) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Categorytype" width="100px" align="left">
        <template slot-scope="scope">
          <span>{{ GetCategoryTypeName(scope.row.CategorytypeId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="BusinessOwner" width="180px" align="center">
        <template slot-scope="scope">
          <span>{{ GetBusinessOwnerName(scope.row.BusinessOwnerId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Status" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetStatusName(scope.row.Status) }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="Resolved" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetResolvedName(scope.row.Resolved) }}</span>
        </template>
      </el-table-column>
        <el-table-column label="Verification" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetVerificationName(scope.row.Verification) }}</span>
        </template>
      </el-table-column>-->
      <el-table-column fixed="right" label="Action" width="100">
        <template slot-scope="{row}">
          <router-link :to="'/Functions/eDfa/edit/'+row.Id" class="link-type">
            <el-button type="text" size="small">Detail</el-button>
          </router-link>
           <el-button
            size="mini"
            type="text"
            @click="handleDelete(row)"  :disabled = "!canOperater">Delete</el-button>
        </template>
      </el-table-column>
      <!-- <el-table-column  label="Remark" width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.Remark }}</span>
        </template>
      </el-table-column>-->
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

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" width="80%">
      <el-form ref="dataForm" :model="listQueryAdv" label-position="left" label-width="120px">
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Workcell">
              <el-select
                v-model="listQueryAdv.WorkcellId"
                placeholder="Site"
                clearable
                style="width: 120px"
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
            <el-form-item class="postInfo-container-item" label="Producttype">
              <el-select
                v-model="listQueryAdv.ProducttypeId"
                placeholder="ProducttypeId"
                clearable
                style="width: 120px;"
                class="filter-item"
                size="mini"
              >
                <el-option
                  v-for="item in productTypeOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="ProductPhase">
              <el-select
                v-model="listQueryAdv.ProductPhaseId"
                placeholder="Product Phase"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in productPhaseOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="DataSource">
              <el-select
                v-model="listQueryAdv.DataSourceId"
                placeholder="Data Source"
                clearable
                class="filter-item"
                style="width: 120px"
                default-first-option
                remote
                size="mini"
              >
                <el-option
                  v-for="item in dataSourceOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
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
            <el-form-item class="postInfo-container-item" label="Severity">
              <el-select
                v-model="listQueryAdv.Severity"
                placeholder="Severity"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in severityOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Categorytype">
              <el-select
                v-model="listQueryAdv.CategorytypeId"
                placeholder="Category type"
                clearable
                class="filter-item"
                style="width: 120px"
                default-first-option
                remote
                size="mini"
              >
                <el-option
                  v-for="item in categorytypeOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="BusinessOwner">
              <el-select
                v-model="listQueryAdv.BusinessOwnerId"
                placeholder="Business Owner"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in businessOwnerOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Assign">
              <el-select
                v-model="listQueryAdv.AssignUId"
                placeholder="AssignUId"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
                remote
                :remote-method="getUsersOptions"
                filterable
              >
                <el-option
                  v-for="item in assignOptions"
                  :key="item.ID"
                  :label="item.Password"
                  :value="item.ID"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Status">
              <el-select
                v-model="listQueryAdv.Status"
                placeholder="Status"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in statusOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Resolved">
              <el-select
                v-model="listQueryAdv.Resolved"
                placeholder="Resolved"
                clearable
                class="filter-item"
                style="width: 120px"
                size="mini"
              >
                <el-option
                  v-for="item in resolvedOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="Verification">
              <el-select
                v-model="listQueryAdv.Verification"
                placeholder="Verification"
                clearable
                class="filter-item"
                style="width:120px"
                size="mini"
              >
                <el-option
                  v-for="item in verificationOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="From">
              <el-date-picker
                v-model="listQueryAdv.StartDate"
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
                v-model="listQueryAdv.DueDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select end date and time"
                size="mini"
                style="width: 120px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item class="postInfo-container-item" label="MaterialType">
              <el-select
                v-model="listQueryAdv.MaterialTypeId"
                placeholder="Verification"
                clearable
                class="filter-item"
                style="width:120px"
                size="mini"
              >
                <el-option
                  v-for="item in materialTypeOptions"
                  :key="item.EnumValue"
                  :label="item.EnumName"
                  :value="item.EnumValue"
                />
              </el-select>
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
import { getWorkcellOptions, getProcessesOptions } from "@/api/downtime";

import { getUsersByQuery } from "@/api/user";

import {
  getAllLists,
  getListsByQuery,
  Create,
  Update,
  Delete,
  getAllProductTypes,
  getProductPhaseOptions,
  getDataSourceOptions,
  getMaterialTypeOptions,
  getSeverityOptions,
  getCategoryOptions,
  getBusinessOwnerOptions,
  getDFAStatusOptions
} from "@/api/eDfa";

export default {
  name: "DFA Lists",
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
      canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
      listQuery: {
        page: 1,
        limit: 10,
        ProducttypeId: undefined,
        ProductPhaseId: undefined,
        WorkcellId: store.getters.workcellId,
        DataSourceId: undefined,
        StartDate: undefined,
        DueDate: undefined,
        ProcessId: undefined,
        MaterialTypeId: undefined,
        Severity: undefined,
        CategorytypeId: undefined,
        BusinessOwnerId: undefined,
        AssignUId: undefined,
        Status: undefined,
        Resolved: undefined,
        Verification: undefined,
        sort: "+id"
      },
      listQueryAdv: {
        page: 1,
        limit: 10,
        ProducttypeId: undefined,
        ProductPhaseId: undefined,
        WorkcellId: undefined,
        DataSourceId: undefined,
        StartDate: undefined,
        DueDate: undefined,
        ProcessId: undefined,
        MaterialTypeId: undefined,
        Severity: undefined,
        CategorytypeId: undefined,
        BusinessOwnerId: undefined,
        AssignUId: undefined,
        Status: undefined,
        Resolved: undefined,
        Verification: undefined,
        sort: "+id"
      },
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
    this.getProcessesOptions();
    this.getWorkcellOptions();
    this.getAllProductTypes();
    this.getProductPhaseOptions();
    this.getDataSourceOptions();
    this.getMaterialTypeOptions();
    this.getSeverityOptions();
    this.getCategoryOptions();
    this.getBusinessOwnerOptions();
    this.getDFAStatusOptions();
    // this.getList();

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

    this.getUsersOptions("");
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
    GetProductTypeName(val) {
      if (val) {
        var targetObs = this.productTypeOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
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
    GetProductPhaseName(val) {
      if (val) {
        var targetObs = this.productPhaseOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetDataSourceName(val) {
      if (val) {
        var targetObs = this.dataSourceOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetMaterialTypeName(val) {
      if (val) {
        var targetObs = this.materialTypeOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetServerityName(val) {
      if (val) {
        var targetObs = this.severityOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetCategoryTypeName(val) {
      if (val) {
        var targetObs = this.categorytypeOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetBusinessOwnerName(val) {
      if (val) {
        var targetObs = this.businessOwnerOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetStatusName(val) {
      if (val) {
        var targetObs = this.statusOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetResolvedName(val) {
      if (val) {
        var targetObs = this.resolvedOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetVerificationName(val) {
      if (val) {
        var targetObs = this.verificationOptions.filter(p => {
          return p.EnumValue == val;
        });

        return targetObs[0].EnumName;
      }
    },
    GetAssignUidName(val) {
      if (val) {
        var targetObs = this.assignOptions.filter(p => {
          return p.ID == val;
        });

        return targetObs[0].Password;
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
    getAllProductTypes() {
      this.listLoading = true;
      getAllProductTypes().then(response => {
        this.productTypeOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getProductPhaseOptions() {
      this.listLoading = true;
      getProductPhaseOptions().then(response => {
        this.productPhaseOptions = response.data;
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
    getDataSourceOptions() {
      this.listLoading = true;
      getDataSourceOptions().then(response => {
        this.dataSourceOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getMaterialTypeOptions(query) {
      this.listLoading = true;
      getMaterialTypeOptions(query).then(response => {
        this.materialTypeOptions = response.data;
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
    getSeverityOptions() {
      this.listLoading = true;
      getSeverityOptions().then(response => {
        this.severityOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getCategoryOptions() {
      this.listLoading = true;
      getCategoryOptions().then(response => {
        this.categorytypeOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getBusinessOwnerOptions() {
      this.listLoading = true;
      getBusinessOwnerOptions().then(response => {
        this.businessOwnerOptions = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getDFAStatusOptions() {
      this.listLoading = true;
      getDFAStatusOptions().then(response => {
        this.statusOptions = response.data;
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
      this.$router.push({ path: "/Functions/eDfa/create" });

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
          "Id",
          "Producttype",
          "Workcell",
          "ProductPhase",
          "DataSource",
          "Process",
          "AffectedNo",
          "MaterialType",
          "Severity",
          "Categorytype",
          "Suggestion",
          "Solution",
          "BusinessOwner",
          "Assign",
          "StartDate",
          "DueDate",
          "CustomerFeedback",
          "Status",
          "Resolved",
          "Verification"
        ];
        const filterVal = [
          "Id",
          "ProducttypeId",
          "WorkcellId",
          "ProductPhaseId",
          "DataSourceId",
          "ProcessId",
          "AffectedNo",
          "MaterialTypeId",
          "Severity",
          "CategorytypeId",
          "Suggestion",
          "Solution",
          "BusinessOwnerId",
          "AssignUId",
          "StartDate",
          "DueDate",
          "CustomerFeedback",
          "Status",
          "Resolved",
          "Verification"
        ];
        const data = this.formatJson(filterVal, this.list);
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: "DFAlists"
        });
        this.downloadLoading = false;
      });
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v =>
        filterVal.map(j => {
          if (j === "ProducttypeId") {
            return this.GetProductTypeName(v[j]);
          } else if (j == "WorkcellId") {
            return this.GetWCName(v[j]);
          } else if (j == "ProductPhaseId") {
            return this.GetProductPhaseName(v[j]);
          } else if (j == "DataSourceId") {
            return this.GetDataSourceName(v[j]);
          } else if (j == "ProcessId") {
            return this.GetProcessName(v[j]);
          } else if (j == "MaterialTypeId") {
            return this.GetMaterialTypeName(v[j]);
          } else if (j == "Severity") {
            return this.GetServerityName(v[j]);
          } else if (j == "CategorytypeId") {
            return this.GetCategoryTypeName(v[j]);
          } else if (j == "BusinessOwnerId") {
            return this.GetBusinessOwnerName(v[j]);
          } else if (j == "AssignUId") {
            return this.GetAssignUidName(v[j]);
          } else if (j == "Status") {
            return this.GetStatusName(v[j]);
          } else if (j == "Resolved") {
            return this.GetResolvedName(v[j]);
          } else if (j == "Verification") {
            return this.GetVerificationName(v[j]);
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
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.customWidth {
  width: 80%;
}
</style>
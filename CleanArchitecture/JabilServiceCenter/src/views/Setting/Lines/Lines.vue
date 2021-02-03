<template>
  <div class="app-container">
    <el-form class="form-container" size="mini">
      <sticky :z-index="10" :class-name="'sub-navbar'">
       
        <div class="filter-container">
           <el-row type="flex" justify="center" align="middle">
          <el-col :span="4" :align="left">
                <el-select
            v-model="listQuery.workcellId"
            placeholder="Workcell"
            clearable
            class="filter-item"
            style="width: 180px"
            size="mini"
          >
            <el-option
              v-for="item in workcellOptions"
              :key="item.Id"
              :label="item.Name"
              :value="item.Id"
            />
          </el-select>
          </el-col>
          <el-col :span="6" :align="left">
               <el-select
            v-model="listQuery.LineId"
            placeholder="Line"
            clearable
            class="filter-item"
            style="width: 240px"
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
          </el-col>
          <el-col :span="14" :align="right" style="vertical-align: middle;">

             <el-button
            class="filter-item"
            type="success"
            icon="el-icon-search"
            @click="handleFilter"
            size="mini"
          >Search</el-button>
          <el-button
            class="filter-item"
            style="margin-left: 10px;"
            type="warning"
            icon="el-icon-edit"
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
          </el-col>
        </el-row>         
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
      height="450px"
    >
      <el-table-column
        label="ID"
        prop="id"
        sortable="custom"
        align="center"
        width="100"
        :class-name="getSortClass('id')"
      >
        <template slot-scope="{row}">
            <span>{{ row.Id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Workcell" min-width="140px" align="center">
        <template slot-scope="scope">
          <span>{{ GetWCName(scope.row.WorkcellId) }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Line" min-width="110px" align="center">
        <template slot-scope="scope">
          <span>{{ GetLineName(scope.row.Id) }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="Actions"
        align="right"
        width="230"
        class-name="small-padding fixed-width"
        v-if="canOperater"
      >
        <template slot-scope="{row}"  >
          <el-button type="text" size="mini" @click="handleUpdate(row)">Edit</el-button>
          <el-button
            v-if="row.status!='deleted'"
            size="mini"
            type="text"
            @click="handleDelete(row)"
          >Delete</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-position="left"
        label-width="120px"
        style="width: 400px; margin-left:50px;"
      >
        <el-form-item class="postInfo-container-item" label="Workcell" prop="WorkcellId">
          <el-select
            v-model="temp.WorkcellId"
            clearable
            class="filter-item"
            placeholder="Please select"
          >
            <el-option
              v-for="item in workcellOptions"
              :key="item.Id"
              :label="item.Name"
              :value="item.Id"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="LineCode" prop="Name">
          <el-input v-model="temp.Name" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Cancel</el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">Confirm</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
/* eslint-disable */
import waves from "@/directive/waves"; // waves directive
import { parseTime } from "@/utils";
import Sticky from "@/components/Sticky"; // 粘性header组件
import store from "@/store";
import { getWorkcellOptions } from "@/api/downtime";

import {
  getLineOptions,
  getLineOptionsByQuery,
  getLineByQuery,
  CreateLine,
  UpdateLine,
  DeleteLine
} from "@/api/lines";

// arr to obj, such as { CN : "China", US : "USA" }

export default {
  name: "Lines",
  components: { Sticky },
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
    const validateRequire = (rule, value, callback) => {
      if (value === "" || value === undefined) {
        this.$message({
          message: rule.field + "为必传项",
          type: "error"
        });
        callback(new Error(rule.field + "为必传项"));
      } else {
        callback();
      }
    };
    return {
      tableKey: 0,
      list: undefined,
      total: 0,
      listLoading: true,
      showDetails: false,
      canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
      listQuery: {
        workcellId: store.getters.workcellId,
        lineId: undefined,
        sort: "+id"
      },
      siteOptions: undefined,
      sectorOptions: undefined,
      workcellOptions: undefined,
      lineOptions: undefined,
      sortOptions: [
        { label: "ID Ascending", key: "+id" },
        { label: "ID Descending", key: "-id" }
      ],
      statusOptions: ["published", "draft", "deleted"],
      temp: {
        Id: undefined,
        Name: 1,
        WorkcellId: undefined
      },
      dialogFormVisible: false,
      dialogStatus: "",
      textMap: {
        update: "Edit",
        create: "Create"
      },
      rules: {
        WorkcellId: [{ validator: validateRequire, trigger: "change" }],
        Name: [{ validator: validateRequire, trigger: "blur" }]
      },
      downloadLoading: false
    };
  },
  created() {
    this.getWorkcellOptions();
    this.getLineByQuery(this.listQuery);
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
    GetLineName(val) {
      if (val) {
        var targetObs = this.list.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
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
        debugger;
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
    getLineByQuery(query) {
      this.listLoading = true;
      getLineByQuery(query).then(response => {
        this.list = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    handleFilter() {
      this.getLineByQuery(this.listQuery);
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
        Id: undefined,
        Name: "",
        WorkcellId: undefined
      };
    },
    handleCreate() {
      this.resetTemp();
      this.dialogStatus = "create";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          this.temp.Id = parseInt(Math.random() * 100) + 1024; // mock a id
          CreateLine(this.temp).then(() => {
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
          // tempData.timestamp = +new Date(tempData.timestamp); // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          UpdateLine(tempData).then(() => {
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
      DeleteLine(tempData).then(() => {
        for (const v of this.list) {
          if (v.id === this.temp.id) {
            this.$notify({
              title: "Success",
              message: "Delete Successfully",
              type: "success",
              duration: 2000
            });
            const index = this.list.indexOf(row);
            this.list.splice(index, 1);
          }
        }
      });
    },
    handleDownload() {
      this.downloadLoading = true;
      import("@/vendor/Export2Excel").then(excel => {
        const tHeader = [
          "Id",
          "Name",
          "WorkcellId",
        ];
        const filterVal = [
          "Id",
          "Name",
          "WorkcellId",
        ];
        const data = this.formatJson(filterVal, this.list);
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: "Jabil_IE_Lines"
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
</style>
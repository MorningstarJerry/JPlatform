<template>
  <div class="app-container">
    <el-container style="height: 90%; border: 1px solid #eee">
      <el-header>
        <sticky :z-index="10" :class-name="'sub-header'">
          <el-row>
            <el-col :span="18" style="text-align:left;">
              <el-input
                placeholder="Model"
                v-model="listQuery.modelCode"
                clearable
                style=" margin-left:10px; width:190px; margin-right:20px;"
                size="mini"
              ></el-input>
              <el-date-picker
                v-model="listQuery.StartDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select start date and time"
                size="mini"
                style="width: 200px;margin-right:20px;"
              />
              <el-date-picker
                v-model="listQuery.EndDate"
                type="datetime"
                format="yyyy-MM-dd HH:mm:ss"
                placeholder="Select end date and time"
                size="mini"
                style="width: 200px;margin-right:20px;"
              />
            </el-col>
            <el-col :span="6">
              <div class="grid-content">
                <div class="filter-container">
                  <el-button
                    class="filter-item"
                    type="success"
                    icon="el-icon-search"
                    @click="handlerSearch"
                    size="mini"
                    >Search</el-button
                  >
                  <el-button
                    class="filter-item"
                    style="margin-left: 10px;"
                    type="warning"
                    icon="el-icon-circle-plus-outline"
                    size="mini"
                    @click="handleCreate"
                    :disabled="!canOperater"
                    >Add</el-button
                  >
                </div>
              </div>
            </el-col>
          </el-row>
        </sticky>
      </el-header>
      <el-container>
        <el-aside
          width="230px"
          style="background-color: rgb(255, 255, 255); padding:0; margin:0;"
        >
          <el-container>
            <el-main>
              <el-input
                placeholder="Bay Filter"
                v-model="filterText"
                clearable
                style="margin-top:10px;"
                size="mini"
              ></el-input>
              <div style="height:400px;">
                <el-tree
                  class="filter-tree"
                  :data="nodes"
                  :node-key="id"
                  :props="defaultProps"
                  @node-click="handleNodeClick"
                  :default-expand-all="false"
                  :filter-node-method="filterNode"
                  :highlight-current="true"
                  ref="tree"
                ></el-tree>
              </div>
            </el-main>
          </el-container>
        </el-aside>
        <el-container>
          <el-main style="padding:0; margin:0;">
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
              :row-style="{ height: '15px' }"
              height="400px"
            >
              <el-table-column
                label="ID"
                prop="id"
                sortable="custom"
                align="center"
                width="40"
                :class-name="getSortClass('id')"
                fixed
              >
                <template slot-scope="{ row }">
                  <span>{{ row.Id }}</span>
                </template>
              </el-table-column>

              <!-- <el-table-column label="Guid" width="160px" :v-show="false">
                <template slot-scope="{ row }">
                  <span>{{ row.SmedGuid }}</span>
                </template>
              </el-table-column> -->

              <el-table-column
                label="Workcell"
                min-width="140px"
                align="center"
              >
                <template slot-scope="scope">
                  <span>{{ GetWCName(scope.row.WorkcellId) }}</span>
                </template>
              </el-table-column>
              <el-table-column label="Bay" min-width="140px" align="center">
                <template slot-scope="scope">
                  <span>{{ GetLineName(scope.row.LineId) }}</span>
                </template>
              </el-table-column>
              <el-table-column label="Model" width="160px">
                <template slot-scope="{ row }">
                  <span>{{ row.ModelCode }}</span>
                </template>
              </el-table-column>
              <el-table-column label="ChangeTime" width="160px" align="center">
                <template slot-scope="scope">
                  <span>{{ scope.row.ChangeTime }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="ChangeBy"
                min-width="140px"
                align="center"
              >
                <template slot-scope="scope">
                  <span>{{ scope.row.ChangeBy }}</span>
                </template>
              </el-table-column>
              <el-table-column
                label="Actions"
                align="center"
                width="150"
                fixed="right"
                class-name="small-padding fixed-width"
                v-if="canOperater"
              >
                <template slot-scope="{ row }">
                  <el-button type="text" size="mini" @click="handleUpdate(row)"
                    >View</el-button
                  >
                  <!-- <el-button
                    v-if="row.status != 'deleted'"
                    size="mini"
                    type="text"
                    @click="handleDelete(row)"
                    >Delete</el-button
                  > -->
                </template>
              </el-table-column>
            </el-table>
            <pagination
              v-show="total > 0"
              :total="total"
              :page.sync="listQuery.page"
              :limit.sync="listQuery.limit"
              @pagination="getPageList"
            />
          </el-main>
        </el-container>
      </el-container>
    </el-container>
    <el-drawer
      :title="textMap[dialogStatus]"
      :size="500"
      :visible.sync="drawer"
      :direction="direction"
      :before-close="handleClose"
    >
      <div style="height:600px">
        <el-form
          ref="dataForm"
          :rules="rules"
          :model="temp"
          label-position="left"
          label-width="120px"
          size="mini"
          style="padding-left:10px; padding-right:20px;"
        >
          <el-form-item
            class="postInfo-container-item"
            label="Workcell"
            prop="WorkcellId"
          >
            <el-select
              v-model="temp.WorkcellId"
              clearable
              class="filter-item"
              placeholder="Please select"
              style="width: 300px"
              :disabled="dialogStatus != 'create'"
            >
              <el-option
                v-for="item in workcellOptions"
                :key="item.Id"
                :label="item.Name"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-form-item
            class="postInfo-container-item"
            label="Bay"
            prop="LineId"
          >
            <el-select
              v-model="temp.LineId"
              placeholder="Line"
              clearable
              class="filter-item"
              style="width: 300px"
              :remote-method="getLineOptionsByQuery"
              filterable
              default-first-option
              remote
              :disabled="dialogStatus != 'create'"
            >
              <el-option
                v-for="item in lineOptions"
                :key="item.Id"
                :label="item.Name"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="Model-Code" prop="ModelCode">
            <el-input
              v-model="temp.ModelCode"
              style="width: 300px"
              :disabled="dialogStatus != 'create'"
            />
          </el-form-item>
          <el-form-item label="SmedGuid" prop="SmedGuid">
            <el-input
              v-model="temp.SmedGuid"
              style="width: 300px"
              disabled="true"
            />
          </el-form-item>
          <el-form-item label="ChangeTime" prop="ChangeTime">
            <el-date-picker
              v-model="temp.ChangeTime"
              type="datetime"
              format="yyyy-MM-dd HH:mm:ss"
              placeholder="Select end date and time"
              size="mini"
              style="width: 200px;margin-right:20px;"
              disabled="true"
            />
          </el-form-item>
          <el-form-item label="ChangeBy" prop="ChangeBy">
            <el-input
              :disabled="dialogStatus != 'create'"
              v-model="temp.ChangeBy"
              style="width: 200px"
            />
          </el-form-item>

          <!-- <el-form-item label="Remark" prop="Remark" :v-show="false">
          <el-input  :disabled = "dialogStatus != 'create'" v-model="temp.Remark" style="width: 300px" />
        </el-form-item> -->

          <el-table
            :key="detailTableKey"
            :data="detailList"
            fit
            border
            stripe
            highlight-current-row
            size="mini"
            :row-style="{ height: '15px' }"
            height="200"
            width="300px"
            v-show="dialogStatus === 'update'"
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
              <template slot-scope="{ row }">
                <span>{{ row.Id }}</span>
              </template>
            </el-table-column>
            <el-table-column
              label="Station"
              min-width="100px"
              align="center"
              fixed
            >
              <template slot-scope="scope">
                <span>{{ scope.row.Station }}</span>
              </template>
            </el-table-column>
            <el-table-column label="MacAddr" min-width="140px" align="center">
              <template slot-scope="scope">
                <span>{{ scope.row.MacAddr }}</span>
              </template>
            </el-table-column>
            <el-table-column label="Status" width="80px">
              <template slot-scope="scope">
                <span>{{ scope.row.Status }}</span>
              </template>
            </el-table-column>
            <el-table-column label="FinishBy" width="160px" align="center">
              <template slot-scope="scope">
                <span>{{ scope.row.FinishBy }}</span>
              </template>
            </el-table-column>
            <el-table-column
              label="CreateTime"
              min-width="140px"
              align="center"
            >
              <template slot-scope="scope">
                <span>{{ scope.row.CreateTime }}</span>
              </template>
            </el-table-column>
            <el-table-column
              label="FinishTime"
              min-width="140px"
              align="center"
            >
              <template slot-scope="scope">
                <span>{{ scope.row.FinishTime }}</span>
              </template>
            </el-table-column>
            <el-table-column
              label="ChangeTimeUse"
              min-width="140px"
              align="center"
            >
              <template slot-scope="scope">
                <span>{{ scope.row.ChangeTimeUse }}</span>
              </template>
            </el-table-column>
          </el-table>

          <el-form-item label-width="0px">
            <el-button @click="cancelForm">Cancel</el-button>
            <el-button
              :disabled="dialogStatus != 'create'"
              type="primary"
              @click="dialogStatus === 'create' ? createData() : updateData()"
              >Confirm</el-button
            >
          </el-form-item>
        </el-form>
      </div>
    </el-drawer>
  </div>
</template>

<script>
/* eslint-disable */
import waves from "@/directive/waves"; // waves directive
import { parseTime } from "@/utils";
import Sticky from "@/components/Sticky"; // 粘性header组件
import store from "@/store";
import tree from "vue-giant-tree";
import { TheMask } from "vue-the-mask";
import Pagination from "@/components/Pagination";
import {
  getWorkcellOptions,
  getEquipmentTypeOptionsByQuery
} from "@/api/downtime";

import { getListsByQuery, getFeedbackByGUID, Create, Update } from "@/api/Smed";

import { getLineTrees, getLineOptionsByQuery } from "@/api/lines";

export default {
  name: "Smed",
  components: { Sticky, tree, Pagination, TheMask },
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
      detailTableKey: 0,
      detailList: undefined,
      total: 0,
      listLoading: false,
      showDetails: false,
      lineOptions: undefined,
      canOperater:
        store.getters.roles.indexOf("admin") >= 0 ||
        store.getters.roles.indexOf("editor") >= 0,
      listQuery: {
        id: undefined,
        type: undefined,
        modelCode: undefined,
        workcellId: undefined,
        lineId: undefined,
        StartDate: undefined,
        EndDate: undefined,
        MacAddr: undefined,
        page: 1,
        limit: 10,
        sort: "+id"
      },
      dialog: false,
      loading: false,
      formLabelWidth: "80px",
      timer: null,
      siteOptions: undefined,
      sectorOptions: undefined,
      workcellOptions: undefined,
      equipmentTypeOptions: undefined,
      sortOptions: [
        { label: "ID Ascending", key: "+id" },
        { label: "ID Descending", key: "-id" }
      ],
      tempNode: {
        WorkcellId: undefined,
        LineId: undefined
      },
      temp: {
        Id: undefined,
        SmedGuid: undefined,
        ModelCode: undefined,
        WorkcellId: undefined,
        LineId: undefined,
        Remark: undefined,
        ChangeTime: undefined,
        ChangeBy: store.getters.name
      },
      dialogStatus: "",
      textMap: {
        update: "Edit",
        create: "Create"
      },
      rules: {
        WorkcellId: [{ validator: validateRequire, trigger: "change" }],
        LineId: [{ validator: validateRequire, trigger: "change" }],
        ModelCode: [{ validator: validateRequire, trigger: "blur" }]
      },
      filterText: "",
      nodes: [],
      defaultProps: {
        children: "children",
        label: "name"
      },
      downloadLoading: false,
      drawer: false,
      direction: "rtl"
    };
  },
  created() {
    this.getWorkcellOptions();
    this.getLineOptionsByQuery();
    this.getLineTrees();
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val);
    }
  },
  methods: {
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
    handleClose(done) {
      if (this.loading) {
        return;
      }
      this.drawer = false;
    },
    cancelForm() {
      this.loading = false;
      this.drawer = false;
    },
    handleNodeClick(data) {
      if (data.type == "workcell") {
        this.tempNode.WorkcellId = parseInt(data.id);
        this.listQuery.workcellId = parseInt(data.id);
      } else {
        this.tempNode.LineId = parseInt(data.id);
        this.listQuery.lineId = parseInt(data.id);
      }

      this.getListsByQuery(this.listQuery);
    },
    handlerSearch() {
      this.getListsByQuery(this.listQuery);
    },
    filterNode(value, data) {
      if (!value) return true;
      return data.name.indexOf(value) !== -1;
    },
    GetWCName(val) {
      if (val) {
        var targetObs = this.workcellOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetEquipmentTypeName(val) {
      if (val) {
        var targetObs = this.equipmentTypeOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
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
    getListsByQuery(query) {
      this.listLoading = true;
      getListsByQuery(query).then(response => {
        this.list = response.data;
        this.total = response.counts;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getFeedbacks(row) {
      this.listLoading = true;
      getFeedbackByGUID(row.SmedGuid).then(response => {
        this.detailList = response.data;
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getPageList(page) {
      this.listQuery.page = page.page;
      this.listQuery.limit = page.limit;
      this.getListsByQuery(this.listQuery);
    },
    SearchByCode(query) {
      this.listLoading = true;
      SearchByCode(query).then(response => {
        this.list = response.data;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
    },
    getLineTrees() {
      this.listLoading = true;
      getLineTrees().then(response => {
        this.nodes = response.nodes;
        // Just to simulate the time of the request
        this.getListsByQuery(this.listQuery);
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
      this.getListsByQuery(this.listQuery);
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
        SmedGuid: undefined,
        ModelCode: undefined,
        WorkcellId: undefined,
        LineId: undefined,
        Remark: undefined,
        ChangeTime: undefined,
        ChangeBy: store.getters.name
      };
    },
    handleCreate() {
      this.resetTemp();
      if (
        this.tempNode.WorkcellId != undefined ||
        this.tempNode.LineId != undefined
      ) {
        this.temp.WorkcellId = this.tempNode.WorkcellId;
        this.temp.LineId = this.tempNode.LineId;
      }

      this.dialogStatus = "create";
      this.drawer = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    createData() {
      this.$confirm("确定要提交表单吗？")
        .then(_ => {
          this.loading = true;
          this.$refs["dataForm"].validate(valid => {
            if (valid) {
              this.temp.Id = parseInt(Math.random() * 100) + 1024; // mock a id
              Create(this.temp).then(() => {
                this.getListsByQuery(this.listQuery);
                this.$refs["dataForm"].resetFields();
                this.$notify({
                  title: "Success",
                  message: "Created Successfully",
                  type: "success",
                  duration: 2000
                });
              });
            }
          });

          // this.resetTemp();
          this.loading = false;
          this.drawer = false;
        })
        .catch(_ => {});
    },
    handleUpdate(row) {
      this.dialogStatus = "update";
      this.temp = Object.assign({}, row); // copy obj
      this.getFeedbacks(row);
      this.drawer = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    updateData() {
      this.$confirm("确定要提交表单吗？")
        .then(_ => {
          this.loading = true;
          this.$refs["dataForm"].validate(valid => {
            if (valid) {
              const tempData = Object.assign({}, this.temp);
              // tempData.timestamp = +new Date(tempData.timestamp); // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
              Update(tempData).then(() => {
                this.getListsByQuery(this.listQuery);
                this.loading = false;
                this.drawer = false;
                this.$notify({
                  title: "Success",
                  message: "Update Successfully",
                  type: "success",
                  duration: 2000
                });
              });
            }
          });
        })
        .catch(_ => {});
    },
    handleDelete(row) {
      this.$confirm("确定要删除此项吗？")
        .then(_ => {
          const tempData = Object.assign({}, row);
          Delete(tempData).then(() => {
            this.getListsByQuery(this.listQuery);
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
    handleDownload() {
      this.downloadLoading = true;
      import("@/vendor/Export2Excel").then(excel => {
        const tHeader = ["Id", "Code", "Name", "WorkcellId", "EquipmentTypeId"];
        const filterVal = [
          "Id",
          "Code",
          "Name",
          "WorkcellId",
          "EquipmentTypeId"
        ];
        const data = this.formatJson(filterVal, this.list);
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: "Jabil_IE_Equipments"
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
          } else if (j == "SectorId") {
            return this.GetSectorName(v[j]);
          } else if (j == "EquipmentTypeId") {
            return this.GetEquipmentTypeName(v[j]);
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
    }
  }
};
</script>

<style>
.el-header {
  background-color: #20b6f9;
  /* background: linear-gradient(90deg, rgba(32, 182, 249, 1) 0%, rgba(32, 182, 249, 1) 0%, rgba(33, 120, 241, 1) 100%, rgba(33, 120, 241, 1) 100%); */
  color: #333;
  line-height: 60px;
  padding: 0;
  margin: 0;
}

.el-aside {
  color: #333;
  padding: 0;
  margin: 0;
}

.el-main {
  padding: 0;
  margin: 0;
}

.sub-header {
  height: 60px;
  line-height: 45px;
  position: relative;
  width: 100%;
  text-align: right;
  padding-top: 10px;
  padding-right: 10px;
  transition: 600ms ease position;
  /* background: #b3c0d1; */
  margin-bottom: 10px;
  background: linear-gradient(
    90deg,
    rgba(32, 182, 249, 1) 0%,
    rgba(32, 182, 249, 1) 0%,
    rgba(33, 120, 241, 1) 100%,
    rgba(33, 120, 241, 1) 100%
  );
}
</style>

<template>
  <div class="app-container">
    <el-container style="height: 100%; border: 1px solid #eee">
      <el-aside
        width="0px"
        style="background-color: rgb(255, 255, 255); padding:0; margin:0;"
      >
        <el-container>
          <el-main>
            <div style="height:400px;">
              <!-- <el-table
                :data="nodes"
                style="width: 100%;margin-bottom: 20px;"
                row-key="Id"
                border
                size="mini"
                highlight-current-row="true"
                height="300px"
                @row-click="handleLeftTableClick"
              >
                <el-table-column prop="Code" label="Code" width="100">
                </el-table-column>
                <el-table-column prop="Name" label="Name" width="120">
                </el-table-column>
                <el-table-column
                  prop="Gid"
                  label="Gid"
                  v-if="showRow"
                  sortable
                  width="280"
                >
                </el-table-column>
              </el-table> -->
            </div>
          </el-main>
        </el-container>
      </el-aside>
      <el-main style="padding:0; margin:0;">
        <el-container>
          <el-header>
            <sticky :z-index="10" :class-name="'sub-header'">
              <el-row>
                <el-col :span="4" :align="left">
                  <div class="grid-content">
                    <el-select
                      v-model="listQuery.workcellId"
                      clearable
                      class="filter-item"
                      placeholder="select workcell"
                      size="mini"
                    >
                      <el-option
                        v-for="item in workcellOptions"
                        :key="item.Id"
                        :label="item.Name"
                        :value="item.Id"
                      />
                    </el-select>
                  </div>
                </el-col>

                <el-col :span="4" :align="left">
                  <div class="grid-content">
                    <treeselect
                      v-model="listQuery.SId"
                      :options="treeoptions"
                      :disable-branch-nodes="true"
                      :show-count="true"
                      placeholder="choose the sapce?"
                      height="30px"
                    />
                  </div>
                </el-col>
                <el-col :span="16" :align="right">
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
                      <!-- <el-button
                        v-waves
                        :loading="downloadLoading"
                        class="filter-item"
                        type="primary"
                        icon="el-icon-download"
                        @click="handleDownload"
                        size="mini"
                        >Export</el-button
                      > -->
                    </div>
                  </div>
                </el-col>
              </el-row>
            </sticky>
          </el-header>
          <el-main>
            <div>
              <el-table
                :key="tableKey"
                v-loading="listLoading"
                :data="list"
                ref="multipleTable"
                highlight-current-row
                size="mini"
                @sort-change="sortChange"
                :row-style="detailTableStyle"
                @selection-change="handleSelectionChange"
                height="380px"
              >
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
                <el-table-column label="Workcell" width="160px">
                  <template slot-scope="{ row }">
                    <span>{{ GetWCName(row.WorkcellId) }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Space Code" width="160px" align="left">
                  <template slot-scope="{ row }">
                    <span>{{ GetSpaceName(row.Sid) }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Space Type" width="160px" align="left">
                  <template slot-scope="{ row }">
                    <span>{{ GetTypeName(row.Tid) }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Date" width="160px" align="left">
                  <template slot-scope="scope">
                    <span>{{ scope.row.CreateDate }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Allocation" width="160px" align="left">
                  <template slot-scope="scope">
                    <span>{{ scope.row.Allocation }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Surplus" width="160px" align="left">
                  <template slot-scope="scope">
                    <span>{{ scope.row.Surplus }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Avaiable" width="160px" align="left">
                  <template slot-scope="scope">
                    <span>{{ scope.row.Avaiable }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Total" width="160px" align="left">
                  <template slot-scope="scope">
                    <span>{{ scope.row.Total }}</span>
                  </template>
                </el-table-column>
                <el-table-column label="Desc" width="260px" align="left">
                  <template slot-scope="scope">
                    <span>{{ scope.row.Desc }}</span>
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
            </div>
          </el-main>
        </el-container>
      </el-main>
    </el-container>
    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-position="left"
        label-width="120px"
        style="width: 400px; margin-left:50px;"
        size="mini"
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
          label="Space Code"
          prop="SId"
        >
          <treeselect
            v-model="temp.SId"
            :options="treeoptions"
            :disable-branch-nodes="true"
            :show-count="true"
            placeholder="choose the sapce?"
            @select="treeSelChange"
          />
        </el-form-item>

        <el-form-item
          class="postInfo-container-item"
          label="Space Type"
          prop="SId"
        >
          <el-select
            v-model="temp.TId"
            clearable
            class="filter-item"
            placeholder="Please select"
          >
            <el-option
              v-for="item in selection"
              :key="item.Gid"
              :label="item.Name"
              :value="item.Gid"
            />
          </el-select>
        </el-form-item>

        <el-form-item
          class="postInfo-container-item"
          label="Create Date"
          prop="BayId"
        >
          <el-date-picker
            v-model="temp.CreateDate"
            placeholder=""
          ></el-date-picker>
        </el-form-item>
        <el-form-item label="Total Space" prop="Total">
          <el-input v-model="temp.Total" disabled="true" />
        </el-form-item>
        <el-form-item label="Avaiable" prop="Avaiable">
          <el-input v-model="temp.Avaiable" disabled="true" />
        </el-form-item>
        <el-form-item label="Allocation" prop="Allocation">
          <el-input v-model="Allocation" />
        </el-form-item>
        <el-form-item label="Surplus" prop="Surplus">
          <el-input v-model="temp.Surplus" disabled="true" />
        </el-form-item>
        <el-form-item label="Remark" prop="Remark">
          <el-input type="textarea" v-model="temp.Desc" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Cancel</el-button>
        <el-button
          type="primary"
          @click="dialogStatus === 'create' ? createData() : updateData()"
          >Confirm</el-button
        >
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
import tree from "vue-giant-tree";
import VueIp from "vue-ip";
import { TheMask } from "vue-the-mask";
import Pagination from "@/components/Pagination";
import Treeselect from "@riophae/vue-treeselect";
import "@riophae/vue-treeselect/dist/vue-treeselect.css";
import {
  getWorkcellOptions,
  getEquipmentTypeOptionsByQuery
} from "@/api/downtime";

import {
  Create,
  update,
  Delete,
  getActivityListsByQuery,
  getSpace,
  mapSpace,
  getSpaceSelTree,
  getSpaceTypeOptions,
  getSpaceOptions,
  getTypeOptions
} from "@/api/space";

export default {
  name: "Allocation",
  components: { Sticky, tree, VueIp, TheMask, Pagination, Treeselect },
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
      selTrees: undefined,
      total: 0,
      listLoading: false,
      showDetails: false,
      showRow: false,
      spaceListsOptions: undefined,
      AllSpaceOptions: [
        {
          Id: 1,
          Code: "M",
          Name: "MAIN PLANT",
          Gid: "2e5b7764-5a1a-40a6-a77c-8a5c807cdc47",
          Pid: "af15747d-fcf7-4082-a4de-b240aa61350b",
          Size: 120.0,
          Level: 1,
          children: []
        },
        {
          Id: 2,
          Code: "A",
          Name: "BUILD-A",
          Gid: "42761c38-9cc9-49a9-9fd2-8b455444f46f",
          Pid: "af15747d-fcf7-4082-a4de-b240aa61350b",
          Size: 1000.0,
          Level: 1,
          children: []
        },
        {
          Id: 3,
          Code: "B",
          Name: "BUILD-B",
          Gid: "552bc4e1-f539-42f0-97b9-f6836865b7fd",
          Pid: "af15747d-fcf7-4082-a4de-b240aa61350b",
          Size: 1000.0,
          Level: 1,
          children: []
        },
        {
          Id: 4,
          Code: "C",
          Name: "BUILD-C",
          Gid: "ae62c12b-070f-4c8a-8966-b417e4d97768",
          Pid: "af15747d-fcf7-4082-a4de-b240aa61350b",
          Size: 1000.0,
          Level: 1,
          children: []
        },
        {
          Id: 5,
          Code: "A1",
          Name: "BUILD-A1",
          Gid: "96931401-4117-4d84-8682-699c54a1f6a1",
          Pid: "42761c38-9cc9-49a9-9fd2-8b455444f46f",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 6,
          Code: "A2",
          Name: "BUILD-A2",
          Gid: "d3696204-2a12-430a-8eda-b0e73b82df9b",
          Pid: "42761c38-9cc9-49a9-9fd2-8b455444f46f",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 7,
          Code: "A3",
          Name: "BUILD-A3",
          Gid: "2259df8b-aaef-478c-b7b3-e435444dfdb0",
          Pid: "42761c38-9cc9-49a9-9fd2-8b455444f46f",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 8,
          Code: "A4",
          Name: "BUILD-A4",
          Gid: "e706fdfb-29b2-41a8-a068-b33919d2151d",
          Pid: "42761c38-9cc9-49a9-9fd2-8b455444f46f",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 9,
          Code: "B1",
          Name: "BUILD-B1",
          Gid: "26d27ee3-77be-4c0f-bc36-0ef1044fd5df",
          Pid: "552bc4e1-f539-42f0-97b9-f6836865b7fd",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 10,
          Code: "B2",
          Name: "BUILD-B2",
          Gid: "471eb224-3a94-4f15-a2ae-f0221043b56f",
          Pid: "552bc4e1-f539-42f0-97b9-f6836865b7fd",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 11,
          Code: "B3",
          Name: "BUILD-B3",
          Gid: "b75a532e-a6e0-41a5-8de2-96f39aa867c1",
          Pid: "552bc4e1-f539-42f0-97b9-f6836865b7fd",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 12,
          Code: "B4",
          Name: "BUILD-B4",
          Gid: "dfd97f71-e651-49ed-846f-e79dcc32a3cb",
          Pid: "552bc4e1-f539-42f0-97b9-f6836865b7fd",
          Size: 250.0,
          Level: 2,
          children: []
        },
        {
          Id: 13,
          Code: "C1",
          Name: "BUILD-C1F",
          Gid: "b828e0e2-ac82-49b5-90e5-0b59fcf9f613",
          Pid: "ae62c12b-070f-4c8a-8966-b417e4d97768",
          Size: 1000.0,
          Level: 2,
          children: []
        },
        {
          Id: 14,
          Code: "C2",
          Name: "BUILD-C1F",
          Gid: "b2c95f28-241c-4f83-af78-59a67102c65e",
          Pid: "ae62c12b-070f-4c8a-8966-b417e4d97768",
          Size: 1000.0,
          Level: 2,
          children: []
        },
        {
          Id: 15,
          Code: "M1",
          Name: "M1F",
          Gid: "13eb6575-7e25-431b-a1f2-11837e97b51a",
          Pid: "2e5b7764-5a1a-40a6-a77c-8a5c807cdc47",
          Size: 120.0,
          Level: 2,
          children: []
        },
        {
          Id: 16,
          Code: "M2",
          Name: "M2F",
          Gid: "11926cb5-6db8-425f-9584-3622f97950f9",
          Pid: "2e5b7764-5a1a-40a6-a77c-8a5c807cdc47",
          Size: 120.0,
          Level: 2,
          children: []
        },
        {
          Id: 17,
          Code: "M3",
          Name: "M3F",
          Gid: "79669943-4413-4cbc-ad20-e7992eed4d2f",
          Pid: "2e5b7764-5a1a-40a6-a77c-8a5c807cdc47",
          Size: 120.0,
          Level: 2,
          children: []
        },
        {
          Id: 18,
          Code: "M4",
          Name: "M4F",
          Gid: "118ea6c7-346d-46b5-bcf7-249f0c6c2246",
          Pid: "2e5b7764-5a1a-40a6-a77c-8a5c807cdc47",
          Size: 120.0,
          Level: 2,
          children: []
        },
         {
          Id: 19,
          Code: "P5",
          Name: "Phase5",
          Gid: "A39F9F8B-7AEE-4D4A-BBC4-C1291243A636",
          Pid: "AF15747D-FCF7-4082-A4DE-B240AA61350B",
          Size: 120.0,
          Level: 1,
          children: []
        },
         {
          Id: 20,
          Code: "P51",
          Name: "Phase5 P1",
          Gid: "E07FB8A4-7E1F-4185-87A8-E526C6CF1B94",
          Pid: "A39F9F8B-7AEE-4D4A-BBC4-C1291243A636",
          Size: 120.0,
          Level: 2,
          children: []
        }
        ,
         {
          Id: 21,
          Code: "P52",
          Name: "Phase5 P2",
          Gid: "C99AFDCB-4B12-4AE7-9D4D-979B8021DA1A",
          Pid: "A39F9F8B-7AEE-4D4A-BBC4-C1291243A636",
          Size: 120.0,
          Level: 2,
          children: []
        }
      ],
      AllTypeOptions: [
        {
          Id: 1,
          Code: "AREA-001",
          Name: "ESD MFG Area",
          Gid: "8388c22e-191b-4fa5-b3f5-d39a29c8e869",
          Sid: "8388c22e-191b-4fa5-b3f5-d39a29c8e869",
          Checked: null
        },
        {
          Id: 2,
          Code: "AREA-002",
          Name: "NON ESD MFG Area",
          Gid: "a2c39c38-fa2a-4348-b7c8-83eb0ce6a5e1",
          Sid: "a2c39c38-fa2a-4348-b7c8-83eb0ce6a5e1",
          Checked: null
        },
        {
          Id: 3,
          Code: "AREA-003",
          Name: "WAREHOURSE",
          Gid: "22488a51-a519-4246-adbf-badc885d41ad",
          Sid: "22488a51-a519-4246-adbf-badc885d41ad",
          Checked: null
        },
        {
          Id: 4,
          Code: "AREA-004",
          Name: "SHIPPING",
          Gid: "3565db87-ed0c-4ef6-83a8-a23b4956048b",
          Sid: "3565db87-ed0c-4ef6-83a8-a23b4956048b",
          Checked: null
        },
        {
          Id: 5,
          Code: "AREA-005",
          Name: "STOCK ROOM",
          Gid: "6d7643fe-7339-4674-845d-e61853072990",
          Sid: "6d7643fe-7339-4674-845d-e61853072990",
          Checked: null
        },
        {
          Id: 6,
          Code: "AREA-006",
          Name: "RECEIVING",
          Gid: "e311c580-2d37-45ca-a205-1d35e85a3b80",
          Sid: "e311c580-2d37-45ca-a205-1d35e85a3b80",
          Checked: null
        },
        {
          Id: 7,
          Code: "AREA-007",
          Name: "MAIN OFFICES",
          Gid: "dd1ebfce-7bc2-434f-8ef4-5eebc6237a87",
          Sid: "dd1ebfce-7bc2-434f-8ef4-5eebc6237a87",
          Checked: null
        },
        {
          Id: 8,
          Code: "AREA-008",
          Name: "SG&A OFFICE",
          Gid: "9df05e57-5b5b-449b-84c1-e71ff07d8d4c",
          Sid: "9df05e57-5b5b-449b-84c1-e71ff07d8d4c",
          Checked: null
        },
        {
          Id: 9,
          Code: "AREA-009",
          Name: "LOBBY",
          Gid: "1e02964c-753a-4b75-8305-1509969e2764",
          Sid: "1e02964c-753a-4b75-8305-1509969e2764",
          Checked: null
        },
        {
          Id: 10,
          Code: "AREA-010",
          Name: "CONFERENCE ROOMS",
          Gid: "71abe75c-8485-48bc-869e-fc0e11d1bba5",
          Sid: "71abe75c-8485-48bc-869e-fc0e11d1bba5",
          Checked: null
        },
        {
          Id: 11,
          Code: "AREA-011",
          Name: "CAFETERIA",
          Gid: "918e23b7-0547-440f-ad54-9b901b497961",
          Sid: "918e23b7-0547-440f-ad54-9b901b497961",
          Checked: null
        },
        {
          Id: 12,
          Code: "AREA-012",
          Name: "RESTROOMS",
          Gid: "5e91d1b7-a889-45b2-8133-ee4aa8917b22",
          Sid: "5e91d1b7-a889-45b2-8133-ee4aa8917b22",
          Checked: null
        },
        {
          Id: 13,
          Code: "AREA-013",
          Name: "UTILITY ROOMS",
          Gid: "c8218bcf-e06e-4722-b3fb-7ea8c6d656b2",
          Sid: "c8218bcf-e06e-4722-b3fb-7ea8c6d656b2",
          Checked: null
        },
        {
          Id: 14,
          Code: "AREA-014",
          Name: "GENERAL SHARED SPACES",
          Gid: "ceb95996-6a47-403a-8ddc-fa9398de3fa9",
          Sid: "ceb95996-6a47-403a-8ddc-fa9398de3fa9",
          Checked: null
        },
        {
          Id: 15,
          Code: "AREA-015",
          Name: "UNUSEABLE SPACE",
          Gid: "1619be2a-565d-46cd-9877-96665fbc410c",
          Sid: "1619be2a-565d-46cd-9877-96665fbc410c",
          Checked: null
        }
      ],
      multipleSelection: [],
      MapLists: {
        Sid: undefined,
        SelLists: []
      },
      listQuery: {
        workcellId: undefined,
        Sid: undefined,
        Tid: undefined,
        StartDate: undefined,
        EndDate: undefined,
        id: undefined,
        page: 1,
        limit: 10,
        sort: "+id"
      },
      dialog: false,
      loading: false,
      formLabelWidth: "80px",
      timer: null,
      canOperater:
        store.getters.roles.indexOf("admin") >= 0 ||
        store.getters.roles.indexOf("editor") >= 0,
      workcellOptions: undefined,
      equipmentTypeOptions: undefined,
      sortOptions: [
        { label: "ID Ascending", key: "+id" },
        { label: "ID Descending", key: "-id" }
      ],
      statusOptions: ["published", "draft", "deleted"],
      tempNode: {
        WorkcellId: undefined,
        LineId: undefined
      },
      temp: {
        Id: undefined,
        WorkcellId: undefined,
        SId: undefined,
        Tid: undefined,
        CreateDate: undefined,
        Total: undefined,
        Avaiable: undefined,
        Allocation: undefined,
        Surplus: undefined,
        Desc: undefined
      },
      dialogFormVisible: false,
      dialogStatus: "",
      textMap: {
        update: "Edit",
        create: "Create"
      },
      rules: {
        WorkcellId: [
          { required: true, validator: validateRequire, trigger: "change" }
        ],
        SId: [
          { required: true, validator: validateRequire, trigger: "change" }
        ],
        TId: [{ required: true, validator: validateRequire, trigger: "blur" }],
        CreateDate: [
          { required: true, validator: validateRequire, trigger: "blur" }
        ],
        Total: [
          { required: true, message: "Total Space不能为空", trigger: "blur" },
          { message: "请输入数字值", pattern: /^[0-9]*[1-9][0-9]*$/ }
        ],
        Avaiable: [
          { required: true, message: "Avaiable不能为空", trigger: "blur" },
          { message: "请输入数字值", pattern: /^[0-9]*[1-9][0-9]*$/ }
        ],
        Allocation: [
          { required: true, message: "Allocation不能为空", trigger: "blur" },
          { message: "请输入数字值", pattern: /^[0-9]*[1-9][0-9]*$/ }
        ],
        Surplus: [
          { required: true, message: "Surplus不能为空", trigger: "blur" },
          { message: "请输入数字值", pattern: /^[0-9]*[1-9][0-9]*$/ }
        ]
      },
      filterText: "",
      Allocation: 0,
      nodes: [],
      defaultProps: {
        children: "children",
        label: "name"
      },
      downloadLoading: false,
      ip: "000000000000",
      treevalue: null,
      spacetypeoptions: [],
      selection: [],
      // define options
      treeoptions: [
        {
          id: "42761c38-9cc9-49a9-9fd2-8b455444f46f",
          label: "BUILD-A",
          children: [
            {
              id: "96931401-4117-4d84-8682-699c54a1f6a1",
              label: "BUILD-A1"
            },
            {
              id: "d3696204-2a12-430a-8eda-b0e73b82df9b",
              label: "BUILD-A2"
            },
            {
              id: "2259df8b-aaef-478c-b7b3-e435444dfdb0",
              label: "BUILD-A3"
            },
            {
              id: "e706fdfb-29b2-41a8-a068-b33919d2151d",
              label: "BUILD-A4"
            }
          ]
        },
        {
          id: "552bc4e1-f539-42f0-97b9-f6836865b7fd",
          label: "BUILD-B",
          children: [
            {
              id: "26d27ee3-77be-4c0f-bc36-0ef1044fd5df",
              label: "BUILD-B1"
            },
            {
              id: "471eb224-3a94-4f15-a2ae-f0221043b56f",
              label: "BUILD-B2"
            },
            {
              id: "b75a532e-a6e0-41a5-8de2-96f39aa867c1",
              label: "BUILD-B3"
            },
            {
              id: "dfd97f71-e651-49ed-846f-e79dcc32a3cb",
              label: "BUILD-B4"
            }
          ]
        },
        {
          id: "ae62c12b-070f-4c8a-8966-b417e4d97768",
          label: "BUILD-C",
          children: [
            {
              id: "b828e0e2-ac82-49b5-90e5-0b59fcf9f613",
              label: "BUILD-C1F"
            },
            {
              id: "b2c95f28-241c-4f83-af78-59a67102c65e",
              label: "BUILD-C1F"
            }
          ]
        },
        {
          id: "2e5b7764-5a1a-40a6-a77c-8a5c807cdc47",
          label: "MAIN PLANT",
          children: [
            {
              id: "13eb6575-7e25-431b-a1f2-11837e97b51a",
              label: "M1F"
            },
            {
              id: "11926cb5-6db8-425f-9584-3622f97950f9",
              label: "M2F"
            },
            {
              id: "79669943-4413-4cbc-ad20-e7992eed4d2f",
              label: "M3F"
            },
            {
              id: "118ea6c7-346d-46b5-bcf7-249f0c6c2246",
              label: "M4F"
            }
          ]
        }
        ,
        {
          id: "A39F9F8B-7AEE-4D4A-BBC4-C1291243A636",
          label: "P5 ",
          children: [
            {
              id: "E07FB8A4-7E1F-4185-87A8-E526C6CF1B94",
              label: "P5-1"
            },
            {
              id: "C99AFDCB-4B12-4AE7-9D4D-979B8021DA1A",
              label: "P5-2"
            }
          ]
        }
      ]
    };
  },
  created() {
    //this.getSpaceSelTree();
    this.getWorkcellOptions();
    //this.getSpaceOptions();
    //this.getTypeOptions();
    this.getSpaceTypeOptions();

    let that = this;
    that.$nextTick(function() {});
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val);
    },
    Allocation: function(newval, oldval) {
      this.temp.Allocation = newval;
      this.temp.Surplus =
        Number.parseFloat(this.temp.Avaiable) -
        Number.parseFloat(this.temp.Allocation);
    }
  },
  methods: {
    loadOptions({ action, searchQuery, callback }) {
      if (action === ASYNC_SEARCH) {
        simulateAsyncOperation(() => {
          const options = [1, 2, 3, 4, 5].map(i => ({
            id: `${searchQuery}-${i}`,
            label: `${searchQuery}-${i}`
          }));
          callback(null, options);
        });
      }
    },
    handleSelectionChange(val) {
      debugger;
      this.multipleSelection = val;
      this.MapLists.SelLists = val;
    },
    toggleSelection(rows) {
      rows.forEach(row => {
        if (row.Checked === "Y") {
          this.$refs.multipleTable.toggleRowSelection(row, true);
        }
      });
    },
    ipChange(ip, port, valid) {
      this.temp.StationIp = ip;
    },
    handleClose(done) {
      if (this.loading) {
        return;
      }
      this.$confirm("确定要提交表单吗？")
        .then(_ => {
          this.loading = true;
          this.timer = setTimeout(() => {
            done();
            // 动画关闭需要一定的时间
            setTimeout(() => {
              this.loading = false;
            }, 400);
          }, 2000);
        })
        .catch(_ => {});
    },
    cancelForm() {
      debugger;
      this.loading = false;
      this.dialog = false;
      clearTimeout(this.timer);
    },
    handleNodeClick(data) {
      this.listQuery.id = data.id;
      this.listQuery.type = data.type;
      if (data.type == "workcell") {
        this.tempNode.WorkcellId = parseInt(data.id);
        this.listQuery.workcellId = parseInt(data.id);
      } else {
        this.tempNode.LineId = parseInt(data.id);
        this.listQuery.lineId = parseInt(data.id);
      }

      this.getActivityListsByQuery(this.listQuery);
    },

    handleLeftTableClick(row, event, column) {
      this.listQuery.Sid = row.Gid;
      this.MapLists.Sid = row.Gid;

      this.getActivityListsByQuery(this.listQuery);
    },

    getPageList(page) {
      this.listQuery.page = page.page;
      this.listQuery.limit = page.limit;
      this.getActivityListsByQuery(this.listQuery);
    },

    handlerSearch() {
      this.getActivityListsByQuery(this.listQuery);
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
    GetSpaceName(val) {
      if (val) {
        var targetObs = this.AllSpaceOptions.filter(p => {
          return p.Gid === val;
        });

        return targetObs[0].Code;
      }
    },
    GetTypeName(val) {
      debugger;
      if (val) {
        var targetObs = this.AllTypeOptions.filter(p => {
          return p.Gid === val;
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
    getActivityListsByQuery(query) {
      this.listLoading = true;
      getActivityListsByQuery(query).then(response => {
        this.list = response.data;
        this.total = response.total;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 4000);
      });
    },
    getSpace() {
      this.listLoading = true;
      getSpace().then(response => {
        this.nodes = response.nodes;
        this.getActivityListsByQuery(this.listQuery);
        //    this.getSpaceSelTree();
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 2000);
      });
    },
    getSpaceSelTree() {
      getSpaceSelTree().then(response => {
        this.treeoptions = response.trees;
        this.getSpaceTypeOptions();
      });s
    },
    getSpaceTypeOptions() {
      getSpaceTypeOptions().then(response => {
        this.spacetypeoptions = response.data;
        this.getActivityListsByQuery(this.listQuery);
      });
    },
    getSpaceOptions() {
      getSpaceOptions().then(response => {
        this.AllSpaceOptions = response.nodes;
      });
    },
    getTypeOptions() {
      getTypeOptions().then(response => {
        this.AllTypeOptions = response.data;
      });
    },
    treeSelChange(node, instanceId) {
      // debugger;
      // for (var i = 0; i < this.spacetypeoptions.length; i++) {
      //   if (this.spacetypeoptions[i].Sid === this.temp.SId) {
      //     this.temp.Total = this.spacetypeoptions[i].Size;
      //     this.temp.Avaiable = this.spacetypeoptions[i].Size;
      //     this.selection = this.spacetypeoptions[i].SelLists;
      //   }
      // }
      debugger;
      var targetOps = this.spacetypeoptions.filter(p => {
        return p.Sid == node.id;
      });

      this.selection = targetOps[0].SelLists;
      this.temp.Total = targetOps[0].Size;
      this.temp.Avaiable = targetOps[0].Size;
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
      this.getActivityListsByQuery(this.listQuery);
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
        WorkcellId: undefined,
        SId: undefined,
        Tid: undefined,
        CreateDate: undefined,
        Total: undefined,
        Avaiable: undefined,
        Allocation: undefined,
        Surplus: undefined,
        Desc: undefined
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
          Create(this.temp).then(() => {
            this.getActivityListsByQuery(this.listQuery);
            this.dialogFormVisible = false;
            this.resetTemp();
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
      debugger;
      this.temp = Object.assign({}, row); // copy obj
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
          update(tempData).then(() => {
            this.getActivityListsByQuery(this.listQuery);
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
      Delete(tempData).then(() => {
        this.getActivityListsByQuery(this.listQuery);
        this.$notify({
          title: "Success",
          message: "Delete Successfully",
          type: "success",
          duration: 2000
        });
      });
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
    },
    tableRowClassName({ row, rowIndex }) {
      return "warning-row";
    },
    detailTableStyle({ row, rowIndex }) {
      return "detail-row";
    }
  }
};
</script>

<style>
.el-table .warning-row {
  background: rgb(79, 83, 87);
}

.el-table .detail-row {
  background: rgb(79, 83, 87);
  height: 15px;
}

.el-table .success-row {
  background: #f0f9eb;
}

.el-table {
  cursor: pointer;
}
.el-header {
  background-color: #20b6f9;
  color: #333;
  padding: 0;
  margin: 0;
  height: 30px;
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
  padding-left: 10px;
  padding-right: 10px;
  transition: 600ms ease position;
  /* background: #b3c0d1; */
  background: linear-gradient(
    90deg,
    rgba(32, 182, 249, 1) 0%,
    rgba(32, 182, 249, 1) 0%,
    rgba(33, 120, 241, 1) 100%,
    rgba(33, 120, 241, 1) 100%
  );
  margin-bottom: 10px;
}
.vue-treeselect__control {
  margin-top: 10px;
  margin-left: 4px;
  height: 28px !important;
}
.vue-treeselect__input-container {
  height: 26px !important;
}
</style>

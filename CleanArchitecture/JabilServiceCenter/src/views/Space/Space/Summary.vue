<template>
  <div class="app-container">
    <el-container style="height: 100%; border: 1px solid #eee">
      <el-main style="padding:0; margin:0;">
        <el-container>
          <el-header style="text-align: right; font-size: 12px">
            <sticky :z-index="10" :class-name="'sub-header'">
              <el-row>
                <el-col :span="4" :align="left">
                  <div class="grid-content" style="align:left">
                    <el-select
                      v-model="listQuery.WorkcellId"
                      clearable
                      class="filter-item"
                      placeholder="Select workcell"
                      style="width:140px;"
                      size="small"
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
                <el-col :span="9" :align="left">
                  <div class="block" style="align:left">
                    <el-date-picker
                      v-model="value7"
                      type="daterange"
                      align="right"
                      style="width:380px; margin-left:10px;"
                      size="small"
                      unlink-panels
                      range-separator="To"
                      start-placeholder="Start date"
                      end-placeholder="End date"
                      :picker-options="pickerOptions2"
                    >
                    </el-date-picker>
                  </div>
                </el-col>
                <!-- <el-col :span="4" :align="left">
                  <div class="grid-content">
                    <el-select
                      v-model="listQuery.Tid"
                      clearable
                      class="filter-item"
                      placeholder="Space Type"
                      style=" width:140px; margin-left:10px;"
                      size="small"
                    >
                      <el-option
                        v-for="item in AllTypeOptions"
                        :key="item.Id"
                        :label="item.Name"
                        :value="item.Id"
                      />
                    </el-select>
                  </div>
                </el-col> -->
                <el-col :span="11">
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
                    </div>
                  </div>
                </el-col>
              </el-row>
            </sticky>
          </el-header>
          <el-main>
            <div>
              <h2>Space Monthly Summary Report/Group By WCs</h2>
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
                :cell-style="yellowBg"
                :header-cell-style="tableHeaderColor"
              >
                <el-table-column
                  label="W/C"
                  min-width="140px"
                  align="center"
                  fixed
                >
                  <template slot-scope="scope">
                    <span>{{ GetWCName(scope.row.WorkcellId) }}</span>
                  </template>
                </el-table-column>
                <!-- <el-table-column
                  label="SpaceType"
                  min-width="140px"
                  align="center"
                  fixed
                >
                  <template slot-scope="scope">
                    <span>{{ GetSpaceTypeName(scope.row.SpaceType) }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  label="Building"
                  min-width="140px"
                  align="center"
                  fixed
                >
                  <template slot-scope="scope">
                    <span>{{ GetSpaceName(scope.row.SpaceId) }}</span>
                  </template>
                </el-table-column> -->
                <el-table-column
                  v-for="item of tableHeader"
                  :key="item"
                  :prop="item"
                  width="150px"
                  :label="item"
                />
              </el-table>
              <pagination
                v-show="total > 0"
                :total="total"
                :page.sync="listQuery.page"
                :limit.sync="listQuery.limit"
                @pagination="getPageList"
              />
              <sticky :z-index="10" :class-name="'stickyFoot'">
                <div></div>
              </sticky>
               <h2>Space Monthly Summary Report/Group By Space Types</h2>
                <el-table
                :key="tableKey"
                v-loading="listLoading"
                :data="list2"
                border
                fit
                stripe
                highlight-current-row
                size="mini"
                @sort-change="sortChange"
                :row-style="{ height: '15px' }"
                height="400px"
                :cell-style="rowBgSet"
                :header-cell-style="tableHeaderColor"
              >
                <el-table-column
                  label="SpaceType"
                  min-width="140px"
                  align="center"
                  fixed
                >
                  <template slot-scope="scope">
                    <span>{{ GetSpaceTypeName(scope.row.SpaceType) }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  label="Building"
                  min-width="140px"
                  align="center"
                  fixed
                >
                  <template slot-scope="scope">
                    <span>{{ GetSpaceName(scope.row.SpaceId) }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  v-for="item of tableHeader2"
                  :key="item"
                  :prop="item"
                  width="150px"
                  :label="item"
                />
              </el-table>
            </div>
          </el-main>
        </el-container>
      </el-main>
    </el-container>
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
import {
  getWorkcellOptions,
  getEquipmentTypeOptionsByQuery
} from "@/api/downtime";

import {
  getListsByQuery,
  Create,
  Update,
  Delete,
  SearchByCode
} from "@/api/station";

import { getForecastLists, getSummaryForecastListsByWcs } from "@/api/space";

import { getLineTrees, getLineOptionsByQuery } from "@/api/lines";

export default {
  name: "Summary",
  components: { Sticky, tree, VueIp, TheMask, Pagination },
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
      list2: undefined,
      total: 0,
      listLoading: false,
      showDetails: false,
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
      lineOptions: undefined,
      pickerOptions2: {
        shortcuts: [
          {
            text: "Last week",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "Last month",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "Last 3 months",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      value6: "",
      value7: "",
      listQuery: {
        id: undefined,
        WorkcellId: undefined,
        Tid: undefined,
        StartDate: undefined,
        EndDate: undefined,
        page: 1,
        limit: 10,
        sort: "+id"
      },
      dialog: false,
      loading: false,
      form: {
        name: "",
        region: "",
        date1: "",
        date2: "",
        delivery: false,
        type: [],
        resource: "",
        desc: ""
      },
      formLabelWidth: "80px",
      timer: null,
      canOperater:
        store.getters.roles.indexOf("admin") >= 0 ||
        store.getters.roles.indexOf("editor") >= 0,
      siteOptions: undefined,
      sectorOptions: undefined,
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
        Code: undefined,
        Name: undefined,
        WorkcellId: undefined,
        LineId: undefined,
        Remark: undefined,
        StationIp: "127.0.0.1"
      },
      dialogFormVisible: false,
      dialogStatus: "",
      textMap: {
        update: "Edit",
        create: "Create"
      },
      rules: {
        WorkcellId: [{ validator: validateRequire, trigger: "change" }],
        EquipmentTypeId: [{ validator: validateRequire, trigger: "change" }],
        Name: [{ validator: validateRequire, trigger: "blur" }],
        Code: [{ validator: validateRequire, trigger: "blur" }]
      },
      filterText: "",
      nodes: [],
      tableHeader: [],
      tableHeader2: [],
      defaultProps: {
        children: "children",
        label: "name"
      },
      downloadLoading: false,
      ip: "000000000000"
    };
  },
  created() {
    this.getLineTrees();
    this.getWorkcellOptions();
    this.getLineOptionsByQuery();
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val);
    }
  },
  methods: {
    yellowBg({ row, column, rowIndex, columnIndex }) {
      if (row.WorkcellId == "Summary") {
        if (columnIndex === 0) {
          return {
            background: "#13c4f0",
            color: "white"
          };
        } else {
          return {
            color: "#13c4f0"
          };
        }
      }

      if (row.WorkcellId == "Total Available") {
        if (columnIndex === 0) {
          return {
            background: "#13c4f0",
            color: "white"
          };
        } else {
          return {
            background: "#edc209",
            color: "black"
          };
        }
      }

      if (row.WorkcellId == "Gap") {
        if (columnIndex === 0) {
          return {
            background: "#13c4f0",
            color: "white"
          };
        } else {
          debugger;
          if (row[this.tableHeader[columnIndex-1]] > 0) {
            return {
              color: "#13c4f0"
            };
          } else {
            return {
              background: "red",
              color: "black"
            };
          }
        }
      }
      if (columnIndex === 0) {
        return {
          background: "#eceb3c"
        };
      }
    },
    tableHeaderColor({ row, column, rowIndex, columnIndex }) {
      if (rowIndex === 0) {
        return "background-color: #396b92;color: #fff;font-weight: 500;";
      }
    },
    rowBgSet({ row, column, rowIndex, columnIndex }) {
      if (row.SpaceType == "Summary") {
        if (columnIndex === 0 || columnIndex === 1) {
          return {
            background: "#13c4f0",
            color: "white"
          };
        } else {
          return {
            color: "#13c4f0"
          };
        }
      }

      if (row.SpaceType == "Total Available") {
        if (columnIndex === 0 || columnIndex === 1) {
          return {
            background: "#13c4f0",
            color: "white"
          };
        } else {
          return {
            background: "#edc209",
            color: "black"
          };
        }
      }

      if (row.SpaceType == "Gap") {
        if (columnIndex === 0 || columnIndex === 1) {
          return {
            background: "#13c4f0",
            color: "white"
          };
        } else {
          debugger;
          if (row[this.tableHeader[columnIndex-1]] > 0) {
            return {
              color: "#13c4f0"
            };
          } else {
            return {
              background: "red",
              color: "black"
            };
          }
        }
      }
      if (columnIndex === 0) {
        return {
          background: "#eceb3c"
        };
      }
    },
    ipChange(ip, port, valid) {
      this.temp.StationIp = ip;
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

      this.getListsByQuery(this.listQuery);
    },
    getPageList(page) {
      this.listQuery.page = page.page;
      this.listQuery.limit = page.limit;
      this.getListsByQuery(this.listQuery);
    },

    handlerSearch() {
      debugger;
      var obj = this.value7;
      if (this.value7[0]) {
        this.listQuery.StartDate = this.value7[0];
        this.listQuery.EndDate = this.value7[1];
      }

      this.getListsByQuery(this.listQuery);
    },
    filterNode(value, data) {
      if (!value) return true;
      return data.name.indexOf(value) !== -1;
    },
    GetWCName(val) {
      if (val == "Summary" || val == "Total Available" || val == "Gap")
        return val;
      if (val) {
        var targetObs = this.workcellOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetSpaceTypeName(val) {
       if (val == "Summary" || val == "Total Available" || val == "Gap")
        return val;
      if (val) {
        var targetObs = this.AllTypeOptions.filter(p => {
          return p.Id == val;
        });

        return targetObs[0].Name;
      }
    },
    GetSpaceName(val) {
       if (val == "N/A")
        return val;
      if (val) {
        var targetObs = this.AllSpaceOptions.filter(p => {
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
      // getForecastLists(query);
      debugger;
      getSummaryForecastListsByWcs(query).then(response => {
        debugger;
        this.list = response.data;
        this.tableHeader = response.header;
        this.list2 =  response.data2;
         this.tableHeader2 = response.header2;
        this.total = response.total;
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 1000);
      });
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
        this.getListsByQuery(this.listQuery);
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 2000);
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
        Code: undefined,
        Name: undefined,
        WorkcellId: undefined,
        LineId: undefined,
        Remark: undefined,
        StationIp: "000000000000"
      };
    },
    handleCreate() {
      this.resetTemp();
      debugger;
      if (
        this.tempNode.WorkcellId != undefined ||
        this.tempNode.LineId != undefined
      ) {
        this.temp.WorkcellId = this.tempNode.WorkcellId;
        this.temp.LineId = this.tempNode.LineId;
      }

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
            this.getListsByQuery(this.listQuery);
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
          Update(tempData).then(() => {
            this.getListsByQuery(this.listQuery);
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
        this.getListsByQuery(this.listQuery);
        this.$notify({
          title: "Success",
          message: "Delete Successfully",
          type: "success",
          duration: 2000
        });
      });
    },
    handleUpload() {
      this.$router.push({ path: "/Functions/Space/SpaceUpload" });
      // this.$router.push({ path: "/Functions/Downtime/ExcelUpload" });
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
    getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "Total sq.ft";
          return;
        }
        const values = data.map(item => Number(item[column.property]));
        if (!values.every(value => isNaN(value))) {
          sums[index] = values.reduce((prev, curr) => {
            const value = Number(curr);
            if (!isNaN(value)) {
              return prev + curr;
            } else {
              return prev;
            }
          }, 0);
        } else {
          sums[index] = "N/A";
        }
      });

      return sums;
    }
  }
};
</script>

<style>
.el-header {
  background-color: #20b6f9;
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
  background: linear-gradient(
    90deg,
    rgba(32, 182, 249, 1) 0%,
    rgba(32, 182, 249, 1) 0%,
    rgba(33, 120, 241, 1) 100%,
    rgba(33, 120, 241, 1) 100%
  );
  margin-bottom: 10px;
}

.stickyFoot {
  height: 40px;
  line-height: 25px;
  position: relative;
  width: 100%;
  text-align: right;
  padding-top: 5px;
  padding-right: 10px;
  transition: 600ms ease position;
  /* background: #b3c0d1; */
  background: linear-gradient(
    90deg,
    rgb(240, 242, 243) 0%,
    rgb(239, 241, 241) 0%,
    rgb(217, 221, 226) 100%,
    rgb(196, 202, 211) 100%
  );
  margin-bottom: 10px;
}

.el-table_footer {
  background-color: #20b6f9;
}
</style>

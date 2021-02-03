<template>
  <div class="app-container">
    <el-row>
      <el-col :span="12" :align="left">
        <a ref="download" v-show="true" :href="downloadUrl" download
          >Download Space Template</a
        >
      </el-col>
      <el-col :span="12" :align="left"> </el-col>
    </el-row>

    <upload-excel-component
      :on-success="handleSuccess"
      :before-upload="beforeUpload"
    />

    <el-table
      size="mini"
      height="250px"
      :data="tableData"
      border
      highlight-current-row
      style="margin-top:10px;"
    >
      <el-table-column
        v-for="item of tableHeader"
        :key="item"
        :prop="item"
        width="150px"
        :label="item"
      />
    </el-table>
    <sticky :z-index="10" :class-name="'sub-header'">
      <el-row>
        <!-- <el-col :span="10" :align="left">
          <div class="block">
            <el-date-picker
              v-model="value7"
              type="daterange"
              align="right"
              style="width:500px;"
              size="small"
              unlink-panels
              range-separator="To"
              start-placeholder="Start date"
              end-placeholder="End date"
              :picker-options="pickerOptions2"
            >
            </el-date-picker>
             <el-select
                      v-model="value7"
                      clearable
                      class="filter-item"
                      placeholder="Space Type"
                      style=" width:300px; margin-left:10px;"
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
        <el-col :span="24">
          <div class="grid-content">
            <div class="filter-container">
              <!-- <el-button
                class="filter-item"
                type="success"
                icon="el-icon-search"
                size="mini"
                >Data Verification</el-button
              > -->
              <el-button
                class="filter-item"
                style="margin-left: 10px;"
                type="warning"
                icon="el-icon-circle-plus-outline"
                size="mini"
                :disabled="!canOperater"
                @click="UploadData"
                >Submit</el-button
              >
              <!-- <el-button
                class="filter-item"
                icon="el-icon-upload"
                size="mini"
                :disabled="!canOperater"
                >Upload</el-button
              > -->
            </div>
          </div>
        </el-col>
      </el-row>
    </sticky>
    <!-- <el-button
      :loading="loading"
      style="margin-left:16px;"
      size="mini"
      type="primary"
      @click="UploadData"
      >upload</el-button
    >-->
  </div>
</template>

<script>
/* eslint-disable */
import UploadExcelComponent from "@/components/UploadExcel/index.vue";
import { UploadForecast } from "@/api/space";
import { Message, MessageBox } from "element-ui";
import store from "@/store";
import Warning from "./Warning";
import Sticky from "@/components/Sticky"; // 粘性header组件
import {
  getWorkcellOptions,
  getEquipmentTypeOptionsByQuery
} from "@/api/downtime";

export default {
  name: "SpaceUpload",
  components: { UploadExcelComponent, Warning, Sticky },
  data() {
    return {
      loading: false,
      downloadUrl: process.env.BASE_API + "/space/template?filename=SpacePlan",
      tableData: undefined,
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
      workcellOptions: [],
      tableHeader: [],
      uploadfiles: null,
      canOperater:
        store.getters.roles.indexOf("admin") >= 0 ||
        store.getters.roles.indexOf("editor") >= 0,
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
      value7: ""
    };
  },
  created() {
    this.getWorkcellOptions();
  },
  methods: {
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
    beforeUpload(file) {
      const isLt1M = file.size / 1024 / 1024 < 1;

      if (isLt1M) {
        return true;
      }

      this.$message({
        message: "Please do not upload files larger than 1m in size.",
        type: "warning"
      });
      return false;
    },

    handleSuccess({ results, header, files }) {
      this.tableData = results;
      this.tableHeader = header;
      this.uploadfiles = files;
    },

    UploadData() {
      this.loading = true;
      console.log(this.uploadfiles);
      console.log(this.uploadfiles.name);
      var wcs = this.workcellOptions;
      var stype = this.AllTypeOptions;
      var buildings = ['M','A','B','C','P5'];
      debugger;
      var isOk = true;
      var tempWC="";
      var tempType="";
      this.tableData.forEach(function(item) {
        var targetObs = wcs.filter(p => p.Name.toUpperCase() == item.WC.toUpperCase());
        if (targetObs.length == 0) {
         isOk = false;
         tempWC = item.WC;
        }
      });

      if(!isOk)
      {
           this.$message({
            message: "the workcell ["+ tempWC +"] not exsit",
            type: "error",
            duration: 5 * 1000
          });

          return false;
      }

      this.tableData.forEach(function(item) {
        var targetObs = stype.filter(p => p.Name.toUpperCase() == item.SpaceType.toUpperCase());
        if (targetObs.length == 0) {
         isOk = false;
         tempType = item.SpaceType;
        }
      });

      if(!isOk)
      {
           this.$message({
            message: "the space type ["+ tempType +"] not exsit",
            type: "error",
            duration: 5 * 1000
          });

          return false;
      }

       this.tableData.forEach(function(item) {
        var targetObs = buildings.filter(p => p.toUpperCase() == item.Building.toUpperCase());
        if (targetObs.length == 0) {
         isOk = false;
         tempType = item.Building;
        }
      });

      if(!isOk)
      {
           this.$message({
            message: "the building ["+ tempType +"] not exsit",
            type: "error",
            duration: 5 * 1000
          });

          return false;
      }


      let fileFormData = new FormData();
      fileFormData.append("file", this.uploadfiles, this.uploadfiles.name);
      UploadForecast(this.tableData).then(response => {
        if (response.code == 20000) {
          this.tableData = [];
          this.tableHeader = [];
          this.uploadfiles = null;
          Message({
             message: 'space upload success',
            type: "success",
            duration: 5 * 1000
          });
        } else {
          this.$message({
              message: 'space upload faild',
            type: "error",
            duration: 5 * 1000
          });
        }
      });

      this.loading = false;
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
  padding-left: 20px;
  transition: 600ms ease position;
  /* background: #b3c0d1; */
  background: linear-gradient(
    90deg,
    rgba(32, 182, 249, 1) 0%,
    rgba(32, 182, 249, 1) 0%,
    rgba(33, 120, 241, 1) 100%,
    rgba(33, 120, 241, 1) 100%
  );
  margin-bottom: 30px;
}
</style>

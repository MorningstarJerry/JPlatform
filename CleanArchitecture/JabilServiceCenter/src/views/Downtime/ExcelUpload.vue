<template>
  <div class="app-container">
     <Warning />
    <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
    <el-button
      :loading="loading"
      style="margin-left:16px;"
      size="mini"
      type="primary"
      @click="UploadData"
    >upload</el-button>
    <el-table size="mini"  height="280px" :data="tableData" border highlight-current-row style="margin-top:20px;">
      <el-table-column v-for="item of tableHeader" :key="item" :prop="item" width="150px" :label="item" />
    </el-table>
  </div>
</template>

<script>
/* eslint-disable */
import UploadExcelComponent from "@/components/UploadExcel/index.vue";
import { upload } from "@/api/downtime";
import { Message, MessageBox } from "element-ui";
import Warning from "./Warning";

export default {
  name: "UploadExcel",
  components: { UploadExcelComponent, Warning },
  data() {
    return {
      loading: false,
      tableData: [],
      tableHeader: [],
      uploadfiles: null
    };
  },
  methods: {
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
      debugger;
      let fileFormData = new FormData();
      fileFormData.append("file", this.uploadfiles, this.uploadfiles.name);
      upload(this.tableData).then(response => {
        if (response.code == 20000) {
          this.tableData = [];
          this.tableHeader = [];
          this.uploadfiles = null;
          Message({
            message: response.message,
            type: "success",
            duration: 5 * 1000
          });
        } else {
          this.$message({
            message: response.message,
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

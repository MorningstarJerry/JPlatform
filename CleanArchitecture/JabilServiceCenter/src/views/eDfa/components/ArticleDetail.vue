<template>
  <div class="createPost-container">
    <el-form
      ref="postForm"
      :model="postForm"
      :rules="rules"
      class="form-container"
      label-position="left"
      size="mini"
    >
      <sticky :z-index="10" :class-name="'sub-navbar '+postForm.status">
        <el-button
          v-loading="loading"
          style="margin-left: 10px;"
          type="success"
          @click="submitForm"
          size="mini"
           :disabled = "!canOperater"
        >{{FormStr}}</el-button>
        <el-button v-loading="loading" type="warning" @click="draftForm" size="mini"  :disabled = "!canOperater">Reset</el-button>
      </sticky>
      <div class="createPost-main-container">
        <el-row>
          <el-col :span="24">
            <div class="postInfo-container">
              <el-row>
                <el-col :span="12">
                  <el-form-item label-width="120px" label="Workcell" prop="WorkcellId">
                    <el-select
                      v-model="postForm.WorkcellId"
                      placeholder="Workcell"
                      clearable
                      class="filter-item"
                      style="width:300px"
                      filterable
                      remote
                      :remote-method="getWCOptionsByQuery"
                      default-first-option
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    label="Product Type"
                    class="postInfo-container-item"
                    prop="ProducttypeId"
                  >
                    <el-select
                      v-model="postForm.ProducttypeId"
                      placeholder="Product Type"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    label="Product Phase"
                    class="postInfo-container-item"
                    prop="ProductPhaseId"
                  >
                    <el-select
                      v-model="postForm.ProductPhaseId"
                      style="width:300px"
                      clearable
                      class="filter-item"
                      placeholder="Product Phase"
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    label="Data Source"
                    prop="DataSourceId"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.DataSourceId"
                      placeholder="Data Source"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
              </el-row>
              <el-row>
                <el-col></el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    label="Process"
                    prop="ProcessId"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.ProcessId"
                      placeholder="Process"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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

                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    label="Severity"
                    class="postInfo-container-item"
                    prop="Severity"
                  >
                    <el-select
                      v-model="postForm.Severity"
                      placeholder="Severity"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="MaterialTypeId"
                    label="MaterialType"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.MaterialTypeId"
                      placeholder="N/A"
                      clearable
                      class="filter-item"
                      style="width: 300px"
                      remote
                      filterable
                      :remote-method="getMaterialTypeOptionsByQuery"
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="CategorytypeId"
                    label="CategoryType"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.CategorytypeId"
                      placeholder="N/A"
                      clearable
                      class="filter-item"
                      style="width: 300px"
                      default-first-option
                      remote
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
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    label="StartDate"
                    class="postInfo-container-item"
                    prop="StartDate"
                  >
                    <el-date-picker
                      v-model="postForm.StartDate"
                      type="date"
                      placeholder="Select start date and time"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="DueDate"
                    label="DueDate"
                    class="postInfo-container-item"
                  >
                    <el-date-picker
                      v-model="postForm.DueDate"
                      type="date"
                      placeholder="Select end date and time"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="BusinessOwnerId"
                    label="BO"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.BusinessOwnerId"
                      placeholder="Business Owner"
                      clearable
                      class="filter-item"
                      style="width: 300px"
                      default-first-option
                      remote
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="AssignUId"
                    label="Assign"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.AssignUId"
                      placeholder="choose user"
                      clearable
                      class="filter-item"
                      style="width: 300px"
                      default-first-option
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="Status"
                    label="Status"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.Status"
                      placeholder="Status"
                      clearable
                      class="filter-item"
                      style="width: 300px"
                      default-first-option
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
                <el-col :span="12">
                  <el-form-item
                    label-width="120px"
                    prop="Resolved"
                    label="Resolved"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.Resolved"
                      placeholder="Resolved"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
              </el-row>
              <el-row>
                <el-col :span="8">
                  <el-form-item
                    label-width="120px"
                    prop="Verification"
                    label="Verification"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.Verification"
                      placeholder="Verification"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
            </div>
          </el-col>
        </el-row>
        <el-form-item style="margin-bottom: 40px;" prop="Suggestion">
          <MDinput
            v-model="postForm.Suggestion"
            :maxlength="400"
            name="Problem Description"
            required
          >Problem Description</MDinput>
        </el-form-item>
        <el-form-item style="margin-bottom: 40px;" prop="Solution">
          <MDinput v-model="postForm.Solution" :maxlength="400" name="Solution" required>Solution</MDinput>
        </el-form-item>
        <el-row>
          <el-col :span="16"></el-col>
          <el-col :span="8">
            <el-form-item style="margin-bottom: 40px;" prop="AffectedNo">
              <MDinput
                v-model="postForm.AffectedNo"
                :maxlength="400"
                name="AffectedNo"
              >AffectedNo</MDinput>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item style="margin-bottom: 40px;" prop="CustomerFeedback">
          <MDinput
            v-model="postForm.CustomerFeedback"
            :maxlength="400"
            name="CustomerFeedback"
          >CustomerFeedback</MDinput>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="BeforePicture">
          <vue-dropzone
              ref="myVueDropzone1"
              id="dropzone1"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone1"
              @vdropzone-mounted="InitDropzoneData"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccess"
              v-on:vdropzone-thumbnail="thumbnail"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  DFA Before Picture
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
        </el-form-item>
          </el-col>
          <el-col :span="12">
             <vue-dropzone
              ref="myVueDropzone2"
              id="dropzone2"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone2"
              @vdropzone-mounted="InitDropzoneData2"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccess2"
              v-on:vdropzone-thumbnail="thumbnail"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  DFA After Picture
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-col>
          
        </el-row>
        

        <el-form-item prop="Remark" style="margin-bottom:30px;">
          <Tinymce ref="editor" v-model="postForm.Remark" :height="400" />
        </el-form-item>
      </div>
    </el-form>
  </div>
</template>

<script>
/* eslint-disable */
import Tinymce from "@/components/Tinymce";
import Upload from "@/components/Upload/SingleImage3";
import MDinput from "@/components/MDinput";
import Sticky from "@/components/Sticky"; // 粘性header组件
import { validURL, validNumber } from "@/utils/validate";
import { timeSubstract } from "@/utils/index";
import { fetchArticle } from "@/api/article";
import { searchUser } from "@/api/remote-search";
import Warning from "./Warning";
import store from "@/store";
import {
  CommentDropdown,
  PlatformDropdown,
  SourceUrlDropdown
} from "./Dropdown";

import {
getWCOptionsByQuery
} from "@/api/workcells";


import { getProcessesOptions } from "@/api/downtime";
import vue2Dropzone from "vue2-dropzone";
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
  getMaterialTypeOptionsByQuery,
  getSeverityOptions,
  getCategoryOptions,
  getBusinessOwnerOptions,
  getDFAStatusOptions,
  getById
} from "@/api/eDfa";

const defaultForm = {
  Id: undefined,
  Gid:undefined,
  ProducttypeId: undefined,
  WorkcellId: undefined,
  ProductPhaseId: undefined,
  DataSourceId: undefined,
  ProcessId: undefined,
  AffectedNo: undefined,
  MaterialTypeId: undefined,
  Severity: undefined,
  CategorytypeId: undefined,
  Suggestion: undefined,
  Solution: undefined,
  BusinessOwnerId: undefined,
  AssignUId: undefined,
  StartDate: undefined,
  DueDate: undefined,
  CustomerFeedback: undefined,
  Status: undefined,
  Resolved: undefined,
  Verification: undefined,
  Remark: undefined,
  Attachs: []
};

export default {
  name: "ArticleDetail",
  components: {
    vueDropzone: vue2Dropzone,
    Tinymce,
    MDinput,
    Upload,
    Sticky,
    Warning,
    CommentDropdown,
    PlatformDropdown,
    SourceUrlDropdown
  },
  inject: ["reload"],
  props: {
    isEdit: {
      type: Boolean,
      default: false
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
    const validateSourceUri = (rule, value, callback) => {
      if (value) {
        if (validURL(value)) {
          callback();
        } else {
          this.$message({
            message: "外链url填写不正确",
            type: "error"
          });
          callback(new Error("外链url填写不正确"));
        }
      } else {
        callback();
      }
    };
    const validateNumbers = (rule, value, callback) => {
      if (value) {
        if (!validNumber(value)) {
          this.$message({
            message: "Impact Working Hour Only Number Allowed",
            type: "error"
          });
          callback(new Error("Impact Working Hour Only Number Allowed"));
        } else {
          callback();
        }
      } else {
        callback();
      }
    };
    return {
        dropzoneOptions: {
        url: process.env.BASE_API + "/files/upload",
        maxFilesize: 20,
        maxFiles: 1,
        thumbnailWidth: 300, // px
        thumbnailHeight: 200,
        addRemoveLinks: true,
         canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
        dictRemoveFileConfirmation: "are you sure",
        dictCancelUpload: "cancel",
        previewTemplate: this.template(),
      },
      postForm: Object.assign({}, defaultForm),
      loading: false,
      userListOptions: [],
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
      resolvedOptions: undefined,
      verificationOptions: undefined,
      materialTypeOptions: undefined,
      activeNames: ["1"],
      resolvedOptions: [
        { EnumName: "Yes", EnumValue: 1 },
        { EnumName: "No", EnumValue: 0 }
      ],
      verificationOptions: [
        { EnumName: "Yes", EnumValue: 1 },
        { EnumName: "No", EnumValue: 0 }
      ],
      FormStr: "Submit",
      rules: {
        ProducttypeId: [{ validator: validateRequire }],
        WorkcellId: [{ validator: validateRequire }],
        ProductPhaseId: [{ validator: validateRequire }],
        DataSourceId: [{ validator: validateRequire }],
        ProcessId: [{ validator: validateRequire }],
        MaterialTypeId: [{ validator: validateRequire }],
        Severity: [{ validator: validateRequire }],
        CategorytypeId: [{ validator: validateRequire }],
        Suggestion: [{ validator: validateRequire, trigger: "blur" }],
        Solution: [{ validator: validateRequire, trigger: "blur" }],
        BusinessOwnerId: [{ validator: validateRequire }],
        StartDate: [{ validator: validateRequire }],
        DueDate: [{ validator: validateRequire }],
        Status: [{ validator: validateRequire}],
        Resolved: [{ validator: validateRequire}],
        Verification: [{ validator: validateRequire}],
        Remark: [{ validator: validateRequire, trigger: "blur" }]
      },
      tempRoute: {}
    };
  },
  created() {
    this.getProcessesOptions();
    this.getAllProductTypes();
    this.getProductPhaseOptions();
    this.getDataSourceOptions();
    this.getSeverityOptions();
    this.getCategoryOptions();
    this.getBusinessOwnerOptions();
    this.getDFAStatusOptions();
    this.getUsersOptions("");
    this.getWCOptionsByQuery("");
    this.getMaterialTypeOptionsByQuery("")

    if (this.isEdit) {
      debugger;
      const id = this.$route.params && this.$route.params.id;
      this.fetchData(id);
      this.FormStr = "Update";
    } else {      
      this.FormStr = "Submmit";
    }

    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route);
  },
  methods: {
        template: function () {
      return `<div class="dz-preview dz-file-preview">
              <div class="dz-image">
                 <img  data-dz-thumbnail-bg width="360" height="345">
              </div>
              <div class="dz-details">
                  <div class="dz-size"><span data-dz-size></span></div>
                  <div class="dz-filename"><span data-dz-name></span></div>
              </div>
              <div class="dz-progress"><span class="dz-upload" data-dz-uploadprogress></span></div>
              <div class="dz-error-message"><span data-dz-errormessage></span></div>
              <div class="dz-success-mark"><i class="fa fa-check"></i></div>
              <div class="dz-error-mark"><i class="fa fa-close"></i></div>
          </div>
      `;
    },
    thumbnail: function(file, dataUrl) {
      debugger
      var j, len, ref, thumbnailElement;
      if (file.previewElement) {
          file.previewElement.classList.remove("dz-file-preview");
          ref = file.previewElement.querySelectorAll("[data-dz-thumbnail-bg]");
          for (j = 0, len = ref.length; j < len; j++) {
              thumbnailElement = ref[j];
              thumbnailElement.alt = file.name;
              thumbnailElement.src = dataUrl ;
              // thumbnailElement.style.backgroundImage = 'url("' + dataUrl + '")';
          }
          return setTimeout(((function(_this) {
              return function() {
                  // return file.previewElement.classList.add("dz-image-preview");
              };
          })(this)), 1);
      }
    },
    InitDropzoneData() {
				debugger;
				for (var i = 0; i < this.postForm.Attachs.length; i++) {
					var file = {
						name: this.postForm.Attachs[i].name,
						fileuuid: this.postForm.Attachs[i].fileuuid,
						size: Number.parseInt(this.postForm.Attachs[i].size),
						url: this.postForm.Attachs[i].url
					};

					var url = this.postForm.Attachs[i].url;
					var typeId = this.postForm.Attachs[i].typeId;
					if (typeId == 1) {
            this.$refs.myVueDropzone1.manuallyAddFile(file, url);
					}
				}
      },
      InitDropzoneData2() {
				debugger;
				for (var i = 0; i < this.postForm.Attachs.length; i++) {
					var file = {
						name: this.postForm.Attachs[i].name,
						fileuuid: this.postForm.Attachs[i].fileuuid,
						size: Number.parseInt(this.postForm.Attachs[i].size),
						url: this.postForm.Attachs[i].url
					};

					var url = this.postForm.Attachs[i].url;
					var typeId = this.postForm.Attachs[i].typeId;
					if (typeId == 2) {
            this.$refs.myVueDropzone2.manuallyAddFile(file, url);
					}
				}
			},
    handlerFileSuccess(file) {
				debugger;
				if (file.url) {
					var a = document.createElement("a");
					a.setAttribute("href", file.url);
					a.setAttribute("class", "dz-open");
					a.innerHTML = "Download";
          file.previewTemplate.appendChild(a);
          
          this.thumbnail(file,file.url);
          
				}
			},
			handlerUploadFileSuccess(file, response) {
				debugger;
				var a = document.createElement("a");
				a.setAttribute("href", response.FileUrl);
				a.setAttribute("class", "dz-open");
				a.innerHTML = "Download";
				file.previewTemplate.appendChild(a);
				file.fileuuid = response.FileuuId;
				file.url = response.FileUrl;
				file.typeId = 1;
      },
      	handlerUploadFileSuccess2(file, response) {
				debugger;
				var a = document.createElement("a");
				a.setAttribute("href", response.FileUrl);
				a.setAttribute("class", "dz-open");
				a.innerHTML = "Download";
				file.previewTemplate.appendChild(a);
				file.fileuuid = response.FileuuId;
				file.url = response.FileUrl;
				file.typeId = 2;
			},
    fetchData(id) {
      getById(id)
        .then(response => {
          this.postForm = response.data[0];
            for (var i = 0; i < this.postForm.Attachs.length; i++) {
            var file = {
              name: this.postForm.Attachs[i].name,
              fileuuid: this.postForm.Attachs[i].fileuuid,
              size: Number.parseInt(this.postForm.Attachs[i].size),
              url: this.postForm.Attachs[i].url,
              typeId: this.postForm.Attachs[i].typeId
            };

            var url = this.postForm.Attachs[i].url;
            var typeId = this.postForm.Attachs[i].typeId;

            if (typeId == 1) {
              this.$refs.myVueDropzone1.manuallyAddFile(file, url);
            } else if (typeId == 2) {
              this.$refs.myVueDropzone2.manuallyAddFile(file, url);
            } 
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    submitForm() {
      this.$refs.postForm.validate(valid => {
        if (valid) {
          this.loading = true;
          var sDate = this.postForm.StartDate.toLocaleString();
          var eDate = this.postForm.DueDate.toLocaleString();
         
          this.postForm.StartDate = sDate;
          this.postForm.DueDate = eDate;

          var files = this.$refs.myVueDropzone1.dropzone.files;

          var ary = new Array();
          for (var i = 0; i < files.length; i++) {
            var attach = {
              RFQGid: this.postForm.Gid,
              name: files[i].name,
              fileuuid: files[i].fileuuid,
              size: files[i].size,
              url: files[i].url,
              typeId: files[i].typeId
            };

            ary.push(attach);
          }

          files = this.$refs.myVueDropzone2.dropzone.files;
          for (var i = 0; i < files.length; i++) {
            var attach = {
              RFQGid: this.postForm.Gid,
              name: files[i].name,
              fileuuid: files[i].fileuuid,
              size: files[i].size,
              url: files[i].url,
              typeId: files[i].typeId
            };

            ary.push(attach);
          }

          this.postForm.Attachs = ary;

          if (this.isEdit) {
            Update(this.postForm).then(response => {
              var result = response;
              // Just to simulate the time of the request
              if (result.code == 20000) {
                this.$notify({
                  title: "Success",
                  message: "your DFA submit successfully",
                  type: "success",
                  duration: 2000
                });
                this.resetForm();
              } else {
                console.log("error submit!!");
                return false;
              }
              this.loading = false;
            });
          } else {
              Create(this.postForm).then(response => {
              var result = response;
              // Just to simulate the time of the request
              if (result.code == 20000) {
                this.$notify({
                  title: "Success",
                  message: "your DFA submit successfully",
                  type: "success",
                  duration: 2000
                });
                this.resetForm();
              } else {
                console.log("error submit!!");
                return false;
              }
              this.loading = false;
            });
          }
        }
      });
    },
    resetForm() {
      this.$refs.postForm.resetFields();
    },
    draftForm() {
      this.$refs.postForm.resetFields();
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
     getUsersOptions(query) {
       if(query == undefined)
       {
         query ="";
       }
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
      getMaterialTypeOptionsByQuery(query) {
        if(query == undefined)
        {
          query = "";
        }

      this.listLoading = true;
      getMaterialTypeOptionsByQuery(query).then(response => {
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
    getWCOptionsByQuery(query) {
      if(query == undefined)
      {
        query = "";
      }
      this.listLoading = true;
      debugger;
      getWCOptionsByQuery(query).then(response => {
        this.workcellOptions = response.data;
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
    }
  }
};
</script>
<style lang="css">
	@import "../assset/vue2DropzoneDfa.min.css";

	.dropzone-custom-content {
   
		/* position: absolute;
		top: 45%;
		left: 10%; */
		/* transform: translate(-50%, -50%); */
		text-align: center;
	}

	.dropzone-custom-title {
		margin-top: 0;
		color: #4ead92;
	}

	.subtitle {
		color: #314b5f;
	}
</style>
<style lang="scss" scoped>
@import "~@/styles/mixin.scss";

.createPost-container {
  position: relative;

  .createPost-main-container {
    padding: 40px 45px 20px 50px;

    .postInfo-container {
      position: relative;
      @include clearfix;
      margin-bottom: 10px;

      .postInfo-container-item {
        float: left;
      }
    }
  }

  .word-counter {
    width: 40px;
    position: absolute;
    right: 10px;
    top: 0px;
  }
}

.article-textarea /deep/ {
  textarea {
    padding-right: 40px;
    resize: none;
    border: none;
    border-radius: 0px;
    border-bottom: 1px solid #bfcbd9;
  }
}
</style>
<style>
  #customdropzone {
    background-color: orange;
    font-family: 'Arial', sans-serif;
    letter-spacing: 0.2px;
    color: #777;
    transition: background-color .2s linear;
    height: 200px;
    padding: 40px;
  }
  #customdropzone .dz-preview {
    width: 160px;
    display: inline-block
  }
 #customdropzone .dz-preview .dz-image {
    width: 80px;
    height: 80px;
    margin-left: 40px;
    margin-bottom: 10px;
  }
  #customdropzone .dz-preview .dz-image > div {
    width: inherit;
    height: inherit;
    border-radius: 50%;
    background-size: contain;
  }
  #customdropzone .dz-preview .dz-image > img {
    width: 100%;
  }
   #customdropzone .dz-preview .dz-details {
    color: white;
    transition: opacity .2s linear;
    text-align: center;
  }
  #customdropzone .dz-success-mark, .dz-error-mark, .dz-remove {
    display: none;
  }
</style>
<template>
  <div class="createPost-container">
    <el-form ref="postForm" :model="postForm" :rules="rules" class="form-container" label-position="left" size="mini">
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
                  <el-form-item
                    label-width="100px"
                    label="SiteId"
                    class="postInfo-container-item"
                    prop="SiteId"
                  >
                    <el-select
                      v-model="postForm.SiteId"
                      placeholder="Site"
                      clearable
                      style="width: 300px"
                      class="filter-item"
                      default-first-option
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
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    label="SectorId"
                    class="postInfo-container-item"
                    prop="SectorId"
                  >
                    <el-select
                      v-model="postForm.SectorId"
                      placeholder="Sector"
                      clearable
                      style="width:300px"
                      class="filter-item"
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
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    label="WorkcellId"
                    class="postInfo-container-item"
                    prop="WorkcellId"
                  >
                    <el-select
                      v-model="postForm.WorkcellId"
                      placeholder="Workcell"
                      clearable
                      class="filter-item"
                      style="width:300px"
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
                    label-width="100px"
                    label="DepartmentId"
                    class="postInfo-container-item"
                    prop="DepartmentId"
                  >
                    <el-select
                      v-model="postForm.DepartmentId"
                      placeholder="Department"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    label="Equipment"
                    class="postInfo-container-item"
                    prop="EquipmentId"
                  >
                    <el-select
                      v-model="postForm.EquipmentId"
                      style="width:300px"
                      :remote-method="getEquipmentOptionsByQuery"
                      filterable
                      default-first-option
                      remote
                      placeholder="Choose Equipment"
                    >
                      <el-option
                        v-for="item in equipmentOptions"
                        :key="item.ID"
                        :label="item.Code+'-'+item.Name"
                        :value="item.Code"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    label="DownDate"
                    class="postInfo-container-item"
                    prop="DownDate"
                  >
                    <el-date-picker
                      v-model="Down_Date"
                      type="datetime"
                      format="yyyy-MM-dd HH:mm:ss"
                      placeholder="Select start date and time"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
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
                    label-width="100px"
                    label="DownType"
                    class="postInfo-container-item"
                    prop="Downtimetype"
                  >
                    <el-select
                      v-model="postForm.Downtimetype"
                      placeholder="DownType"
                      clearable
                      class="filter-item"
                      style="width: 300px"
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
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    prop="LineId"
                    label="Line"
                    class="postInfo-container-item"
                  >
                    <el-select
                      v-model="postForm.LineId"
                      placeholder="Line"
                      clearable
                      class="filter-item"
                      style="width: 300px"
                      :remote-method="getLineOptionsByQuery"
                      filterable
                      default-first-option
                      remote
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
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    label="Pic"
                    prop="PIC"
                    class="postInfo-container-item"
                  >
                    <el-input
                      v-model="postForm.PIC"
                      :rows="1"
                      type="textarea"
                      class="article-textarea"
                      autosize
                      style=" width:300px"
                      placeholder="Please enter the content"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    label="StartDate"
                    class="postInfo-container-item"
                    prop="StartDate"
                  >
                    <el-date-picker
                      v-model="Start_Date"
                      type="datetime"
                      format="yyyy-MM-dd HH:mm:ss"
                      placeholder="Select start date and time"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label-width="100px"
                    prop="EndDate"
                    label="EndDate"
                    class="postInfo-container-item"
                  >
                    <el-date-picker
                      v-model="End_Date"
                      type="datetime"
                      format="yyyy-MM-dd HH:mm:ss"
                      placeholder="Select end date and time"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label-width="100px"
                    label="DurationMins"
                    class="postInfo-container-item"
                    prop="DurationMins"
                  >
                    <el-input
                      v-model="cDurationMins"
                      :rows="1"
                      :disabled="true"
                      type="textarea"
                      class="article-textarea"
                      autosize
                      style=" width:400px"
                    />

                    <!-- <span v-show="DurationMins" class="word-counter">{{ DurationMins }}</span> -->
                  </el-form-item>
                </el-col>
              </el-row>
            </div>
          </el-col>
        </el-row>

        <!-- <el-form-item style="margin-bottom: 40px;" label-width="70px" label="Summary:">
          <el-input
            v-model="postForm.content_short"
            :rows="1"
            type="textarea"
            class="article-textarea"
            autosize
            placeholder="Please enter the content"
          />
          <span v-show="contentShortLength" class="word-counter">{{ contentShortLength }}words</span>
        </el-form-item>-->
        <el-form-item style="margin-bottom: 40px;" prop="DowntimeDescription">
          <MDinput
            v-model="postForm.DowntimeDescription"
            :maxlength="100"
            name="DowntimeDescription"
            required
          >Downtime Description</MDinput>
        </el-form-item>
        <el-form-item style="margin-bottom: 40px;" prop="RootCause">
          <MDinput
            v-model="postForm.RootCause"
            :maxlength="100"
            name="RootCause"
            required
          >Root Cause</MDinput>
        </el-form-item>
        <el-row>
          <el-col :span="16">
            <el-form-item style="margin-bottom: 40px;" prop="Impact" label="Scope">
              <!-- <MDinput v-model="postForm.Impact" :maxlength="100" name="Impact" required>Impact</MDinput> -->
              <el-select
                v-model="postForm.Impact"
                placeholder="Impact"
                clearable
                class="filter-item"
                style="width: 300px"
              >
                <el-option
                  v-for="item in impactOptions"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item style="margin-bottom: 40px;" prop="ImpactWorkingHours">
              <MDinput
                v-model="postForm.ImpactWorkingHours"
                :maxlength="100"
                name="ImpactWorkingHours"
                required
              >ImpactWorkingHours</MDinput>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item style="margin-bottom: 40px;" prop="ContainmentSolution">
          <MDinput
            v-model="postForm.ContainmentSolution"
            :maxlength="100"
            name="ContainmentSolution"
            required
          >Containment Solution</MDinput>
        </el-form-item>
        <el-form-item style="margin-bottom: 40px;" prop="LongTermSolution">
          <MDinput
            v-model="postForm.LongTermSolution"
            :maxlength="100"
            name="LongTermSolution"
            required
          >Long Term Solution</MDinput>
        </el-form-item>
        <el-form-item prop="Remark" style="margin-bottom:30px;">
          <Tinymce ref="editor" v-model="postForm.Remark" :height="400" />
        </el-form-item>
        <!-- <el-form-item prop="image_uri" style="margin-bottom: 30px;">
          <Upload v-model="postForm.image_uri" />
        </el-form-item>-->
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
  getLineOptions,
  getSiteOptions,
  getWorkcellOptions,
  getsectorsOptions,
  getProcessesOptions,
  getDowntimeTypesOptions,
  getDepartmentsOptions,
  getEquipmentOptionsByQuery,
  submitDowntime,
  UpdateDowntime,
  getLineOptionsByQuery
} from "@/api/downtime";

const defaultForm = {
  ID: undefined,
  DownDate: new Date(),
  SiteId: store.getters.siteId,
  SectorId: store.getters.sectorId,
  WorkcellId: store.getters.workcellId,
  LineId: undefined,
  ProcessId: undefined,
  Downtimetype: undefined,
  EquipmentId: undefined,
  EquipmentName: undefined,
  DowntimeDescription: undefined,
  StartDate: undefined,
  EndDate: undefined,
  DurationMins: undefined,
  RootCause: undefined,
  Impact: "1",
  ImpactWorkingHours: undefined,
  ContainmentSolution: undefined,
  LongTermSolution: undefined,
  DepartmentId: store.getters.DepartmentId,
  PIC: undefined,
  VendorId: undefined,
  SupportMethod: undefined,
  Remark: undefined
};

export default {
  name: "ArticleDetail",
  components: {
    Tinymce,
    MDinput,
    Upload,
    Sticky,
    Warning,
    CommentDropdown,
    PlatformDropdown,
    SourceUrlDropdown
  },
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
      postForm: Object.assign({}, defaultForm),
      loading: false,
      userListOptions: [],
      siteOptions: undefined,
      sectorOptions: undefined,
      workcellOptions: undefined,
      lineOptions: undefined,
      canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
      processOptions: undefined,
      downtimetypeOptions: undefined,
      departmentOptions: undefined,
      impactOptions: [
        { id: "1", name: "Station" },
        { id: "2", name: "Line" },
        { id: "3", name: "Workcell" }
      ],
      equipmentOptions: undefined,
      FormStr: "Submit",
      rules: {
        WorkcellId: [{ validator: validateRequire }],
        SectorId: [{ validator: validateRequire }],
        SiteId: [{ validator: validateRequire }],
        DepartmentId: [{ validator: validateRequire }],
        DownDate: [{ validator: validateRequire }],
        LineId: [{ validator: validateRequire }],
        ProcessId: [{ validator: validateRequire }],
        Downtimetype: [{ validator: validateRequire }],
        EquipmentId: [{ validator: validateRequire }],
        StartDate: [{ validator: validateRequire }],
        EndDate: [{ validator: validateRequire }],
        DurationMins: [
          { validator: validateRequire, trigger: "blur" },
          { validator: validateNumbers, trigger: "blur" }
        ],
        ImpactWorkingHours: [
          { validator: validateRequire, trigger: "blur" },
          { validator: validateNumbers, trigger: "blur" }
        ],
        RootCause: [{ validator: validateRequire, trigger: "blur" }],
        Impact: [{ validator: validateRequire, trigger: "blur" }],
        ContainmentSolution: [{ validator: validateRequire, trigger: "blur" }],
        LongTermSolution: [{ validator: validateRequire, trigger: "blur" }],
        PIC: [{ validator: validateRequire, trigger: "blur" }],
        DowntimeDescription: [{ validator: validateRequire, trigger: "blur" }]
      },
      tempRoute: {}
    };
  },
  computed: {
    cDurationMins() {
      if (
        this.postForm.StartDate === undefined ||
        this.postForm.EndDate === undefined
      ) {
        this.postForm.DurationMins = "0";
        return "0";
      }

      this.postForm.DurationMins = timeSubstract(
        this.postForm.StartDate,
        this.postForm.EndDate
      );
      return this.postForm.DurationMins;
    },

    contentShortLength() {
      return this.postForm.content_short.length;
    },
    Start_Date: {
      get() {
        return +new Date(this.postForm.StartDate);
      },
      set(val) {
        this.postForm.StartDate = new Date(val);
      }
    },

    Down_Date: {
      // set and get is useful when the data
      // returned by the back end api is different from the front end
      // back end return => "2013-06-25 06:59:25"
      // front end need timestamp => 1372114765000
      get() {
        return +new Date(this.postForm.DownDate);
      },
      set(val) {
        debugger
           this.postForm.DownDate = new Date(val);
        }
    },
    End_Date: {
      get() {
        return +new Date(this.postForm.EndDate);
      },
      set(val) {
        this.postForm.EndDate = new Date(val);
      }
    }
  },
  created() {
    this.getSitesOps();
    this.getsectorsOptions();
    this.getLineOptions();
    this.getProcessesOptions();
    this.getDowntimeTypesOptions();
    this.getDepartmentsOptions();
    this.getWorkcellOptions();

    if (this.isEdit) {
      debugger;
      const id = this.$route.params && this.$route.params.id;
      this.fetchData(id);
      this.FormStr = "Update";
    } else {
      this.postForm = Object.assign({}, defaultForm);
      this.FormStr = "Submmit";
    }

    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route);
  },
  methods: {
    fetchData(id) {
      fetchArticle(id)
        .then(response => {
          this.postForm = response.data[0];
          this.Start_Date = this.postForm.StartDate;
          this.End_Date = this.postForm.EndDate;
          this.cDurationMins = this.postForm.DurationMins;
          this.Down_Date = this.postForm.DownDate;
        })
        .catch(err => {
          console.log(err);
        });
    },
    setTagsViewTitle() {
      const title = "Edit Article";
      const route = Object.assign({}, this.tempRoute, {
        title: `${title}-${this.postForm.id}`
      });
      this.$store.dispatch("tagsView/updateVisitedView", route);
    },
    setPageTitle() {
      const title = "Edit Article";
      document.title = `${title} - ${this.postForm.id}`;
    },
    submitForm() {
      this.$refs.postForm.validate(valid => {
        if (valid) {
          debugger
          this.loading = true;
            var sDate = this.postForm.StartDate.toLocaleString();
            var eDate = this.postForm.EndDate.toLocaleString();
            var dDate = this.postForm.DownDate.toLocaleString();

          this.postForm.StartDate =  sDate;
          this.postForm.EndDate = eDate;
          this.postForm.DownDate = dDate;

          if (this.isEdit) {
            UpdateDowntime(this.postForm).then(response => {
              var result = response;
              // Just to simulate the time of the request
              if (result.code == 20000) {
                this.$notify({
                  title: "Success",
                  message: "your downtime submit successfully",
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
            submitDowntime(this.postForm).then(response => {
              var result = response;
              // Just to simulate the time of the request
              if (result.code == 20000) {
                this.$notify({
                  title: "Success",
                  message: "your downtime submit successfully",
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
    getLineOptionsByQuery(query) {
      this.listLoading = true;
      debugger;
      getLineOptionsByQuery(query).then(response => {
        this.lineOptions = response.data;
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
    }
  }
};
</script>

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

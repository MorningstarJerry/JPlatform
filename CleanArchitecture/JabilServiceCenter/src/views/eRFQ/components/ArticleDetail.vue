<template>
  <div class="createPost-container">
    <el-form
      ref="postForm"
      :model="postForm"
      :rules="rules"
      class="form-container"
      label-position="left"
    >
      <sticky :z-index="10" :class-name="'sub-navbar ' + postForm.status">
        <el-button
          v-loading="loading"
          style="margin-left: 10px;"
          type="success"
          @click="submitForm"
          size="mini"
          :disabled = "!canOperater"
          >{{ FormStr }}</el-button>
        <el-button
          v-loading="loading"
          type="warning"
          @click="draftForm"
          size="mini"
          :disabled = "!canOperater"
          >Reset</el-button>
      </sticky>
      <div class="createPost-main-container">
        <p>
          <a ref="download" v-show="true" :href="downloadUrl" download
            >Download RFQ Template</a
          >
        </p>
        <el-tabs v-model="activeName" @tab-click="handleClick">
          <el-tab-pane label="RFQ Attachments" name="RFQ">
            <vue-dropzone
              ref="myVueDropzone"
              id="dropzone1"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone1"
              @vdropzone-mounted="InitDropzoneData"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccess"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  Drag and drop to upload content!
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-tab-pane>
          <el-tab-pane label="BOM" name="BOM"
            ><vue-dropzone
              ref="myVueDropzoneBOM"
              id="dropzone2"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone2"
              @vdropzone-mounted="InitDropzoneDataBOM"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccessBOM"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  Drag and drop to upload content!
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-tab-pane>
          <el-tab-pane label="Assembly Drawing" name="third">
            <vue-dropzone
              ref="myVueDropzoneAssembly"
              id="dropzone3"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone"
              @vdropzone-mounted="InitDropzoneDataAssembly"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccessAssembly"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  Drag and drop to upload content!
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-tab-pane>
          <el-tab-pane label="EAU" name="fourth">
            <vue-dropzone
              ref="myVueDropzoneEAU"
              id="dropzone4"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone"
              @vdropzone-mounted="InitDropzoneDataEAU"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccessEAU"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  Drag and drop to upload content!
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-tab-pane>
          <el-tab-pane label="Part Specific" name="Specific">
            <vue-dropzone
              ref="myVueDropzoneSpecific"
              id="dropzone5"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone"
              @vdropzone-mounted="InitDropzoneDataSpec"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccessPart"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  Drag and drop to upload content!
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-tab-pane>
          <el-tab-pane label="Others" name="Others">
            <vue-dropzone
              ref="myVueDropzoneOthers"
              id="dropzone6"
              :options="dropzoneOptions"
              :duplicateCheck="true"
              :useCustomSlot="true"
              prop="dropzone"
              @vdropzone-mounted="InitDropzoneDataOthers"
              @vdropzone-file-added="handlerFileSuccess"
              @vdropzone-success="handlerUploadFileSuccessOthers"
            >
              <div class="dropzone-custom-content">
                <h3 class="dropzone-custom-title">
                  Drag and drop to upload content!
                </h3>
                <div class="subtitle">
                  ...or click to select a file from your computer
                </div>
              </div>
            </vue-dropzone>
          </el-tab-pane>
        </el-tabs>
      </div>
      <el-tabs type="card">
        <el-tab-pane label="Summary">
          <div class="createPost-main-container">
            <el-row>
              <el-col :span="24">
                <div class="postInfo-container">
                  <el-divider
                    content-position="left"
                    style="font-weight:bold; font-size:large;text-decoration: underline"
                    >Basic</el-divider
                  >
                  <p></p>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Plant"
                        size="mini"
                        prop="PlantId"
                      >
                        <el-select
                          v-model="postForm.Summary.PlantId"
                          placeholder="PlantId"
                          clearable
                          class="filter-item"
                          style="width:200px"
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
                        label-width="250px"
                        label="Date"
                        class="postInfo-container-item"
                        size="mini"
                        prop="Date"
                      >
                        <el-date-picker
                          v-model="postForm.Summary.Date"
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
                        label-width="230px"
                        label="Workcell"
                        size="mini"
                      >
                        <el-select
                          v-model="postForm.Summary.WorkcellId"
                          placeholder="Workcell"
                          clearable
                          class="filter-item"
                          style="width:200px"
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
                        label-width="250px"
                        label="Revision"
                        class="postInfo-container-item"
                        size="mini"
                        prop="Revision"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.Revision"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Product"
                        class="postInfo-container-item"
                        size="mini"
                        prop="ProductsId"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.ProductId"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        label-width="250px"
                        label="Quoted By"
                        prop="QuotedBy"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.QuotedBy"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Product #"
                        prop="ProductNo"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.ProductNo"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        label-width="250px"
                        label="Approved By"
                        class="postInfo-container-item"
                        prop="ApprovedBy"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.ApprovedBy"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-divider
                    content-position="left"
                    style="font-weight:bold; font-size:large;text-decoration: underline"
                    >Quotation Income</el-divider
                  >
                  <p></p>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Number of Shifts:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="ShiftNums"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.ShiftNums"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        label-width="250px"
                        label="Number of working days/Month:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="WorkDayNums"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.WorkDayNums"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Minutes per working shift:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="ShiftMinutes"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.ShiftMinutes"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        label-width="250px"
                        label="PFD Factor:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="PFDFactory"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.PFDFactor"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Realization Factor:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="RealizationFactor"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.RealizationFactor"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12"></el-col>
                  </el-row>
                  <el-divider
                    content-position="left"
                    style="font-weight:bold; font-size:large;text-decoration: underline"
                    >Elements of Realization Factor</el-divider>
                  <p></p>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Efficiency:"
                        class="postInfo-container-item"
                        size="mini">
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.Efficiency"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        label-width="250px"
                        label="Estimating Error:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="Estimating">
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.Estimating"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Setup:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="Setup">
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.Setup"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <el-form-item
                        label-width="250px"
                        label="Rework:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="Rework">
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.Rework"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="12">
                      <el-form-item
                        label-width="230px"
                        label="Labor Scrap:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="LaborScrap"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.LaborScrap"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12"></el-col>
                  </el-row>
                  <el-divider
                    content-position="left"
                    style="font-weight:bold; font-size:large;text-decoration: underline"
                    >Quotation Outcome</el-divider
                  >
                  <p></p>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="Adjusted Capacity:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="AdjustedCapacity"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.AdjustedCapacity"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="Total DL Req'd:"
                        class="postInfo-container-item"
                        size="mini"
                        prop="TotalDL"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.TotalDL"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="Run Rate UPH (units/hour)"
                        class="postInfo-container-item"
                        size="mini"
                        prop="SUPH"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.UPH"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="UPPH (units/person/hour):"
                        class="postInfo-container-item"
                        size="mini"
                        prop="SUPPH"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.UPPH"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="Total Area Req'd (Square Feet)"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.TotalArea"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="Total Capital Investments (US$)"
                        class="postInfo-container-item"
                        size="mini"
                        prop="TotalCapitalInverstment"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.TotalCapitalInvestments"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="230px"
                        label="Total NRE Req'd (US$)"
                        class="postInfo-container-item"
                        size="mini"
                        prop="TotalNRE"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Summary.TotalNRE"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12"></el-col>
                  </el-row>
                </div>
              </el-col>
            </el-row>
          </div>
        </el-tab-pane>
        <el-tab-pane label="Investments Rqmt">
          <div class="createPost-main-container">
            <el-row>
              <el-col :span="24">
                <div class="postInfo-container">
                  <el-divider
                    content-position="left"
                    style="font-weight:bold; font-size:large;text-decoration: underline"
                    >Capital Investments & NRE</el-divider
                  >
                  <p></p>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        label="Process Tools / Equipments"
                        class="postInfo-container-item"
                        size="mini"
                        prop="ProcessTools"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.ProcessTools"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        label="Mat. Handling Tools / Equipments"
                        class="postInfo-container-item"
                        size="mini"
                        prop="HandingTools"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.HandlingTools"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        label="Audit&OBA Tools / Equipments"
                        class="postInfo-container-item"
                        size="mini"
                        prop="AOTools"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.AOTools"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        prop="DRTools"
                        label="Diag&Repair Tools/ Equipments"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.DRTools"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        label="Basic Infrastructure"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.BasicInfrastructure"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        prop="TtlInvestments"
                        label="Ttl Investments (USD)"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.TtlInvestments"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="6"></el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="18">
                      <el-form-item
                        label-width="270px"
                        prop="TtlNRE"
                        label="Total NRE Req'd (US$)"
                        class="postInfo-container-item"
                        size="mini"
                      >
                        <el-input
                          placeholder="Please input"
                          style="width: 200px"
                          v-model="postForm.Investments.TtlNRE"
                        ></el-input>
                      </el-form-item>
                    </el-col>
                    <el-col :span="12"></el-col>
                  </el-row>
                  <el-row>
                    <el-col>
                      <p style="color:red">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />Notes:Investmetn is not include the SMT and
                        testing
                      </p>
                    </el-col>
                  </el-row>
                </div>
              </el-col>
            </el-row>
          </div>
        </el-tab-pane>
        <el-tab-pane label="Conceptual Layout & Shop Supply">
          <div class="createPost-main-container">
            <el-row>
              <el-col :span="24">
                <div class="postInfo-container">
                  <el-divider
                    content-position="left"
                    style="font-weight:bold; font-size:large;text-decoration: underline"
                    >Shop Supply_Consumption</el-divider
                  >
                  <p></p>
                  <el-table
                    :data="postForm.LayoutDatas"
                    border
                    height="200"
                    :summary-method="getSummaries"
                    show-summary
                    size="mini"
                    style="width: 100%; margin-top: 20px"
                  >
                    <el-table-column
                      prop="RFQGid"
                      label="Guid"
                      width="180"
                    ></el-table-column>
                    <el-table-column prop="Name" label="Name"></el-table-column>
                    <el-table-column
                      prop="SquareMeters"
                      label="Square Meters"
                    ></el-table-column>
                    <el-table-column
                      prop="SquareFeet"
                      label="Square Feet"
                    ></el-table-column>
                  </el-table>

                  <el-row>
                    <el-col>
                      <p style="color:red">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />Notes:Investmetn is not include the SMT and
                        testing
                      </p>
                    </el-col>
                  </el-row>
                </div>
              </el-col>
            </el-row>
          </div>
        </el-tab-pane>
        <el-tab-pane label="MOST">
          <div class="createPost-main-container">
            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Assumptions</el-divider
            >
            <p></p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # 27.8 TMU = 1 Sec
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # 1,667 TMU = 1 Min
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # 100,000 TMU = 1 hour
            </p>
            <el-table :data="postForm.MostData" style="width: 100%">
              <el-table-column type="expand">
                <template slot-scope="props">
                  <p>Secs: {{ props.row.Secs }}</p>
                  <p>Mins: {{ props.row.Mins }}</p>
                  <p>Hrs: {{ props.row.Hrs }}</p>
                </template>
              </el-table-column>
              <el-table-column
                label="AssyName"
                prop="AssyName"
              ></el-table-column>
              <el-table-column label="TMU" prop="TMU"></el-table-column>
            </el-table>
          </div>
        </el-tab-pane>
        <el-tab-pane label="Material Handling">
          <div class="createPost-main-container">
            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Assumptions</el-divider
            >
            <p></p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # All the components high item density are assumed to be 2.0 days
              inventory
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # All the fasteners, nuts, screws, and etc are assumed to be 7.0
              days inventory
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # All the low item density and fragile items are assumed to be 0.5
              days inventory
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # Assumed all the materials are kitted to production with 4.0
              times per shift
            </p>

            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Materials Handling</el-divider
            >
            <el-row>
              <el-col :span="4">
                <el-form-item
                  style="margin-bottom: 40px;"
                  prop="MaterialsHandling"
                >
                  <MDinput
                    v-model="postForm.MaterialsHandling"
                    :maxlength="100"
                    name="MaterialsHandling"
                    >Materials Handling</MDinput
                  >
                </el-form-item>
              </el-col>
              <el-col :span="20"></el-col>
            </el-row>
          </div>
        </el-tab-pane>
        <el-tab-pane label="AO">
          <div class="createPost-main-container">
            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Assumptions</el-divider
            >
            <p></p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # Sample size is estimated
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # Audit and OBA process time is estimated
            </p>
            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Audit & OBA</el-divider
            >
            <el-row>
              <el-col :span="4">
                <el-form-item style="margin-bottom: 40px;" prop="Audit">
                  <MDinput
                    v-model="postForm.TotalAudit"
                    :maxlength="100"
                    name="Audit"
                    >Total Audit</MDinput
                  >
                </el-form-item>
              </el-col>
              <el-col :span="20"></el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-form-item style="margin-bottom: 40px;" prop="TotalOBA">
                  <MDinput
                    v-model="postForm.TotalOBA"
                    :maxlength="100"
                    name="TotalOBA"
                    >Total OBA</MDinput
                  >
                </el-form-item>
              </el-col>
              <el-col :span="20"></el-col>
            </el-row>
          </div>
        </el-tab-pane>
        <el-tab-pane label="DR">
          <div class="createPost-main-container">
            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Assumptions</el-divider
            >
            <p></p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # All time is estimated
            </p>
            <p style="font-weight:bold; font-size:small; color:#4ead92;">
              # FPY is estimated
            </p>
            <el-divider
              content-position="left"
              style="font-weight:bold; font-size:large;text-decoration: underline"
              >Diagnostic & Repair</el-divider
            >
            <el-row>
              <el-col :span="4">
                <el-form-item style="margin-bottom: 40px;" prop="Diagnostic">
                  <MDinput
                    v-model="postForm.TotalDiagnostic"
                    :maxlength="100"
                    name="Diagnostic"
                    >Total Diagnostic</MDinput
                  >
                </el-form-item>
              </el-col>
              <el-col :span="20"></el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-form-item style="margin-bottom: 40px;" prop="Repair">
                  <MDinput
                    v-model="postForm.TotalRepair"
                    :maxlength="100"
                    name="Repair"
                    >Total Repair</MDinput
                  >
                </el-form-item>
              </el-col>
              <el-col :span="20"></el-col>
            </el-row>
          </div>
        </el-tab-pane>
      </el-tabs>
    </el-form>
  </div>
</template>

<script>
/* eslint-disable */
import Upload from "@/components/Upload/SingleImage3";
import MDinput from "@/components/MDinput";
import Sticky from "@/components/Sticky"; // 粘性header组件
import { validURL, validNumber } from "@/utils/validate";
import Warning from "./Warning";
import store from "@/store";
import vue2Dropzone from "vue2-dropzone";

import { getWorkcellOptions, getSiteOptions } from "@/api/downtime";

import { Create, Update, getById, DownloadRfq } from "@/api/eRfq";

const defaultForm = {
  Id: undefined,
  RFQGid: undefined,
  MaterialsHandling: undefined,
  TotalAudit: undefined,
  TotalOBA: undefined,
  TotalDiagnostic: undefined,
  TotalRepair: undefined,
  Summary: {
    Id: undefined,
    RFQGid: undefined,
    PlantId: undefined,
    WorkcellId: undefined,
    ProductId: undefined,
    ProductNo: undefined,
    Date: undefined,
    Revision: undefined,
    QuotedBy: undefined,
    ApprovedBy: undefined,
    ShiftNums: undefined,
    WorkDayNums: undefined,
    ShiftMinutes: undefined,
    PFDFactor: undefined,
    RealizationFactor: undefined,
    Efficiency: undefined,
    LaborScrap: undefined,
    Estimating: undefined,
    Setup: undefined,
    Rework: undefined,
    AdjustedCapacity: undefined,
    TotalDL: undefined,
    UPH: undefined,
    UPPH: undefined,
    TotalArea: undefined,
    TotalCapitalInvestments: undefined,
    TotalNRE: undefined
  },
  Investments: {
    Id: undefined,
    RFQGid: undefined,
    ProcessTools: undefined,
    TestTools: undefined,
    HandlingTools: undefined,
    AOTools: undefined,
    DRTools: undefined,
    BasicInfrastructure: undefined,
    TtlInvestments: undefined,
    TtlNRE: undefined
  },
  LayoutDatas: [
  ],
  MostData: [
  ],
  Attachs: []
};
export default {
  name: "ArticleDetail",
  components: {
    vueDropzone: vue2Dropzone,
    MDinput,
    Upload,
    Sticky,
    Warning
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
      downloadUrl: process.env.BASE_API + "/rfq/template",
      activeName: "RFQ",
      dropzoneOptions: {
        url: process.env.BASE_API + "/files/upload",
        maxFilesize: 20,
        maxFiles: 5,
        thumbnailWidth: 50, // px
        thumbnailHeight: 100,
        addRemoveLinks: true,
        dictRemoveFileConfirmation: "are you sure",
        dictCancelUpload: "cancel"
      },
      postForm: Object.assign({}, defaultForm),
      canOperater: store.getters.roles.indexOf('admin')>=0 || store.getters.roles.indexOf('editor')>=0,
      loading: false,
      userListOptions: [],
      siteOptions: undefined,
      workcellOptions: undefined,
      activeNames: ["1"],
      resolvedOptions: [
        {
          Id: "Yes",
          Type: 1,
          FileuuId: "89c529eb-6264-4bd1-afaa-012d5ff46e66",
          FileUrl: "www.baidu.com"
        }
      ],
      verificationOptions: [
        { EnumName: "Yes", EnumValue: 1 },
        { EnumName: "No", EnumValue: 0 }
      ],
      FormStr: "Submit",
      rules: {
        ProducttypeId: [{ validator: validateRequire }],
        WorkcellId: [{ validator: validateRequire }]
      },
      tempRoute: {}
    };
  },
  created() {
    this.getWorkcellOptions();
    this.getSitesOps();

    if (this.isEdit) {
      const id = this.$route.params && this.$route.params.id;
      this.fetchData(id);
      this.FormStr = "Update";
    } else {
      this.postForm = Object.assign({}, defaultForm);
      this.FormStr = "Submmit";
    }

    this.tempRoute = Object.assign({}, this.$route);
  },
  methods: {
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
          this.$refs.myVueDropzone.manuallyAddFile(file, url);
        }
      }
    },
    InitDropzoneDataBOM() {
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
          this.$refs.myVueDropzoneBOM.manuallyAddFile(file, url);
        }
      }
    },
    InitDropzoneDataAssembly() {
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
        if (typeId == 3) {
          this.$refs.myVueDropzoneAssembly.manuallyAddFile(file, url);
        }
      }
    },
    InitDropzoneDataEAU() {
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
        if (typeId == 4) {
          this.$refs.handlerUploadFileSuccessEAU.manuallyAddFile(file, url);
        }
      }
    },
    InitDropzoneDataSpec() {
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
        if (typeId == 5) {
          this.$refs.myVueDropzoneSpecific.manuallyAddFile(file, url);
        }
      }
    },
    InitDropzoneDataOthers() {
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
        if (typeId == 6) {
          this.$refs.myVueDropzoneOthers.manuallyAddFile(file, url);
        }
      }
    },
    handleClick(tab, event) {
      console.log(tab, event);
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
              this.$refs.myVueDropzone.manuallyAddFile(file, url);
            } else if (typeId == 2) {
              this.$refs.myVueDropzoneBOM.manuallyAddFile(file, url);
            } else if (typeId == 3) {
              this.$refs.myVueDropzoneAssembly.manuallyAddFile(file, url);
            } else if (typeId == 4) {
              this.$refs.myVueDropzoneEAU.manuallyAddFile(file, url);
            } else if (typeId == 5) {
              this.$refs.myVueDropzoneSpecific.manuallyAddFile(file, url);
            } else if (typeId == 6) {
              this.$refs.myVueDropzoneOthers.manuallyAddFile(file, url);
            }
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    handlerFileSuccess(file) {
      debugger;
      if (file.url) {
        var a = document.createElement("a");
        a.setAttribute("href", file.url);
        a.setAttribute("class", "dz-open");
        a.innerHTML = "Download";
        file.previewTemplate.appendChild(a);
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

      if (response.data.RFQGid) {
        this.postForm = response.data;
      }
    },
    handlerUploadFileSuccessBOM(file, response) {
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

    handlerUploadFileSuccessAssembly(file, response) {
      debugger;
      var a = document.createElement("a");
      a.setAttribute("href", response.FileUrl);
      a.setAttribute("class", "dz-open");
      a.innerHTML = "Download";
      file.previewTemplate.appendChild(a);
      file.fileuuid = response.FileuuId;
      file.url = response.FileUrl;
      file.typeId = 3;
    },
    handlerUploadFileSuccessEAU(file, response) {
      debugger;
      var a = document.createElement("a");
      a.setAttribute("href", response.FileUrl);
      a.setAttribute("class", "dz-open");
      a.innerHTML = "Download";
      file.previewTemplate.appendChild(a);
      file.fileuuid = response.FileuuId;
      file.url = response.FileUrl;
      file.typeId = 4;
    },
    handlerUploadFileSuccessPart(file, response) {
      debugger;
      var a = document.createElement("a");
      a.setAttribute("href", response.FileUrl);
      a.setAttribute("class", "dz-open");
      a.innerHTML = "Download";
      file.previewTemplate.appendChild(a);
      file.fileuuid = response.FileuuId;
      file.url = response.FileUrl;
      file.typeId = 5;
    },
    handlerUploadFileSuccessOthers(file, response) {
      debugger;
      var a = document.createElement("a");
      a.setAttribute("href", response.FileUrl);
      a.setAttribute("class", "dz-open");
      a.innerHTML = "Download";
      file.previewTemplate.appendChild(a);
      file.fileuuid = response.FileuuId;
      file.url = response.FileUrl;
      file.typeId = 6;
    },
    submitForm() {
      var files = this.$refs.myVueDropzone.dropzone.files;

      var ary = new Array();
      for (var i = 0; i < files.length; i++) {
        var attach = {
          RFQGid: this.postForm.RFQGid,
          name: files[i].name,
          fileuuid: files[i].fileuuid,
          size: files[i].size,
          url: files[i].url,
          typeId: files[i].typeId
        };

        ary.push(attach);
      }

      files = this.$refs.myVueDropzoneBOM.dropzone.files;
      for (var i = 0; i < files.length; i++) {
        var attach = {
          RFQGid: this.postForm.RFQGid,
          name: files[i].name,
          fileuuid: files[i].fileuuid,
          size: files[i].size,
          url: files[i].url,
          typeId: files[i].typeId
        };

        ary.push(attach);
      }

      files = this.$refs.myVueDropzoneAssembly.dropzone.files;
      for (var i = 0; i < files.length; i++) {
        var attach = {
          RFQGid: this.postForm.RFQGid,
          name: files[i].name,
          fileuuid: files[i].fileuuid,
          size: files[i].size,
          url: files[i].url,
          typeId: files[i].typeId
        };

        ary.push(attach);
      }

      files = this.$refs.myVueDropzoneEAU.dropzone.files;
      for (var i = 0; i < files.length; i++) {
        var attach = {
          RFQGid: this.postForm.RFQGid,
          name: files[i].name,
          fileuuid: files[i].fileuuid,
          size: files[i].size,
          url: files[i].url,
          typeId: files[i].typeId
        };

        ary.push(attach);
      }

      files = this.$refs.myVueDropzoneSpecific.dropzone.files;
      for (var i = 0; i < files.length; i++) {
        var attach = {
          RFQGid: this.postForm.RFQGid,
          name: files[i].name,
          fileuuid: files[i].fileuuid,
          size: files[i].size,
          url: files[i].url,
          typeId: files[i].typeId
        };

        ary.push(attach);
      }

      files = this.$refs.myVueDropzoneOthers.dropzone.files;
      for (var i = 0; i < files.length; i++) {
        var attach = {
          RFQGid: this.postForm.RFQGid,
          name: files[i].name,
          fileuuid: files[i].fileuuid,
          size: files[i].size,
          url: files[i].url,
          typeId: files[i].typeId
        };

        ary.push(attach);
      }

      this.postForm.Attachs = ary;
      this.$refs.postForm.validate(valid => {
        if (valid) {
          if (this.isEdit) {
            Update(this.postForm).then(response => {
              var result = response;
              // Just to simulate the time of the request
              if (result.code == 20000) {
                this.$notify({
                  title: "Success",
                  message: "your RFQ submit successfully",
                  type: "success",
                  duration: 2000
                });
                this.resetForm();
                // this.$router.push('/eRFQ/lists')
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
                  message: "your RFQ submit successfully",
                  type: "success",
                  duration: 2000
                });
                this.resetForm();
                //this.$router.push('/eRFQ/lists')
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
      //this.$refs.postForm.resetFields();
      this.reload();
    },
    draftForm() {
      // this.$refs.postForm.resetFields();
      this.reload();
    },

    //=================Get Options=====================
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
    download() {
      // Dynamic settings download link
      this.downloadUrl = process.env.BASE_API + "/rfq/template";

      // If setTimeout is not set, the download file will be empty
      setTimeout(() => {
        this.$refs.download.click();
      }, 2000);
    },
    getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "Total Area for lines";
          return;
        }
        const values = data.map(item => Number(item[column.property]));
        if (!values.every(value => isNaN(value))) {
          sums[index] =
            // "$ " +
            values.reduce((prev, curr) => {
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

<style lang="scss" scoped>
@import "~@/styles/mixin.scss";

.createPost-container {
  position: relative;

  .createPost-main-container {
    padding: 10px 25px 20px 25px;

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
<style lang="css">
@import "../asssets/vue2Dropzone.min.css";

.dropzone-custom-content {
  position: absolute;
  top: 35%;
  left: 50%;
  transform: translate(-50%, -50%);
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
<style lang="scss">
p {
  height: 10px;
  margin: 0;
  text-align: right;
  text-decoration: underline;
}
a {
  text-decoration: underline;
}
/*默认状态*/
p a:link {
  color: blue;
}
/*访问后得状态*/
p a:visited {
  color: #19242c;
}
/*鼠标悬浮状态*/
p a:hover {
  color: #ff0000;
}
/*点击状态*/
p a:active {
  color: aqua;
}
</style>

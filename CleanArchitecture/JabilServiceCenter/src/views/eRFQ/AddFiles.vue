   <template>
  <div class="createPost-container">
    <sticky :z-index="10" :class-name="'sub-navbar'">
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
          >Add</el-button>
          <el-button
            v-waves
            :loading="downloadLoading"
            class="filter-item"
            type="primary"
            icon="el-icon-download"
            @click="handleDownload"
            size="mini"
          >Export</el-button>
    </sticky>
    <vue-dropzone
      ref="myVueDropzone"
      id="dropzone"
      :options="dropzoneOptions"
      :duplicateCheck="true"
      :useCustomSlot="true"
      @vdropzone-files-added = "handlerFileSuccess"
    >
      <div class="dropzone-custom-content">
        <h3 class="dropzone-custom-title">Drag and drop to upload content!</h3>
        <div class="subtitle">...or click to select a file from your computer</div>
      </div>
    </vue-dropzone>
  </div>
</template>

<script>
import vue2Dropzone from 'vue2-dropzone'
import Sticky from '@/components/Sticky'// 粘性header组件
// import 'vue2-dropzone/dist/vue2Dropzone.min.css'
export default {
  components: {
    vueDropzone: vue2Dropzone,
    Sticky
  },
  data: function() {
    return {
      dropzoneOptions: {
        url: 'http://localhost:59789/api/files/upload',
        maxFilesize: 5,
        maxFiles: 5,
        thumbnailWidth: 50, // px
        thumbnailHeight: 100,
        addRemoveLinks: true,
        dictRemoveFileConfirmation: 'are you sure',
        dictCancelUpload: 'cancel'
        // headers: { 'My-Awesome-Header': 'header value' }
      }
    }
  },
  methods: {
    handlerFileSuccess(file, response) {
      debugger
      if (file.url) {
        var a = document.createElement('a')
        a.setAttribute('href', file.url)
        a.setAttribute('target', '_blank')
        a.innerHTML = 'Prenos'
        file.previewTemplate.appendChild(a)
      }
    }
  }
}
</script>
<style lang="css">
@import "/asssets/vue2Dropzone.min.css";

.dropzone-custom-content {
  position: absolute;
  top: 20%;
  left: 50%;
  transform: translate(-50%, -50%);
  text-align: center;
}

.dropzone-custom-title {
  margin-top: 0;
  color: #00b782;
}

.subtitle {
  color: #314b5f;
}
</style>

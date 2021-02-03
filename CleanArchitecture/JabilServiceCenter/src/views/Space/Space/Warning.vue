<template>
  <aside>
    <sticky :z-index="10" :class-name="'sub-header'">
      <el-row>
        <el-col :span="4" :align="left">
          <div class="grid-content">
            <el-input
              placeholder="Station Code"
              style="width:200px;"
              size="mini"
            ></el-input>
          </div>
        </el-col>
        <el-col :span="20">
          <div class="grid-content">
            <div class="filter-container">
              <el-button
                class="filter-item"
                type="success"
                icon="el-icon-search"
                size="mini"
                >Search</el-button
              >
              <el-button
                class="filter-item"
                style="margin-left: 10px;"
                type="warning"
                icon="el-icon-circle-plus-outline"
                size="mini"
                :disabled="!canOperater"
                >Add</el-button
              >
              <el-button
                class="filter-item"
                icon="el-icon-upload"
                size="mini"
                :disabled="!canOperater"
                >Upload</el-button
              >
            </div>
          </div>
        </el-col>
      </el-row>
    </sticky>
  </aside>
</template>
<script>
/* eslint-disable */
import Sticky from "@/components/Sticky"; // 粘性header组件
import store from "@/store";
export default {
  name: "Warning",
  components: { Sticky },
  data() {
    return;
    {
      canOperater: store.getters.roles.indexOf("admin") >= 0 ||
        store.getters.roles.indexOf("editor") >= 0;
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
  transition: 600ms ease position;
  /* background: #b3c0d1; */
  background: linear-gradient(
    90deg,
    rgba(32, 182, 249, 1) 0%,
    rgba(32, 182, 249, 1) 0%,
    rgba(33, 120, 241, 1) 100%,
    rgba(33, 120, 241, 1) 100%
  );
  margin: 0;
  padding: 0;
}
</style>

<template>
  <section class="app-main">
    <div class="pageHeader">
      <el-page-header @back="handleBack" content="Detail"></el-page-header>
    </div>

    <transition name="fade" mode="out-in">
      <!-- <router-view :key="key"></router-view> -->
      <router-view></router-view>
    </transition>
  </section>
</template>

<script>
/* eslint-disable */
export default {
  name: "AppMain",
  computed: {
    // key() {
    //   return this.$route.name !== undefined ? this.$route.name + +new Date() : this.$route + +new Date()
    // }
  },
  methods: {
    handleBack() {
      this.$router.go(-1);
    }
  }
};
</script>

<style scoped>
.app-main {
  /*50 = navbar  */
  min-height: calc(100vh - 50px);
  position: relative;
  overflow: hidden;
}

.pageHeader {
  padding-left: 20px;
  padding-top: 5px;
  padding-bottom: 0;
  margin: 0;
}
</style>

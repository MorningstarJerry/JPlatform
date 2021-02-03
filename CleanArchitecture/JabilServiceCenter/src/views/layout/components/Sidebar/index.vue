<template>
  <el-scrollbar wrapClass="scrollbar-wrapper">
    <div>
      <img class="LeftTopTitle" src="@/assets/logo7.png">
    </div>
      <!-- mode="vertical" -->
    <el-menu
      mode="vertical"
      :show-timeout="200"
      :default-active="$route.path"
      :collapse="isCollapse"
      background-color="#ebedf0"
    >
      <Sidebar-item v-for="route in permission_routes" :key="route.name" :item="route" :base-path="route.path"></Sidebar-item>
       <!-- <el-menu-item>
              <svg-icon icon-class="cloud-machine-fill"></svg-icon>
              <a href="http://cnhuam0appprd03/WSM/SCC/Workcell" target="_blank">E-Machines</a>
        </el-menu-item> -->
    </el-menu>
  </el-scrollbar>
</template>

<script>
import { mapGetters } from 'vuex'
import SidebarItem from './SidebarItem'

export default {
  components: { SidebarItem },
  computed: {
    ...mapGetters([
      'permission_routes',
      'sidebar'
    ]),
    routes() {
      return this.$router.options.routes
    },
    isCollapse() {
      return !this.sidebar.opened
    }
  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.LeftTopTitle {
        width: 230px;
        height: 48px;
      }
</style>

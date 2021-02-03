<template>
  <div class="createPost-container">
    <!-- <iframe
      id="ifra"
      name="ifra"
      class="frame"
      src="http://cnhuam0appprd03/WSM/SCC/Workcell"
      frameborder="0"
    ></iframe> -->


       <iframe ref="iframe" id="bdIframe" :src="bdTokenUrl" frameborder="0"></iframe>
  </div>
</template>

<script>
 /* eslint-disable */
export default {
  name: "Machine",
  inject: ["reload"],
  data() {
    return {
      bdTokenUrl: "https://app.powerbi.com/groups/me/reports/9e2562ba-a6f5-472d-8bf2-cc9db478802f?ctid=bc876b21-f134-4c12-a265-8ed26b7f0f3b"
      // https://app.powerbi.com/groups/me/reports/9e2562ba-a6f5-472d-8bf2-cc9db478802f?ctid=bc876b21-f134-4c12-a265-8ed26b7f0f3b
      // http://cnhuam0appprd03/WSM/SCC/Workcell
    };
  },
  mounted() {
       const oIframe = document.getElementById('bdIframe');
            const deviceWidth = document.documentElement.clientWidth;
            const deviceHeight = document.documentElement.clientHeight;
            oIframe.style.width = (Number(deviceWidth))-40 + 'px'; //数字是页面布局宽度差值
            oIframe.style.height = (Number(deviceHeight)-70) + 'px'; //数字是页面布局高度差
            this.$store.dispatch('ToggleSideBar');
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
}

.frame {
  width: 100%;
  height: 800px;
}
</style>

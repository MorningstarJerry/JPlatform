<template>
  <div class="createPost-container">
    <iframe
      ref="iframe"
      id="bdIframe"
      :src="bdTokenUrl"
      frameborder="0"
    ></iframe>
  </div>
</template>

<script>
 /* eslint-disable */
export default {
  name: "Moonshine",
  inject: ["reload"],
  data() {
    return {
      bdTokenUrl: "http://cnhuam0web10/moonshine/Default"
    };
  },
  mounted() {
    const oIframe = document.getElementById("bdIframe");
    const deviceWidth = document.documentElement.clientWidth;
    const deviceHeight = document.documentElement.clientHeight;
    oIframe.style.width = Number(deviceWidth)-40 + "px"; //数字是页面布局宽度差值
    oIframe.style.height = Number(deviceHeight) - 70 + "px"; //数字是页面布局高度差
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

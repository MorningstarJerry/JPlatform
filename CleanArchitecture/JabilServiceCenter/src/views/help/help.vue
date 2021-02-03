<template>
  <div class="app-container">
      <div>
          <h1>Video Tutorials</h1>
          <h3>Here you can find all video tutorials that will guide you through IE Platform.</h3>


      </div>
      <div>
      <!-- <vue-core-video-player src="./your_video_source.mp4"></vue-core-video-player> -->
      <!-- <vue-player src="http://techslides.com/demos/sample-videos/small.mp4" poster="https://via.placeholder.com/150" title="this is a title" v-model="playing"></vue-player> -->
       <video-player  class="video-player-box"
                 ref="videoPlayer"
                 :options="playerOptions"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>

        <video-player  class="video-player-box"
                 ref="videoPlayer1"
                 :options="playerOptions1"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>

        <video-player  class="video-player-box"
                 ref="videoPlayer2"
                 :options="playerOptions2"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>
         <video-player  class="video-player-box"
                 ref="videoPlayer3"
                 :options="playerOptions3"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>
         <video-player  class="video-player-box"
                 ref="videoPlayer4"
                 :options="playerOptions4"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>
          <video-player  class="video-player-box"
                 ref="videoPlayer5"
                 :options="playerOptions5"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>
          <video-player  class="video-player-box"
                 ref="videoPlayer6"
                 :options="playerOptions6"
                 :playsinline="true"
                 customEventName="customstatechangedeventname"
                    >
        </video-player>
        </div>
        <div class="footer">
        </div>
    </div>
 
</template>
<script>
/* eslint-disable */
//   import VueCoreVideoPlayer from 'vue-core-video-player'
// import CanvasVideo from "vue-canvasvideo";
// import "vue-canvasvideo/dist/vuecanvasvideo.min.css";
import 'video.js/dist/video-js.css'
import { videoPlayer } from 'vue-video-player'

export default {
  name: "help",
  components: {
    videoPlayer
  },
    data() {
      return {
        playerOptions: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://10.114.18.244/Medias/Platform.mp4"
          }],
          poster: "http://10.114.18.244/Medias/Dashboard.png",
        },
           playerOptions1: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://10.114.18.244/Medias/Setting.mp4"
          }],
          poster: "http://10.114.18.244/Medias/Setting.png",
        },
         playerOptions2: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://10.114.18.244/Medias/Dfa.mp4"
          }],
          poster: "http://10.114.18.244/Medias/Dfa.png",
        },
         playerOptions3: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://10.114.18.244/Medias/Rfq.mp4"
          }],
          poster: "http://10.114.18.244/Medias/Rfq.png",
        },
        playerOptions4: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://10.114.18.244/Medias/Downtime.mp4"
          }],
          poster: "http://10.114.18.244/Medias/Downtimes.png",
        },
         playerOptions5: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://10.114.18.244/Medias/Smed.mp4"
          }],
          poster: "http://10.114.18.244/Medias/Smed.png",
        },
         playerOptions6: {
          // videojs options
          muted: true,
          language: 'en',
          width: 300,
          height: 300,
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          metadata: 'test',
          sources: [{
            type: "video/mp4",
            src: "http://localhost/Medias/spc.mp4"
          }],
          poster: "http://localhost/Medias/CleanRoom.png",
        }
      }
    },
    mounted() {
        this.$store.dispatch('ToggleSideBar');
    },
    computed: {
      player() {
        return this.$refs.videoPlayer.player
      }
    },
    methods: {
      // listen event
      onPlayerPlay(player) {
        // console.log('player play!', player)
      },
      onPlayerPause(player) {
        // console.log('player pause!', player)
      },
      // ...player event

      // or listen state event
      playerStateChanged(playerCurrentState) {
        // console.log('player current update state', playerCurrentState)
      },

      // player is ready
      playerReadied(player) {
        console.log('the player is readied', player)
        // you can use it to do something...
        // player.[methods]
      }
    }
};
</script>
<style lang="scss" scoped>
@import "~@/styles/mixin.scss";
.player-container {
  width: 100%;
}
.video-player-box{
    float: left;
    margin-left: 20px;
    margin-top: 20px;
}
.footer{
    clear: both;
 height: 150px;
 float: left;
}
</style>

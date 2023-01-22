<template>
  <div>
    <Sidebar />
    <Header :title="'Live Streaming'" />
    <div style="margin-left: 50px">
      <b-col style="text-align: start">
        <b-row class="p-3">
          <h5 class="pr-3">Layout :</h5>

          <b-button-group size="sm">
            <b-button variant="outline-secondary" @click="setSelected(1)">
              <b-icon icon="dice1" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(2)">
              <b-icon icon="dice2" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(4)">
              <b-icon icon="dice4" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(6)">
              <b-icon icon="dice6" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(9)">
              <b-icon icon="grid3x3" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(-1)">
              <b-icon icon="ui-checks-grid" />
            </b-button>
            <b-button variant="outline-secondary" @click="toggle()">
              <b-icon icon="fullscreen" />
            </b-button>
          </b-button-group>
        </b-row>
        <b-row class="fullscreen-wrapper">
          <Video
            v-for="(gate, index) in optionsSelected"
            :key="index"
            :index="index"
            :md="mdSelected"
            v-show="live"
          />
          <VideoSetup
            v-for="(gate, index) in optionsSelected"
            :key="setup + index"
            :index="index"
            :md="mdSelected"
            v-show="!live"
          />
        </b-row>
      </b-col>
    </div>
  </div>
</template>

<script>
import Header from "../components/Header";
import Sidebar from "../components/Sidebar";
import Video from "../components/Video";
import VideoSetup from "../components/VideoSetup.vue";
import { api as fullscreen } from "vue-fullscreen";

export default {
  name: "Camera",
  components: { Sidebar, Header, Video, VideoSetup },
  data() {
    return {
      live: true,
      fullscreen: false,
      optionsSelected: 1,
      mdSelected: 6,
    };
  },
  mounted() {
    this.onInit();
  },
  methods: {
    async toggle() {
      await fullscreen.toggle(this.$el.querySelector(".fullscreen-wrapper"), {
        teleport: this.teleport,
        pageOnly: this.pageOnly,
        callback: (isFullscreen) => {
          this.fullscreen = isFullscreen;
          console.log(fullscreen.element);
          console.log(fullscreen.isFullscreen);
        },
      });
      this.fullscreen = fullscreen.isFullscreen;
    },

    async onInit() {
      await this.checkLogin();
      this.live = true;
      this.optionsSelected = 1;
      this.mdSelected = 6;
    },

    setSelected(selected) {
      if (selected != null) {
        this.optionsSelected = selected;
        if (this.optionsSelected == 1) {
          this.mdSelected = 6;
          this.live = true;
        } else if (this.optionsSelected == 2) {
          this.mdSelected = 6;
          this.live = true;
        } else if (this.optionsSelected == 4) {
          this.mdSelected = 6;
          this.live = true;
        } else if (this.optionsSelected == 6) {
          this.mdSelected = 4;
          this.live = true;
        } else if (this.optionsSelected == 9) {
          this.mdSelected = 4;
          this.live = true;
        } else if (this.optionsSelected == -1) {
          this.live = false;
          this.optionsSelected = 9;
          this.mdSelected = 4;
        }
      }
    },
  },
};
</script>

<style scoped></style>
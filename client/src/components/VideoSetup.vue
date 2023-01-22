<template>
  <b-col :md="md" class="mb-3">
    <b-overlay :show="!loadingList" rounded="sm">
      <b-card class="center_card">
        <b-form-select
          v-model="videoSelected"
          v-on:change="selected"
          style="width: 100%"
        >
          <option :value="null">
            -- Pilih Camera {{ this.index + 1 }} --
          </option>
          <option
            v-for="video in videoList"
            :value="video.name"
            :key="video.name"
          >
            {{ video.name }}
          </option>
        </b-form-select>
      </b-card>
    </b-overlay>
  </b-col>
</template>

<script>
import CameraService from "../services/CameraServices";

export default {
  name: "VideoSetup",
  props: {
    md: Number,
    index: Number,
  },
  data() {
    return {
      loadingTableList: false,
      videoList: [],
      videoSelected: null,
      activeNames: ["1"],
      video_url: "http://127.0.0.1:5000/api/camera/",
    };
  },

  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      await this.getCameraCameraList();
    },

    selected() {
      if (this.videoSelected == null)
        this.$session.set("CAMERA" + this.index, this.video_url + "index.m3u8");
      else
        this.$session.set(
          "CAMERA" + this.index,
          this.video_url + this.videoSelected + "/index.m3u8"
        );
    },

    async getCameraCameraList() {
      this.loadingTableList = false;
      CameraService.getCameraCameraList(this.baseApi, this.jwtToken)
        .then((response) => {
          this.loadingList = true;
          this.videoList = response.data.data;
        })
        .catch((error) => {
          console.log(error);
          this.loadingTableList = true;
          let text = error.response.data.error;
          if (text == "Network Error") {
            window.location.href = this.baseApi;
          }
        });
    },
  },
};
</script>

<style scoped>
.input_video {
  height: 100%;
}

.center_card {
  margin: 0 auto;
  float: none;
  border: none;
  background-color: #e0e0e0;
  /* min-height: 40vh; */
}

.card-body {
  padding: 0;
}
</style>
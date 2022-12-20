<template>
  <div>
    <b-overlay :show="!loadingList" rounded="sm">
      <b-card class="center_card">
        <b-row align-h="between" align-v="center" class="mx-1 my-2">
          <b-form-select v-model="videoSelected" style="width: 150px">
            <option :value="null" disabled>-- Pilih CCTV --</option>
            <option
              v-for="video in videoList"
              :value="video.cctvUrl"
              :key="video.cctvName"
            >
              {{ video.cctvName }}
            </option>
          </b-form-select>
          <b-button
            v-b-tooltip.hover
            title="Perbesar"
            variant="link"
            style="color: grey"
            v-b-modal="'modal-video-' + videoSelected"
          >
            <b-icon icon="arrows-fullscreen" font-scale="1"></b-icon>
          </b-button>

          <b-modal :id="'modal-video-' + videoSelected" size="lg" hide-footer>
            <b-embed type="iframe" :src="videoSelected" allowfullscreen />
          </b-modal>
        </b-row>
        <b-embed
          type="iframe"
          aspect="16by9"
          :src="videoSelected"
          allowfullscreen
        ></b-embed>
      </b-card>
    </b-overlay>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import CCTVService from "../services/CCTVServices";

export default {
  name: "Video",
  data() {
    return {
      videoSelected: null,
      videoList: [],
    };
  },
  mounted() {
    this.onInit();
  },
  methods: {
    async onInit() {
      await this.getCCTVCameraList();
    },

    async getCCTVCameraList() {
      this.loadingTableList = false;
      CCTVService.getCCTVCameraList(this.baseApi, this.jwtToken)
        .then((response) => {
          // console.log(response);
          this.loadingList = true;
          this.videoList = response.data.data;
        })
        .catch((error) => {
          // console.log(error);
          this.loadingTableList = true;
          let text = error.response.data.error;
          let statusCode = error.response.status;
          if (text == "Network Error") {
            window.location.href = this.baseApi;
          }
          if (statusCode == 401) {
            Swal.fire({
              icon: "error",
              title: "Error " + statusCode + " - Unauthorized",
              text: "Sorry, Your request could not be processed.",
            });
          }
          if (statusCode != 200 && statusCode != 401) {
            Swal.fire({
              icon: "error",
              title: "Error " + statusCode,
              text: text,
            });
          }
        });
    },
  },
};
</script>

<style scoped>
.center_card {
  margin: 0 auto;
  float: none;
  border: none;
  background-color: #e0e0e0;
}
</style>
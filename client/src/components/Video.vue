<template>
  <div>
    <b-overlay :show="!loadingList" rounded="sm">
      <b-card class="center_card">
        <b-row align-h="between" align-v="center" class="mx-1 my-2">
          <b-form-select v-model="videoSelected" style="width: 150px">
            <option :value="null" disabled>-- Pilih Camera --</option>
            <option
              v-for="video in videoList"
              :value="video.videoUrl"
              :key="video.name"
            >
              {{ video.name }}
            </option>
          </b-form-select>
          <b-button
            v-if="videoSelected != null"
            v-b-tooltip.hover
            title="Perbesar"
            variant="link"
            style="color: grey"
            v-b-modal="'modal-video-' + videoSelected"
          >
            <b-icon icon="arrows-fullscreen" font-scale="1"></b-icon>
          </b-button>

          <b-modal :id="'modal-video-' + videoSelected" size="xl" hide-footer>
            <!-- <b-embed
              type="iframe"
              :src="videoSelected"
              allowfullscreen
              aspect="16by9"
            /> -->
            <b-img
              :src="videoSelected + '?' + Date.now()"
              fluid-grow
              v-if="renderComponent"
              alt=""
            ></b-img>
          </b-modal>
        </b-row>
        <!-- <b-embed
          type="iframe"
          :src="videoSelected"
          allowfullscreen
          aspect="16by9"
        ></b-embed> -->
        <b-img
          :src="videoSelected + '?' + Date.now()"
          fluid-grow
          v-if="renderComponent"
          alt=""
        ></b-img>
      </b-card>
    </b-overlay>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import CameraService from "../services/CameraServices";

export default {
  name: "Video",
  data() {
    return {
      auto: 5000,
      renderComponent: true,
      videoSelected: null,
      videoList: [],
    };
  },

  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      await this.getCameraCameraList();
      this.ForcesUpdateComponent();
    },

    async getCameraCameraList() {
      this.loadingTableList = false;
      CameraService.getCameraCameraList(this.baseApi, this.jwtToken)
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

    ForcesUpdateComponent() {
      this.renderComponent = false;

      setTimeout(() => this.ForcesUpdateComponent(), this.auto);
      this.$nextTick(() => {
        this.renderComponent = true;
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
  min-height: 40vh;
}
</style>
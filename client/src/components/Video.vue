<template>
  <video-player
    class="video-player vjs-custom-skin"
    ref="videoPlayer"
    :playsinline="true"
    :options="playerOptions"
  >
  </video-player>
  <!-- <div>
    <b-overlay :show="!loadingList" rounded="sm">
      <b-card class="center_card">
        <div class="input_video">
          <b-form-select
            v-model="videoSelected"
            v-on:change="selected"
            style="width: 150px"
          >
            <option :value="null" disabled>-- Pilih Camera --</option>
            <option
              v-for="video in videoList"
              :value="video.name"
              :key="video.name"
            >
              {{ video.name }}
            </option>
          </b-form-select>
          <br />
        </div>
        <video-player
          class="video-player vjs-custom-skin"
          ref="videoPlayer"
          :playsinline="true"
          :options="playerOptions"
        >
        </video-player>
      </b-card>
    </b-overlay>
  </div> -->
</template>

<script>
// import Swal from "sweetalert2";
// import CameraService from "../services/CameraServices";

export default {
  name: "Video",
  data() {
    return {
      videoSelected: null,
      playerOptions: {
        autoplay: true,
        muted: true,
        loop: false,
        preload: "auto",
        aspectRatio: "16:9",
        fluid: true,
        liveui: true,
        liveTracker: true,
        sources: [
          {
            type: "application/x-mpegURL",
            src: "/index.m3u8",
          },
        ],
        controlBar: {
          timeDivider: false,
          durationDisplay: false,
          remainingTimeDisplay: false,
          fullsreenToggle: true,
        },
      },
      activeNames: ["1"],
    };
  },

  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      await this.getCameraCameraList();
      // this.ForcesUpdateComponent();
    },

    selected() {
      this.playerOptions.sources[0].src =
        "http://127.0.0.1/videos/" + this.videoSelected + "/index.m3u8";

      // CameraService.getCameraStreaming(
      //   this.baseApi,
      //   `?name=` + this.videoSelected,
      //   this.jwtToken
      // );
    },

    // async getCameraCameraList() {
    //   this.loadingTableList = false;
    //   CameraService.getCameraCameraList(this.baseApi, this.jwtToken)
    //     .then((response) => {
    //       // console.log(response);
    //       this.loadingList = true;
    //       this.videoList = response.data.data;
    //     })
    //     .catch((error) => {
    //       // console.log(error);
    //       this.loadingTableList = true;
    //       let text = error.response.data.error;
    //       let statusCode = error.response.status;
    //       if (text == "Network Error") {
    //         window.location.href = this.baseApi;
    //       }
    //       if (statusCode == 401) {
    //         Swal.fire({
    //           icon: "error",
    //           title: "Error " + statusCode + " - Unauthorized",
    //           text: "Sorry, Your request could not be processed.",
    //         });
    //       }
    //       if (statusCode != 200 && statusCode != 401) {
    //         Swal.fire({
    //           icon: "error",
    //           title: "Error " + statusCode,
    //           text: text,
    //         });
    //       }
    //     });
    // },

    // ForcesUpdateComponent() {
    //   this.renderComponent = false;

    //   setTimeout(() => this.ForcesUpdateComponent(), this.auto);
    //   this.$nextTick(() => {
    //     this.renderComponent = true;
    //   });
    // },
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
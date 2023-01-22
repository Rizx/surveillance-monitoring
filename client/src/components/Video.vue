<template>
  <b-col class="remove-padding-margin" :md="md">
    <video-player
      class="video-player vjs-custom-skin"
      ref="videoPlayer"
      :playsinline="true"
      :options="playerOptions"
    >
    </video-player>
  </b-col>
</template>

<script>
export default {
  name: "Video",
  props: {
    md: Number,
    index: Number,
  },
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
      this.playerOptions.sources[0].src = this.$session.get(
        "CAMERA" + this.index
      );
    },
  },
};
</script>

<style scoped></style>
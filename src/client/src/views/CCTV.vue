<template>
  <div>
    <Sidebar />
    <Header :title="'CCTV Monitoring'" />
    <div style="margin-left: 50px">
      <b-col style="text-align: start">
        <b-row class="p-3">
          <h5 class="pr-3">Layout :</h5>
          <v-select
            :options="options"
            v-model="optionsSelected"
            @input="setSelected"
          />
        </b-row>
        <div>
          <b-row class="remove-padding-margin" align-h="between">
            <b-col
              v-for="(gate, index) in optionsSelected"
              :key="index"
              :md="mdSelected"
              class="mb-3"
            >
              <Video />
            </b-col>
          </b-row>
        </div>
      </b-col>
    </div>
  </div>
</template>

<script>
import Sidebar from "../components/Sidebar";
import Header from "../components/Header";
import Video from "../components/Video";

export default {
  name: "CCTV",
  components: { Sidebar, Header, Video },
  data() {
    return {
      options: [2, 4, 6, 8],
      optionsSelected: null,
      mdSelected: null,
    };
  },
  mounted() {
    this.onInit();
  },
  methods: {
    async onInit() {
      await this.checkLogin();
      this.optionsSelected = 2;
      this.mdSelected = 6;
    },

    setSelected(value) {
      this.optionsSelected = value;
      if (this.optionsSelected == 2) {
        this.mdSelected = 6;
      } else if (this.optionsSelected == 4) {
        this.mdSelected = 4;
      } else if (this.optionsSelected == 6) {
        this.mdSelected = 4;
      } else if (this.optionsSelected == 8) {
        this.mdSelected = 3;
      }
    },
  },
};
</script>


<style scoped>
</style>
<template>
  <div>
    <Sidebar />
    <!-- <Header :title="'Live Streaming'" /> -->
    <div style="margin-left: 50px">
      <b-col style="text-align: start">
        <b-row class="p-3">
          <h5 class="pr-3">Layout :</h5>
          <!-- <v-select
            :options="options"
            label="title"
            v-model="optionsSelected"
            @input="setSelected"
          >
            <template v-slot:option="option">
              <b-icon :icon="option.icon"></b-icon>
            </template>
          </v-select> -->

          <b-button-group size="sm">
            <b-button variant="outline-secondary" @click="setSelected(1)">
              <b-icon icon="box" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(2)">
              <b-icon icon="bounding-box" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(4)">
              <b-icon icon="grid" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(6)">
              <b-icon icon="grid3x2" />
            </b-button>
            <b-button variant="outline-secondary" @click="setSelected(9)">
              <b-icon icon="grid3x3" />
            </b-button>
          </b-button-group>
        </b-row>
        <div>
          <b-row>
            <b-col
              v-for="(gate, index) in optionsSelected"
              :key="index"
              :md="mdSelected"
              class="remove-padding-margin"
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
// import Header from "../components/Header";
import Video from "../components/Video";

export default {
  name: "Camera",
  components: { Sidebar, Video },
  data() {
    return {
      options: [
        {
          title: "2",
          value: 2,
          icon: "bounding-box",
        },
        {
          title: "4",
          value: 4,
          icon: "grid",
        },
        {
          title: "6",
          value: 6,
          icon: "grid3x2",
        },
        {
          title: "8",
          value: 8,
          icon: "grid3x3",
        },
      ],
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

    setSelected(selected) {
      if (selected != null) {
        this.optionsSelected = selected;
        if (this.optionsSelected == 1) {
          this.mdSelected = 6;
        } else if (this.optionsSelected == 2) {
          this.mdSelected = 6;
        } else if (this.optionsSelected == 4) {
          this.mdSelected = 6;
        } else if (this.optionsSelected == 6) {
          this.mdSelected = 4;
        } else if (this.optionsSelected == 9) {
          this.mdSelected = 4;
        }
      }
    },
  },
};
</script>

<style scoped></style>
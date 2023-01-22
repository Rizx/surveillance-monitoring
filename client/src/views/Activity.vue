<template>
  <div>
    <Sidebar />
    <Header :title="'Activity'" />
    <div style="margin-left: 50px">
      <b-container fluid>
        <b-skeleton-table
          class="mt-3"
          v-if="!loadingTableList"
          :rows="totalRows"
          :columns="3"
          :table-props="{ bordered: true, striped: true }"
        ></b-skeleton-table>
        <ActivityTable
          v-if="loadingTableList"
          :items="items"
          :fields="fields"
          :currentPage="currentPage"
          :perPage="perPage"
          :totalRows="totalRows"
        />
        <b-pagination
          v-model="currentPage"
          :total-rows="totalRows"
          :per-page="perPage"
          aria-controls="table"
          align="right"
        ></b-pagination>
      </b-container>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import Sidebar from "../components/Sidebar";
import Header from "../components/Header";
import ActivityTable from "../components/ActivityTable";
import ActivityService from "../services/ActivityServices";

export default {
  name: "Activity",
  components: { Sidebar, Header, ActivityTable },
  data() {
    return {
      currentPage: 1,
      perPage: 20,
      totalRows: 1,
      items: [],
      options: [],
      fields: [
        { key: "activity", label: "AKTIVITAS" },
        { key: "cardid", label: "No Kartu" },
        { key: "name", label: "NAMA" },
        { key: "address", label: "NO RUMAH" },
        { key: "date", label: "WAKTU" },
        { key: "state", label: "STATUS" },
        { key: "gambar", label: "GAMBAR" },
        { key: "action", label: "ACTION" },
      ],
    };
  },

  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      await this.checkLogin();
      await this.getTransactionList();
    },

    async getTransactionList() {
      this.loadingTableList = false;
      ActivityService.getTransactionList(this.baseApi, this.jwtToken)
        .then((response) => {
          // console.log(response);
          this.loadingTableList = true;
          this.totalRows = response.data.data.length;
          this.items = response.data.data;
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

<style></style>
<template>
  <div>
    <Sidebar />
    <Header :title="'Manajemen Warga'" />
    <div style="margin-left: 50px">
      <b-container fluid>
        <b-row class="mx-1 mt-4" align-h="end">
          <div>
            <b-button
              variant="info"
              size="sm"
              class="mx-2"
              @click="clear(), showModal()"
            >
              <i class="fas fa-people-group mr-1" />
              Tambah Warga
            </b-button>
            <b-modal
              :id="'new-warga'"
              ref="modal"
              v-model="modalShow"
              hide-header-close
              title="Tambah Warga"
              ok-title="Simpan"
              @ok="handleOk"
            >
              <form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group
                  label="Nama :"
                  label-for="form-ip-cctv"
                  invalid-feedback="Nama Warga Wajib Diisi"
                  :state="wargaNameState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-ip-cctv"
                    placeholder="Masukkan Nama Warga"
                    v-model="wargaBody.wargaName"
                    :state="wargaNameState"
                    required
                  ></b-form-input>
                </b-form-group>

                <b-form-group
                  label="Nomor Rumah :"
                  label-for="form-name-cctv"
                  invalid-feedback="Nomor Rumah Wajib Diisi"
                  :state="noHouseState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-name-cctv"
                    placeholder="Masukkan Nomor Rumah"
                    v-model="wargaBody.noHouse"
                    :state="noHouseState"
                    required
                  ></b-form-input>
                </b-form-group>

                <b-form-group
                  label="Nomor Kartu :"
                  label-for="form-name-cctv"
                  invalid-feedback="Nomor Kartu Wajib Diisi"
                  :state="noCardState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-name-cctv"
                    placeholder="Masukkan Nomor Kartu"
                    v-model="wargaBody.noCard"
                    :state="noCardState"
                    required
                  ></b-form-input>
                </b-form-group>
              </form>
            </b-modal>
          </div>
        </b-row>
        <b-skeleton-table
          class="mt-3"
          v-if="!loadingTableList"
          :rows="totalRows"
          :columns="3"
          :table-props="{ bordered: true, striped: true }"
        ></b-skeleton-table>
        <TableListWarga
          v-if="loadingTableList"
          :items="items"
          :fields="fields"
          :currentPage="currentPage"
          :perPage="perPage"
          :totalRows="totalRows"
        />
      </b-container>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import Sidebar from "../components/Sidebar";
import Header from "../components/Header";
import TableListWarga from "../components/TableListWarga";
import WargaService from "../services/WargaServices";

export default {
  name: "ManajemenWarga",
  components: { Sidebar, Header, TableListWarga },
  data() {
    return {
      modalShow: false,
      wargaNameState: null,
      noHouseState: null,
      noCardState: null,

      wargaBody: {
        id: 0,
        wargaName: "",
        noHouse: "",
        noCard: "",
      },

      currentPage: 1,
      perPage: 20,
      totalRows: 1,
      items: [],
      fields: [
        {
          key: "nama_warga",
          label: "NAMA",
        },
        {
          key: "nomor_rumah",
          label: "NOMOR RUMAH",
        },
        {
          key: "nomor_kartu",
          label: "NOMOR KARTU",
        },
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
      await this.getWargaList();
    },

    async clear() {
      this.wargaBody = {
        id: 0,
        wargaName: "",
        noHouse: "",
        noCard: "",
      };
    },

    showModal() {
      this.modalShow = true;
    },

    handleOk(bvModalEvent) {
      bvModalEvent.preventDefault();
      this.handleSubmit();
    },

    handleSubmit() {
      if (!this.checkFormValidity()) {
        return;
      } else {
        this.modalShow = false;
        this.postWargaRegister();
      }
    },

    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.wargaNameState = valid;
      this.noHouseState = valid;
      this.noCardState = valid;
      return valid;
    },
    
    async getWargaList() {
      this.loadingTableList = false;
      WargaService.getWargaList(this.baseApi, this.jwtToken)
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

    async postWargaRegister() {
      this.loadingTableList = false;
      WargaService.postWargaRegister(this.baseApi, this.jwtToken, this.userBody)
        .then((response) => {
          if (response.data.success == true) {
            Swal.fire({
              icon: "success",
              title: "Success !",
              text:
                "Warga " +
                this.wargaBody.wargaName +
                " have been successfully Added",
            });
            this.getUserList();
          }
        })
        .catch((error) => {
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
      this.clear();
    },
  },
};
</script>

<style>
</style>
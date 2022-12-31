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
                  label="Username :"
                  label-for="form-username"
                  invalid-feedback="Username Wajib Diisi"
                  :state="wargaUsernameState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-username"
                    placeholder="Masukkan Username"
                    v-model="wargaBody.username"
                    :state="wargaUsernameState"
                    required
                  ></b-form-input>
                </b-form-group>

                <b-form-group label="Password :" label-for="form-password">
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-password"
                    placeholder="Masukkan Password"
                    v-model="wargaBody.password"
                  ></b-form-input>
                </b-form-group>

                <b-form-group
                  label="Nama :"
                  label-for="form-ip-camera"
                  invalid-feedback="Nama Warga Wajib Diisi"
                  :state="fullnameState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-ip-camera"
                    placeholder="Masukkan Nama Warga"
                    v-model="wargaBody.fullname"
                    :state="fullnameState"
                    required
                  ></b-form-input>
                </b-form-group>

                <b-form-group
                  label="Nomor Rumah :"
                  label-for="form-name-camera"
                  invalid-feedback="Nomor Rumah Wajib Diisi"
                  :state="addressState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-name-camera"
                    placeholder="Masukkan Nomor Rumah"
                    v-model="wargaBody.address"
                    :state="addressState"
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
        <PeopleTable
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
import PeopleTable from "../components/PeopleTable";
import PeopleService from "../services/PeopleServices";

export default {
  name: "PeopleManagement",
  components: { Sidebar, Header, PeopleTable },
  data() {
    return {
      modalShow: false,
      fullnameState: null,
      addressState: null,

      wargaBody: {
        id: 0,
        fullname: "",
        address: "",
        username: "",
        password: "",
      },

      currentPage: 1,
      perPage: 20,
      totalRows: 1,
      items: [],
      fields: [
        {
          key: "username",
          label: "USERNAME",
        },
        {
          key: "fullname",
          label: "NAMA",
        },
        {
          key: "address",
          label: "NO RUMAH",
        },
        {
          key: "cardid",
          label: "NO KARTU",
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
      this.fullnameState = valid;
      this.addressState = valid;
      return valid;
    },

    async getWargaList() {
      this.loadingTableList = false;
      PeopleService.getWargaList(this.baseApi, this.jwtToken)
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
      PeopleService.postWargaRegister(
        this.baseApi,
        this.jwtToken,
        this.wargaBody
      )
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

<style></style>
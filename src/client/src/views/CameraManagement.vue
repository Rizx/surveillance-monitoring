<template>
  <div>
    <Sidebar />
    <Header :title="'Manajemen Camera'" />
    <div style="margin-left: 50px">
      <b-container fluid>
        <!-- <b-row class="mx-1 mt-4" align-h="end">
          <div>
            <b-button
              variant="info"
              size="sm"
              class="mx-2"
              @click="clear(), showModal()"
            >
              <i class="fas fa-video mr-1" />
              Tambah Camera
            </b-button>
            <b-modal
              :id="'new-camera'"
              ref="modal"
              v-model="modalShow"
              hide-header-close
              title="Tambah Camera"
              ok-title="Simpan"
              @ok="handleOk"
            >
              <form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group
                  label="IP :"
                  label-for="form-ip-camera"
                  invalid-feedback="IP Camera Wajib Diisi"
                  :state="cameraIpState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-ip-camera"
                    placeholder="Masukkan IP Camera"
                    v-model="cameraBody.cameraIp"
                    :state="cameraIpState"
                    required
                  >
                  </b-form-input>
                </b-form-group>

                <b-form-group
                  label="Nama :"
                  label-for="form-name-camera"
                  invalid-feedback="Nama Camera Wajib Diisi"
                  :state="cameraNameState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-name-camera"
                    placeholder="Masukkan Nama Camera"
                    v-model="cameraBody.cameraName"
                    :state="cameraNameState"
                    required
                  >
                  </b-form-input>
                </b-form-group>

                <b-form-group label="Username :" label-for="form-username-camera">
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-username-camera"
                    placeholder="Masukkan Username Camera"
                    v-model="cameraBody.cameraUsername"
                  >
                  </b-form-input>
                </b-form-group>

                <b-form-group label="Password :" label-for="form-password-camera">
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-password-camera"
                    placeholder="Masukkan Password Camera"
                    v-model="cameraBody.cameraPassword"
                  >
                  </b-form-input>
                </b-form-group>
              </form>
            </b-modal>
          </div>
        </b-row> -->
        <b-skeleton-table
          class="mt-3"
          v-if="!loadingTableList"
          :rows="totalRows"
          :columns="3"
          :table-props="{ bordered: true, striped: true }"
        >
        </b-skeleton-table>
        <TableListCamera
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
import TableListCamera from "../components/TableListCamera";
import CameraService from "../services/CameraServices";

export default {
  name: "CameraManagement",
  components: { Sidebar, Header, TableListCamera },
  data() {
    return {
      modalShow: false,
      cameraIpState: null,
      cameraNameState: null,

      cameraBody: {
        id: 0,
        cameraIp: "",
        cameraName: "",
        cameraUsername: "",
        cameraPassword: "",
      },

      currentPage: 1,
      perPage: 100,
      totalRows: 1,
      items: [],
      fields: [
        {
          key: "name",
          label: "NAMA",
        },
        {
          key: "model",
          label: "Model",
        },
        {
          key: "ipAddress",
          label: "IP ADDRESS",
        },
        {
          key: "username",
          label: "USERNAME",
        },
        {
          key: "password",
          label: "PASSWORD",
        },
      ],
    };
  },

  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      await this.checkLogin();
      await this.getCameraList();
    },

    async clear() {
      this.cameraBody = {
        id: 0,
        cameraIp: "",
        cameraName: "",
        cameraUsername: "",
        cameraPassword: "",
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
      }
    },

    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.cameraIpState = valid;
      this.cameraNameState = valid;
      return valid;
    },

    async getCameraList() {
      this.loadingTableList = false;
      CameraService.getCameraList(this.baseApi, this.jwtToken)
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

    async postCameraRegister() {
      this.loadingTableList = false;
      CameraService.postCameraRegister(this.baseApi, this.jwtToken, this.userBody)
        .then((response) => {
          if (response.data.success == true) {
            Swal.fire({
              icon: "success",
              title: "Success !",
              text:
                "Camera " +
                this.cameraBody.cameraName +
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

<style scoped></style>
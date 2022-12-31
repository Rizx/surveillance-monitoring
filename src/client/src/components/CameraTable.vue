<template>
  <div class="mt-4">
    <b-table
      small
      head-variant="dark"
      :items="items"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
      :sort-direction="sortDirection"
      striped
      hover
      responsive
    >
      <template #cell(action)="row">
        <b-button
          size="sm"
          variant="success"
          class="mr-1"
          v-b-tooltip.hover
          title="Edit"
          v-b-modal="'edit-camera-modal-' + row.item.id"
        >
          <i class="fas fa-pen"> </i>
        </b-button>

        <b-modal
          :id="'edit-camera-modal-' + row.item.id"
          hide-header-close
          :title="'Edit Camera - ' + row.item.name_camera"
          ok-title="Simpan"
          @ok="postCameraUpdate(row.item)"
        >
          <form ref="form">
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
                v-model="row.item.ip_camera"
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
                v-model="row.item.name_camera"
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
                v-model="row.item.cameraUsername"
              >
              </b-form-input>
            </b-form-group>

            <b-form-group label="Password :" label-for="form-password-camera">
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-password-camera"
                placeholder="Masukkan Password Camera"
                v-model="row.item.cameraPassword"
              >
              </b-form-input>
            </b-form-group>
          </form>
        </b-modal>
      </template>
    </b-table>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import CameraService from "../services/CameraServices";

export default {
  name: "CameraTable",
  props: {
    items: Array,
    fields: Array,
    currentPage: Number,
    perPage: Number,
    totalRows: Number,
  },
  data() {
    return {
      pageOptions: [5, 10, 20, 50, 100],
      sortBy: "",
      sortDesc: false,
      sortDirection: "desc",

      modalShow: false,
      cameraIpState: null,
      cameraNameState: null,
    };
  },
  mounted() {},

  methods: {
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
      this.cameraIpState = valid;
      this.cameraNameState = valid;
      return valid;
    },

    postCameraUpdate(item) {
      this.loadingTableList = true;
      CameraService.postCameraUpdate(this.baseApi, this.jwtToken, {
        id: item.id,
        ip_camera: item.ip_camera,
        name_camera: item.name_camera,
        username: item.username,
        password: item.password,
      })
        .then((response) => {
          if (response.data.success == true) {
            this.loadingTableList = false;
            Swal.fire({
              icon: "success",
              title: "Success !",
              text: "Camera " + item.name_camera + " have been successfully Edited",
            });
          }
        })
        .catch((error) => {
          let text = error.response.data.error;
          let statusCode = error.response.status;
          if (text == "Network Error") {
            window.location.href = this.baseApi;
          }
          if (statusCode != 200) {
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

<style>
#edit-user-modal___BV_modal_header_ {
  background-color: #11cdef !important;
  border-bottom: 0 none;
}

#edit-user-modal___BV_modal_footer_ {
  border-top: 0 none;
}

#edit-user-modal___BV_modal_footer_ > button.btn-secondary {
  color: #212529;
  background-color: #f7fafc;
  border-color: #f7fafc;
  box-shadow: 0 4px 6px rgb(50 50 93 / 11%), 0 1px 3px rgb(0 0 0 / 8%);
}

#edit-user-modal___BV_modal_footer_ > button.btn-primary {
  color: #fff;
  background-color: #5e72e4;
  border-color: #5e72e4;
  box-shadow: 0 4px 6px rgb(50 50 93 / 11%), 0 1px 3px rgb(0 0 0 / 8%);
}

.input-password {
  border-right: none !important;
}

.input-append-icon {
  background: transparent !important;
  border-left: none !important;
  cursor: pointer;
}
</style>
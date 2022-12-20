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
          v-b-modal="'edit-cctv-modal-' + row.item.id"
        >
          <i class="fas fa-pen"> </i>
        </b-button>

        <b-modal
          :id="'edit-cctv-modal-' + row.item.id"
          hide-header-close
          :title="'Edit CCTV - ' + row.item.name_camera"
          ok-title="Simpan"
          @ok="postCCTVUpdate(row.item)"
        >
          <form ref="form">
            <b-form-group
              label="IP :"
              label-for="form-ip-cctv"
              invalid-feedback="IP CCTV Wajib Diisi"
              :state="cameraIpState"
            >
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-ip-cctv"
                placeholder="Masukkan IP CCTV"
                v-model="row.item.ip_camera"
                :state="cameraIpState"
                required
              >
              </b-form-input>
            </b-form-group>

            <b-form-group
              label="Nama :"
              label-for="form-name-cctv"
              invalid-feedback="Nama CCTV Wajib Diisi"
              :state="cameraNameState"
            >
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-name-cctv"
                placeholder="Masukkan Nama CCTV"
                v-model="row.item.name_camera"
                :state="cameraNameState"
                required
              >
              </b-form-input>
            </b-form-group>

            <b-form-group label="Username :" label-for="form-username-cctv">
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-username-cctv"
                placeholder="Masukkan Username CCTV"
                v-model="row.item.cameraUsername"
              >
              </b-form-input>
            </b-form-group>

            <b-form-group label="Password :" label-for="form-password-cctv">
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-password-cctv"
                placeholder="Masukkan Password CCTV"
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
import CCTVService from "../services/CCTVServices";

export default {
  name: "TableListCCTV",
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

    postCCTVUpdate(item) {
      this.loadingTableList = true;
      CCTVService.postCCTVUpdate(this.baseApi, this.jwtToken, {
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
              text: "CCTV " + item.name_camera + " have been successfully Edited",
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
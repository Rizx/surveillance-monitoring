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
          v-b-modal="'edit-warga-modal-' + row.item.id"
        >
          <i class="fas fa-pen"> </i>
        </b-button>

        <b-modal
          :id="'edit-warga-modal-' + row.item.id"
          hide-header-close
          :title="'Warga - ' + row.item.fullname"
          ok-title="Simpan"
          cancel-title="Batal"
          @ok="putWargaUpdate(row.item)"
        >
          <form ref="form">
            <b-form-group
              label="Nama :"
              label-for="form-name-warga"
              invalid-feedback="Nama Warga Wajib Diisi"
              :state="wargaNameState"
            >
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-name-warga"
                placeholder="Masukkan Nama Warga"
                v-model="row.item.fullname"
                :state="wargaNameState"
                required
              ></b-form-input>
            </b-form-group>

            <b-form-group
              label="Nomor Rumah :"
              label-for="form-no-house"
              invalid-feedback="No Rumah Wajib Diisi"
              :state="noHouseState"
            >
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-no-house"
                placeholder="Masukkan Nomor Rumah"
                v-model="row.item.address"
                :state="noHouseState"
                required
              ></b-form-input>
            </b-form-group>

            <b-form-group
              label="Nomor Kartu :"
              label-for="form-no-card"
              invalid-feedback="Nomor Kartu Wajib Diisi"
              :state="noCardState"
            >
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-no-card"
                placeholder="Masukkan Nomor Kartu"
                v-model="row.item.cardid"
                :state="noCardState"
                required
              ></b-form-input>
            </b-form-group>

            <b-form-group
              label="Password :"
              label-for="form-password"
            >
              <b-form-input
                size="sm"
                class="mb-3"
                id="form-password"
                placeholder="Ganti Password"
                v-model="row.item.password"
              ></b-form-input>
            </b-form-group>
          </form>
        </b-modal>
      </template>
    </b-table>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import WargaService from "../services/WargaServices";

export default {
  name: "TableListWarga",
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
      wargaNameState: null,
      noHouseState: null,
      noCardState: null,
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
        // this.modalShow = false;
        // this.putWargaUpdate();
      }
    },

    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.wargaNameState = valid;
      this.noHouseState = valid;
      this.noCardState = valid;
      return valid;
    },

    putWargaUpdate(item) {
      this.loadingTableList = true;
      WargaService.putWargaUpdate(this.baseApi, this.jwtToken, {
        id: item.id,
        username: item.username,
        password: item.password,
        fullname: item.fullname,
        address: item.address,
        cardid: item.cardid,
        active: item.active,
      })
        .then((response) => {
          if (response.data.success == true) {
            this.loadingTableList = false;
            Swal.fire({
              icon: "success",
              title: "Success !",
              text: "Warga " + item.fullname + " have been successfully Edited",
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
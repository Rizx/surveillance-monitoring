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
      <template #cell(gambar)="row">
        <b-button
          size="sm"
          variant="info"
          class="mr-1"
          v-b-tooltip.hover
          v-b-modal="'image-warga-modal-' + row.item.id"
        >
          Gambar
        </b-button>

        <b-modal
          :id="'image-warga-modal-' + row.item.id"
          :title="row.item.activity + ' - ' + row.item.name"
          hide-footer
        >
          <b-carousel 
            :interval="0"
            data-interval="false"
            controls
            indicators
            background="#ababab"
            img-width="1024"
            img-height="480"
            style="text-shadow: 1px 1px 2px #333"
          >
            <b-carousel-slide
              v-for="capture in row.item.photos"
              :key="capture"
              :img-src="capture"
            />
          </b-carousel>
        </b-modal>
      </template>

      <template #cell(action)="row">
        <b-button
          size="sm"
          variant="success"
          class="mr-1"
          v-b-tooltip.hover
          v-b-modal="'register-warga-modal-' + row.item.id"
          v-if="row.item.name == '' || row.item.name == null"
        >
          Daftar
        </b-button>

        <b-modal
          :id="'register-warga-modal-' + row.item.id"
          hide-header-close
          :title="'Register Kartu'"
          ok-title="Simpan"
          cancel-title="Batal"
          @ok="postTransactionRegister(row.item)"
        >
          <form ref="form">
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
                v-model="row.item.cardid"
                value="row.item.cardid"
                disabled
              ></b-form-input>
            </b-form-group>

            <b-form-group
              label="Nama Warga:"
              label-for="form-no-card"
              invalid-feedback="Nama Wajib Diisi"
            >
              <b-form-select
                size="sm"
                class="mb-3"
                id="form-name"
                v-model="row.item.username"
              >
                <option
                  v-for="item in options"
                  :value="item.username"
                  :key="item.fullname"
                >
                  {{ item.fullname }}
                </option></b-form-select
              >
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
  name: "TableListAktivitas",
  props: {
    items: Array,
    fields: Array,
    currentPage: Number,
    perPage: Number,
    totalRows: Number,
  },
  data() {
    return {
      options: [],
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
  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      await this.getWargaList();
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

    async getWargaList() {
      this.loadingTableList = false;
      WargaService.getWargaList(this.baseApi, this.jwtToken)
        .then((response) => {
          console.log(response);
          this.options = response.data.data;
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

    postTransactionRegister(item) {
      this.loadingTableList = true;
      WargaService.postCardRegister(this.baseApi, this.jwtToken, {
        id: item.id,
        cardid: item.cardid,
        username: item.username,
      })
        .then((response) => {
          if (response.data.success == true) {
            this.loadingTableList = false;
            Swal.fire({
              icon: "success",
              title: "Success !",
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
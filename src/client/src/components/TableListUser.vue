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
      <!-- <template #cell(role)="row">
        <p class="remove-padding">{{ row.item.role.join(", ") }}</p>
      </template> -->

      <template #cell(active)="row">
        <div v-if="row.item.active == false">
          <i class="fas fa-circle-xmark" style="color: red"> </i>
        </div>
        <div v-if="row.item.active == true">
          <i class="fas fa-circle-check" style="color: green"> </i>
        </div>
      </template>

      <template #cell(action)="row">
        <b-button
          size="sm"
          variant="success"
          class="mr-1"
          v-b-tooltip.hover
          title="Edit"
          v-b-modal="'edit-user-modal-' + row.item.id"
        >
          <i class="fas fa-user-pen"> </i>
        </b-button>

        <b-modal
          :id="'edit-user-modal-' + row.item.id"
          hide-header-close
          title="Edit User"
          ok-title="Save"
          @ok="putUserUpdate(row.item)"
        >
          <b-form-group label="Nama Lengakp :" label-for="form-fullname">
            <b-form-input
              id="form-fullname"
              v-model="row.item.fullname"
              placeholder="Masukkan Nama Lengkap"
              size="sm"
              class="mb-3"
            ></b-form-input>
          </b-form-group>

          <b-form-group label="Username :" label-for="form-username">
            <b-form-input
              id="form-username"
              v-model="row.item.username"
              placeholder="Masukkan Username"
              size="sm"
              class="mb-3"
            ></b-form-input>
          </b-form-group>

          <b-form-group label="Password Baru :" label-for="form-password">
            <b-input-group size="sm" class="mb-3">
              <b-form-input
                id="form-password"
                class="input-password"
                v-model="passwordUser"
                placeholder="Masukkan Password Baru"
                :type="showPassword ? 'text' : 'password'"
              ></b-form-input>

              <b-input-group-append>
                <span
                  class="input-group-text input-append-icon"
                  @click="showPassword = !showPassword"
                >
                  <b-icon :icon="showPassword ? 'eye-fill' : 'eye-slash-fill'">
                  </b-icon>
                </span>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>

          <b-form-group
            class="mb-3"
            label="Status :"
            v-slot="{ ariaDescribedby }"
          >
            <b-form-radio
              v-model="row.item.active"
              :aria-describedby="ariaDescribedby"
              name="radio-active-user"
              :value="true"
            >
              Aktif
            </b-form-radio>
            <b-form-radio
              v-model="row.item.active"
              :aria-describedby="ariaDescribedby"
              name="radio-active-user"
              :value="false"
            >
              Tidak Aktif
            </b-form-radio>
          </b-form-group>
          <!-- <b-spinner variant="primary" v-if="!loadingComponentList"></b-spinner>
          <b-form-group
            class="mb-3"
            label="Assign Role :"
            v-slot="{ ariaDescribedby }"
            v-if="loadingComponentList"
          >
            <b-form-checkbox-group
              id="checkbox-group-role-user"
              v-model="row.item.role"
              :options="roleList"
              :aria-describedby="ariaDescribedby"
            ></b-form-checkbox-group>
          </b-form-group> -->
        </b-modal>
      </template>
    </b-table>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import UserService from "../services/UserServices";

export default {
  name: "TableListUser",
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
      showPassword: false,
      passwordUser: "",
      roleList: [],
    };
  },

  mounted() {
    // this.getRoleList();
  },

  methods: {
    splitedRow(row) {
      if (row?.includes("|")) {
        return row.split("|")[0];
      } else {
        return row;
      }
    },

    splitedRowTooltip(row) {
      if (row?.includes("|")) {
        return row.split("|")[1];
      } else {
        return row;
      }
    },

    putUserUpdate(item) {
      this.loadingTableList = true;
      UserService.putUserUpdate(this.baseApi, this.jwtToken, {
        id: item.id,
        fullname: item.fullname,
        username: item.username,
        password: this.passwordUser,
        role: "Administrator"
      })
        .then((response) => {
          if (response.data.success == true) {
            this.loadingTableList = false;
            Swal.fire({
              icon: "success",
              title: "Success !",
              text: "User " + item.fullname + " have been successfully Edited",
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
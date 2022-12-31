<template>
  <div>
    <Sidebar />
    <Header :title="'Manajemen User'" />
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
              <i class="fas fa-user-plus mr-1"> </i>
              Tambah User
            </b-button>
            <b-modal
              id="new-user"
              ref="modal"
              v-model="modalShow"
              hide-header-close
              title="Tambah User"
              ok-title="Simpan"
              @ok="handleOk"
            >
              <form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group
                  label="Nama Lengkap :"
                  label-for="form-user-fullname"
                  invalid-feedback="Nama Lengkap Wajib Diisi"
                  :state="userFullnameState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-user-fullname"
                    placeholder="Masukkan Nama Lengkap"
                    v-model="userBody.fullname"
                    :state="userFullnameState"
                    required
                  >
                  </b-form-input>
                </b-form-group>

                <b-form-group
                  label="Username :"
                  label-for="form-user-name"
                  invalid-feedback="Username Wajib Diisi"
                  :state="userNameState"
                >
                  <b-form-input
                    size="sm"
                    class="mb-3"
                    id="form-user-name"
                    placeholder="Masukkan Username"
                    v-model="userBody.username"
                    :state="userNameState"
                    required
                  >
                  </b-form-input>
                </b-form-group>

                <b-form-group
                  label="Password :"
                  label-for="form-user-password"
                  invalid-feedback="Password Wajib Diisi"
                  :state="userPasswordState"
                >
                  <b-input-group size="sm" class="mb-3">
                    <b-form-input
                      size="sm"
                      class="login-input-password"
                      id="form-user-password"
                      placeholder="Masukkan Password"
                      v-model="userBody.password"
                      :state="userPasswordState"
                      :type="showPassword ? 'text' : 'password'"
                      required
                    >
                    </b-form-input>
                    <b-input-group-append>
                      <span
                        class="input-group-text input-append-icon"
                        @click="showPassword = !showPassword"
                      >
                        <b-icon
                          :icon="showPassword ? 'eye-slash-fill' : 'eye-fill'"
                        >
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
                    v-model="userBody.active"
                    :aria-describedby="ariaDescribedby"
                    name="radio-active-user"
                    :value="true"
                  >
                    Aktif
                  </b-form-radio>
                  <b-form-radio
                    v-model="userBody.active"
                    :aria-describedby="ariaDescribedby"
                    name="radio-active-user"
                    :value="false"
                  >
                    Tidak Aktif
                  </b-form-radio>
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
        <TableListUser
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
import TableListUser from "../components/TableListUser";
import UserService from "../../services/UserServices";

export default {
  name: "UserManagement",
  components: { Sidebar, Header, TableListUser },
  data() {
    return {
      modalShow: false,
      userFullnameState: null,
      userNameState: null,
      userPasswordState: null,
      showPassword: false,

      userBody: {
        id: 0,
        fullname: "",
        username: "",
        password: "",
        role: "Administrator",
        active: true,
      },

      currentPage: 1,
      perPage: 20,
      totalRows: 1,
      items: [],
      fields: [
        {
          key: "fullname",
          label: "NAMA",
        },
        {
          key: "username",
          label: "USERNAME",
        },
        {
          key: "active",
          label: "STATUS",
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
      await this.getUserList();
    },

    async clear() {
      this.userBody = {
        id: 0,
        fullname: "",
        username: "",
        password: "",
        active: false,
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
        this.postUserRegister();
      }
    },

    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.userFullnameState = valid;
      this.userNameState = valid;
      this.userPasswordState = valid;
      return valid;
    },

    async getUserList() {
      this.loadingTableList = false;
      UserService.getUserList(this.baseApi, this.jwtToken)
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

    async postUserRegister() {
      this.loadingTableList = false;
      UserService.postUserRegister(this.baseApi, this.jwtToken, this.userBody)
        .then((response) => {
          if (response.data.success == true) {
            Swal.fire({
              icon: "success",
              title: "Success !",
              text:
                "User " +
                this.userBody.fullname +
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

<style scoped>
.login-input-password {
  border-right: none !important;
}
</style>
<template>
  <div>
    <div class="d-none d-md-block" style="overflow: hidden">
      <b-row class="remove-padding">
        <b-col class="remove-padding">
          <div class="left-side"></div>
        </b-col>
        <b-col class="remove-padding">
          <div class="right-side">
            <div class="login-form my-auto mx-5">
              <b-row>
                <div class="mb-3 mx-auto">
                  <b-col>
                    <b-img
                      :src="require('../assets/images/logo.png')"
                      fluid
                      alt="Logo"
                      class="login-image-logo"
                    >
                    </b-img>
                    <h3 class="login-title my-3 mx-3">
                      Surveillance <br />
                      System Monitoring
                    </h3>
                  </b-col>
                </div>
              </b-row>

              <b-col style="text-align: center">
                <b-input-group size="sm" class="mb-3">
                  <b-form-input
                    class="login-input"
                    placeholder="Username"
                    v-model="modelUsername"
                  ></b-form-input>
                </b-input-group>

                <b-input-group size="sm" class="mb-3">
                  <b-form-input
                    class="login-input-password"
                    placeholder="Password"
                    v-model="modelPassword"
                    :type="showPassword ? 'text' : 'password'"
                    v-on:keyup.enter="onEnter"
                  >
                  </b-form-input>
                  <b-input-group-append>
                    <span
                      class="input-group-text login-input-append-icon"
                      @click="showPassword = !showPassword"
                    >
                      <b-icon
                        :icon="showPassword ? 'eye-fill' : 'eye-slash-fill'"
                      >
                      </b-icon>
                    </span>
                  </b-input-group-append>
                </b-input-group>
                <b-row align-h="center">
                  <b-button block variant="secondary" @click="postUserLogin()">
                    Login
                  </b-button>
                </b-row>
                <div
                  class="mt-3"
                  v-if="loadingSubmit"
                  style="text-align: center"
                >
                  <b-col>
                    <b-spinner class="mb-2"></b-spinner>
                    <h5>Loading, Please wait...</h5>
                  </b-col>
                </div>
              </b-col>
            </div>
          </div>
        </b-col>
      </b-row>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import UserService from "../services/UserServices";

export default {
  name: "Login",
  data() {
    return {
      authenticationBody: {
        username: "",
        password: "",
      },

      modelUsername: "",
      modelPassword: "",
      showPassword: false,
    };
  },

  mounted() {
    this.onInit();
  },

  methods: {
    async onInit() {
      this.setSession();
    },

    onEnter: function () {
      this.postUserLogin();
    },

    async postUserLogin() {
      this.loadingSubmit = true;
      this.authenticationBody.username = this.modelUsername;
      this.authenticationBody.password = this.modelPassword;
      UserService.postUserLogin(
        this.baseApi,
        this.authenticationBody
      )
        .then((response) => {
          if (response.data.success == true) {
            this.loadingSubmit = false;
            this.$session.start();
            this.$session.set("USER_ID", response.data.data.id);
            this.$session.set("USER_NAME", response.data.data.username);
            this.$session.set("USER_FULLNAME", response.data.data.fullname);
            this.$session.set("USER_ROLE", response.data.data.role);
            this.$session.set("USER_JWT_TOKEN", response.data.data.token);
            this.$session.set("isLogin", true);
            this.$router.push("/cctv");
          } else {
            this.loadingSubmit = false;
            Swal.fire({
              icon: "error",
              title: "Login Failed",
              text: response.data.error,
            });
          }
        })
        .catch((error) => {
          this.$session.destroy();
          this.loadingSubmit = false;
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

<style scoped>
.right-side {
  background-position: center bottom;
  background-repeat: no-repeat;
  background-size: cover;
  display: block;
  max-width: 37vw;
  min-height: 100vh;
}

.left-side {
  background-image: url("../assets/images/login-background.jpg");
  background-position: center bottom;
  background-repeat: no-repeat;
  background-size: cover;
  display: block;
  min-width: 63vw;
  min-height: 100vh;
  margin: 0px;
  place-items: center;
  grid-template-areas: "inner-div";
}

.login-form {
  width: 30vw;
  border-radius: 8px;
  border: solid 1px #e8eaed;
  text-align: center;
  position: fixed;
  top: 45%;
  transform: translate(0%, -50%);
  padding: 32px;
}

.login-image-logo {
  width: 80px;
}

.login-title {
  color: #1989fa;
  text-align: center;
  font-weight: bold;
}

.login-input {
  border-radius: 10rem !important;
  padding: 1.5rem 1rem !important;
  font-size: 0.8rem !important;
}

.login-input-password {
  border-radius: 10rem 0rem 0rem 10rem !important;
  padding: 1.5rem 1rem !important;
  font-size: 0.8rem !important;
  border-right: none !important;
}

.login-input-append-icon {
  background: transparent !important;
  border-radius: 0rem 10rem 10rem 0rem !important;
  border-left: none !important;
  padding-right: 15px !important;
}

.btn-secondary {
  width: 10rem;
  color: #fff;
  background-color: #0b4f9f;
  border-radius: 10rem;
  border-color: transparent;
}

.btn-secondary:hover {
  background-color: #07346a;
  border-color: transparent;
}
</style>
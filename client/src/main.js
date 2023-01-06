import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import { BootstrapVue, BootstrapVueIcons } from "bootstrap-vue";
import "./assets/css/main.css";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import VueSidebarMenu from "vue-sidebar-menu";
import "vue-sidebar-menu/dist/vue-sidebar-menu.css";
import vSelect from "vue-select";
import "vue-select/dist/vue-select.css";
import VueSweetalert2 from "vue-sweetalert2";
import "sweetalert2/dist/sweetalert2.min.css";
import VueSession from "vue-session";

Vue.mixin({
  data: function () {
    return {
      loadingList: false,
      loadingTableList: false,
      loadingComponentList: false,
      loadingSubmit: false,
      baseApi: null,
      jwtToken: "",
      accountFullname: "",
    };
  },

  mounted() {
    this.baseApi = this.$config.API_LOCATION;
    this.jwtToken = this.$session.get("USER_JWT_TOKEN");
    this.accountFullname =
      this.$session.get("USER_FULLNAME") == ""
        ? "Administrator"
        : this.$session.get("USER_FULLNAME");
  },

  methods: {
    setSession() {
      this.$session.start();
      this.$session.set("USER_ID", 0);
      this.$session.set("USER_NAME", "");
      this.$session.set("USER_FULLNAME", "");
      this.$session.set("USER_ROLE", "");
      this.$session.set("USER_JWT_TOKEN", "");
      this.$session.set("isLogin", false);
    },

    //Check Login
    checkLogin() {
      this.$session.start();
      if (!this.$session.has("isLogin")) {
        this.$router.push("/");
      }
    },

    // Logout
    postUserLogout() {
      this.$session.start();
      this.$session.destroy();
      this.$router.push("/");
    },
  },
});

Vue.config.productionTip = false;
Vue.use(VueSession);
Vue.use(BootstrapVue);
Vue.use(BootstrapVueIcons);
Vue.use(VueSidebarMenu);
Vue.component("font-awesome-icon", FontAwesomeIcon);
Vue.use(VueSweetalert2);
Vue.component("v-select", vSelect);

fetch("appsettings.json")
  .then((response) => response.json())
  .then((config) => {
    Vue.prototype.$config = config;
    new Vue({
      router,
      render: (h) => h(App),
    }).$mount("#app");
  });

<template>
  <div style="margin-left: 50px">
    <b-navbar
      toggleable="md"
      type="dark"
      variant="light"
      class="header-container-head"
    >
      <b-navbar-brand>
        <b-row class="header-container-box">
          <b-col>
            <b-row>
              <b-img
                class="header-logo"
                :src="require('../assets/images/logo.png')"
                alt="logo"
              />
              <h5 class="my-auto">
                <strong style="color: #1989fa">{{ title }} </strong>
              </h5>
            </b-row>
          </b-col>
        </b-row>
      </b-navbar-brand>
      <b-collapse id="nav-collapse" is-nav>
        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <div class="d-flex align-items-center">
            <b-avatar variant="dark" class="mr-2" />
            <b-dropdown
              id="dropdown-navbar-profile"
              :text="accountFullname"
              class="m-md-2"
              no-caret
              right
            >
              <!-- <b-dropdown-header id="dropdown-header-label">
                {{ accountRole.join(", ") }}
              </b-dropdown-header> -->
              <!-- <b-dropdown-divider></b-dropdown-divider> -->
              <b-dropdown-item v-b-modal.modal-logout>
                <i class="fas fa-door-open"> </i>
                Logout
              </b-dropdown-item>
            </b-dropdown>
          </div>
        </b-navbar-nav>
        <b-modal
          id="modal-logout"
          hide-header-close
          title="Logging Out"
          @ok="postUserLogout()"
        >
          <p class="my-4">Are you sure you want to logout?</p>
        </b-modal>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
  },
};
</script>

<style>
.header-container-head {
  box-shadow: 0px 1px 2px rgba(25, 39, 52, 0.05),
    0px 0px 4px rgba(25, 39, 52, 0.1);
}

.header-container-box {
  padding: 10px;
  height: 100% !important;
}

.header-logo {
  padding: 8px;
  width: 40px;
}

</style>
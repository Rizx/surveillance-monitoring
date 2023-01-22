<template>
  <div>
    <sidebar-menu :menu="menuItems" :collapsed="true" :hideToggle="true">
      <template v-slot:header>
        <div style="height: 50px"></div>
      </template>
    </sidebar-menu>
  </div>
</template>

<script>
export default {
  name: "Sidebar",
  data() {
    return {
      menuItems: [],
    };
  },
  mounted() {
    this.onInit();
  },
  methods: {
    onInit() {
      this.menuItems = [
        {
          header: true,
          title: "Menu",
          hiddenOnCollapse: true,
        },
        {
          href: "/camera",
          title: "Live Streaming",
          icon: "fa fa-video",
          role: "Administrator|Guest",
        },
        {
          href: "/aktivitas",
          title: "Aktivitas",
          icon: "fa fa-arrow-right-arrow-left",
          role: "Administrator|Guest",
        },
        {
          href: "/manajemen-camera",
          title: "Manajemen Camera",
          icon: "fa fa-gears",
          role: "Administrator",
        },
        {
          href: "/manajemen-warga",
          title: "Manajemen Warga",
          icon: "fa fa-people-group",
          role: "Administrator",
        },
        {
          href: "/manajemen-user",
          title: "Manajemen User",
          icon: "fa fa-user",
          role: "Administrator",
        },
      ];

      const user_role = String(this.$session.get("USER_ROLE"));
      this.menuItems = this.menuItems.filter(function (el) {
        return user_role.includes(el.role);
      });
    },
  },
};
</script>

<style scoped>
.v-sidebar-menu {
  background-color: #11101d;
}
</style>
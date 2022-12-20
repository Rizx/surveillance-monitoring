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
      menuItems: [
        {
          header: true,
          title: "Menu",
          hiddenOnCollapse: true,
        },
        {
          href: "/cctv",
          title: "CCTV",
          icon: "fa fa-video",
        },
        {
          href: "/transaksi",
          title: "Transaksi",
          icon: "fa fa-arrow-right-arrow-left",
        },
        {
          href: "/manajemen-cctv",
          title: "Manajemen CCTV",
          icon: "fa fa-gears",
        },
        {
          href: "/manajemen-warga",
          title: "Manajemen Warga",
          icon: "fa fa-people-group",
        },
        {
          href: "/manajemen-user",
          title: "Manajemen User",
          icon: "fa fa-user",
        },
      ],
    };
  },
};
</script>

<style scoped>
.v-sidebar-menu {
    background-color: #11101d;
}
</style>
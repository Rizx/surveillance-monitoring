import Vue from "vue";
import VueRouter from "vue-router";
import Login from "../views/Login";
import CCTV from "../views/CCTV";
import Transaksi from "../views/Transaksi";
import ManajemenCCTV from "../views/ManajemenCCTV";
import ManajemenWarga from "../views/ManajemenWarga";
import ManajemenUser from "../views/ManajemenUser";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "login",
    component: Login,
  },
  {
    path: "/cctv",
    name: "CCTV",
    component: CCTV,
  },
  {
    path: "/transaksi",
    name: "Transaksi",
    component: Transaksi,
  },
  {
    path: "/manajemen-cctv",
    name: "ManajemenCCTV",
    component: ManajemenCCTV,
  },
  {
    path: "/manajemen-warga",
    name: "ManajemenWarga",
    component: ManajemenWarga,
  },
  {
    path: "/manajemen-user",
    name: "ManajemenUser",
    component: ManajemenUser,
  },
];

const router = new VueRouter({
  routes,
  mode: "history",
});

export default router;

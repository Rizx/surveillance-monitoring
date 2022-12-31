import Vue from "vue";
import VueRouter from "vue-router";
import Login from "../views/Login";
import Camera from "../views/Camera";
import Activity from "../views/Activity";
import CameraManagement from "../views/CameraManagement";
import MemberManagement from "../views/MemberManagement";
import UserManagement from "../views/UserManagement";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "login",
    component: Login,
  },
  {
    path: "/camera",
    name: "Camera",
    component: Camera,
  },
  {
    path: "/aktivitas",
    name: "Activity",
    component: Activity,
  },
  {
    path: "/manajemen-camera",
    name: "CameraManagement",
    component: CameraManagement,
  },
  {
    path: "/manajemen-warga",
    name: "MemberManagement",
    component: MemberManagement,
  },
  {
    path: "/manajemen-user",
    name: "UserManagement",
    component: UserManagement,
  },
];

const router = new VueRouter({
  routes,
  mode: "history",
});

export default router;

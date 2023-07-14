import { createRouter, createWebHistory } from "vue-router";
import AssetList from "@/views/asset/AssetList.vue";
import ReportList from "@/views/report/ReportList.vue";
import SettingIndex from "@/views/setting/SettingIndex.vue";
import HomePage from "@/views/Index.vue";

const routers = [
  {
    path: "/assets",
    name: "AssetRouter",
    component: AssetList,
  },
  {
    path: "/reports",
    name: "ReportRouter",
    component: ReportList,
  },
  {
    path: "/setting",
    name: "SettingRouter",
    component: SettingIndex,
  },
  {
    path: "/",
    name: "HomeRouter",
    component: HomePage,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routers,
});

export default router;

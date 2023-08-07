import { createRouter, createWebHistory } from "vue-router";
import AssetList from "@/views/asset/AssetList.vue";

const routers = [
  {
    path: "/assets",
    name: "AssetRouter",
    component: AssetList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routers,
});

export default router;

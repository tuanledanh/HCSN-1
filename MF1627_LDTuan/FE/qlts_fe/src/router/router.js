import { createRouter, createWebHistory } from "vue-router";
import AssetList from "@/views/asset/AssetList.vue";
import AssetTransferList from "@/views/assetTransfer/AssetTransferList.vue";

const routers = [
  {
    path: "/assets",
    name: "AssetRouter",
    component: AssetList,
  },
  {
    path: "/assetsTransfer",
    name: "AssetTransferRouter",
    component: AssetTransferList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routers,
});

export default router;

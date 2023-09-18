import { createApp } from "vue";
import App from "./App.vue";

import router from "./router/router";
import emitter from "tiny-emitter/instance";
import axios from "axios";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";
import vi from "element-plus/dist/locale/vi.mjs";
import dayjs from "dayjs";
import "dayjs/locale/vi";
//===================================== Base =====================================
import MISAInput from "@/components/base/input/MISAInput.vue";
import MISAInputDatePicker from "@/components/base/input/MISAInputDatePicker.vue";
import MISAInputYear from "@/components/base/input/MISAInputYear.vue";
import MISAIcon from "@/components/base/icon/MISAIcon.vue";
import MISALabel from "@/components/base/label/MISALabel.vue";
import MISAButton from "@/components/base/button/MISAButton.vue";
import MISASidebarItem from "@/components/base/sidebarItem/MISASidebarItem.vue";
import MISALoading from "@/components/base/loading/MISALoading.vue";
import MISACombobox from "@/components/base/combobox/MISACombobox.vue";
import MISADropdownList from "@/components/base/combobox/MISADropdownList.vue";
import MISACalculator from "@/components/base/calculator/MISACalculator.vue";
import MISAPaging from "@/components/base/paging/MISAPaging.vue";
import MISAPagingFE from "@/components/base/paging/MISAPagingFE.vue";
import MISATooltip from "@/components/base/tooltip/MISATooltip.vue";
import MISATooltipVisible from "@/components/base/tooltip/MISATooltipVisible.vue";
import MISAToast from "@/components/base/toast/MISAToast.vue";
import MISAContextMenu from "@/components/base/contextMenu/MISAContextMenu.vue";

//===================================== Helpers =================================
import MISAApi from "./helpers/api";
import MISAResource from "./helpers/resource";
import MISAEnum from "./helpers/enum";
import * as MISACommon from "./helpers/common/commonFunction";

const clickOutside = {
  beforeMount: (el, binding) => {
    el.clickOutsideEvent = (event) => {
      // here I check that click was outside the el and his children
      if (!(el == event.target || el.contains(event.target))) {
        // and if it did, call method provided in attribute value
        binding.value();
      }
    };
    document.addEventListener("click", el.clickOutsideEvent);
  },
  unmounted: (el) => {
    document.removeEventListener("click", el.clickOutsideEvent);
  },
};

const app = createApp(App);

app.directive("click-outside", clickOutside);

//===================================== Base =====================================
app.component("MISAInput", MISAInput);
app.component("MISAInputDatePicker", MISAInputDatePicker);
app.component("MISAInputYear", MISAInputYear);
app.component("MISAIcon", MISAIcon);
app.component("MISALabel", MISALabel);
app.component("MISAButton", MISAButton);
app.component("MISASidebarItem", MISASidebarItem);
app.component("MISALoading", MISALoading);
app.component("MISACombobox", MISACombobox);
app.component("MISADropdownList", MISADropdownList);
app.component("MISACalculator", MISACalculator);
app.component("MISAPaging", MISAPaging);
app.component("MISAPagingFE", MISAPagingFE);
app.component("MISATooltip", MISATooltip);
app.component("MISATooltipVisible", MISATooltipVisible);
app.component("MISAToast", MISAToast);
app.component("VueDatePicker", VueDatePicker);
app.component("MISAContextMenu", MISAContextMenu);

const global = app.config.globalProperties;

global.$_axios = axios;
global.$_emitter = emitter;
//===================================== Helpers =================================

global.$_MISAApi = MISAApi;
global.$_MISAResource = MISAResource;
global.$_MISAEnum = MISAEnum;
Object.keys(MISACommon).forEach((key) => {
  app.config.globalProperties[`$${key}`] = MISACommon[key];
});
global.$_LANG_CODE = "VN";

dayjs.locale(vi);
app.use(router);
app.use(ElementPlus, {
  locale: vi,
});

app.mount("#app");

export default app;

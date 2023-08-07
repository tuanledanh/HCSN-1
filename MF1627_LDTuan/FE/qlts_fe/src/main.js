import { createApp } from 'vue'
import App from './App.vue'

import router from './router/router'
import emitter from 'tiny-emitter/instance'
import axios from 'axios'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
//===================================== Base =====================================
import MISAInput from '@/components/base/input/MISAInput.vue'
import MISAInputDatePicker from '@/components/base/input/MISAInputDatePicker.vue'
import MISAInputYear from '@/components/base/input/MISAInputYear.vue'
import MISAIcon from '@/components/base/icon/MISAIcon.vue'
import MISALabel from '@/components/base/label/MISALabel.vue'
import MISAButton from '@/components/base/button/MISAButton.vue'
import MISASidebarItem from '@/components/base/sidebarItem/MISASidebarItem.vue'
import MISALoading from '@/components/base/loading/MISALoading.vue'
import MISACombobox from '@/components/base/combobox/MISACombobox.vue'
import MISADropdownList from '@/components/base/combobox/MISADropdownList.vue'
import MISAPaging from '@/components/base/paging/MISAPaging.vue'
import MISATooltip from '@/components/base/tooltip/MISATooltip.vue'
import MISAToast from '@/components/base/toast/MISAToast.vue'

//===================================== Helpers =================================
import MISAApi from './helpers/api'
import MISAResource from './helpers/resource'
import MISAEnum from './helpers/enum'
import * as MISACommon from './helpers/common/commonFunction'



const app = createApp(App);

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
app.component("MISAPaging", MISAPaging);
app.component("MISATooltip", MISATooltip);
app.component("MISAToast", MISAToast);
app.component('VueDatePicker', VueDatePicker);

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




app.use(router);

app.mount('#app');

export default app;

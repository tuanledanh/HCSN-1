import { createApp } from 'vue'
import App from './App.vue'

import router from './router/router'
import emitter from 'tiny-emitter/instance'
import axios from 'axios'

//===================================== Base =====================================
import MISAButton from '@/components/base/button/MISAButton.vue'
import MISAIcon from '@/components/base/icon/MISAIcon.vue'
import MISADialogNotice from '@/components/base/dialog/MISADialogNotice.vue'
import MISAInput from '@/components/base/input/MISAInput.vue'
import MISAInputDate from '@/components/base/input/MISAInputDate.vue'
import MISAInputQuantity from '@/components/base/input/MISAInputQuantity.vue'
import MISALabel from '@/components/base/label/MISALabel.vue'
import MISALoading from '@/components/base/loading/MISALoading.vue'
import MISATable from '@/components/base/table/MISATable.vue'

//===================================== Helpers =================================
import MSIAApi from './helpers/api'
import MISAResource from './helpers/resource'
import MISAEnum from './helpers/enum'
import * as MISACommon from './helpers/common/commonFunction'






const app = createApp(App);
//===================================== Base =====================================
app.component("MISAButton", MISAButton);
app.component("MISAIcon", MISAIcon);
app.component("MISADialogNotice", MISADialogNotice);
app.component("MISAInput", MISAInput);
app.component("MISAInputDate", MISAInputDate);
app.component("MISAInputQuantity", MISAInputQuantity);
app.component("MISALabel", MISALabel);
app.component("MISALoading", MISALoading);
app.component("MISATable", MISATable);

const global = app.config.globalProperties;
//===================================== Helpers =================================

global.$_emitter = emitter;
global.$_axios = axios;
global.$_MSIAApi = MSIAApi;
global.$_MISAResource = MISAResource;
global.$_MISAEnum = MISAEnum;
Object.keys(MISACommon).forEach((key) => {
    app.config.globalProperties[`$${key}`] = MISACommon[key];
  });
global.$_LANG_CODE = "VN";
app.use(router);

app.mount('#app');

export default app;

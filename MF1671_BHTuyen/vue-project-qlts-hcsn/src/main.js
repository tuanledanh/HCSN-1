import '/src/css/app.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import MISAButton from '/src/components/base/MISAButton.vue'
import MISASidebarItem from '/src/components/base/MISASidebarItem.vue'
import MISAInput from '/src/components/base/MISAInput.vue'
import MISACombobox from '/src/components/base/MISACombobox.vue'
import MISAPopup from '/src/components/base/MISAPopup.vue'
import MISAToast from '/src/components/base/MISAToast.vue'
import MISATooltip from '/src/components/base/MISATooltip.vue'

import MISAResource from '/src/helper/resource.js'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.component('m-button', MISAButton)
app.component('m-input', MISAInput)
app.component('m-sidebarItem', MISASidebarItem)
app.component('m-combobox', MISACombobox)
app.component('m-popup', MISAPopup)
app.component('m-toast', MISAToast)
app.component('m-tooltip', MISATooltip)

app.config.globalProperties.$_MISAResource = MISAResource

app.mount('#app')

import '/src/css/app.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { clickOutside } from './helper/directive.js'

import MISAButton from '/src/components/base/MISAButton.vue'
import MISACheckBox from '/src/components/base/MISACheckBox.vue'
import MISASidebarItem from '/src/components/base/MISASidebarItem.vue'
import MISAInput from '/src/components/base/MISAInput.vue'
import MISACombobox from '/src/components/base/MISACombobox.vue'
import MISAPopup from '/src/components/base/MISAPopup.vue'
import MISAToast from '/src/components/base/MISAToast.vue'
import MISATooltip from '/src/components/base/MISATooltip.vue'
import MISADropdown from '/src/components/base/MISADropdown.vue'
import MISAPaging from '/src/components/base/MISAPaging.vue'
import MISAResource from '/src/helper/resource.js'
import MISAEnum from '/src/helper/enum.js'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.directive('click-outside', clickOutside)

app.use(createPinia())
app.use(router)

app.component('m-button', MISAButton)
app.component('m-input', MISAInput)
app.component('m-sidebarItem', MISASidebarItem)
app.component('m-combobox', MISACombobox)
app.component('m-popup', MISAPopup)
app.component('m-toast', MISAToast)
app.component('m-tooltip', MISATooltip)
app.component('m-dropdown', MISADropdown)
app.component('m-paging', MISAPaging)
app.component('m-checkbox', MISACheckBox)

app.config.globalProperties.$_MISAResource = MISAResource
app.config.globalProperties.$_MISAEnum = MISAEnum

app.mount('#app')

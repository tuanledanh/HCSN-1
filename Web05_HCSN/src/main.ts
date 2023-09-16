import './scss/main.scss'

import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import { createPinia } from 'pinia'
import 'element-plus/dist/index.css'

import App from './App.vue'
import router from './router'

// Base components
import MISAPopup from '@/components/popup'
import MISAInput from '@/components/input'
import MISATable from '@/components/table'
import MISATableColumn from '@/components/table_column'
import MISAButton from '@/components/button'
import MISADropdown from '@/components/dropdown'
import MISACheckbox from '@/components/checkbox'
import MISAPagination from '@/components/pagination'
import MISADatePicker from '@/components/date_picker'
import MISASidebarItem from '@/components/sidebar_item'
import MISASelectInfo from '@/components/select_info'

// Create the app instance
const app = createApp(App)

// App configuration
app.use(createPinia())
app.use(router)
app.use(ElementPlus)

// Register base components
app.component('m-popup', MISAPopup)
app.component('m-input', MISAInput)
app.component('m-table', MISATable)
app.component('m-button', MISAButton)
app.component('m-dropdown', MISADropdown)
app.component('m-checkbox', MISACheckbox)
app.component('m-pagination', MISAPagination)
app.component('m-date-picker', MISADatePicker)
app.component('m-table-column', MISATableColumn)
app.component('m-sidebar-item', MISASidebarItem)
app.component('m-select-info', MISASelectInfo)

app.mount('#app')

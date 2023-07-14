import { createApp } from 'vue'
import App from './App.vue'
import MisaCombobox from './components/base/MisaCombobox/MisaCombobox.vue'
import MisaInput from './components/base/MisaInput/MisaInput.vue'

const app = createApp(App);
app.component("MisaCombobox", MisaCombobox)
app.component("MisaInput", MisaInput)

app.mount('#app')

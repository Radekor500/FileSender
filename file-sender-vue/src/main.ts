import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import filesApi from './shared/httpClient/filesService'
import { AxiosKey } from './shared/httpClient/symbo'

// Object.defineProperty(Vue.prototype, '$filesApi', {
//     get() {
//         return filesApi;
//     }
// })



const app = createApp(App)
app.provide(AxiosKey, new filesApi());
app.use(router).mount('#app');


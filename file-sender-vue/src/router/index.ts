import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import FileShare from '../views/fileShare.vue'
import FileDownload from '../views/fileDownload.vue'

const routes: Array<RouteRecordRaw> = [
  {path: '/', component: FileShare},
  {path: '/files/:guid', component: FileDownload}
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

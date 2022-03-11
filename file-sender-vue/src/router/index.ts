import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import FileShare from '../views/fileShare.vue'

const routes: Array<RouteRecordRaw> = [
  {path: '/', component: FileShare}
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

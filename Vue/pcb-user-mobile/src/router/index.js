import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component:  () => import('../views/login/login.vue')
    },
    {
      path: '/login',
      name: 'login2',
      component:  () => import('../views/login/login.vue')
    },
    {
      path: '/home',
      name: 'home',
      component:  () => import('../views/home/home.vue'),
      children:[
        {
          path: '/order',
          name: 'order',
          component:  () => import('../views/home/order.vue'),
        },
        {
          path: '/my',
          name: 'my',
          component:  () => import('../views/home/my.vue'),
        },
        {
          path: '/orderadd',
          name: 'orderadd',
          component:  () => import('../views/home/detail/add.vue'),
        },
      ]
    },
    {
      path: '/register',
      name: 'register',
      component:  () => import('../views/login/register.vue')
    },
  ]
})






export default router

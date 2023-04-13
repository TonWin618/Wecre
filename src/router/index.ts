import { createRouter, createWebHistory } from 'vue-router'
import Homeview from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import SignUpView from '../views/SignUpView.vue'
import TestView from '../views/TestView.vue'
import DeveloperView from '../views/DeveloperView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path:'/',
      name:'Home',
      component:Homeview
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/signUp',
      name: 'signUp',
      component: SignUpView
    },
    {
      path: '/Test',
      name:'Test',
      component:TestView
    },
    {
      path: '/:name',
      name:'Name',
      component:DeveloperView
    }
  ]
})

export default router

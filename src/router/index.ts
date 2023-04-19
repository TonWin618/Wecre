import { createRouter, createWebHistory } from 'vue-router'
import Homeview from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import SignUpView from '../views/SignUpView.vue'
import TestView from '../components/Project/Publish/Overview.vue'
import DeveloperView from '../views/DeveloperView.vue'
import ProjectView from '../views/ProjectView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path:'/',
      name:'Home',
      component:Homeview,
      meta: { requiresAuth: true }
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
    },
    {
      path:'/:name/:project',
      name:'project',
      component:ProjectView
    }
  ]
})
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!userLoggedIn()) {
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    } else {
      next()
    }
  } else {
    next()
  }
})

function userLoggedIn(){
  return true;
}
export default router

import axios from 'axios'
import { useAuthStore } from '@/store/auth'

//const authStore = useAuthStore()
//All requests do not need to include the 'username' parameter, as the backend can extract the username through the token
const service = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL,
  timeout: 5000,
  headers:{
    //'Authorization':`Bearer ${authStore.token}`
  }
})

export default service
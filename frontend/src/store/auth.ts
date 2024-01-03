import { defineStore } from 'pinia'
import service from '@/utils/service'

interface User {
  nickname: string
  username: string
  email: string
}

interface AuthState {
  user: User | null
  token: string | null
}

export const useAuthStore = defineStore({
  id: 'auth',
  state: (): AuthState => ({
    user: null,
    token: null
  }),
  actions: {
    async loginWithUsername(username: string, password: string) {
      try {
        const res = await service.post<{ user: User, token: string }>('/login/username', { username, password })
        this.user = res.data.user
        this.token = res.data.token
      } catch (error) {
        console.error(error)
      }
    },
    async loginWithEmail(email: string, password: string) {
      try {
        const res = await service.post<{ user: User, token: string }>('/login/email', { email, password })
        this.user = res.data.user
        this.token = res.data.token
      } catch (error) {
        console.error(error)
      }
    },
    logout() {
      this.user = null
      this.token = null
    }
  },
  getters: {
    isLoggedIn(): boolean {
      return Boolean(this.token)
    },
    userDisplayName(): string {
      return this.user?.nickname || ''
    },
    userEmail(): string {
      return this.user?.email || ''
    }
  }
})

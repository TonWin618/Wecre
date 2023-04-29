import { defineStore } from 'pinia'
import service from '@/utils/service'

interface Profile {
    nickname: string
    username: string
    email:string
    avatar: string
    bio: string
    followers: number
    following: number
    projects: string[]
}

export const useProfileStore = defineStore('profile', {
    state: () => ({
        profile: null as Profile | null,
    }),
    actions: {
        async fetchProfile(username:string) {
            const { data } = await service.get<Profile>(`/api/profile/${username}`)
            this.profile = data
        },
    },
})

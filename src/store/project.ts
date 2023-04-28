import { defineStore } from 'pinia'
import service from '@/utils/axios'

export interface Project {
    username: string
    name: string
    description: string
    tags: string[]
    readmeFile: ProjectFile
    versions: Version[]
    creationTime: Date
    updateTime: Date
}

export interface Version {
    name: string
    description: string
    downloads: number
    firmwareVersionName: string
    modelVersionName: string
    creationTime: Date
}

export interface FirmwareVersion {
    name: string
    description: string
    files: ProjectFile[]
    downloads: number
}

export interface ModelVersion {
    name: string
    description: string
    files: ProjectFile[]
    downloads: number
}

export interface ProjectFile {
    name: string
    path: string
    size: number
    description: string
    downloads: number
}

export const useProjectStore = defineStore('project', {
    state: () => ({
        project: null as Project | null,
        firmwareVersions: [] as FirmwareVersion[],
        modelVersions: [] as ModelVersion[],
    }),

    actions: {
        async fetchProject(username: string, projectName: string) {
            try {
                const response = await service.get(`/api/project/${username}/${projectName}`)
                this.project = response.data
            } catch (error) {
                console.error(error)
            }
        },

        async fetchFirmwareVersions(username: string, projectName: string, firmwareVersionName: string) {
            try {
                const response = await service.get(`/api/project/${username}/${projectName}/firmware/${firmwareVersionName}`)
                this.firmwareVersions = response.data
            } catch (error) {
                console.error(error)
            }
        },

        async fetchModelVersions(username: string, projectName: string, modelVersionName: string) {
            try {
                const response = await service.get(`/api/project/${username}/${projectName}/model/${modelVersionName}`)
                this.modelVersions = response.data
            } catch (error) {
                console.error(error)
            }
        },
    },
})


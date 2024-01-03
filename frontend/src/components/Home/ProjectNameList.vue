<template>
    <h3>Recent Projects</h3>
    <a-input />
    <a-list item-layout="horizontal" :data-source="projectList">
        <template #renderItem="{ item }">
            <a-list-item>
                <a-list-item-meta>
                    <template #title>
                        <a style="font-size: 16px;" v-bind:href="`/TonWin/${item.name}`">{{ item.name }}</a>
                    </template>
                    <template #description>
                        <span style="font-size: 12px;">Updated On {{ item.updateTime }}</span>
                    </template>
                    <template #avatar>
                        <a-avatar src="https://p.qqan.com/up/2021-7/16267471499300276.jpg" />
                    </template>
                </a-list-item-meta>
            </a-list-item>
        </template>
    </a-list>
</template>

<script setup lang="ts">
import { useProfileStore } from '@/store/profile'
import { useAuthStore } from '@/store/auth';
import { useProjectStore,type Project } from '@/store/project';

interface ProjectInfo {
    icon: string
    name: string
    updateTime: string
}

//pinia store
const authStore = useAuthStore()
const profileStore = useProfileStore()
const projectStore = useProjectStore()

const projectList: ProjectInfo[] = []
const projectNameList: string[] = profileStore.profile?.projects!

try{
    await profileStore.fetchProfile(authStore.user?.username!)
    
    projectNameList.forEach(element => {
    const temp:ProjectInfo = {
        icon:"",
        name:"",
        updateTime:Date.now.toString()
    }
    projectStore.fetchProject(authStore.user?.username!, element)
    const project:Project = projectStore.project!
    temp.name = project.name
    temp.updateTime  = new Date(project.updateTime).toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' })

    projectList.push(temp)
});
}catch(e){
    console.log(e)
}




</script>

<style scoped lang="less">

</style>
  
  
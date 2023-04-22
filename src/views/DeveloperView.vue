<template>
    <a-layout>
        <a-layout-sider width="350px">
            <Profile />
        </a-layout-sider>
        <a-layout-content>
            <a-layout-header>
                <a-menu v-model:selectedKeys="current" mode="horizontal">
                    <a-menu-item key="overview">
                        <EyeOutlined/>
                        Overview
                    </a-menu-item>
                    <a-menu-item key="projects">
                        <BookOutlined/>
                        Projects
                    </a-menu-item>
                    <a-menu-item key="timeline" disabled>
                        <ClockCircleOutlined/>
                        Timeline
                    </a-menu-item>
                    <a-menu-item key="stars" disabled>
                        <StarOutlined/>
                        <a href="https://antdv.com" target="_blank" rel="noopener noreferrer">
                            Stars
                        </a>
                    </a-menu-item>
                </a-menu>
            </a-layout-header>
            <ProjectCardList v-if="current[0] === 'overview'"/>
            <ProjectColumnList v-if="current[0] === 'projects'"/>
        </a-layout-content>
    </a-layout>
</template>
  
<script setup lang="ts">
import ProjectColumnList from '@/components/Developer/ProjectColumnList.vue';
import ProjectCardList from '@/components/Developer/ProjectCardList.vue';
import Profile from '@/components/Developer/Profile.vue';
import { EyeOutlined,BookOutlined,ClockCircleOutlined,StarOutlined } from '@ant-design/icons-vue';
import { ref } from 'vue';

const current = ref<string[]>(['overview']);
</script>
<style lang="less" scoped>
.ant-layout {
    max-width: 100%;
    padding-top: 10px;
    padding-left: 10%;
    padding-right: 10%;
    background-color: @white;
}

.ant-layout-header {
    padding: 0 0;
    margin-bottom: 10px;
    background-color: @white;
}
.ant-layout-sider {
    height: 100%;
    padding: 10px;
    text-align: center;
    background-color: @white;
}
.ant-layout-content {
    margin: 0;
    height: 100%;
    width: 100%;
}
.ant-menu{
    background-color:@white;
}
</style>
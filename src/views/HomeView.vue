<template>
    <a-row class="home">
        <a-col class="home-sider" :span="5">
            <ProjectNameList />
        </a-col>
        <a-col :span="1"></a-col>
        <a-col class="home-content" :span="11">
            <RecommendProjects />
            <Footer></Footer>
        </a-col>
        <a-col :span="1"></a-col>
        <a-col :span="6">
            
        </a-col>
    </a-row>
</template>

<script setup lang="ts">
import Footer from '@/components/Public/Footer.vue';
import ProjectNameList from '@/components/Home/ProjectNameList.vue';
import RecommendProjects from '@/components/Home/RecommendProjects.vue';
</script>

<style lang="less" scoped>
.home {
    width: 100%;
    margin: 0;
    background-color: @body-background;
}

.home-sider {
    padding: 20px;
    border-right: 1px solid @border-color-base;
    background-color: @white;
}

.home-content {
    padding-top: 20px;
}
</style>

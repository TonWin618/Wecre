<template>
    <a-layout>
        <a-layout-sider width="350px">
            <ProjectNameList />
        </a-layout-sider>
        <a-layout>
            <a-layout-content>

            </a-layout-content>
            <a-layout-footer>
                <Footer></Footer>
            </a-layout-footer>
        </a-layout>
    </a-layout>
</template>

<script setup lang="ts">
import Footer from '@/components/Public/Footer.vue';
import ProjectNameList from '@/components/Home/ProjectNameList.vue';
</script>

<style lang="less" scoped>
.ant-layout {
    width: 100%;
    margin: 0;
    background-color: @body-background;
}

.ant-layout-sider {
    height: 100%;
    padding: 10px;
    border-right: 1px solid @border-color-base;
    background-color: white;
}

.ant-layout-content {
    margin: 0;
    height: 100%;
    width: 100%;
    background-color: @body-background;
}

.ant-layout-footer{
    background-color: @body-background;
}
</style>

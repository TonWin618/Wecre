<template>
    <a-layout>
        <a-layout-header>
            <div class="header-row1" style="width: 100%;">
                <a-breadcrumb>
                    <a-breadcrumb-item class="user-name"><a :href="`/${$route.params.name}`">{{ $route.params.name
                    }}</a></a-breadcrumb-item>
                    <a-breadcrumb-item class="project-name"><a :href="`/${$route.params.name}/${$route.params.project}`">{{
                        $route.params.project }}</a></a-breadcrumb-item>
                </a-breadcrumb>
            </div>
            <div class="header-row2" style="width: 100%;">
                <a-menu v-model:selectedKeys="current" mode="horizontal">
                    <a-menu-item key="publish">
                        <FileOutlined />
                        Publish
                    </a-menu-item>
                    <a-menu-item key="firmwares">
                        <FileZipOutlined />
                        Firmwares
                    </a-menu-item>
                    <a-menu-item key="models">
                        <CodepenOutlined />
                        Models
                    </a-menu-item>
                    <a-menu-item key="comments">
                        <CommentOutlined />
                        Comments
                    </a-menu-item>
                </a-menu>
            </div>
            <div class="header-version-form" style="">
                <a-space>
                <span style="font-size: 18px;color: black;">version</span>
                <a-select v-model:value="value" show-search placeholder="Select a version" style="width: 200px"
                    :options="options" :filter-option="filterOption" @focus="handleFocus" @blur="handleBlur"
                    @change="handleChange"></a-select>
                <a @click='ShowHistory'>
                    <HistoryOutlined style="color: black;"/>
                </a>
            </a-space>
            </div>
        </a-layout-header>
        <a-layout-content>
            <div style="margin: 20px 100px;">
                <PublishContainer v-if="current [0]==='publish'"/>
                <Replay v-if="current[0] === 'comments'" />
                <Comments v-if="current[0] === 'comments'" />
                <History v-if="current[0] === 'history'"/>
            </div>
        </a-layout-content>
    </a-layout>
</template>
    
<script setup lang="ts" scoped>
import { CodepenOutlined, StarOutlined, FileOutlined, FileZipOutlined, CommentOutlined, HistoryOutlined } from '@ant-design/icons-vue';
import { ref } from 'vue';
import type { SelectProps } from 'ant-design-vue';

import Comments from '@/components/Project/Comments/Comments.vue';
import Replay from '@/components/Project/Comments/Replay.vue';
import History from '@/components/Project/History/History.vue';
import PublishContainer from '@/containers/PublishContainer.vue';

function ShowHistory(){
    current.value[0] = 'history'
}
const current = ref<string[]>(['publish']);
const options = ref<SelectProps['options']>([
    { value: 'V1.01', label: 'V1.01' },
    { value: 'V1.01 Alpha', label: 'V1.01 Alpha' },
    { value: 'V1.00', label: 'V1.00' },
]);
const value = ref();
const handleChange = (value: string) => {
    console.log(`selected ${value}`);
};
const handleBlur = () => {
    console.log('blur');
};
const handleFocus = () => {
    console.log('focus');
};
const filterOption = (input: string, option: any) => {
    return option.value.toLowerCase().indexOf(input.toLowerCase()) >= 0;
};
</script>
<style lang="less" scoped>
.user-name a {
    font-size: 20px;
    font-style: normal;
    color: @link-color;
}

.project-name a {
    font-size: 20px;
    font-weight: bolder;
    color: @link-color !important;
}
.header-version-form{
    width: 100%;
    display: flex;
    align-items: center;
    margin: 0 0;
    padding-left: 200px;
}
.header-version-form .ant-select:not(.ant-select-customize-input) .ant-select-selector{
    background-color: white;
}
.ant-layout {
    width: 100%;
    margin: 0;
}

.ant-layout-header {
    display: flex;
    align-items: center;
    border-bottom: 1px solid @border-color-base;
    background-color: @component-background;
    height: 65px;
}

.ant-layout-sider {
    height: 100%;
    padding: 10px;
    text-align: center;
    background-color: red;
}

.ant-layout-content {
    margin: 0;
    height: 100%;
    width: 100%;
    background-color: white;
}
</style>
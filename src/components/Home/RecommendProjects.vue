<template>
    <div>
        <h3>Following</h3>
        <a-list item-layout="vertical" size="large" :data-source="listData">
            <template #renderItem="{ item }">
                <a-list-item key="item.title">
                    <div class="projects-item">
                    <a-skeleton :loading="loading" active avatar>
                            <a-list-item-meta>
                                <template #title>
                                    <a :href="item.href">{{ item.title }}</a>
                                </template>
                                <template #avatar>
                                    <a-avatar :src="item.avatar" />
                                </template>
                                <template #description>
                                    <span>item.description</span>
                                </template>
                            </a-list-item-meta>
                            {{ item.content }}
                    </a-skeleton>
                </div>
                </a-list-item>
            </template>
        </a-list>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

interface DataItem {
    href: string;
    title: string;
    avatar: string;
    description: string;
    content: string;
}

const listData: DataItem[] = [];

for (let i = 0; i < 7; i++) {
    listData.push({
        href: 'https://www.antdv.com/',
        title: `ant design vue part ${i}`,
        avatar: 'https://joeschmoe.io/api/v1/random',
        description:
            'Ant Design, a design language for background applications, is refined by Ant UED Team.',
        content:
            'We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently.',
    });
}

const loading = ref<boolean>(true);
</script>

<style scoped lang="less">
.projects-item{
    padding: 20px 20px;
    border-radius: @border-radius-base;
    border: 1px solid @border-color-base;
    background-color: @white;
    box-shadow: 0px 3px 6px fade(@border-color-base,50%)
}
.ant-list-split .ant-list-item{
    border: 0px;
}
</style>
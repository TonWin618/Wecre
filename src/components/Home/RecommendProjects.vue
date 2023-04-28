<template>
    <div>
        <a-list item-layout="vertical" size="large" :data-source="listData">
            <template #renderItem="{ item }">
                <a-list-item key="item.title">
                    <template v-if="!loading" #actions>
                        <span v-for="{ type, text } in actions" :key="type">
                            <component is="star-outlined" style="margin-right: 8px"></component>
                            {{ text }}
                        </span>
                    </template>

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

const actions = [
    { type: 'star-outlined', text: '156' },
    { type: 'like-outlined', text: '156' },
    { type: 'message-outlined', text: '2' },
];
</script>

<style scoped lang="less">
.projects-item{
    padding: 20px 20px;
    border-radius: @border-radius-base;
    border: 1px solid @border-color-base;
    background-color: @white;
}
.ant-list-split .ant-list-item{
    border: 0px;
}
</style>
<template>
    <a-list item-layout="vertical" size="large" :pagination="pagination" :data-source="listData">
        <template #renderItem="{ item }">
            <a-list-item key="item.title">
                <template #actions>
                    <span v-for="{ type, text } in actions" :key="type">
                        <component :is="type" style="margin-right: 8px" />
                        {{ text }}
                    </span>
                </template>
                <a-list-item-meta :description="item.description">
                    <template #title>
                        <a style="font-size: 24px;" :href="item.href">{{ item.title }}</a>
                    </template>
                </a-list-item-meta>
                {{ item.content }}
            </a-list-item>
        </template>
    </a-list>
</template>

<script setup lang="ts">
import { StarOutlined, LikeOutlined, MessageOutlined } from '@ant-design/icons-vue';
const listData: Record<string, string>[] = [];

for (let i = 0; i < 230; i++) {
    listData.push({
        href: `/tonwin/test-project${i}`,
        title: `test-project${i}`,
        avatar: 'https://joeschmoe.io/api/v1/random',
        description:
            'Ant Design, a design language for background applications, is refined by Ant UED Team.',
        content:
            'We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently.',
    });
}

const pagination = {
    onChange: (page: number) => {
        console.log(page);
    },
    pageSize: 5,
};
const actions: Record<string, string>[] = [
    { type: 'StarOutlined', text: '156' },
    { type: 'LikeOutlined', text: '156' },
    { type: 'MessageOutlined', text: '2' },
];

const MyComponent = {
    components: {
        StarOutlined,
        LikeOutlined,
        MessageOutlined,
    },
    setup() {
        return {
            listData,
            pagination,
            actions,
        };
    },
};
</script>


  
  
  
  
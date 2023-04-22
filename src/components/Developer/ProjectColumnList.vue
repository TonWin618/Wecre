<template>
    <a-list item-layout="vertical" size="large" :pagination="pagination" :data-source="listData">
        <template #renderItem="{ item }">
            <a-list-item key="item.title">
                <template #actions>
                    <div>
                        <DownloadOutlined />
                        <span> {{ item.downloads }}</span>
                    </div>
                    <div>
                        <StarOutlined />
                        <span> {{ item.star }}</span>
                    </div>
                    <span>Updated 3 years ago</span>
                </template>
                <a-list-item-meta>
                    <template #title>
                        <a style="font-size: 24px;" :href="item.href">{{ item.title }}</a>
                    </template>
                    <template #description>
                        <p>{{ item.description }}</p>
                        <div class="card-tags">
                            <a-tag v-for="(tag, index) in item.tags" :key="index">#{{ tag }}</a-tag>
                        </div>
                    </template>
                </a-list-item-meta>
                {{ item.content }}
            </a-list-item>
        </template>
    </a-list>
</template>

<script setup lang="ts">
import { StarOutlined, LikeOutlined, MessageOutlined, DownloadOutlined } from '@ant-design/icons-vue';
const listData: Record<string, string>[] = [];

for (let i = 0; i < 230; i++) {
    listData.push({
        href: `/tonwin/test-project${i}`,
        title: `test-project${i}`,
        downloads: '1,760,219',
        star: '157',
        description:
            'Ant Design, a design language for background applications, is refined by Ant UED Team.',
        tags:['C++','vue','antd']
    });
}

const pagination = {
    onChange: (page: number) => {
        console.log(page);
    },
    pageSize: 5,
};
</script>

<style lang="less" scoped></style>


  
  
  
  
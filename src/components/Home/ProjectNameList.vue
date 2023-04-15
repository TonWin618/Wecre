<template>
    <a-list item-layout="horizontal" :data-source="data">
        <template #renderItem="{ item }">
            <a-list-item>
                <a-list-item-meta>
                    <template #title>
                        <a style="font-size: 16px;" v-bind:href="`/TonWin/${item.title}`">{{ item.title }}</a>
                    </template>
                    <template #description>
                            <span style="font-size: 12px;">Updated On {{ updatedTime }}</span>
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
import { ref } from 'vue';

interface DataItem {
    title: string;
}
const updatedTime =  ref();
updatedTime.value = new Date().toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' });
console.log(updatedTime.value)
const data: DataItem[] = [
    {
        title: 'wecre-frontend',
    },
    {
        title: 'wecre-backend',
    },
    {
        title: 'hi-webserver',
    },
    {
        title: 'hi-webclient',
    },
    {
        title: 'MemoCard',
    },
];
</script>
  
  
<template>
    <a-list item-layout="horizontal" :data-source="data">
      <template #renderItem="{ item }">
        <a-list-item>
          <a-list-item-meta
            description="Ant Design, a design language for background applications"
          >
            <template #title>
              <a v-bind:href= "`/TonWin/${item.title}`">{{ item.title }}</a>
            </template>
            <template #avatar>
              <a-avatar src="https://p.qqan.com/up/2021-7/16267471499300276.jpg" />
            </template>
          </a-list-item-meta>
        </a-list-item>
      </template>
    </a-list>
  </template>
  <script lang="ts">
  import { defineComponent } from 'vue';
  interface DataItem {
    title: string;
  }
  const data: DataItem[] = [
    {
      title: 'Ant Design Title 1',
    },
    {
      title: 'Ant Design Title 2',
    },
    {
      title: 'Ant Design Title 3',
    },
    {
      title: 'Ant Design Title 4',
    },
    {
      title: 'Ant Design Title 4 - Ant Design Title 4909009090',
    },
  ];
  export default defineComponent({
    setup() {
      return {
        data,
      };
    },
  });
  </script>
  
  
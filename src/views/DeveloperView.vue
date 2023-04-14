<template>
  <a-layout>
    <a-layout-sider width="350px">
      <Profile/>
    </a-layout-sider>
    <a-layout-content>
      <ProjectCardList/>
    </a-layout-content>
  </a-layout>
</template>
  
<script setup lang="ts">
import ProjectCardList from '@/components/Developer/ProjectCardList.vue';
import Profile from '@/components/Developer/Profile.vue';
</script>
<style scoped>
.ant-layout{
  width: 100%;
  margin: 0;
}
.ant-layout-sider{
  height: 100%;
  padding: 10px;
  text-align: center;
  background-color: #ffffff;
}

.ant-layout-content{
  margin: 0;
  height: 100%;
  width: 100%;
  background-color: #ffffff;
}
</style>
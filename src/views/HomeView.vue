<template>
  <a-layout>
    <a-layout-sider width="350px">
      <ProjectList/>
    </a-layout-sider>
    <a-layout>
      <a-layout-content>
        
      </a-layout-content>
      <a-layout-footer>
        <Footer/>
      </a-layout-footer>
    </a-layout>
  </a-layout>
</template>

<script setup lang="ts">
import Footer from '@/components/Footer.vue';
import ProjectList from '@/components/ProjectList.vue';
</script>

<style scoped>
.ant-layout{
  width: 100%;
  margin: 0;
}
.ant-layout-sider{
  height: 100%;
  padding: 10px;
  border-right: 1px solid #D0D7DE;
  background-color: #ffffff;
}
.ant-layout-content{
  margin: 0;
  height: 100%;
  width: 100%;
}
</style>

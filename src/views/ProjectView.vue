<template>
    <a-layout>
      <a-layout-header>
        <a-breadcrumb>
            <a-breadcrumb-item class="user-name"><a :href="`/${$route.params.name}`">{{$route.params.name}}</a></a-breadcrumb-item>
            <a-breadcrumb-item class="project-name"><a :href="`/${$route.params.name}/${$route.params.project}`">{{$route.params.project}}</a></a-breadcrumb-item>
        </a-breadcrumb>
      </a-layout-header>
      <a-layout-content>
      </a-layout-content>
    </a-layout>
  </template>
    
  <script setup lang="ts">
  
  </script>
  <style scoped>
  .user-name{
    font-size: 32px;
    font-style: normal;
    color: #ffffff;
  }
  .project-name{
    font-size: 32px;
    font-weight: bold;
  }
  .ant-layout{
    width: 100%;
    margin: 0;
  }

  .ant-layout-header{
  display: flex;
  color: #ffffff;
  background-color: #ffffff;
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
<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { ref } from 'vue';
import SearchBar from './components/SearchBar.vue';
var selectedKeys = ref<string[]>(['']);
</script>

<template>
  <a-layout>
    <a-layout-header>
      <div class="header-logo">
        <span>We</span>
      </div>
      <div class="header-search">
        <SearchBar />
      </div>
      <div class="header-menu">
        <a-menu
        @selectedKeys="selectedKeys"
        mode="horizontal"
        theme="dark"
        selectable=false
        triggerSubMenuAction="click"
        >
          <a-menu-item key="1">
            <RouterLink to="/">Home</RouterLink>
          </a-menu-item>
          <a-menu-item key="2">
            <RouterLink to="/test">Test</RouterLink>
          </a-menu-item>
        </a-menu>
      </div>
      <div class="header-right">
        <a-space>
          <RouterLink to="/login">
            <a-button ghost>Login
            </a-button>
          </RouterLink>
          <a-divider type="vertical" />
          <RouterLink to="/signUp">
            <a-button ghost>
              Sign up
            </a-button>
          </RouterLink>
          
        </a-space>
      </div>
    </a-layout-header>
    <a-layout-content>
      <RouterView />
    </a-layout-content>
  </a-layout>
</template>

<style scoped>

.header-logo{
  color: #ffffff;
  font-size:x-large;
}
.header-search{
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 15px;
}
.header-right {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  flex-grow: 1;
}
.ant-layout{
  height: 100%;
}
.ant-layout-header{
  display: flex;
  color: #ffffff;
}
</style>

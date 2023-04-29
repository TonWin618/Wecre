<template>
    <a-row class="header" align="middle">
        <a-col :span="1"></a-col>
        <a-col class="header-logo" :span="1" >
            <div class="header-logo">
                <img src="https://i.postimg.cc/3wzpbsz1/wecre.png" width="48" />
            </div>
        </a-col>
        <a-col :span="5">
            <div class="header-search">
                <SearchBar />
            </div>
        </a-col>
        <a-col :span="13">
            <div class="header-menu">
                <a-menu @selectedKeys="selectedKeys" mode="horizontal" theme="dark" triggerSubMenuAction="click">
                    <a-menu-item key="1">
                        <RouterLink to="/">Home</RouterLink>
                    </a-menu-item>
                    <a-menu-item key="2">
                        <RouterLink to="/test">Test</RouterLink>
                    </a-menu-item>
                </a-menu>
            </div>
        </a-col>
        <a-col :span="3">
            <div class="header-right">
                <div v-if="!authStore.isLoggedIn">
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
            </div>
        </a-col>
        <a-col :span="1"></a-col>
    </a-row>
    <a-row>
        <a-col :span="24">
            <RouterView/>
        </a-col>
    </a-row>
</template>

<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { ref } from 'vue';
import SearchBar from './components/Public/SearchBar.vue'
import { useAuthStore } from './store/auth';

const authStore = useAuthStore()
var selectedKeys = ref<string[]>(['']);
</script>

<style lang="less" scoped>
.header {
    height: 60px;
    background-color: @heading-color;
}
.header .header-logo{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
}
</style>

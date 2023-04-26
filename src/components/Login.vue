<template>
    <div class="login-form">
        <div class="login-item">
            <a-typography-text>Username / Email</a-typography-text>
            <a-input type="text" id="username" v-model="username" required />
        </div>
        <div class="login-item">
            <a-typography-text>Password</a-typography-text>
            <a-input-password type="password" id="password" v-model="password" required />
        </div>
        <a-button class="login-button" type="primary" @click.prevent="authStore.loginWithUsername(username,password)">Login</a-button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useAuthStore } from '@/stores/auth';
const username = ref('')
const password = ref('')
const authStore = useAuthStore();


</script>

<style lang="less" scoped>
.login-form {
    max-width: 300px;
}

.login-item {
    margin-top: 10px;
}

.login-button {
    margin-top: 15px;
    width: 100%;
}
</style>
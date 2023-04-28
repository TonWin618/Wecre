<template>
    <div class="login-form">
        <div class="login-item">
            <a-typography-text>Username / Email</a-typography-text>
            <a-input type="text" id="username" v-model:value="usernameOrEmail" required />
        </div>
        <div class="login-item">
            <a-typography-text>Password</a-typography-text>
            <a-input-password type="password" id="password" v-model:value="password" required />
        </div>
        <a-button class="login-button" type="primary" @click.prevent="login()">Login</a-button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useAuthStore } from '@/store/auth';

const authStore = useAuthStore()

const usernameOrEmail = ref('')

const password = ref('')

function isEmail(input: string): boolean {
    const pattern: RegExp = /^[a-zA-Z0-9]+[\.\-\_]?[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,5}$/
    return pattern.test(input)
}

function login() {
    console.log(usernameOrEmail.value);
    if (isEmail(usernameOrEmail.value)) {
        authStore.loginWithEmail(usernameOrEmail.value, password.value)
    } else {
        authStore.loginWithUsername(usernameOrEmail.value, password.value)
    }
}
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
<template>
    <div class="login-form">
        <div class="login-form-header">
            <img class="icon" src="wecre.png">
            <h1>Sign in to Wecre</h1>
        </div>
        <Login class="login-form-body" />
        <Footer></Footer>
    </div>
</template>

<script setup lang="ts">
import Login from '@/components/Login.vue';
import Footer from '@/components/Public/Footer.vue';
</script>

<style scoped>
.login-form{
    margin-bottom: 0px;
}
.login-form-header {
    margin-top: 50px;
    text-align: center;
}

.login-form-header img {
    width: 48px;
}

.login-form-header h1 {
    font-weight: lighter;
    font-size: 24px;
}

.login-form-body {
    margin: 0 auto;
}
</style>

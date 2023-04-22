<script setup lang="ts">
import SignUp from '@/components/SignUp.vue';
import Footer from '@/components/Public/Footer.vue';
</script>

<template>
    <div class="signup-form-header">
        <img class="icon" src="wecre.png">
        <h1>Welcome to Wecre!</h1>
    </div>
    <SignUp class="signup-form-body" />
    <Footer></Footer>
</template>

<style lang="less" scoped>
.signup-form-header{
    margin-top: 50px;
    text-align: center;
}
.signup-form-header img {
    width: 48px;
}
.signup-form-header h1{
    font-size: 24px;
    font-weight: lighter;
}
.signup-form-body{
    margin: 0 auto;
}
</style>

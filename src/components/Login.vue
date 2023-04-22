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
      <a-button class="login-button" type="primary" @click.prevent="submit">Login</a-button>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import axios from 'axios';

const username = ref('');
const password = ref('');

const submit = async () => {
  try {
    const response = await axios.post('/api/login', {
      username: username.value,
      password: password.value,
    });

    console.log(response.data); // Do something with the response data
  } catch (error) {
    console.error(error);
  }
};
</script>

<style scoped>
.login-form{
  width: 300px;
}
.login-item{
  margin-top: 10px;
}
.login-button{
  margin-top: 15px;
  width: 100%;
}
</style>
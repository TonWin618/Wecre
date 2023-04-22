<template>
    <div class="signUp-form">
      <form>
        <div class="signup-item">
          <a-typography-text>Username</a-typography-text>
          <a-input type="text" id="username" v-model="username" required />
        </div>
        <div class="signup-item">
          <a-typography-text>Email Address</a-typography-text>
          <a-input type="text" id="password" v-model="email" required />
        </div>
        <div class="signup-item">
          <a-typography-text>Password</a-typography-text>
          <a-input-password type="password" id="password" v-model="password" required />
        </div>
        <div class="signup-item">
          <a-typography-text>Confirm Password</a-typography-text>
          <a-input-password type="password" id="password" v-model="confirmPassword" required />
        </div>
          <a-button class="signup-button" type="primary" @click.prevent="submit">Sign Up</a-button>
      </form>
    </div>
</template>

<script setup lang="ts">
  import { ref } from 'vue';
  import axios from 'axios';

  const username = ref('');
  const email = ref('');
  const password = ref('');
  const confirmPassword = ref('');

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
.signUp-form{
  max-width: 300px;
}
.signup-item{
  margin-top: 10px;
}
.signup-button{
  margin-top: 15px;
  width: 100%;
}
</style>
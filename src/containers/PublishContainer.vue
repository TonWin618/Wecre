<template>
    <div class="container">
        <Readme class="readme" />
        <Overview class="overview" />
        <Footer class="footer"></Footer>
    </div>
</template>

<script setup lang="ts">
import Overview from '@/components/Project/Publish/Overview.vue';
import Readme from '@/components/Project/Publish/Readme.vue';
import Footer from '@/components/Public/Footer.vue';

</script>

<style scoped>
.container {
    display: grid;
    grid-template-rows: auto 1fr auto;
    grid-template-columns: 1fr 1fr;
    height: 100vh;
}

.footer {
    background-color: #ffffff;
    grid-row: 3 / 4;
    grid-column: 1 / -1;
}

.overview {
    background-color: #ffffff;
    grid-row: 1 / -1;
    grid-column: 2 / 3;
}

.readme {
    background-color: #ffffff;
    grid-row: 1 / -1;
    grid-column: 1 / 2;
}</style>
<template>
    <div class="container">
        <div class="model-list">
            <h1>Model Version 1.00</h1>
            <ModelsList />
        </div>
        <div class="model-preview">
            <h1>Preview</h1>
            <a-image src="https://aliyuncdn.antdv.com/logo.png" :preview="{
                src: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png',
            }" />
        </div>
    </div>
</template>

<script setup lang="ts">
import ModelsList from '@/components/Project/Models/ModelsList.vue';
</script>

<style scoped lang="less">
.container {
    display: grid;
    grid-template-rows: auto auto;
    grid-template-columns: 1fr 1fr;
}
.model-preview{
    width: 80%;
}
</style>
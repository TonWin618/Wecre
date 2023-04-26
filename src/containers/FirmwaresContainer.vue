<template>
    <div class="container">
        <div class="firmware-list">
            <h1>Firmware Version 1.01</h1>
            <FirmwareList />
        </div>
    </div>
</template>

<script setup lang="ts">
import FirmwareList from '@/components/Project/Firmwares/FirmwareList.vue';
</script>

<style scoped lang="less">
.container {
    display: grid;
    place-items: center;
}
</style>
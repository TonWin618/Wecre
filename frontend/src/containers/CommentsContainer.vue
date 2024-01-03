<template>
    <div class="container">
        <div class="Comments-area">
            <div class="replay">
                <Replay />
            </div>
            <div class="comments">
                <h1>2 Comments</h1>
                <Comments />
            </div>
        </div>
        <div class="calender">
            <h1>Calender</h1>
            <div :style="{ width: '300px', border: '1px solid #d9d9d9', borderRadius: '4px' }">
                <a-calendar v-model:value="value" :fullscreen="false" @panelChange="onPanelChange" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import Comments from '@/components/Project/Comments/Comments.vue';
import Replay from '@/components/Project/Comments/Replay.vue';
import { ref } from 'vue';
import type { Dayjs } from 'dayjs';

const value = ref();
const onPanelChange = (value: Dayjs, mode: string) => {
    console.log(value, mode);
};
</script>

<style scoped lang="less">
.container {
    display: grid;
    grid-template-columns: 3fr 1fr;
}
</style>
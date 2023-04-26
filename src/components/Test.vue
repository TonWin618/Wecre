<template>
    <h1>{{ counterStore.count }}</h1>
    <button @click="counterStore.increment"> + </button>
    <button @click="counterStore.decrement"> - </button>
</template>

<script setup lang="ts">
import {useCounterStore} from '@/stores/counter.js'
const counterStore = useCounterStore()
</script>
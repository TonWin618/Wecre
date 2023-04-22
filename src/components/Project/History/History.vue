<template>
    <a-timeline>
        <a-timeline-item>
            <a-card title="V1.0.1 Alpha" style="width: 600px;background-color:white">
                <template #extra><a href="#">more</a></template>
                <p>1. Solve initial network problems 2015-09-01</p>
                <p>2. Technical testing 2015-09-01</p>
                <p>3. ard content</p>
            </a-card>
            <br />
        </a-timeline-item>
        <a-timeline-item>Solve initial network problems 2015-09-01</a-timeline-item>
        <a-timeline-item>Technical testing 2015-09-01</a-timeline-item>
        <a-timeline-item>Network problems being solved 2015-09-01</a-timeline-item>
    </a-timeline>
</template>  
<script setup lang="ts">

</script>

<style lang="less" scoped>
.ant-card-header{
    background-color: red;
}
</style>
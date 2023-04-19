<template>
    <div class="overview">
        <div class="contributors">
            <h3>Contributors</h3>
            <a-avatar-group :max-count="4" size="large" :max-style="{
                color: '#f56a00',
                backgroundColor: '#fde3cf',
            }">
                <a-avatar src="https://joeschmoe.io/api/v1/random" />
                <a-avatar style="background-color: #1890ff">K</a-avatar>
                <a-avatar style="background-color: #1890ff">
                    <template #icon>
                        <UserOutlined />
                    </template>
                </a-avatar>
                <a-avatar style="background-color: #1890ff">
                    <template #icon>
                        <UserOutlined />
                    </template>
                </a-avatar>
                <a-avatar style="background-color: #1890ff">
                    <template #icon>
                        <UserOutlined />
                    </template>
                </a-avatar>
                <a-avatar style="background-color: #1890ff">
                    <template #icon>
                        <UserOutlined />
                    </template>
                </a-avatar>
                <a-avatar style="background-color: #1890ff">
                    <template #icon>
                        <UserOutlined />
                    </template>
                </a-avatar>
                <a-avatar style="background-color: #1890ff">
                    <template #icon>
                        <UserOutlined />
                    </template>
                </a-avatar>
            </a-avatar-group>
        </div>
        <a-divider />
        <div class="about">
            <h3>About</h3>
            <p>This template should help get you started developing with Vue 3 in Vite.</p>
        </div>
        <a-divider />
        <div class="update">
            <h3>Update</h3>
            <p>Updated at {{ updatedTime }}</p>
            <p>Created at {{ createdTime }}</p>
        </div>
        <a-divider />
        <div class="tags">
            <h3>Tags</h3>
            <a-tag>MQTT</a-tag>
            <a-tag>Bluetooth</a-tag>
            <a-tag>HTTP</a-tag>
            <a-tag>Transform</a-tag>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { UserOutlined } from '@ant-design/icons-vue';
const createdTime = ref('');
const updatedTime = ref('');
</script>

<style scoped>
.overview {
    width: 400px;
    padding: 1.2rem;
}
</style>
<template>
    <div class="overview">
        <div class="actions">
            <a-button>
                <template #icon>
                    <StarOutlined/>
                </template>
                Star
            </a-button>
        </div>
        <div class="statistic">
            <a-statistic title="Weekly Donwloads" :value="11293221">
                <template #suffix>
                    <DownloadOutlined />
                </template>
            </a-statistic>
            <a-divider />
            <a-statistic title="Last Published" :value="'2 years ago'">
            </a-statistic>
        </div>
        <a-divider />
        <div class="about">
            <h3>About</h3>
            <p>This template should help get you started developing with Vue 3 in Vite.</p>
        </div>
        <a-divider />
        <div class="tags">
            <h3>Tags</h3>
            <a-tag>MQTT</a-tag>
            <a-tag>Bluetooth</a-tag>
            <a-tag>HTTP</a-tag>
            <a-tag>Transform</a-tag>
        </div>
        <a-divider />
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

    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { UserOutlined, DownloadOutlined, StarOutlined, StarFilled } from '@ant-design/icons-vue';
const createdTime = ref('');
const updatedTime = ref('');
</script>

<style lang="less" scoped>
.overview {
    width: 400px;
    padding: 1.2rem;
}
.actions{
    text-align: right;
}
</style>
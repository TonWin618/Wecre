<template>
    <div class="model-form">
        <a-list item-layout="horizontal" :data-source="data">
            <template #renderItem="{ item }">
                <a-list-item>
                    <a-list-item-meta class="model-item">
                        <template #title>
                            <div class="item-title">
                                <a href="https://www.antdv.com/">{{ item.title }}</a>
                                <div class="download-btn">
                                    <a-button size="small">
                                        <template #icon>
                                            <DownloadOutlined />
                                        </template>
                                        Download
                                    </a-button>
                                </div>
                            </div>
                        </template>
                        <template #description>
                            <p>English word learning equipment based on stm32</p>
                            <div class="item-footer">
                                <div class="downloads">
                                    <a-statistic value="299320" :value-style="{ 'font-size': '14px', 'color': '#656D76' }">
                                        <template #prefix>
                                            <DownloadOutlined />
                                        </template>
                                    </a-statistic>
                                </div>
                                <a-divider type="vertical" />
                                <div>
                                    <span>6.32MB</span>
                                </div>
                                <a-divider type="vertical" />
                                <div class="uploader">
                                    <a-avatar :size=18 src="https://p.qqan.com/up/2021-7/16267471499300276.jpg">
                                        <template #icon>
                                            <UserOutlined />
                                        </template>
                                    </a-avatar>
                                    <a href="/TonWin618"> TonWin618</a>
                                    <span> uploaded at 2 years ago</span>
                                </div>
                            </div>
                        </template>
                    </a-list-item-meta>
                </a-list-item>
            </template>
        </a-list>
    </div>
</template>
<script setup lang="ts">
import { DownloadOutlined } from '@ant-design/icons-vue';
interface DataItem {
    title: string;
}
const data: DataItem[] = [
    {
        title: 'memocard-green.obj',
    },
    {
        title: 'memocard-red.obj',
    },
    {
        title: 'memocard-blue.obj',
    },
    {
        title: 'memocard-black.obj',
    },
];
</script>
  
<style lang="less" scoped>
.model-form {
    width: 600px;
    border: 1px solid @border-color-base;
    border-radius: @border-radius-base;
}

.model-item {
    margin: 5px 20px;
    display: flex;
}

.item-footer {
    display: flex;
    align-items: center;
}

.downloads {
    display: flex;
    align-items: flex-start;
}

.item-title {
    display: flex;
}

.download-btn {
    margin-left: auto;
}
</style>
  
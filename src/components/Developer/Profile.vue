<template>
    <div class="profile">
        <div class="profile-avatar">
            <a-avatar :size=256 src="https://p.qqan.com/up/2021-7/16267471499300276.jpg">
            </a-avatar>
        </div>
        <div class="profile-info" style="text-align: left;">
            <h1 class="nickname">TonWin</h1>
            <h1 class="username">@{{ $route.params.name }}</h1>
            <p class="profile">A hobbyist in front-end , back-end , embedded systems , and game development.</p>
            <a-button class="edit-btn" block>Edit profile</a-button>
        </div>
        <div class="profile-follow">
            <TeamOutlined />
            <span class="number">210</span>
            <span class="describe"> follower</span>
            <span> · </span>
            <span class="number">10</span>
            <span class="describe"> following</span>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { TeamOutlined } from '@ant-design/icons-vue';
</script>

<style lang="less" scoped>
.profile {
    width: 256px;
}

.profile-avatar .ant-avatar {
    border: 2px solid @border-color-base;
    text-align: center;
}

.profile-info {
    text-align: center;
}

.profile-info .nickname {
    font-size: 24px;
    font-weight: normal;
    margin-bottom: 0px;
}

.profile-info .username {
    font-size: 20px;
    font-weight: lighter;
    margin-bottom: 10px;
}

.profile-info .edit-btn {
    width: 100%;
}
.profile-follow{
    padding-top: 10px;
    text-align: left;
}
.profile-follow span {
    width: 50px;
    font-size: 12px;
    font-weight: normal;
}

.profile-follow .number {
    font-weight: bolder;

}
</style>
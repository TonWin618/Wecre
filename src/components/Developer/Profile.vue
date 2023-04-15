<template>
    <div>
        <a-avatar :size=256 
        src="https://p.qqan.com/up/2021-7/16267471499300276.jpg"
        style="border: 2px solid #D0D7DE;">
        </a-avatar>
    </div>
    <div class="info" style="text-align: left;">
        <p class="nickname">Nick Name</p>
        <p class="username">@{{$route.params.name}}</p>
    </div>
</template>

<style scoped>
.info{
    text-align: left;
    margin-left: 50px;
}
.nickname{
    font-size: 26px;
    margin-bottom: 0;
}
.username{
    font-size: 18px;
    color: darkgray;
    margin-bottom: 0;
}
.a-avatar{
    border: 2px solid #000;
}
</style>
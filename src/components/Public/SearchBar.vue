<template>
    <div class="search-bar">
        <a-input-search placeholder="input search text" @search=""/>
    </div>
</template>

<style lang="less">
.search-bar{
    max-width: 400px;
    margin: auto auto;
}
.search-bar .ant-input{
    color: @white !important;
    background-color: @heading-color !important;
}
.search-bar .ant-btn{
    background-color: @heading-color !important;
}
.search-bar .ant-btn:hover{
    border-color: @primary-color !important;
}
.search-bar .ant-input-search-button{
    color: @white !important;
}
</style>
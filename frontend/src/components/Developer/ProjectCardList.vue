<template>
    <p>Popular Projects</p>
    <div class="card-container">
        <div class="card-row" v-for="(row, index) in cardRows()" :key="index">
            <a-card class="card-item" v-for="(card, index) in row" :key="index" >
                <a :href="`/tonwin/${card.title}`" slot="title">{{ card.title }}</a>
                <p>{{ card.description }}</p>
                <div class="card-tags">
                    <a-tag v-for="(tag, index) in card.tags" :key="index">#{{ tag }}</a-tag>
                </div>
            </a-card>
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineComponent } from 'vue'

type Card = {
    title: string
    link: string
    description: string
    tags: string[]
}

const cardList: Card[] = [
    {
        title: 'Card 1',
        link: '/card-1',
        description: 'This is the first card',
        tags: ['Vue', 'Axios']
    },
    {
        title: 'Card 2',
        link: '/card-2',
        description: 'This is the second card',
        tags: ['Tag C', 'Tag D']
    },
    {
        title: 'Card 3',
        link: '/card-3',
        description: 'This is the third card',
        tags: ['Tag E', 'Tag F']
    },
    {
        title: 'Card 4',
        link: '/card-4',
        description: 'This is the fourth card',
        tags: ['Tag G', 'Tag H']
    },
    {
        title: 'Card 5',
        link: '/card-5',
        description: 'This is the fifth card',
        tags: ['Tag I', 'Tag J']
    },
    {
        title: 'Card 6',
        link: '/card-6',
        description: 'This is the fourth card',
        tags: ['Tag G', 'Tag H']
    }
]

const cardRows = () => {
    const rows: Card[][] = []
    for (let i = 0; i < cardList.length; i += 2) {
        if (cardList[i + 1]) {
            rows.push([cardList[i], cardList[i + 1]])
        } else {
            rows.push([cardList[i]])
        }
    }
    return rows
}

defineComponent({
    computed: {
        cardRows: cardRows
    }
})
</script>

<style lang="less" scoped>
.ant-card-body{
    padding: 0px !important;
}
.card-row {
    display: flex;
    justify-content: left;
    margin-bottom: 5px;
    width: 100%;
}
.card-item {
    width: 400px;
    height: 120px;
    padding: 0px;
    margin-bottom: 5px;
    margin-right: 10px;
    background-color: @white;
}
.card-item a{
    font-size: 14px;
    font-weight: bolder;
}
</style>

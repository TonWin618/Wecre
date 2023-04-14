<template>
  <h1>Projects</h1>
  <div class="card-container">
    <div class="card-row" v-for="(row, index) in cardRows()" :key="index">
      <a-card
        v-for="(card, index) in row"
        :key="index"
        class="card-item"
      >
        <a style="font-size: 20px;" href="card" slot="title">{{ card.title }}</a>
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
    tags: ['Tag A', 'Tag B']
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

<style scoped>
template{

}
.card-container {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
}

.card-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 5px;
  width: 100%;
}

.card-item {
  width: calc(50% - 2.5px);
  height: 140px;
  margin-bottom: 5px;
}

.card-item:not(:last-child) {
  margin-right: 10px;
}



.card-tags span {
  background-color: #eee;
}
</style>

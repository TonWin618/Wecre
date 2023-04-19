<template>
    <h1>README.md</h1>
    <div class='markdown'>
        <div v-html="markdownToHtml"></div>
    </div>
</template>

<script setup lang="ts">
import axios from 'axios';
import { marked } from 'marked';
import { ref, shallowRef } from 'vue';

const render = new marked.Renderer;

marked.setOptions({
    renderer: render,
    gfm: true,
    pedantic: false,
    sanitize: false
})

const markdown = ref('');
const markdownToHtml = shallowRef('');
axios.get("https://raw.githubusercontent.com/TonWin618/wecre-frontend/master/README.md").then(
    response => {
        markdown.value = response.data
        markdownToHtml.value = marked(markdown.value);
    }
)

</script>

<style>
.markdown {
    width: 800px;
    border: 1px solid #D0D7DE;
    border-radius: 0.8rem;
    padding: 1.2rem;
}

h1 {
    font-size: 1.8em;
}

/* https://raw.githubusercontent.com/TonWin618/wecre-frontend/master/README.md */
</style>
  
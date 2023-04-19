<template>
    <h1>README.md</h1>
    <div style="width: 800px;border: 1.5px solid #D0D7DE;border-radius: 0.8rem;padding: 1.2rem;">
        <div v-html="markdownToHtml"></div>
    </div>
    <Footer></Footer>
</template>

<script setup lang="ts">
import axios from 'axios';
import { marked } from 'marked';
import { ref, shallowRef } from 'vue';
import Footer from '../../Public/Footer.vue';

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
h1{
    font-size: 2em;
}
/* https://raw.githubusercontent.com/TonWin618/wecre-frontend/master/README.md */
</style>
  
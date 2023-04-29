import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [vue()],
    server:{
        host:'0.0.0.0',
        port:5173,
        https:false
    },
    css: {
        preprocessorOptions: {
            dark: false,
            less: {
                modifyVars: {
                    hack: 'true;@import (reference) "ant-design-vue/lib/style/themes/default.less";',
                    'primary-color': '#0969DA',
                    'link-color': '#0969DA',
                    'success-color': '#1F883D',
                    'warning-color': '#FD8C73',
                    'error-color': '#CF222E',
                    'font-size-base': '14px',
                    'heading-color': '#24292F',
                    'text-color': '#1F2328',
                    'text-color-secondary': '#656D76',
                    'body-background': '#F6F8FA',
                    'component-background': '#F6F8FA',
                    'border-color-base': '#D0D7DE',
                    'border-color-split': '#D0D7DE',
                    'border-radius-base': '6px',
                    'btn-primary-bg': '#1F883D',
                    'menu-dark-item-active-bg': '#24292F'
                },
                javascriptEnabled: true
            }
        }
    },
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    }
})

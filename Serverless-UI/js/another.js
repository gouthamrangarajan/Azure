import { createApp } from 'https://unpkg.com/petite-vue?module'
createApp({
    count: 0,
    inc() {
        this.count++;
    }
}).mount('#app')
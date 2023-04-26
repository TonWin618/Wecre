import { defineStore } from 'pinia'

interface CounterState { count: number }

export const useCounterStore = defineStore('counter',
    {
        state: (): CounterState => ({ count: 0, }),
        getters: { doubleCount(): number { return this.count * 2 } },
        actions: {
            increment() { this.count++ },
            decrement() { this.count-- }
        }
    }
)
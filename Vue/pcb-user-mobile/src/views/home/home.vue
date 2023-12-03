<template>
        <RouterView />
    <t-tab-bar v-model="value" theme="tag" :split="false" @change="change">
        <t-tab-bar-item v-for="item in list" :key="item.value" :value="item.value">
            {{ item.label }}
        </t-tab-bar-item>
    </t-tab-bar>
</template>
  
<script setup lang="ts">
import { ref, watch, inject } from 'vue';
const Router = inject("$Router")
import { RouterLink, RouterView } from 'vue-router'
const value = ref('home');
const list = ref([
    { value: '/order', label: '首页' },
    { value: '/my', label: '我的' },
]);

const change = (changeValue:string) => {
    Router.push(changeValue)
};
</script>
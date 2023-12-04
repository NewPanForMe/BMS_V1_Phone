<template>
    <div class="header">
      
    </div>
    <div class="body">
        <RouterView />
    </div>
    <div class="footer">
        <t-tab-bar v-model="value" theme="tag" :split="true" @change="change">
            <t-tab-bar-item v-for="item in list" :key="item.value" :value="item.value">
                {{ item.label }}
                <template #icon>
                    <t-icon :name="item.icon" />
                </template>
            </t-tab-bar-item>
        </t-tab-bar>
    </div>
</template>
  
<script setup lang="ts">
import { ref, watch, inject } from 'vue';
import { Icon as TIcon } from 'tdesign-icons-vue-next';
const Router = inject("$Router")
import { RouterLink, RouterView } from 'vue-router'
const value = ref('home');
const list = ref([
    { value: '/order', label: '首页', icon: 'home' },
    { value: '/my', label: '我的', icon: 'user' },
]);

const change = (changeValue: string) => {
    Router.push(changeValue)
};
//进来自动跳转
const order = () => { Router.push('/order') }
setTimeout(order, 200)


</script>
<template>
    <p>订单查看页</p>
    <t-fab :icon="iconFunc" @click="onClick" style="bottom:65px;" />
</template>

  
<script lang="ts" setup>
import { h } from 'vue';
import { AddIcon } from 'tdesign-icons-vue-next';
import { ref, watch, inject } from 'vue';
const Router = inject("$Router")
const iconFunc = () => h(AddIcon, { size: '24px' });
const onClick = () => {
    Router.push("/orderadd")
};
</script>

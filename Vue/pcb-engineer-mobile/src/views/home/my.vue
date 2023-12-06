<template>
    <t-cell-group bordered>
        <!-- <t-cell title="地址信息" arrow hover  @click="addressClick"/> -->
        <t-cell title="退出登录" arrow hover  @click="logout"/>
    </t-cell-group>
</template>
<script setup>
import { reactive, ref, inject } from 'vue'
const instance = inject("$instance")
const Utils = inject("$Utils")
const Api = inject("$Api")
const Cookies = inject("$Cookies")
const Router = inject("$Router")


const logout=()=>{
      Cookies.removeToken();
    Router.push("/login")
}

</script>
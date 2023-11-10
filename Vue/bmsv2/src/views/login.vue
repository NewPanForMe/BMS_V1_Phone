<template>
    <div class="loginbody">
        <div class="loginTitle"> 后台管理系统v2 </div>
        <div class="loginContent">
            <div class="loginContent-account">
                <n-input type="text" placeholder="用户名" round />
            </div>
            <div class="loginContent-password">
                <n-input type="password" show-password-on="mousedown" placeholder="密码" :maxlength="8" round />
            </div>
            <div class="loginContent-button">
                <n-button tertiary type="success">
                    登录
                </n-button>
            </div>

        </div>

        ;


    </div>
</template>
<script setup >
import { ref } from 'vue'

console.log("浏览器内窗高度" + window.innerHeight + "px")
console.log("浏览器内窗宽度" + window.innerWidth + "px")

</script>
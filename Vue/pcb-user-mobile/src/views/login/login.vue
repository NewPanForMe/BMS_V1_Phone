<template>
    <div>
        <div class="loginText">
            <h1>PCB下单系统</h1>
            <h4>欢迎使用</h4>
        </div>
        <t-form ref="form" :data="formData" @submit="loginBtnClick">
            <div class="loginbox">
                <t-form-item label="账户名" name="account" for="account">
                    <t-input v-model="formData.account" borderless placeholder="账户名"></t-input>
                </t-form-item>
                <t-form-item label="密码" name="password" for="password">
                    <t-input v-model="formData.password" borderless type="password" :clearable="false" placeholder="请输入密码">
                    </t-input>
                </t-form-item>
            </div>
            <div class="loginBtn button-demo">
                <t-button size="large" theme="primary" variant="outline" type="submit">登录</t-button>
            </div>
        </t-form>
        <div class="bottomText">
            <t-link theme="primary" @click="register">没有账号吗？先注册吧</t-link>
        </div>
    </div>
</template>

<script  lang="ts" setup>
import { ref, reactive, inject } from 'vue'
const instance = inject("$instance")
const Utils = inject("$Utils")
const Api = inject("$Api")
const Cookies = inject("$Cookies")
const Router = inject("$Router")



//表单信息
const form = ref(null)
//表单数据
let formData = reactive({
    account: "",
    password: ""
})


//登录按钮点击事件
const loginBtnClick = (e) => {
    e.preventDefault();
    // if (formData.account == "" || formData.password == "") {
    //     Utils.showWarning('账号/密码为空，请检查后重试');
    //     return;
    // }
    // instance.instance.post(Api.CustomerLogin.Login, formData).then(resp => {
    //     if (!resp.success) {
    //         Utils.showError(resp.result);
    //         return;
    //     }
    //     else {
             Utils.showSuccess('登录成功，正在跳转.....');
    //         Cookies.saveToken(resp.result)
    setTimeout(jumpToHome, 2000)
    //     }
    // })
}

const jumpToHome = () => { Router.push('/home') }
const register = () => { Router.push('/register') }




</script>
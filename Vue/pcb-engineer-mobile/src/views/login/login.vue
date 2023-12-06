<template>
    <div>
        <div class="loginText">
            <h1>{{  title }}</h1>
            <h4>欢迎使用</h4>
        </div>
        <t-form ref="form" :data="formData" @submit="loginBtnClick" :rules="rules">
            <div class="loginbox">
                <t-form-item label="账户名" name="account" for="account">
                    <t-input v-model="formData.account" borderless placeholder="账户名"></t-input>
                </t-form-item>
                <t-form-item label="密码" name="password" for="password">
                    <t-input v-model="formData.password" borderless type="password" :clearable="false" placeholder="请输入密码"
                        >
                    </t-input>
                </t-form-item>
            </div>
            <div class="loginBtn button-demo">
                <t-button size="large" theme="primary" variant="outline" type="submit">登录</t-button>
            </div>
        </t-form>
        <div class="bottomText">
            <t-link theme="primary" @click="register">没有账号？快来注册</t-link>
        </div>
    </div>
</template>

<script    setup>
import { ref, reactive, inject } from 'vue'
const instance = inject("$instance")
const Utils = inject("$Utils")
const Api = inject("$Api")
const Cookies = inject("$Cookies")
const Router = inject("$Router")

const title= ref(window.server.projectName)
//表单信息
const form = ref(null)
//表单数据
let formData = reactive({
    account: "",
    password: ""
})


const rules = {
    account: [{ validator: (val) => val !== "", message: '账户名不能为空' }],
    password: [{ validator: (val) => val !== "", message: '密码不能为空' }],
};

//登录按钮点击事件
const loginBtnClick = (e) => {
    if (e.validateResult==true) {
        instance.instance.post(Api.EngineerLogin.Login, formData).then(resp => {
            if (!resp.success) {
                Utils.showError(resp.result);
                return;
            }
            else {
                Utils.showSuccess('登录成功，正在跳转.....');
                Cookies.saveToken(resp.result)
                setTimeout(jumpToHome, 2000)
            }
        })
    }

}

const jumpToHome = () => {   setTimeout(Router.push('/order'), 2000)  }
const register = () => {      setTimeout( Router.push('/register'), 2000) }


</script>
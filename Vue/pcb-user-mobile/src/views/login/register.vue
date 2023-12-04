<template>
    <div>
        <div class="loginText">
            <h4>欢迎注册</h4>
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

                <t-form-item label="再次输入密码" name="passwordTwo" for="passwordTwo">
                    <t-input v-model="formData.passwordTwo" borderless type="password" :clearable="false"
                        placeholder="请输入密码">
                    </t-input>
                </t-form-item>
            </div>
            <div class="loginBtn button-demo">
                <t-button size="large" theme="primary" variant="outline" type="submit">注册</t-button>
            </div>
        </t-form>

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
    password: "",
    passwordTwo: ""
})


//登录按钮点击事件
const loginBtnClick = (e) => {
    e.preventDefault();
    if (formData.account == "" || formData.password == "" || formData.passwordTwo == "") {
        Utils.showWarning('账号/密码为空，请检查后重试');
        return;
    }
    if (formData.password != formData.passwordTwo) {
        Utils.showWarning('两次密码输入不一致');
        return;
    }
    instance.instance.post(Api.CustomerLogin.Register, formData).then(resp => {
        if (!resp.success) {
            Utils.showError(resp.result);
            return;
        }
        else {
            Utils.showSuccess('注册成功，正在登录.....');
            Cookies.saveToken(resp.result)
            setTimeout(jumpToHome, 2000)
        }
    })
}

const jumpToHome = () => { Router.push('/home') }




</script>
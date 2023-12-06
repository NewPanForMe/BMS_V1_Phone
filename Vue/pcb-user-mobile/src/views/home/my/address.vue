<template>
  <t-collapse>
    <t-collapse-panel value="0" header="点击添加地址">
      <div class="content">
        <t-form ref="form" :data="formData" @submit="submitBtnClick">
          <t-form-item label="姓名" name="name" for="name">
            <t-input v-model="formData.name" borderless placeholder="姓名"></t-input>
          </t-form-item>
          <t-form-item label="电话" name="phone" for="phone">
            <t-input v-model="formData.phone" borderless placeholder="电话" :tips="tips" />
          </t-form-item>
          <t-form-item label="地址" name="address" for="address">
            <t-input v-model="formData.address" borderless placeholder="地址" />
          </t-form-item>
          <div class="addressBtn">
            <t-button size="large" theme="primary" variant="outline" type="submit">添加</t-button>
          </div>
        </t-form>
      </div>
    </t-collapse-panel>
  </t-collapse>
  <p class="tipText">我们将严格保密您的个人信息</p>
  <p class="tipText">在选用时，回选用您最新的一条地址</p>
  <br>
  <t-cell-group bordered>
    <t-cell v-for="item in list " :key="item" :title="item.address" />
  </t-cell-group>
</template>
<script setup   >
import { reactive, ref, inject, onBeforeMount, computed } from 'vue'
const instance = inject("$instance")
const Utils = inject("$Utils")
const api = inject("$Api")
const Cookies = inject("$Cookies")
const Router = inject("$Router")

const list = ref([])


const formData = reactive({
  name: "",
  phone: "",
  address: ""
})

const isPhoneNumber = (e) => {
  if (/^[1][3,4,5,7,8,9][0-9]{9}$/.test(e)) {
    return true;
  }
  return false;
}


const submitBtnClick = (e) => {
  if (!isPhoneNumber(formData.phone)) {
    Utils.showWarning('手机号格式不正确');
    return;
  }
  if (e.validateResult == true) {
    instance.instance.post(api.UserAddress.Add, formData).then(resp => {
      if (resp.success) {
        Utils.showSuccess('提交成功');
        loadData()
        formData.address = ""
        formData.name = ""
        formData.phone = ""
        return;
      }
    })
  }
}


const loadData = () => {
  list.value.length = 0;
  instance.instance.post(api.UserAddress.UserAddressList).then(resp => {
    for (let index = 0; index < resp.result.count; index++) {
      list.value.push(resp.result.list[index])
    }
  })
};


onBeforeMount(() => {
  loadData()
})


</script>
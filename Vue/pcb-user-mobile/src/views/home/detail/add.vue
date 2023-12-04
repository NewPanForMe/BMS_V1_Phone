<template>
    <div class=""></div>
    <t-form ref="form" :data="formData" :rules="rules" @submit="submitBtnClick">
        <t-form-item label="标题" name="title" for="title" help="简要描述需求">
            <t-input v-model="formData.title" aria-required="true" borderless placeholder="标题"></t-input>
        </t-form-item>
        <t-form-item label="需求信息" name="comment" for="comment" help="（详细描述需求，描述的越清晰，工程师做的越准确哦~）">
            <t-textarea v-model="formData.comment" name="需求信息" placeholder="请输入文字" autosize />
        </t-form-item>
        <t-form-item label="预计完成时间" name="finishTime" for="finishTime" help="（时间别太短哦~）">
            <t-cell title="" :note="pickerValueText || '点击选择'" @click="visible = true" />
        </t-form-item>
        <t-popup v-model="visible" placement="bottom">
            <t-date-time-picker :value="pickerValue" :mode="['date']" title="选择日期" :start="getCurrentDate()"
                format="YYYY-MM-DD" @confirm="onConfirm" />
        </t-popup>
        <div class="order-add-btn button-demo">
            <t-button size="large" theme="primary" type="submit">提交</t-button>
        </div>
    </t-form>
</template>
<script setup lang="ts">
import { reactive, ref, inject } from 'vue'
const instance = inject("$instance")
const Utils = inject("$Utils")
const Api = inject("$Api")
const Cookies = inject("$Cookies")
const Router = inject("$Router")



const formData = reactive({
    title: "",
    comment: "",
    finishTime: ""
})
//获取当前日期
const getCurrentDate = () => Utils.getCurrentDate()
const visible = ref(false);
const pickerValue = ref('')
const pickerValueText = ref('');
const onConfirm = (value: string) => {
    formData.finishTime = value;
    console.log('confirm: ', value);
    pickerValue.value = value;
    pickerValueText.value = value;
    visible.value = false;
};

const rules = {
    title: [{ validator: (val: any) => val !== "", message: '请填写标题' }],
    comment: [{ validator: (val: any) => val !== "", message: '请填写需求' }],
    finishTime: [{ validator: (val: any) => val !== "", message: '请选择预计完成时间' }],
};

const submitBtnClick = (e: any) => {
    if (e.validateResult == true) {
        instance.instance.post(Api.PcbOrder.Add, formData).then(resp => {
            if (resp.success) {
                Utils.showSuccess('提交成功');
                setTimeout(order, 2000)
                return;
            }
        })
    }
}
const order = () => { Router.push('/order') }

</script>
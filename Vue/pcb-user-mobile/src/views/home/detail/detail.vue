<template>
    <p class=" button-demo"> </p>
    <t-form ref="form" :data="formData" :rules="rules" @submit="submitBtnClick">
        <t-form-item label="标题">
            {{ formData.title }}
        </t-form-item>
        <t-form-item label="需求信息">
            {{ formData.comment }}
        </t-form-item>
        <t-form-item label="预计完成时间">
            {{ formData.finishTime }}
        </t-form-item>

        <t-form-item label="订单状态">
            <t-tag size="large" variant="dark" :theme="tagTheme">{{ formData.orderStatus }}</t-tag>
        </t-form-item>
        <t-form-item label="工程师报价">
            {{ formData.evaluate }} 元
        </t-form-item>
        <t-form-item label="工程师备注">
            {{ formData.remark }}
        </t-form-item>
        <div class="order-add-btn ">
            <div>
                <div style="float: right;width: 50%;">
                    <t-button size="large" theme="danger" @click="cancelClick" variant="text">作废订单</t-button>
                </div>
                <div style="float: right;width: 50%;">
                    <t-button v-if="formData.orderStatus == '已估价'"    @click="comfirmClick"   size="large" theme="primary">确认订单</t-button>
                </div>
            </div>
        </div>
    </t-form>


    <t-dialog v-model:visible="isShowCancel" title="确认作废吗？" content="作废后工程师将无法看到此条订单" cancel-btn="取消" confirm-btn="确认"
        @confirm="sureCancel">
    </t-dialog>
</template>
<script setup>
import { h, ref, reactive, inject, onMounted } from 'vue';
import { useRoute } from 'vue-router'

const route = useRoute()
const instance = inject("$instance")
const router = inject("$Route")
const api = inject("$Api")
const Utils = inject("$Utils")
const entity = ref()
const code = route.query.code;
let param = { code: code }
const tagTheme = ref("")
const isShowCancel = ref(false)


const formData = reactive({
    title: "",
    comment: "",
    finishTime: "",
    acceptDateTime: "",
    acceptEngineer: "",
    code: "",
    evaluate: "0",
    orderStatus: "",
    realFinishTime: "",
    remark: ""
})

const rules = {
    title: [{ validator: (val) => val !== "", message: '请填写标题' }],
    comment: [{ validator: (val) => val !== "", message: '请填写需求' }],
    finishTime: [{ validator: (val) => val !== "", message: '请选择预计完成时间' }],
};

instance.instance.post(api.PcbOrder.Order, param).then(resp => {
    if (!resp.success) {
        Utils.showWarning(resp.result);
        return;
    }
    else {
        entity.value = resp.result.pcbOrder
        formData.title = resp.result.pcbOrder.title
        formData.comment = resp.result.pcbOrder.comment
        formData.finishTime = resp.result.pcbOrder.finishTime
        formData.acceptDateTime = resp.result.pcbOrder.acceptDateTime
        formData.acceptEngineer = resp.result.pcbOrder.acceptEngineer
        formData.code = resp.result.pcbOrder.code
        formData.evaluate = resp.result.pcbOrder.evaluate
        formData.orderStatus = resp.result.pcbOrder.orderStatus
        formData.realFinishTime = resp.result.pcbOrder.realFinishTime
        formData.remark = resp.result.pcbOrder.remark
        if (formData.orderStatus == "未接单") {
            tagTheme.value = "default";
        }
        if (formData.orderStatus == "已接单" || formData.orderStatus == "已估价" || formData.orderStatus == "确认订单" || formData.orderStatus == "已完成待发送快递" || formData.orderStatus == "快递已发出") {
            tagTheme.value = "primary";
        }
        if (formData.orderStatus == "订单完成") {
            tagTheme.value = "success";
        }
        if (formData.orderStatus == "订单拒绝") {
            tagTheme.value = "danger";
        }
        if (formData.orderStatus == "订单作废") {
            tagTheme.value = "danger";
        }
    }
    console.log(formData)
})





const cancelClick = () => {
    isShowCancel.value = true;

}


const sureCancel = () => {
  
    instance.instance.post(api.PcbOrder.Cancel, param).then(resp => {
        if (!resp.success) {
            Utils.showWarning(resp.result);
            return;
        } else {
            Utils.showSuccess(resp.result);
            router.back();
        }
    })

}




const comfirmClick = () => {
    instance.instance.post(api.PcbOrder.Confirm, param).then(resp => {
        if (!resp.success) {
            Utils.showWarning(resp.result);
            return;
        } else {
            Utils.showSuccess(resp.result);
            router.back();
        }
    })

}



</script>
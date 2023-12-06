<template>
    <p class=" button-demo"> </p>
    <t-form ref="form" :data="formData">
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
            <t-textarea v-model="formData.remark" autosize readonly />
        </t-form-item>
        <t-form-item label="物流单号">
            {{ formData.followNum }}
        </t-form-item>

        <div class="order-add-btn ">
            <div>
                <div style="float: right;width: 50%;">
                    <t-button size="large" v-if="formData.orderStatus == '未接单'" @click="receiveClick"
                        theme="text">抢单</t-button>
                </div>
                <div style="float: right;width: 50%;">
                    <t-button v-if="formData.orderStatus == '已接单'" @click="isEvaluate = true" size="large"
                        theme="text">报价</t-button>
                </div>
                <div style="float: right;width: 50%;">
                    <t-button v-if="formData.orderStatus == '确认订单'" size="large" theme="danger"
                        @click="isShowRefuse = true">拒绝订单</t-button>
                </div>
                <div style="float: right;width: 50%;">
                    <t-button v-if="formData.orderStatus == '确认订单'" size="large" theme="text"
                        @click="isFollow = true">输入快递单号</t-button>
                </div>
            </div>
        </div>
    </t-form>


    <t-collapse v-if="formData.orderStatus=='确认订单'">
            <t-collapse-panel value="0" header="点击查看快递信息">
                <div class="content">
                    <t-form>
                        <t-form-item label="姓名" name="name" for="name">
                            {{ addressData.name }}
                        </t-form-item>
                        <t-form-item label="电话" name="phone" for="phone">
                            {{ addressData.phone }}

                        </t-form-item>
                        <t-form-item label="地址" name="address" for="address">
                            {{ addressData.address }}
                        </t-form-item>
                    </t-form>
                </div>
            </t-collapse-panel>
        </t-collapse>

    <t-dialog v-model:visible="isShowRefuse" title="确认拒绝吗？" content="拒绝订单后，您可以进行重新抢单。" cancel-btn="取消" confirm-btn="确认"
        @confirm="sureRefuse">
    </t-dialog>
    <div class="popup-demo">
        <t-popup v-model="isEvaluate" placement="bottom" style="height: 300px">
            <t-form ref="form" :data="evaluateData" @submit="evaluateClick" :rules="evaluateRules">
                <div class="header">
                    <div class="btn btn--cancel" aria-role="button" @click="onHide">取消</div>
                    <div class="title">估价</div>
                    <t-button size="large" type="submit" theme="text">确定</t-button>
                </div>
                <t-form-item label="估价" name="evaluate" for="evaluate">
                    <t-input v-model="evaluateData.evaluate" borderless placeholder="估价"></t-input>
                </t-form-item>
                <t-form-item label="备注" name="remark" for="remark">
                    <t-textarea v-model="evaluateData.remark" name="备注" placeholder="备注" autosize />
                </t-form-item>
            </t-form>
        </t-popup>
    </div>



    <div class="popup-demo">
        <t-popup v-model="isFollow" placement="bottom" style="height: 258px">
            <t-form ref="form" :data="followData" @submit="followClick" :rules="followRules">
                <div class="header">
                    <div class="btn btn--cancel" aria-role="button" @click="isFollowHide">取消</div>
                    <div class="title">物流</div>
                    <t-button size="large" type="submit" theme="text">确定</t-button>
                </div>
                <t-form-item label="物流单号" name="followNum" for="followNum">
                    <t-input v-model="followData.followNum" borderless placeholder="物流单号"></t-input>
                </t-form-item>

            </t-form>
        </t-popup>
    </div>
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
//拒绝确认框
const isShowRefuse = ref(false)
//报价弹出层
const isEvaluate = ref(false);
const onHide = () => (isEvaluate.value = false);

//报价弹出层
const isFollow = ref(false);
const isFollowHide = () => (isFollow.value = false);
//基础数据
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
    remark: "",
    followNum: "",
    createUser: ""
})

//报价数据
const evaluateData = reactive({
    evaluate: "0",
    remark: "",
    code: code
})

//物流数据
const followData = reactive({
    code: code,
    followNum: ""
})

const addressData = reactive({
    name: "",
    phone: "",
    address: ""
})


//数字验证
const numTest = (val) => {
    return (/^(0|[+-]?((0|([1-9]\d*))\.\d+)?)$/).test(val)
}

//报价验证
const evaluateRules = {
    evaluate: [{ validator: (val) => numTest(val), message: '只能输入数字,需要包含小数点' }, { validator: (val) => val !== "", message: '报价不能为空' }],
};

//报价验证
const followRules = {
    followNum: [{ validator: (val) => val !== "", message: '单号不能为空' }],
};


//刷新
const refresh = () => {

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
            formData.followNum = resp.result.pcbOrder.followNum
            formData.createUser = resp.result.pcbOrder.createUser
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

            followAddress()
        }
        console.log(formData)
    })

}

//抢单
const receiveClick = () => {
    // isShowCancel.value = true;
    instance.instance.post(api.PcbOrder.ReceiveOrder, param).then(resp => {
        if (!resp.success) {
            Utils.showWarning(resp.result);
            return;
        } else {
            Utils.showSuccess(resp.result);
            refresh()
        }
    })
}

//拒绝
const sureRefuse = () => {
    instance.instance.post(api.PcbOrder.Refuse, param).then(resp => {
        if (!resp.success) {
            Utils.showWarning(resp.result);
            return;
        } else {
            Utils.showSuccess(resp.result);
        }
        refresh();
    })
}

//报价
const evaluateClick = (e) => {
    if (e.validateResult == true) {
        instance.instance.post(api.PcbOrder.Evaluate, evaluateData).then(resp => {
            if (!resp.success) {
                Utils.showWarning(resp.result);
                return;
            } else {
                Utils.showSuccess(resp.result);
            }
            isEvaluate.value = false;
        })
        refresh()
    }
}


//物流
const followClick = (e) => {
    if (e.validateResult == true) {
        instance.instance.post(api.PcbOrder.Follow, followData).then(resp => {
            if (!resp.success) {
                Utils.showWarning(resp.result);
                return;
            } else {
                Utils.showSuccess(resp.result);
            }
            isFollow.value = false;
            refresh()
        })
    }
}

//物流地址
const followAddress = () => {
    instance.instance.post(api.UserAddress.UserAddressByUserCode, { userCode: formData.createUser }).then(resp => {
        if (!resp.success) {
            Utils.showWarning(resp.result);
            return;
        } else {
            addressData.address = resp.result.address
            addressData.phone = resp.result.phone
            addressData.name = resp.result.name
        }
    })
}



refresh()
</script>

<style >
.popup-demo {
    padding: 0 16px;
}

.header {
    display: flex;
    align-items: center;
    height: 116rpx;
}

.title {
    flex: 1;
    text-align: center;
    font-weight: 600;
    font-size: 18px;
    color: var(--td-text-color-primary, rgba(0, 0, 0, 0.9));
}

.btn {
    font-size: 16px;
    padding: 16px;
}

.btn--cancel {
    color: var(--td-text-color-secondary, rgba(0, 0, 0, 0.6));
}

.btn--confirm {
    color: #0052d9;
}
</style>
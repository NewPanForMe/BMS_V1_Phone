<style scoped>
.upload-title{
    margin-top: 1vh;
    margin-left: 1vh;
}
</style>
<template>
    <div class="upload-demo">
        <div class="upload-title">多选上传</div>
        <t-upload multiple :max="5" :action="action" :on-fail="onFail" :on-success="onSuccess" :headers="header"
            :size-limit="{ size: 300, unit: 'MB', message: '图片大小不超过300MB' }" :default-files="files"
            :before-upload="beforeUpload">
        </t-upload>
    </div>
</template>
<script setup >
import { h, ref, onBeforeMount, inject, onMounted, defineEmits } from 'vue';
const route = inject("$Router")
const utils = inject("$Utils")
const instance = inject("$instance")
const api = inject("$Api")
const cookies = inject("$Cookies")

let header = ref({});
let code = ref([]);
const action = window.server.baseDevUrl + api.File.Upload;
const emit = defineEmits(["codeStrings"]);

const backCode = (e) => {
    emit("codeStrings", e);
};

const onFail = ({ file, e }) => {
    utils.showError(file.name + '上传失败，请检查后重试')
    return null;
};
const onSuccess = ({ file, fileList, response, e }) => {
    for (let i = 0; i < fileList.length; i++) {
        const element = fileList[i];
        element.url = window.server.baseFileUrl + element.response.result.location
        if(!code.value.includes( element.response.result.code)){
            code.value.push( element.response.result.code)
        }
        if (element.response.result.success) {
            files.value.push({
                name: element.raw.name,
                url: window.server.baseFileUrl + element.response.result.location,
                type: 'image/jepg',
            })
        }
    }
    backCode(code)
};

const files = ref([]);

const beforeUpload = (file) => {
    var token = cookies.getToken();
    header.value = {
        Authorization: "Bearer " + token.tokenName,
    };
};
</script>
  
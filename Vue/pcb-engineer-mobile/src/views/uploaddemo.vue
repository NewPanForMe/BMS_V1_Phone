<template>
  <div class="upload-demo">
    <div class="upload-title">多选上传</div>
    <t-upload multiple :max="5" :action="action" :on-fail="onFail" :on-success="onSuccess"    :headers="header"   :before-upload="beforeUpload">
    </t-upload>
  </div>
</template>
<script setup >
import { h, ref, onBeforeMount, inject, onMounted } from 'vue';
const route = inject("$Router")
const instance = inject("$instance")
const api = inject("$Api")
const cookies = inject("$Cookies")
let header = ref({});
const action = window.server.baseDevUrl + api.File.Upload;


const onFail = ({ file, e }) => {
  console.log('---onFail', file, e);
  return null;
};

const onSuccess = ({ file, fileList, response, e }) => {
  console.log('====onSuccess', file, fileList, e, response);
};

const beforeUpload = (file) => {
    var token = cookies.getToken();
    header.value = {
        Authorization: "Bearer " + token.tokenName,
    };
};
</script>

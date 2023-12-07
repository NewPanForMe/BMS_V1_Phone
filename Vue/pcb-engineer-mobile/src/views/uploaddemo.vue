<template>
 <fileUpload   v-on:code-strings="codeString" />
</template>
<script setup >
import { h, ref, onBeforeMount, inject, onMounted } from 'vue';
import fileUpload from '../component/upload.vue'
const route = inject("$Router")
const utils = inject("$Utils")
const instance = inject("$instance")
const api = inject("$Api")
const cookies = inject("$Cookies")
let header = ref({});

const codeString=(e)=>{
  console.log("codeString",e.value)
}

</script>

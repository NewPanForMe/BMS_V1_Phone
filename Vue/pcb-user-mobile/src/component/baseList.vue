<template>
    <div>
        <div class="orderlist" v-for="item in prop.list " :key="item">
            <div class=""></div>
            <div class="orderlistTag"> <t-tag variant="dark" theme="warning">{{ item.orderStatus }}</t-tag></div>
        </div>
    </div>
</template>
  
<script  setup>
import { h, ref, reactive, inject, onMounted ,defineProps} from 'vue';
var prop = defineProps({
    list: { type: Array, default: () => [] },
});
</script>

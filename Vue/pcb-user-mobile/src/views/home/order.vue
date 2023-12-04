<template>
    <div>
        <baseList :list="list" />
    </div>
    <t-table
      row-key="index"
      :data="data"
      :columns="columns"
      :show-header="showHeader"
      cell-empty-content="-"
      @row-click="handleRowClick"
      @cell-click="handleCellClick"
    ></t-table>
    <t-fab :icon="iconFunc" @click="onAddClick" />
</template>

  
<script  setup>
import { AddIcon } from 'tdesign-icons-vue-next';

import baseList from '@/component/baseList.vue'
import { h, ref, reactive, inject, onMounted } from 'vue';
const route = inject("$Router")
const instance = inject("$instance")
const api = inject("$Api")
const iconFunc = () => h(AddIcon, { size: '24px' });
let list = [];




const status = ref(" ")

const onAddClick = () => {
    route.push("/orderadd");

};

const detailClick = (code) => {
    console.log(code)
};

const loadData = () => {
    instance.instance.get(api.PcbOrder.OrderList + "?status=" + status.value).then(resp => {
        for (let index = 0; index < resp.result.count; index++) {
            list.push(resp.result.list[index])
        }
    })
    console.log(list)
};


onMounted(()=>{
    loadData()
})




const data= [];
const total = 10;
for (let i = 0; i < total; i++) {
  data.push({
    index: i + 1,
    applicant: ['内容', '内容', '内容'][i % 3],
    status: ['内容', '内容', '内容'][i % 3],
    channel: ['内容', '内容', '内容'][i % 3],
    detail: {
      email: ['内容', '内容', '内容内容内容'][i % 3],
    },
  });
}

const bordered = ref(true);
const showHeader = ref(true);

const columns = ref([
  { colKey: 'applicant', title: '标题', ellipsis: true },
  {
    colKey: 'status',
    title: '标题',
    ellipsis: true,
  },
]);

const handleRowClick = (e) => {
  console.log('row-click=====', e);
};

const handleCellClick = (e) => {
  console.log('cell-click=====', e);
};


</script>


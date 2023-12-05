<template>
  <t-dropdown-menu>
    <t-dropdown-item label="订单状态" :options="product.options" :value="product.value" @change="onChange" />
  </t-dropdown-menu>


  <t-table row-key="index" :data="list" :columns="columns" :show-header="true" cell-empty-content="-"
    @row-click="handleRowClick" @cell-click="handleCellClick">
    <template #status="{ col, row }">
      {{ row[col.colKey].orderStatus }}
    </template>
  </t-table>
  <t-fab :icon="iconFunc" @click="onAddClick" />
</template>

  
<script  setup>
import { AddIcon } from 'tdesign-icons-vue-next';

import { h, ref, onBeforeMount, inject, onMounted } from 'vue';
const route = inject("$Router")
const instance = inject("$instance")
const api = inject("$Api")


const iconFunc = () => h(AddIcon, { size: '24px' });
let list = ref([]);

const status = ref(" ")

const onAddClick = () => {
  route.push("/orderadd");
};


const loadData = () => {
  let param = { status: status.value }
  list.value.length=0;
  instance.instance.post(api.PcbOrder.OrderList, param).then(resp => {
    for (let index = 0; index < resp.result.count; index++) {
      list.value.push(resp.result.list[index])
    }
  })
};


onBeforeMount(()=>{
  loadData()
})

const columns = ref([
  { colKey: 'title', title: '标题', ellipsis: true },
  { colKey: 'orderStatus', title: '订单状态', ellipsis: true },
  {
    colKey: 'createDateTime',
    title: '创建时间',
    ellipsis: true,
  },
]);

const handleRowClick = (e) => {
  let code = e.row.code
  route.push({ path: "/orderdetail", query: { code: code } })
};

const handleCellClick = (e) => {
};

const product = {
  value: 'all',
  options: [
    {
      value: '',
      label: '全部',
    },
    {
      value: '未接单',
      label: '未接单',
    },
    {
      value: '已接单',
      label: '已接单',
    },
    {
      value: '已估价',
      label: '已估价',
    },
    {
      value: '确认订单',
      label: '确认订单',
    },
    {
      value: '已完成，待发送快递',
      label: '已完成，待发送快递',
    },
    {
      value: '快递已发出',
      label: '快递已发出',
    },
    {
      value: '订单完成',
      label: '订单完成',
    },
    {
      value: '订单拒绝',
      label: '订单拒绝',
    },
    {
      value: '订单作废',
      label: '订单作废',
    },
  ],
};
function onChange(e) {
  status.value = e;
  loadData();
}


</script>


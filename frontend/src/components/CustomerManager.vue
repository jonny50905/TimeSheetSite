<template>
  <div>
    <h2>Customers</h2>
    <form class="row g-2 mb-3" @submit.prevent="addCustomer">
      <div class="col-auto">
        <label class="form-label">名稱
          <input class="form-control" v-model="newCustomer.name" required />
        </label>
      </div>
      <div class="col-auto">
        <label class="form-label">編號
          <input class="form-control" v-model="newCustomer.code" required />
        </label>
      </div>
      <div class="col-auto">
        <label class="form-label">聯絡人
          <input class="form-control" v-model="newCustomer.contact" required />
        </label>
      </div>
      <div class="col-auto align-self-end">
        <button class="btn btn-primary" type="submit">新增客戶</button>
      </div>
    </form>
    <table v-if="customers.length" class="table table-bordered">
      <thead>
        <tr>
          <th>名稱</th>
          <th>編號</th>
          <th>聯絡人</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="c in customers" :key="c.id">
          <td>{{ c.name }}</td>
          <td>{{ c.code }}</td>
          <td>{{ c.contact }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const customers = ref([])
const newCustomer = ref({ name: '', code: '', contact: '' })

async function load() {
  customers.value = await fetch('/api/customers').then(r => r.json())
}

async function addCustomer() {
  await fetch('/api/customers', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(newCustomer.value)
  })
  newCustomer.value = { name: '', code: '', contact: '' }
  load()
}

onMounted(load)
</script>

<style scoped>
</style>

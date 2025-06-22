<template>
  <div>
    <h2>Customers</h2>
    <form class="row g-2 mb-3" @submit.prevent="saveCustomer">
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
        <button class="btn btn-primary" type="submit">{{ editingId ? '儲存' : '新增客戶' }}</button>
        <button v-if="editingId" class="btn btn-secondary ms-2" @click="cancelEdit" type="button">取消</button>
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
          <td><button class="btn btn-sm btn-secondary" @click="editCustomer(c)">Edit</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const customers = ref([])
const newCustomer = ref({ name: '', code: '', contact: '' })
const editingId = ref(null)

async function load() {
  customers.value = await fetch('/api/customers').then(r => r.json())
}

function editCustomer(c) {
  newCustomer.value = { name: c.name, code: c.code, contact: c.contact }
  editingId.value = c.id
}

async function saveCustomer() {
  const url = editingId.value ? `/api/customers/${editingId.value}` : '/api/customers'
  const method = editingId.value ? 'PUT' : 'POST'
  const payload = { ...newCustomer.value }
  if (editingId.value !== null) payload.id = editingId.value
  await fetch(url, {
    method,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload)
  })
  cancelEdit()
  load()
}

function cancelEdit() {
  newCustomer.value = { name: '', code: '', contact: '' }
  editingId.value = null
}

onMounted(load)
</script>

<style scoped>
</style>

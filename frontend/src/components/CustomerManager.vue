<template>
  <div>
    <h2>Customers</h2>
    <form @submit.prevent="addCustomer">
      <label>
        名稱
        <input v-model="newCustomer.name" required />
      </label>
      <label>
        編號
        <input v-model="newCustomer.code" required />
      </label>
      <label>
        聯絡人
        <input v-model="newCustomer.contact" required />
      </label>
      <button type="submit">新增客戶</button>
    </form>
    <table v-if="customers.length">
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
form {
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
  margin-bottom: 1rem;
}
label {
  display: flex;
  flex-direction: column;
}
</style>

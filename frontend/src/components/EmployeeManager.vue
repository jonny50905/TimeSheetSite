<template>
  <div>
    <h2>Employees</h2>
    <form @submit.prevent="addEmployee">
      <label>
        Name
        <input v-model="employee.name" required />
      </label>
      <label>
        Hourly Rate
        <input type="number" step="0.01" v-model.number="employee.hourlyRate" required />
      </label>
      <button type="submit">Add</button>
    </form>
    <table v-if="employees.length">
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Hourly Rate</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="e in employees" :key="e.id">
          <td>{{ e.id }}</td>
          <td>{{ e.name }}</td>
          <td>{{ e.hourlyRate }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const employees = ref([])
const employee = ref({ name: '', hourlyRate: 0 })

async function load() {
  employees.value = await fetch('/api/employees').then(r => r.json())
}

async function addEmployee() {
  await fetch('/api/employees', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(employee.value)
  })
  employee.value = { name: '', hourlyRate: 0 }
  load()
}

onMounted(load)
</script>

<style scoped>
form {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 0.5rem;
}
label {
  display: flex;
  flex-direction: column;
}
table {
  border-collapse: collapse;
}
th, td {
  border: 1px solid #ccc;
  padding: 4px 8px;
}
</style>

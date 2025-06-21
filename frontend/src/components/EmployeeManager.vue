<template>
  <div>
    <h2>Employees</h2>
    <form class="row g-2 mb-3" @submit.prevent="addEmployee">
      <div class="col-auto">
        <label class="form-label">Name
          <input class="form-control" v-model="employee.name" required />
        </label>
      </div>
      <div class="col-auto">
        <label class="form-label">Hourly Rate
          <input class="form-control" type="number" step="0.01" v-model.number="employee.hourlyRate" required />
        </label>
      </div>
      <div class="col-auto align-self-end">
        <button class="btn btn-primary" type="submit">Add</button>
      </div>
    </form>
    <table v-if="employees.length" class="table table-bordered">
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
</style>

<template>
  <div>
    <h2>Employees</h2>
    <form class="row g-2 mb-3" @submit.prevent="saveEmployee">
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
        <button class="btn btn-primary" type="submit">{{ editingId ? 'Save' : 'Add' }}</button>
        <button v-if="editingId" class="btn btn-secondary ms-2" @click="cancelEdit" type="button">Cancel</button>
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
          <td><button class="btn btn-sm btn-secondary" @click="editEmployee(e)">Edit</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const employees = ref([])
const employee = ref({ name: '', hourlyRate: 0 })
const editingId = ref(null)

async function load() {
  employees.value = await fetch('/api/employees').then(r => r.json())
}

function editEmployee(e) {
  employee.value = { name: e.name, hourlyRate: e.hourlyRate, id: e.id }
  editingId.value = e.id
}

async function saveEmployee() {
  const url = editingId.value ? `/api/employees/${editingId.value}` : '/api/employees'
  const method = editingId.value ? 'PUT' : 'POST'
  const payload = { ...employee.value }
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
  employee.value = { name: '', hourlyRate: 0 }
  editingId.value = null
}

onMounted(load)
</script>

<style scoped>
</style>

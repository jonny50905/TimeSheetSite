<template>
  <form @submit.prevent="submitEntry">
    <label>
      Employee
      <select v-model="entry.employeeId" required>
        <option :value="e.id" v-for="e in employees" :key="e.id">{{ e.name }}</option>
      </select>
    </label>
    <label>
      Project
      <select v-model="entry.projectId" required>
        <option :value="p.id" v-for="p in projects" :key="p.id">{{ p.name }}</option>
      </select>
    </label>
    <label>
      Date
      <input type="date" v-model="entry.date" required />
    </label>
    <label>
      Hours
      <input type="number" step="0.1" v-model="entry.hours" required />
    </label>
    <label>
      Notes
      <input type="text" v-model="entry.notes" />
    </label>
    <button type="submit">Add Entry</button>
  </form>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const emit = defineEmits(['added'])
const employees = ref([])
const projects = ref([])
const entry = ref({ employeeId: '', projectId: '', date: '', hours: 0, notes: '' })

onMounted(async () => {
  employees.value = await fetch('/api/employees').then(r => r.json())
  projects.value = await fetch('/api/projects').then(r => r.json())
})

async function submitEntry() {
  await fetch('/api/timeentries', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(entry.value)
  })
  entry.value = { employeeId: '', projectId: '', date: '', hours: 0, notes: '' }
  emit('added')
}
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

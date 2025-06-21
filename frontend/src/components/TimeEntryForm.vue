<template>
  <form class="row g-2 mb-3" @submit.prevent="submitEntry">
    <div class="col-md-2">
      <label class="form-label">Employee
        <select class="form-select" v-model="entry.employeeId" required>
          <option :value="e.id" v-for="e in employees" :key="e.id">{{ e.name }}</option>
        </select>
      </label>
    </div>
    <div class="col-md-2">
      <label class="form-label">Project
        <select class="form-select" v-model="entry.projectId" required>
          <option :value="p.id" v-for="p in projects" :key="p.id">{{ p.name }}</option>
        </select>
      </label>
    </div>
    <div class="col-md-2">
      <label class="form-label">Date
        <input class="form-control" type="date" v-model="entry.date" required />
      </label>
    </div>
    <div class="col-md-2">
      <label class="form-label">Hours
        <input class="form-control" type="number" step="0.1" v-model="entry.hours" required />
      </label>
    </div>
    <div class="col-md-2">
      <label class="form-label">Notes
        <input class="form-control" type="text" v-model="entry.notes" />
      </label>
    </div>
    <div class="col-md-1 align-self-end">
      <button class="btn btn-primary" type="submit">Add Entry</button>
    </div>
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
</style>

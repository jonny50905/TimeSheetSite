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
      <button class="btn btn-primary" type="submit">{{ editing ? 'Save' : 'Add Entry' }}</button>
      <button v-if="editing" class="btn btn-secondary ms-2" @click="cancelEdit" type="button">Cancel</button>
    </div>
  </form>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue'

const emit = defineEmits(['added', 'saved', 'cancel'])
const props = defineProps({ model: Object })
const employees = ref([])
const projects = ref([])
const entry = ref({ employeeId: '', projectId: '', date: '', hours: 0, notes: '' })
const editing = ref(false)

onMounted(async () => {
  employees.value = await fetch('/api/employees').then(r => r.json())
  projects.value = await fetch('/api/projects').then(r => r.json())
})

watch(() => props.model, val => {
  if (val) {
    entry.value = { employeeId: val.employeeId, projectId: val.projectId, date: val.date.substring(0,10), hours: val.hours, notes: val.notes, id: val.id }
    editing.value = true
  } else {
    reset()
  }
}, { immediate: true })

async function submitEntry() {
  if (editing.value) {
    await fetch(`/api/timeentries/${entry.value.id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(entry.value)
    })
    emit('saved')
  } else {
    await fetch('/api/timeentries', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(entry.value)
    })
    emit('added')
  }
  reset()
}

function reset() {
  entry.value = { employeeId: '', projectId: '', date: '', hours: 0, notes: '' }
  editing.value = false
}

function cancelEdit() {
  reset()
  emit('cancel')
}
</script>

<style scoped>
</style>

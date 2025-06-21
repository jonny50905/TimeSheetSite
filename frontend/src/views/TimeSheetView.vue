<template>
  <div>
    <h2>Timesheet</h2>
    <TimeEntryForm :model="editingEntry" @added="loadEntries" @saved="saved" @cancel="cancel" />
    <h3>Entries</h3>
    <table v-if="entries.length" class="table table-bordered">
      <thead>
        <tr>
          <th>Date</th>
          <th>Employee</th>
          <th>Project</th>
          <th>Hours</th>
          <th>Notes</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="e in entries" :key="e.id">
          <td>{{ e.date.substring(0,10) }}</td>
          <td>{{ e.employee?.name }}</td>
          <td>{{ e.project?.name }}</td>
          <td>{{ e.hours }}</td>
          <td>{{ e.notes }}</td>
          <td><button class="btn btn-sm btn-secondary" @click="edit(e)">Edit</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import TimeEntryForm from '../components/TimeEntryForm.vue'

const entries = ref([])
const editingEntry = ref(null)

async function loadEntries() {
  entries.value = await fetch('/api/timeentries').then(r => r.json())
}

function edit(e) {
  editingEntry.value = e
}

function saved() {
  editingEntry.value = null
  loadEntries()
}

function cancel() {
  editingEntry.value = null
}

onMounted(loadEntries)
</script>

<style scoped>
</style>

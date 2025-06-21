<template>
  <h1>TimeSheet App</h1>

  <TimeEntryForm @added="loadEntries" />
  <h2>Entries</h2>
  <table v-if="entries.length">
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
      </tr>
    </tbody>
  </table>

  <h2>Project Report</h2>
  <ProjectReport />

  <CustomerManager />
</template>

<script setup>
import { ref, onMounted } from 'vue'
import TimeEntryForm from './components/TimeEntryForm.vue'
import ProjectReport from './components/ProjectReport.vue'
import CustomerManager from './components/CustomerManager.vue'

const entries = ref([])

async function loadEntries() {
  entries.value = await fetch('/api/timeentries').then(r => r.json())
}

onMounted(loadEntries)
</script>

<style scoped>
table {
  border-collapse: collapse;
  margin-bottom: 1rem;
}
th, td {
  border: 1px solid #ccc;
  padding: 4px 8px;
}

</style>

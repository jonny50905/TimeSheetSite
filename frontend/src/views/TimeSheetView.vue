<template>
  <div>
    <h2>Timesheet</h2>
    <TimeEntryForm @added="loadEntries" />
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
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import TimeEntryForm from '../components/TimeEntryForm.vue'

const entries = ref([])

async function loadEntries() {
  entries.value = await fetch('/api/timeentries').then(r => r.json())
}

onMounted(loadEntries)
</script>

<style scoped>
</style>

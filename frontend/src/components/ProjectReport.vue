<template>
  <div>
    <div class="mb-2">
      <button class="btn btn-secondary me-2" @click="load">Load Reports</button>
      <button class="btn btn-success" @click="exportCsv" :disabled="reports.length === 0">Export CSV</button>
    </div>
    <table v-if="reports.length" class="table table-bordered">
      <thead>
        <tr>
          <th>Project</th>
          <th>Hours</th>
          <th>Cost</th>
          <th>Revenue</th>
          <th>Profit</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="r in reports" :key="r.projectId">
          <td>{{ r.projectName }}</td>
          <td>{{ r.hours.toFixed(2) }}</td>
          <td>{{ r.cost.toFixed(2) }}</td>
          <td>{{ r.revenue.toFixed(2) }}</td>
          <td>{{ r.profit.toFixed(2) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const reports = ref([])

async function load() {
  reports.value = await fetch('/api/reports/projects').then(r => r.json())
}

function exportCsv() {
  const header = 'Project,Hours,Cost,Revenue,Profit\n'
  const rows = reports.value.map(r => `${r.projectName},${r.hours},${r.cost},${r.revenue},${r.profit}`).join('\n')
  const blob = new Blob([header + rows], { type: 'text/csv' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = 'project-report.csv'
  a.click()
  URL.revokeObjectURL(url)
}
</script>

<style scoped>
</style>

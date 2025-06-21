<template>
  <div>
    <h2>Projects</h2>
    <form @submit.prevent="addProject">
      <label>
        客戶
        <input v-model="project.client" required />
      </label>
      <label>
        專案名稱
        <input v-model="project.name" required />
      </label>
      <label>
        起始日
        <input type="date" v-model="project.startDate" required />
      </label>
      <label>
        結束日
        <input type="date" v-model="project.endDate" required />
      </label>
      <label>
        金額
        <input type="number" step="0.01" v-model.number="project.amount" required />
      </label>
      <button type="submit">Add</button>
    </form>
    <table v-if="projects.length">
      <thead>
        <tr>
          <th>客戶</th>
          <th>專案名稱</th>
          <th>起始日</th>
          <th>結束日</th>
          <th>金額</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in projects" :key="p.id">
          <td>{{ p.client }}</td>
          <td>{{ p.name }}</td>
          <td>{{ p.startDate ? p.startDate.substring(0,10) : '' }}</td>
          <td>{{ p.endDate ? p.endDate.substring(0,10) : '' }}</td>
          <td>{{ p.amount }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const projects = ref([])
const project = ref({ client: '', name: '', startDate: '', endDate: '', amount: 0 })

async function load() {
  projects.value = await fetch('/api/projects').then(r => r.json())
}

async function addProject() {
  await fetch('/api/projects', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(project.value)
  })
  project.value = { client: '', name: '', startDate: '', endDate: '', amount: 0 }
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

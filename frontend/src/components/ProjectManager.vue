<template>
  <div>
    <h2>Projects</h2>
    <form class="row g-2 mb-3" @submit.prevent="addProject">
      <div class="col-md-2">
        <label class="form-label">客戶
          <select class="form-select" v-model="project.customerId" required>
            <option :value="c.id" v-for="c in customers" :key="c.id">{{ c.name }}</option>
          </select>
        </label>
      </div>
      <div class="col-md-2">
        <label class="form-label">專案名稱
          <input class="form-control" v-model="project.name" required />
        </label>
      </div>
      <div class="col-md-2">
        <label class="form-label">起始日
          <input class="form-control" type="date" v-model="project.startDate" required />
        </label>
      </div>
      <div class="col-md-2">
        <label class="form-label">結束日
          <input class="form-control" type="date" v-model="project.endDate" required />
        </label>
      </div>
      <div class="col-md-2">
        <label class="form-label">金額
          <input class="form-control" type="number" step="0.01" v-model.number="project.amount" required />
        </label>
      </div>
      <div class="col-md-1 align-self-end">
        <button class="btn btn-primary" type="submit">Add</button>
      </div>
    </form>
    <table v-if="projects.length" class="table table-bordered">
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
          <td>{{ p.customer?.name }}</td>
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
const customers = ref([])
const project = ref({ customerId: '', name: '', startDate: '', endDate: '', amount: 0 })

async function load() {
  projects.value = await fetch('/api/projects').then(r => r.json())
  customers.value = await fetch('/api/customers').then(r => r.json())
}

async function addProject() {
  await fetch('/api/projects', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(project.value)
  })
  project.value = { customerId: '', name: '', startDate: '', endDate: '', amount: 0 }
  load()
}

onMounted(load)
</script>

<style scoped>
</style>

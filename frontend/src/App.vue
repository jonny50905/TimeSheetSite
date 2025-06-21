<template>
  <div class="container-fluid">
    <div class="row min-vh-100">
      <aside class="col-3 col-md-2 bg-light p-0">
        <ul class="nav nav-pills flex-column">
          <li class="nav-item" v-for="item in menu" :key="item.key">
            <a
              href="#"
              class="nav-link"
              :class="{ active: current === item.key }"
              @click.prevent="current = item.key"
            >{{ item.label }}</a>
          </li>
        </ul>
      </aside>
      <main class="col-9 col-md-10 p-3">
        <component :is="currentComponent" />
      </main>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import TimeSheetView from './views/TimeSheetView.vue'
import EmployeeManager from './components/EmployeeManager.vue'
import CustomerManager from './components/CustomerManager.vue'
import ProjectManager from './components/ProjectManager.vue'
import ProjectReport from './components/ProjectReport.vue'

const menu = [
  { key: 'employees', label: '管理員工', component: EmployeeManager },
  { key: 'customers', label: '管理客戶', component: CustomerManager },
  { key: 'projects', label: '專案管理', component: ProjectManager },
  { key: 'timesheet', label: 'Timesheet', component: TimeSheetView },
  { key: 'report', label: 'Report', component: ProjectReport }
]

const current = ref('timesheet')

const currentComponent = computed(() => {
  return menu.find(m => m.key === current.value)?.component || TimeSheetView
})
</script>

<style>
body {
  min-height: 100vh;
}
</style>

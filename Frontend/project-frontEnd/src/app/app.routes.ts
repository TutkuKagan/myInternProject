import { Routes } from '@angular/router';
import { Login } from './components/auth/login/login';
import { Register } from './components/auth/register/register';
import { Dashboard } from './components/dashboard/dashboard/dashboard';
import { TaskList } from './components/tasks/task-list/task-list';
import { TaskForm } from './components/tasks/task-form/task-form';
import { TaskDetail } from './components/tasks/task-detail/task-detail';

export const routes: Routes = [

  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },


  { path: 'auth/login', component: Login },
  { path: 'auth/register', component: Register },


  { path: 'dashboard', component: Dashboard },
  { path: 'tasks', component: TaskList },
  { path: 'tasks/new', component: TaskForm },
  { path: 'tasks/edit/:id', component: TaskForm },
  { path: 'tasks/:id', component: TaskDetail },


  { path: '**', redirectTo: 'auth/login' }
];
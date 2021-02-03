import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '../views/layout/Layout'
import layoutSetting from '../views/layout/LayoutSetting'
/**
* hidden: true                   if `hidden:true` will not show in the sidebar(default is false)
* alwaysShow: true               if set true, will always show the root menu, whatever its child routes length
*                                if not set alwaysShow, only more than one route under the children
*                                it will becomes nested mode, otherwise not show the root menu
* redirect: noredirect           if `redirect:noredirect` will no redirct in the breadcrumb
* name:'router-name'             the name is used by <keep-alive> (must set!!!)
* meta : {
    title: 'title'               the name show in submenu and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar,
  }
**/
export const constantRouterMap = [
  { path: '/login', component: () => import('@/views/login/index'), hidden: true },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        component: () => import('@/views/dashboard/index'),
        name: 'Dashboard',
        meta: { title: 'Dashboard', icon: 'firstPage', affix: true }
      }
    ]
  }
]

export const asyncRouterMap = [
  {
    path: '/Setting',
    component: Layout,
    redirect: '/Setting/Workcells',
    name: 'Setting',
    meta: { title: 'Setting', icon: 'cog-fill', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
    children: [
      {
        path: 'Workcells',
        component: () => import('@/views/Setting/Workcells/Workcells'),
        name: 'Workcells',
        meta: { title: 'Workcells', icon: 'workcell', role: ['admin', 'editor', 'approver', 'user'] }
      },
      {
        path: 'Lines',
        component: () => import('@/views/Setting/Lines/Lines'),
        name: 'Lines',
        meta: { title: 'Bays', icon: 'line', role: ['admin', 'editor', 'approver', 'user'] }
      },
      {
        path: 'Equipments',
        component: () => import('@/views/Setting/Equipment/Equipments'),
        name: 'Equipments',
        meta: { title: 'Equipments', icon: 'equipment', role: ['admin', 'editor', 'approver', 'user'] }
      },
      {
        path: 'Smed',
        component: layoutSetting,
        redirect: '/Setting/Smed/Stations',
        name: 'Smed',
        meta: { title: 'Smed', icon: 'moonshine', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'Stations',
            component: () => import('@/views/Setting/Smed/Stations/Stations'),
            name: 'Stations',
            meta: { title: 'Stations', icon: 'stations', role: ['admin', 'editor', 'approver', 'user'] }
          },
          {
            path: 'Models',
            component: () => import('@/views/Setting/Smed/Models/Models'),
            name: 'Models',
            meta: { title: 'Models', icon: 'model', role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      },
      {
        path: 'Space',
        component: layoutSetting,
        redirect: '/Setting/Space/Functions',
        name: 'Space',
        meta: { title: 'Space', icon: 'space', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'Physics',
            component: () => import('@/views/Setting/Space/Physics/Physics'),
            name: 'Physics',
            meta: { title: 'Physic Areas Type', icon: 'model', role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      }
    ]
  },
  {
    path: '/Functions',
    component: Layout,
    redirect: '/Functions/Downtime',
    name: 'Functions',
    meta: { title: 'Functions', icon: 'functions', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
    children: [
      {
        path: 'Downtime',
        component: layoutSetting,
        redirect: '/Functions/Downtime/downtime-search',
        name: 'Downtime',
        meta: { title: 'E-Downtime', icon: 'example', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'ExcelUpload',
            name: 'ExcelUpload',
            component: () => import('@/views/Downtime/ExcelUpload'),
            meta: { title: 'Upload', icon: 'form', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'create',
            component: () => import('@/views/table/create'),
            name: 'CreateArticle',
            meta: { title: 'Create Downtime', icon: 'form', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'edit/:id(\\d+)',
            component: () => import('@/views/table/edit'),
            name: 'EditArticle',
            meta: { title: 'Edit Downtime', noCache: true, activeMenu: '/Functions/table/complex-table', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'downtime-search',
            name: 'downtimeUpload',
            component: () => import('@/views/table/complex-table'),
            meta: { title: 'E-Downtime', icon: 'downtime', role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      },
      {
        path: 'eDfa',
        component: layoutSetting,
        redirect: '/Functions/eDfa/Dfa',
        name: 'eDfa',
        meta: { title: 'E-Dfa', icon: 'table', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'edit/:id(\\d+)',
            component: () => import('@/views/eDfa/edit'),
            name: 'edit',
            meta: { title: 'Edit DFA', noCache: true, activeMenu: '/Functions/eDfa/edit', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'create',
            component: () => import('@/views/eDfa/create'),
            name: 'create',
            meta: { title: 'Create DFA', icon: 'form', activeMenu: '/Functions/eDfa/create', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'Dfa',
            component: () => import('@/views/eDfa/DfaLists'),
            name: 'DfaLists',
            meta: { title: 'E-DFA', icon: 'dfa', affix: true, role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      },
      {
        path: 'eRFQ',
        component: layoutSetting,
        redirect: '/Functions/eRFQ/lists',
        name: 'eRFQ',
        meta: { title: 'E-RFQ', icon: 'table', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'edit/:id(\\d+)',
            component: () => import('@/views/eRFQ/edit'),
            name: 'edit',
            meta: { title: 'Edit RFQ', activeMenu: '/Functions/eRFQ/edit', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'create',
            component: () => import('@/views/eRFQ/create'),
            name: 'create',
            meta: { title: 'Create RFQ', icon: 'form', activeMenu: '/Functions/eRFQ/create', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'lists',
            component: () => import('@/views/eRFQ/RfqLists'),
            name: 'eRFQ',
            meta: { title: 'E-RFQ', icon: 'example', affix: true, role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      },
      {
        path: 'Smed',
        component: layoutSetting,
        redirect: '/Smed/Smed',
        name: 'Smed',
        meta: { title: 'E-Smed', icon: 'smedb', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'Smed',
            component: () => import('@/views/SMED/Smed'),
            name: 'Smed',
            meta: { title: 'E-Smed Command', icon: 'smedb', role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      },
      {
        path: 'E-Moonshine',
        component: layoutSetting,
        redirect: '/Functions/moonshine',
        meta: { role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'moonshine',
            component: () => import('@/views/eMachine/Moonshine'),
            name: 'E-Moonshine',
            meta: { title: 'E-Moonshine', icon: 'moonshine1', affix: true, role: ['admin', 'editor', 'approver', 'user'] }
          }
        ]
      },
      {
        path: 'Space',
        component: layoutSetting,
        redirect: '/Functions/Space/Allocation',
        name: 'Space',
        meta: { title: 'E-Space', icon: 'spaceb', affix: true, role: ['admin', 'editor', 'approver', 'user'] },
        children: [
          {
            path: 'Revenu',
            component: () => import('@/views/Space/Revenu/Revenu'),
            name: 'Models',
            meta: { title: 'Revenu Plan', icon: 'model', role: ['admin', 'editor', 'approver', 'user'] }
          },
          {
            path: 'Space',
            component: () => import('@/views/Space/Space/Space'),
            name: 'Models',
            meta: { title: 'Space Plan', icon: 'model', role: ['admin', 'editor', 'approver', 'user'] }
          },
          {
            path: 'Allocation',
            component: () => import('@/views/Space/Allocation/Allocation'),
            name: 'Allocation',
            meta: { title: 'Space Activity', icon: 'spacea', role: ['admin', 'editor', 'approver', 'user'] }
          },
          {
            path: 'SpaceUpload',
            name: 'SpaceUpload',
            component: () => import('@/views/Space/Space/SpaceUpload'),
            meta: { title: 'Upload', icon: 'form', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'Summary',
            name: 'Summary',
            component: () => import('@/views/Space/Space/Summary'),
            meta: { title: 'Summary', icon: 'form', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'Graph',
            name: 'Graph',
            component: () => import('@/views/Space/Space/Graph'),
            meta: { title: 'Space Graphs', icon: 'form', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          },
          {
            path: 'RevenuUpload',
            name: 'RevenuUpload',
            component: () => import('@/views/Space/Revenu/RevenuUpload'),
            meta: { title: 'RevenuUpload', icon: 'form', role: ['admin', 'editor', 'approver', 'user'] },
            hidden: true
          }
        ]
      }
    ]
  },
  {
    path: '/help',
    component: Layout,
    redirect: '/help/help',
    meta: { role: ['admin', 'editor', 'approver', 'user'] },
    children: [
      {
        path: 'help',
        component: () => import('@/views/help/help'),
        name: 'help',
        meta: { title: 'Help', icon: 'question-circle', affix: true, role: ['admin', 'editor', 'approver', 'user'] }
      }
    ]
  },
  { path: '*', redirect: '/404', hidden: true, meta: { role: ['admin', 'editor', 'approver', 'user'] }}
]

export default new Router({
  routes: constantRouterMap
})


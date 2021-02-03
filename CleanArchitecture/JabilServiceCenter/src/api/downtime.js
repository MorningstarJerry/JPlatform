import request from '@/utils/request'

export function upload(data) {
  return request({
    url: '/downtime/upload/data',
    method: 'post',
    data: { data }
  })
}

export function getSiteOptions() {
  return request({
    url: '/downtime/sites',
    method: 'get'
  })
}

export function getWorkcellOptions() {
  return request({
    url: '/downtime/workcells',
    method: 'get'
  })
}

export function getsectorsOptions() {
  return request({
    url: '/downtime/sectors',
    method: 'get'
  })
}

export function getProcessesOptions() {
  return request({
    url: '/downtime/processes',
    method: 'get'
  })
}

export function getDowntimeTypesOptions() {
  return request({
    url: '/downtime/types',
    method: 'get'
  })
}

export function getDepartmentsOptions() {
  return request({
    url: '/downtime/departments',
    method: 'get'
  })
}

export function getLineOptions() {
  return request({
    url: '/line/lists',
    method: 'get'
  })
}

export function getEquipmentOptions() {
  return request({
    url: '/downtime/equipments',
    method: 'get'
  })
}

export function getEquipmentOptionsByQuery(query) {
  return request({
    url: '/downtime/equipments',
    method: 'get',
    params: { query }
  })
}

export function getEquipmentTypeOptionsByQuery(query) {
  return request({
    url: '/downtime/equipmentsType',
    method: 'get',
    params: { query }
  })
}

export function getLineOptionsByQuery(query) {
  return request({
    url: '/downtime/lines',
    method: 'get',
    params: { query }
  })
}

export function submitDowntime(data) {
  return request({
    url: '/Downtimes',
    method: 'post',
    data
  })
}

export function UpdateDowntime(data) {
  return request({
    url: '/Downtimes/update',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/Downtimes/delete',
    method: 'post',
    data
  })
}

export function GetDowntimeStatistics() {
  return request({
    url: '/downtimes/statistics',
    method: 'get'
  })
}

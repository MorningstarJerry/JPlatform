import request from '@/utils/request'

export function getAllLists() {
  return request({
    url: '/dfa/lists',
    method: 'get'
  })
}

export function getById(id) {
  return request({
    url: '/dfa/entities',
    method: 'get',
    params: { id }
  })
}

export function getListsByQuery(query) {
  return request({
    url: '/dfa/query',
    method: 'get',
    params: query
  })
}

export function Create(data) {
  return request({
    url: '/dfa/create',
    method: 'post',
    data
  })
}

export function Update(data) {
  return request({
    url: '/dfa/update',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/dfa/delete',
    method: 'post',
    data
  })
}

export function getAllProductTypes() {
  return request({
    url: '/product/type/options',
    method: 'get'
  })
}

export function getProductPhaseOptions() {
  return request({
    url: '/product/phase/options',
    method: 'get'
  })
}

export function getDataSourceOptions() {
  return request({
    url: '/datasource/options',
    method: 'get'
  })
}

export function getMaterialTypeOptions() {
  return request({
    url: '/materialtype/options',
    method: 'get'
  })
}

export function getMaterialTypeOptionsByQuery(query) {
  return request({
    url: '/materialtype/query',
    method: 'get',
    params: { query }
  })
}

export function getSeverityOptions() {
  return request({
    url: '/severity/options',
    method: 'get'
  })
}

export function getCategoryOptions() {
  return request({
    url: '/category/options',
    method: 'get'
  })
}

export function getBusinessOwnerOptions() {
  return request({
    url: '/businessowner/options',
    method: 'get'
  })
}

export function getDFAStatusOptions() {
  return request({
    url: '/dfastatus/options',
    method: 'get'
  })
}

export function getDfaStatistics() {
  return request({
    url: '/dfa/statistics',
    method: 'get'
  })
}

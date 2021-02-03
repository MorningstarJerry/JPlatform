import request from '@/utils/request'

export function getSpace() {
  return request({
    url: '/space/tree',
    method: 'get'
  })
}

export function getListsByQuery(query) {
  return request({
    url: '/space/types',
    method: 'get',
    params: query
  })
}

export function mapSpace(data) {
  return request({
    url: '/space/map',
    method: 'post',
    data
  })
}

export function getSpaceSelTree() {
  return request({
    url: '/space/seltree',
    method: 'get'
  })
}

export function getSpaceTypeOptions() {
  return request({
    url: '/space/map',
    method: 'get'
  })
}

export function getActivityListsByQuery(query) {
  return request({
    url: '/space/activitys',
    method: 'get',
    params: query
  })
}

export function Create(data) {
  return request({
    url: '/space/activitys/create',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/space/activitys/update',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/space/activitys/delete',
    method: 'post',
    data
  })
}

export function getSpaceOptions() {
  return request({
    url: '/space/options',
    method: 'get'
  })
}

export function getTypeOptions() {
  return request({
    url: '/space/typeoptions',
    method: 'get'
  })
}

export function UploadForecast(data) {
  return request({
    url: '/space/upload',
    method: 'post',
    data
  })
}

export function getForecastLists(query) {
  return request({
    url: '/space/forecast',
    method: 'get',
    params: query
  })
}

export function getSummaryForecastListsByWcs(query) {
  return request({
    url: '/space/summary',
    method: 'get',
    params: query
  })
}

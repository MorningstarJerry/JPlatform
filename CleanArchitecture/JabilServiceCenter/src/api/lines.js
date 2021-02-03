import request from '@/utils/request'

export function getLineOptionsByQuery(query) {
  return request({
    url: '/downtime/lines',
    method: 'get',
    params: { query }
  })
}

export function getLineOptions() {
  return request({
    url: '/line/lists',
    method: 'get'
  })
}

export function getLineByQuery(query) {
  return request({
    url: '/line/query',
    method: 'get',
    params: query
  })
}

export function CreateLine(data) {
  return request({
    url: '/line/create',
    method: 'post',
    data
  })
}

export function UpdateLine(data) {
  return request({
    url: '/line/update',
    method: 'post',
    data
  })
}

export function DeleteLine(data) {
  return request({
    url: '/line/delete',
    method: 'post',
    data
  })
}

export function getLineTrees(data) {
  return request({
    url: '/line/wcs',
    method: 'get',
    data
  })
}

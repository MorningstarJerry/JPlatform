import request from '@/utils/request'

export function Create(data) {
  return request({
    url: '/rfq/create',
    method: 'post',
    data
  })
}

export function getById(id) {
  return request({
    url: '/rfq/entities',
    method: 'get',
    params: { id }
  })
}

export function getListsByQuery(query) {
  return request({
    url: '/rfq/query',
    method: 'get',
    params: query
  })
}

export function Update(data) {
  return request({
    url: '/rfq/update',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/rfq/delete',
    method: 'post',
    data
  })
}

export function GetRfqStatistics() {
  return request({
    url: '/rfq/statistics',
    method: 'get'
  })
}

export function DownloadRfq() {
  return request({
    url: '/rfq/template/download',
    method: 'get'
  })
}

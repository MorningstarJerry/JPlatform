import request from '@/utils/request'

export function getListsByQuery(query) {
  return request({
    url: '/station/query',
    method: 'get',
    params: query
  })
}

export function Create(data) {
  return request({
    url: '/station/create',
    method: 'post',
    data
  })
}

export function Update(data) {
  return request({
    url: '/station/edit',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/station/delete',
    method: 'post',
    data
  })
}

export function SearchByCode(query) {
  return request({
    url: '/station/code',
    method: 'get',
    params: query
  })
}


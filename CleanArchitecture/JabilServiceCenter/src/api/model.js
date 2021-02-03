import request from '@/utils/request'

export function getListsByQuery(query) {
  return request({
    url: '/model/query',
    method: 'get',
    params: query
  })
}

export function Create(data) {
  return request({
    url: '/model/create',
    method: 'post',
    data
  })
}

export function Update(data) {
  return request({
    url: '/model/edit',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/model/delete',
    method: 'post',
    data
  })
}

export function SearchByCode(query) {
  return request({
    url: '/model/code',
    method: 'get',
    params: query
  })
}


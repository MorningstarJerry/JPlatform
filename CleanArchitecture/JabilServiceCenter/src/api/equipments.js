import request from '@/utils/request'

export function getAllLists() {
  return request({
    url: '/equipment/lists',
    method: 'get'
  })
}

export function getListsByQuery(query) {
  return request({
    url: '/equipment/query',
    method: 'get',
    params: query
  })
}

export function Create(data) {
  return request({
    url: '/equipment/create',
    method: 'post',
    data
  })
}

export function Update(data) {
  return request({
    url: '/equipment/update',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/equipment/delete',
    method: 'post',
    data
  })
}

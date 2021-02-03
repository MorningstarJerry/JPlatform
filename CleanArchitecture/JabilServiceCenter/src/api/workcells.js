import request from '@/utils/request'

export function getAllLists() {
  return request({
    url: '/workcell/lists',
    method: 'get'
  })
}

export function getListsByQuery(query) {
  return request({
    url: '/workcell/query',
    method: 'get',
    params: query
  })
}

export function getWCOptionsByQuery(query) {
  return request({
    url: '/workcell/conditions/query',
    method: 'get',
    params: { query }
  })
}

export function Create(data) {
  return request({
    url: '/workcell/create',
    method: 'post',
    data
  })
}

export function Update(data) {
  return request({
    url: '/workcell/update',
    method: 'post',
    data
  })
}

export function Delete(data) {
  return request({
    url: '/workcell/delete',
    method: 'post',
    data
  })
}

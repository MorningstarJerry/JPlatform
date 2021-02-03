import request from '@/utils/request'

export function getUsersByQuery(query) {
  return request({
    url: '/user/lists',
    method: 'get',
    params: { query }
  })
}

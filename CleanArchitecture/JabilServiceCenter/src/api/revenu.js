import request from '@/utils/request'

export function UploadForecast(data) {
  return request({
    url: '/space/revenu/upload',
    method: 'post',
    data
  })
}

export function getRevenuSummary(query) {
  return request({
    url: '/space/revenus/summary',
    method: 'get',
    params: query
  })
}

export function getForecastLists(query) {
  return request({
    url: '/space/revenus',
    method: 'get',
    params: query
  })
}

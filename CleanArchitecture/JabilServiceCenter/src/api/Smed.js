import request from '@/utils/request'

export function getListsByQuery(query) {
  return request({
    url: '/smed/lists',
    method: 'get',
    params: query
  })
}

export function getLatestFeedback(query) {
  return request({
    url: '/smed/feedback/latest',
    method: 'get',
    params: query
  })
}

export function getComeInFeedback(query) {
  return request({
    url: '/smed/feedback/come',
    method: 'get',
    params: query
  })
}

export function Create(data) {
  return request({
    url: '/smed/create',
    method: 'post',
    data
  })
}

export function Update(data) {
  return request({
    url: '/smed/feedback/update',
    method: 'post',
    data
  })
}

export function getFeedbackByGUID(messageId) {
  return request({
    url: '/smed/feedbacks',
    method: 'get',
    params: { messageId }
  })
}

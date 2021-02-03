'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  //STG
  //BASE_API: '"http://10.114.18.244/eDowntime/api"',

  //DEV
  // BASE_API: '"http://localhost:59789/api"',
  // BASE_API: '"http://localhost:8082/api"',
  BASE_API: '"https://localhost:5001/api"',
  
  //PROD
  // BASE_API: '"http://10.114.18.250/eDowntime/api"',
})

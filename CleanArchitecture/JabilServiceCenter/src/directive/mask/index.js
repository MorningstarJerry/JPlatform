import VueMaskDirective from 'v-mask'

const install = function(Vue) {
  Vue.directive('mask', VueMaskDirective)
}

if (window.Vue) {
  window.VueMaskDirective = VueMaskDirective
  Vue.use(install); // eslint-disable-line
}

VueMaskDirective.install = install
export default VueMaskDirective

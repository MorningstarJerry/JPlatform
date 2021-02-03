const getters = {
  sidebar: state => state.app.sidebar,
  device: state => state.app.device,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  name: state => state.user.name,
  roles: state => state.user.roles,
  sectorId: state => state.user.sectorId,
  siteId: state => state.user.siteId,
  workcellId: state => state.user.workcellId,
  DepartmentId: state => state.user.DepartmentId,
  permission_routes: state => state.permission.routers,
  addRouters: state => state.permission.addRouters
}
export default getters

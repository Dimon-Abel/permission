# 权限管理

### 权限流程

#### 前端：

前端登录用户，获取用户身份标识集合

根据用户身份标识集合请求接口获取对应身份菜单集合(包含菜单对应的权限)-前端可缓存数据

根据用户身份标识集合请求接口获取对应身份权限集合-前端可缓存数据

全局添加钩子事件(原有定义按钮界面，需要改造)，页面加载时 根据获取的权限集合 过滤当前界面 需要显示的按钮

#### 后端：

维护基础菜单权限数据文件 admin-privilege.json 新增配置文件命名格式 (****-privilege.json)

其他api站点 action 需使用 MethodInfoAttribute 定义 Action，从而维护 Privilge 基础数据


#### 部署

SeedDataInitialize admin-privilege.json 维护基础数据信息

部署新站点时 运行 SeedDataInitialize 填充基础数据资料
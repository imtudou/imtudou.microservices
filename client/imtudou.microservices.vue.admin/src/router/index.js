
// 导入组件
import Router from 'vue-router';

// 导入自定义组件
import Register from '../views/login/Register.vue';
import Login from '../views/login/Login.vue';
import ForgetPwd from '../views/login/ForgetPwd.vue';
import Home from '../views/Home.vue';
import ProductList from '../views/product/ProductList';
import ProductInfo from '../views/product/ProductInfo';

// 创建路由对象并将其暴露出去
export default new Router({
    routes:[
        {path:'/',redirect:'/Home'},// 默认首页
        {name:"Home",path:'/Home',component:Home},
        {name:"Register",path:'/Register',component:Register},
        {name:"Login",path:'/Login',component:Login},
        {name:"ForgetPwd",path:'/ForgetPwd',component:ForgetPwd},
        {name:'ProductList',path:'/ProductList',component:ProductList},
        {name:"ProductInfo",path:'/ProductInfo',component:ProductInfo},
    ]
});


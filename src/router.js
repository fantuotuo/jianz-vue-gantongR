import Vue from "vue";
import VueRouter from "vue-router";
Vue.use(VueRouter);
import store from './store.js';

import Desktop from "./newDesktop/Index.vue";
import Login from "./newLogin/Index.vue";
import Fangan from "./newFangan/Index.vue";
// import Logout from "./newLogout/Index.vue";

const routes=[
	{
		path:"/desktop",
		name:"Desktop",
		component:Desktop,
		meta:{title:"感统训练中心-首页"}
	},{
		path:"/login",
		name:"Login",
		component:Login,
		meta:{title:"感统训练中心-量表"}
	},{
		path:"/fangan",
		name:"Fangan",
		component:Fangan,
		meta:{title:"感统训练中心-训练"}
	},
	// {
	// 	path:"/logout",
	// 	name:"Logout",
	// 	component:Logout,
	// 	meta:{title:"感统训练中心-退出"}
	// },
	{
		path:"*",
		redirect:"/desktop"
	}
];
const router=new VueRouter({
	routes
});
router.beforeEach((to,from,next)=>{
	if(to.meta.title){
		document.title=to.meta.title;
	}
	if(to.name==="Fangan"){
		if(isNaN(parseInt(to.query.u_i))){
			alert("请选择一个量表登陆！");
			next(from.fullPath);
		}else{
			next();
		}
	}else if(to.name==="Login"){
		if(store.state.desktop_liangbiao_times===-1){
			// 说明是直接打开页面，需要先回首页，进行一些数据初始化获取
			next({path:"/"});
		}else if(store.state.desktop_liangbiao_times - store.state.desktop_users.length <= 0){
			alert("你的量表创建次数已经用完！");
			next(from.fullPath);
		}else{
			next();
		}
	}else{
		next()
	}
})

export default router;
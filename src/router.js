import Vue from "vue";
import VueRouter from "vue-router";
Vue.use(VueRouter);
import store from './store.js';

import Desktop from "./newDesktop/Index.vue";
import Login from "./newLogin/Index.vue";
import Fangan from "./newFangan/Index.vue";
import Error from "./newError/Index.vue";
// import Logout from "./newLogout/Index.vue";

const routes = [
	{
		path: "/desktop",
		name: "Desktop",
		component: Desktop,
		meta: { title: "感统训练中心-首页" }
	}, {
		path: "/login",
		name: "Login",
		component: Login,
		meta: { title: "感统训练中心-量表" }
	}, {
		path: "/fangan",
		name: "Fangan",
		component: Fangan,
		meta: { title: "感统训练中心-训练" }
	}, {
		path: "/error",
		name: "Error",
		component: Error,
		meta: { title: "感统训练中心-错误页" }
	}, {
		path: "*",
		redirect: "/desktop"
	}
];
const router=new VueRouter({
	routes
});
router.beforeEach((to,from,next)=>{
	if(to.name==="Error"){
		if(store.state.user_valid.valid){
			next({path:"/desktop"});
		}else if(store.state.user_valid.fetched){
			changeMeta(to.meta);
			next();
		}else{
			fetchValidUser(true);
		}
	}else{
		if(store.state.user_valid.valid){
			validUser();
		}else if(store.state.user_valid.fetched){
			// 已经获取，不需要重复获取了
			next({path:"/error"});
		}else{
			fetchValidUser(false);
		}
	}

	function fetchValidUser(toError){
		Vue.prototype.$http
			.get("./api/vueUserValid.aspx")
			.then(ret=>{
				if(ret.valid===1){
					store.commit("user_valid_set",{fetched:true,valid:true});
					if(toError){
						next({path:"/desktop"});
					}else{
						validUser();
					}
				}else{
					store.commit("user_valid_set",{fetched:true,valid:false});
					next({path:"/error"});
				}
			})
			.catch(()=>{
				store.commit("user_valid_set",{fetched:true,valid:false});
				next({path:"/error"});
			})
	}
	function validUser(){
		if(to.name==="Fangan"){
			validToFangan(to,from,next);
		}else if(to.name==="Login"){
			validToLogin(to,from,next);
		}else{
			// desktop
			validToDesktop(to,from,next);
		}
	}
});
function validToDesktop(to,from,next){
	changeMeta(to.meta);
	next();
}
function validToLogin(to, from, next) {
	var desktop_obj = store.state.desktop_obj;console.log(desktop_obj)
	if(desktop_obj.liangbiao_times===-1){
		// 说明是直接打开页面，需要先回首页，进行一些数据初始化获取
		next({path:"/"});
	} else if (desktop_obj.liangbiao_times - desktop_obj.users.length <= 0) {
		alert("你的量表创建次数已经用完！");
		next(from.fullPath);
	}else{
		changeMeta(to.meta);
		next();
	}
}
function validToFangan(to,from,next){
	if(isNaN(parseInt(to.query.u_i))){
		alert("请选择一个量表登陆！");
		next(from.fullPath);
	}else{
		changeMeta(to.meta);
		next();
	}
}
function changeMeta(meta){
	if(meta && meta.title){
		document.title=meta.title
	}
}

export default router;
import Vue from "vue";
import VueRouter from "vue-router";
Vue.use(VueRouter);

import Desktop from "./newDesktop/Index.vue";
import Login from "./newLogin/Index.vue";
import Fangan from "./newFangan/Index.vue";
import Logout from "./newLogout/Index.vue";

const routes=[
	{path:"/desktop",component:Desktop,meta:{title:"首页"}},
	{path:"/login",component:Login,meta:{title:"填写量表"}},
	{path:"/fangan",component:Fangan,meta:{title:"训练方案"}},
	{path:"/logout",component:Logout,meta:{title:"退出系统"}},
	{path:"*",redirect:"/desktop"},

];
const router=new VueRouter({
	routes
});
router.beforeEach((to,from,next)=>{
	if(to.meta.title){
		document.title=to.meta.title;
	}
	next();
})

export default router;
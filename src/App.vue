<template>
	<div id="app" v-loading.fullscreen='loading'>
		<el-menu 
			:defaultActive="this.$route.path" 
			router 
			class='el-menu-demo' 
			mode='horizontal'
			background-color='#32b16c'
			text-color='#fff'
			active-text-color='#f3f710'
		>
			<div class="el-menu--horizontal layout">
				<el-menu-item index='/desktop'>
					切换量表
				</el-menu-item>
				<el-menu-item index='/login'>
					新增量表
				</el-menu-item>
				<el-menu-item index='/fangan'>
					训练方案
				</el-menu-item>
				<el-menu-item v-if='desktop_zhanghao===""' class='gt-submenu-right' index='zhanghao0' v-on:click='toLogin' >
					未登录
				</el-menu-item>
				<el-submenu v-else class='gt-submenu-right' index='zhanghao1'>
					<template slot='title'>
						<i class='el-icon-user-solid'></i>
						{{desktop_zhanghao}}
					</template>
					<el-menu-item v-on:click='toLogout'>退出此账号</el-menu-item>
				</el-submenu>
			</div>
		</el-menu>
	
		<router-view></router-view>
	</div>
</template>

<script>
import "bootstrap/dist/css/bootstrap.min.css";
import "./common.scss";
import {mapState} from "vuex";
export default {
	name: 'app',
	computed:{
		...mapState({
			loading:state=>state.loading,
			desktop_zhanghao:state=>state.desktop_zhanghao
		})
	},
	// 创建完成后获取用户初始数据
	created:function(){
		console.log("app created");

		this.$http
			.get("./api/vueGetUserList.aspx")
			.then(ret=>{
				this.$store.commit("desktop_user_load",{
					user:ret.user,
					users:ret.users,
					liangbiao_times:ret.times
				});
			})
			.catch(()=>{});
	},
	methods:{
		toLogin:function(){
			location.href="/login.aspx?ReturnUrl=%2fgt%2f";
		},
		toLogout:function(){
			this.$http
				.get("./api/vueLogout.aspx")
				.then(ret=>{
					if(ret.code===1){
						// 重新登陆
						location.href="/login.aspx?ReturnUrl=%2fgt%2f";
					}else{
						alert("退出登陆时发生错误："+ret.msg);
					}
				})
				.catch(()=>{});
		},
	},
	components: {

	},
}
</script>

<style module lang='scss'>




</style>
<style>
.el-menu--horizontal{
	outline:none!important;
}
.el-menu{
	margin-bottom:15px;
}
.el-submenu .el-icon-user-solid{
	/*vertical-align: baseline;*/
}
.el-submenu__title i{
	color:#fff;
}
.el-menu--horizontal .gt-submenu-right{
	float:right;
}
.el-loading-spinner .path{
	stroke: #32b16c;
}
/*#app {
	font-family: 'Avenir', Helvetica, Arial, sans-serif;
	-webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	text-align: center;
	color: #2c3e50;
	margin-top: 60px;
}*/
html,body{
	height:100%;
}
#app{
	min-height:100%;
	padding-bottom:80px;
}
.kt-app{
	width:1024px;
	margin:0 auto;
}
.kt-footer{
	text-align:center;
	padding: 20px 0;
	font-size:0.875rem;
    line-height: 20px;
    height:80px;
    margin-top:-80px;
}
</style>

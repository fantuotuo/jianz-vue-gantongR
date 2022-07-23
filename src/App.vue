<template>
	<div id="app" 
		v-loading.fullscreen='loading'
		element-loading-spinner='el-icon-loading'
		element-loading-text='加载中...'
		element-loading-background='rgba(0,0,0,0.8)'
	>
		<div class='kt-header print-hide'>
			<div class='layout'>
				<img src='./assets/logo.png' alt='健知教育' />
				健知儿童感觉统合能力测评训练系统
			</div>
		</div>
		<el-menu 
			:defaultActive="'-1'" 
			router 
			class='el-menu-demo print-hide' 
			mode='horizontal'
		>
			<div class="el-menu--horizontal layout">
				<el-menu-item :class='{"is-active2":"/login"===defaultActive}' index='/login' :disabled='!user_valid'>
					新增学员
				</el-menu-item>
				<el-menu-item :class='{"is-active2":"/desktop"===defaultActive}' index='/desktop' :disabled='!user_valid'>
					学员管理
				</el-menu-item>
				<!-- <el-menu-item :class='{"is-active2":"/fangan"===defaultActive}' index='/desktop?openFangan=1' :disabled='!user_valid'>
					训练方案
				</el-menu-item> -->
				<el-menu-item :class='{"is-active2":"/gailan"===defaultActive}' index='/gailan' :disabled='!user_valid'>
					训练概览
				</el-menu-item>

				<el-menu-item v-if='user_name===""' class='gt-submenu-right' index='/zhanghao0' v-on:click='toLogin' >
					未登录
				</el-menu-item>
				<el-submenu 
					v-else 
					class='gt-submenu-right' 
					index='/zhanghao1'
				>
					<template slot='title'>
						<i class='el-icon-user-solid'></i>
						{{user_name}}
					</template>
					<el-menu-item 
						v-on:click='toLogout'
						index='/zhanghao1_1'
					>退出此账号</el-menu-item>
				</el-submenu>
			</div>
		</el-menu>

		<router-view></router-view>
	</div>
</template>

<script>
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import "./common.scss";
import {mapState} from "vuex";

export default {
	name: 'app',
	computed:{
		...mapState({
			user_valid:state=>state.user_valid.valid,
			loading:state=>state.loading,
			user_name:state=>state.desktop_obj.user
		}),
		defaultActive(){
			if(this.$route.path==="/desktop" && this.$route.query.openFangan==="1"){
				return "/fangan";
			}
			return this.$route.path;
		}
	},
	// 创建完成后获取用户初始数据
	created:function(){
		console.log("app created");

		this.$http
			.get("./api/vueGetUserList.aspx")
			.then(ret=>{
				this.$store.commit("desktop_obj_set",{
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
<style lang='scss'>
:root{
	// 不能直接使用上面的变量
	--el-color-primary:#32b16c;
	--el-color-primary-darken:#248f54;
	--el-color-primary-transparent:#32b16c40;
}

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
	// color:#fff;
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
	padding-bottom:100px;
}
.kt-app{
	width:1024px;
	margin:0 auto;
}
.kt-header{
	height:90px;
	background:#32b16c;
	.layout{
		position:relative;
		color: #fff;
		line-height: 90px;
		text-align: center;
		font-size: 2.85rem;
		letter-spacing: .16em;

		img{
			position:absolute;
			top:(90px - 57px)/2;
			left:20px;
		}
	}
}
.kt-footer{
	text-align:center;
	padding: 20px 0;
	font-size:0.875rem;
    line-height: 20px;
    height:100px;
    margin-top:-100px;
	visibility: visible!important;
}


body .el-menu--horizontal > .el-menu-item{
	&.is-active{
		border-bottom: 2px solid transparent;
		color: #909399;
	}
	&.is-active2{
		border-bottom: 2px solid #32b16c!important;
		color: #303133;
	}
}
</style>

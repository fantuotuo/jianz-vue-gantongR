<template>
	<div id="app" v-loading.fullscreen='loading'>
		<el-menu :defaultActive="this.$route.path" router class='el-menu-demo' mode='horizontal'>
			<div class="el-menu--horizontal layout">
				<el-menu-item v-for='(item,key) in navList' :key='key' :index='item.name' :disabled='item.disabled'>
					{{item.label}}
				</el-menu-item>
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
			loading:state=>state.loading
		})
	},
	data:function(){
		return {
			navList:[
				{name:"/desktop",label:"切换用户"},
				{name:"/login",label:"填写量表"},
				{name:"/fangan",label:"训练方案",disabled:false},
				{name:"/logout",label:"退出"},
			]
		}
	},
	components: {

	},
}
</script>

<style module lang='scss'>




</style>
<style>
.el-menu-demo{
	margin-bottom:15px;
}
/*#app {
	font-family: 'Avenir', Helvetica, Arial, sans-serif;
	-webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	text-align: center;
	color: #2c3e50;
	margin-top: 60px;
}*/
</style>

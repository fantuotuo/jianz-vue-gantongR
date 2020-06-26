<template>
	<div class='wrapper gt-desktop'>
		<div class='layout'>
			<el-alert
				type='info'
				effect='dark'
				v-bind:closable='false'
				center
			>
				<template slot='title'>
					请选择需要切换的量表方案，或者新增一个量表。
				</template>
			</el-alert>

			<!-- <div class='alert alert-secondary gt-header'>
				<div class='text-center'>
					用户账号：<span class='bg-primary text-white p-1'>{{zhanghao}}</span>
				</div>
				<div class='text-center'>
					请选择登陆感统系统的用户
				</div>
			</div> -->
			<!-- <p 
				v-if='users.length<=0' 
				class='alert alert-secondary'
			>
				此账号还没有已经生成的训练方案，请先点击导航栏<router-link to='../login'>填写量表</router-link>
			</p> -->

			<TheUsersBlockNew 
				v-for='(item) in users' 
				v-bind:key='item.i' 
				v-bind:u-i='item.u_i' 
				v-bind:name='item.name' 
				v-bind:date='item.date' 
				v-on:click='toFangan'
			/>
			<div class='gt-userblock' v-bind:title='getTitlePlus'>
				<el-button type='' v-bind:disabled='getLiangbiaoTimesRemain<=0' v-on:click='toLogin'>
					<div class='gt-userblock__plus'>
						<i class='el-icon-plus'></i>
					</div>
				</el-button>
			</div>
		</div>
	</div>

</template>

<script>
import {mapState} from "vuex";
import TheUsersBlockNew from "./DesktopUsersBlockNew.vue";

export default{
	name:"Desktop",
	computed:{
		...mapState({
			zhanghao:state=>state.desktop_zhanghao,
			users:state=>state.desktop_users,
			liangbiao_times:state=>state.desktop_liangbiao_times
		}),
		getLiangbiaoTimesRemain:function(){
			return this.liangbiao_times-this.users.length;
		},
		getTitlePlus:function(){
			if(this.getLiangbiaoTimesRemain>0){
				return "新增一个量表";
			}else{
				return "你的量表创建次数已经用完，如需继续创建，请联系管理员。"
			}
		}
	},
	created:function(){
		console.log("desktop created");

		this.$http
			.get("./api/vueGetUserList.aspx",{})
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
		toFangan:function(u_i){
			this.$router.push({path:"/fangan",query:{u_i}});
		},
		toLogin:function(){
			this.$router.push({path:"/login"});
		}
	},
	components:{
		TheUsersBlockNew,
	}

}

</script>

<style lang='scss'>
.gt-header{
	line-height:1.8rem;
	margin-bottom:15px;
}
.gt-desktop .el-alert{
	margin-bottom:15px;
}
.gt-userblock{
	.gt-userblock__plus{
		font-size:1.5rem;
	}
}

</style>
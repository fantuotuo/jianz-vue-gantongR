<template>
	<div :class='$style.wrapper'>
		<div :class='$style.layout' class='layout'>
			<div :class='$style.header' class='alert alert-secondary'>
				<div class='text-center'>
					用户账号：<span class='bg-primary text-white p-1'>{{zhanghao}}</span>
				</div>
				<div class='text-center'>
					请选择登陆感统系统的用户
				</div>
			</div>
			
			<p v-if='users.length<=0' class='alert alert-secondary'>
				此账号还没有已经生成的训练方案，请先点击导航栏<router-link to='../login'>填写量表</router-link>
			</p>
			<TheUsersBlock 
				v-for='(item) in users' 
				:key='item.i' 
				:u-i='item.u_i' 
				:name='item.name' 
				:date='item.date' />
		</div>
	</div>

</template>

<script>
import {mapState} from "vuex";
import TheUsersBlock from "./DesktopUsersBlock.vue";

export default{
	name:"desktopUsers",
	computed:{
		...mapState({
			zhanghao:state=>state.desktop_zhanghao,
			users:state=>state.desktop_users,
		}),
	},
	// 创建完成后获取用户数据
	created:function(){
		console.log("desktop_created");

		this.axios.get("./api/vueGetUserList.aspx")
			.then(response=>{
				let ret=response.data;

				this.$store.commit("desktop_user_load",{
					user:ret.user,
					users:ret.users,
				});

				this.$emit("loading",false);
			})
			.catch(error=>{
				alert("发送请求错误："+error);
				this.$emit("loading",false);
			});
		this.$emit("loading",true);
	},
	components:{
		TheUsersBlock
	}
}

</script>

<style module lang="scss">
.wrapper{

}
.header{
	line-height:1.8rem;
	margin-bottom:15px;
}

</style>
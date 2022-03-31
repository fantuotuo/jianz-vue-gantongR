<template>
	<div class='wrapper gt-desktop'>
		<div class='layout'>
			<el-alert
				type='info'
				effect='light'
				:closable='false'
				center
			>
				<template slot='title'>
					点击学员姓名，查看该学员评估结果和训练方案<br />
					创建新学员请点击 <kbd>+</kbd> 号，填写评估量表后可查看评估结果和生成训练方案
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
				:key='item.i' 
				:u-i='item.u_i' 
				:name='item.name' 
				:date='item.date' 
				:sex='item.sex'
			/>
			<div class='gt-userblock' :title='getTitlePlus'>
				<el-button type='' :disabled='getLiangbiaoTimesRemain<=0' @click='toLogin'>
					<div class='gt-userblock__plus'>
						<i class='el-icon-plus'></i>
					</div>
				</el-button>
			</div>
		</div>
	</div>

</template>

<script>
// 系统首页，可以切换使用不同的量表进行训练、查看成绩等
import {mapState} from "vuex";
import TheUsersBlockNew from "./DesktopUsersBlockNew.vue";

export default{
	name:"Desktop",
	computed:{
		...mapState({
			user:state=>state.desktop_obj.user,
			users:state=>state.desktop_obj.users,
			liangbiao_times:state=>state.desktop_obj.liangbiao_times,
		}),
		// 剩余量表数量
		getLiangbiaoTimesRemain:function(){
			return this.liangbiao_times-this.users.length;
		},
		// 添加量表按钮的标题
		getTitlePlus:function(){
			var enabled=this.getLiangbiaoTimesRemain>0;
			return enabled?"新增一个量表":"你的量表创建次数已经用完，如需继续创建，请联系管理员。";
		}
	},
	created:function(){
		console.log("desktop created");

		this.$http
			.get("./api/vueGetUserList.aspx",{})
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
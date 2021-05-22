<template>
	<div>
		<el-steps 
			v-bind:active='step' 
			class='gt-steps'
			finish-status='success'
			simple
		>
			<el-step title='个人信息'></el-step>
			<el-step title='填写量表'></el-step>
		</el-steps>
		
		<transition
			name='transition-step-toggle'
			mode='out-in'
		>
			<TheForm v-if='step==0' v-on:submit='submitForm' />
			<TheLiangbiao v-else v-on:submit='submitLiangbiao' />
		</transition>
	</div>
</template>

<script>
// import {mapState} from "vuex";
import TheForm from "../newLogin/TheForm.vue";				// 已修改
import TheLiangbiao from "../newLogin/TheLiangbiao.vue";	// 已修改

export default{
	name:"Login",
	data:function(){
		// 路由发生变化时，数据重新生成
		return {
			step:0,

			name:"",
			age:"",
			sex:"",
			remark:"",
			str:"",
		}
	},
	created:function(){
		console.log("login created");
	},
	methods:{
		routeChange:function(){
			console.log("change")
		},
		// 提交个人信息
		submitForm:function(obj){
			this.name=obj.name;
			this.sex=obj.sex;
			this.age=obj.age;
			this.remark=obj.remark;

			this.step=1;
		},
		// 提交量表
		submitLiangbiao:function(obj){
			this.str=obj.str;
			
			this.submit();
		},
		submit:function(){
			let url="./api/vueSubmitLB.aspx";
			this.$http
				.get(encodeURI(url),{
					score:this.str,
					name:this.name,
					sex:this.sex,
					age:this.age,
					remark:this.remark
				})
				.then(ret=>{
					if(ret.u_i>=0){
						this.$router.push({
							path:"fangan",query:{u_i:ret.u_i}
						});
					}else{
						alert(ret.msg+"");
					}
				})
				.catch(()=>{});
		}
	},
	components:{
		TheForm,
		TheLiangbiao,
	}

}

</script>

<style lang='scss'>
.gt-steps{
	width:800px;
	margin:0 auto 15px;
}

</style>
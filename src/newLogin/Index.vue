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
		}
	},
	created:function(){
		console.log("login created");
		this.$store.commit("login_init_set",{
			name:"",
			sex:"男",
			age:"3",
			remark:"",
			str:"",
		});
	},
	methods:{
		routeChange:function(){
			console.log("change")
		},
		submitForm:function(){
			this.step=1;
		},
		submitLiangbiao:function(){
			this.submit();
		},
		submit:function(){
			let url="./api/vueSubmitLB.aspx?score="+this.$store.state.login_str;
			url+="&name="+this.$store.state.login_name;
			url+="&sex="+this.$store.state.login_sex;
			url+="&age="+this.$store.state.login_age;
			url+="&remark="+this.$store.state.login_remark;
			this.$http
				.get(encodeURI(url))
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
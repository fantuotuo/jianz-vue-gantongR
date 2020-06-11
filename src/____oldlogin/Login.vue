<template>
	<div :class='$style.wrapper'>
		<transition
			name='transition-step-toggle'
			mode='out-in'
		>
			<TheForm v-if='step==0' @submit='submitForm' />
			<TheLiangbiao v-else @submit='submitLiangbiao' />
		</transition>

		<TheLoading :show='loading' />
	</div>

</template>

<script>
import {mapState} from "vuex";
import TheForm from "./TheForm.vue";
import TheLiangbiao from "./TheLiangbiao.vue";
import TheLoading from "../components/TheLoading.vue";

export default{
	name:"Login",
	data:function(){
		// 路由发生变化时，数据重新生成
		return {
			step:0,
		}
	},
	computed:{
		...mapState({
			loading:state=>state.login_loading,
		}),
	},
	created:function(){
		console.log("login_created");
		this.loadingSet(false);
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
		loadingSet:function(bool){
			this.$store.commit("login_loading_set",{
				loading:bool
			});
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
			this.axios
				.get(url)
				.then(response=>{
					let ret=response.data;
					if(!ret) return;
					if(ret.u_i>=0){
						this.$router.push({
							path:"fangan",query:{u_i:ret.u_i}
						});
						// window.location.href="../fangan?u_i="+ret.u_i;
					}else{
						alert(ret.msg+"请重新提交");
						this.loadingSet(false);
					}
				})
				.catch(error=>{
					alert("发送请求错误："+error);
					this.loadingSet(false);
				});
			this.loadingSet(true);
		}
	},
	components:{
		TheForm,
		TheLiangbiao,
		TheLoading,
	}
}

</script>

<style module lang='scss'>
.wrapper{

}

</style>
<style lang='scss'>
.transition-step-toggle-enter-active,.transition-step-toggle-leave-active{
	transition: all .3s ease;
}
.transition-step-toggle-enter,.transition-step-toggle-leave-to{
	// transform: translateX(500px);
	opacity:0;
}
</style>
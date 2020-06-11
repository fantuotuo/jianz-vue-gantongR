<template>
	<div>

		<TheLoading :show='loading' />
	</div>

</template>

<script>
import TheLoading from "../components/TheLoading.vue";
import {mapState} from "vuex";
export default{
	"name":"Logout",
	computed:{
		...mapState({
			loading:state=>state.logout_loading
		}),
	},
	created:function(){
		console.log("logout_created");

		this.axios.get("./api/vueLogout.aspx")
			.then(response=>{
				let ret=response.data;

				if(ret.code===1){
					// 保存路由，保证登陆后返回desktop
					this.$router.push({"path":"desktop"});
					window.location.reload();
				}else{
					alert("发送请求错误："+ret.msg);
					this.$router.go(-1);
				}
				this.loadingSet(false);
			})
			.catch(error=>{
				alert("发送请求错误："+error);
				this.$router.go(-1);
				this.loadingSet(false);
			});
		this.loadingSet(true);
	},
	methods:{
		loadingSet:function(bool){
			this.$store.commit("logout_loading_set",{
				loading:bool
			});
		}
	},
	components:{
		TheLoading,
	}
}

</script>

<style module lang="scss">
	

</style>
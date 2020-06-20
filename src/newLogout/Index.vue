<template>
	<div>

	</div>

</template>

<script>
export default{
	"name":"Logout",
	computed:{

	},
	created:function(){
		console.log("logout_created");

		this.axios.get("./api/vueLogout.aspx")
			.then(response=>{
				let ret=response.data;

				if(ret.unLogin===true){
					// 未登录
					location.href='/gt/';
				}else if(ret.code===1){
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
			this.$store.commit("loading_set",{
				loading:bool
			});
		}
	},
	components:{

	}
}

</script>

<style module lang="scss">
	

</style>
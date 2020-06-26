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

		this.$http
			.get("./api/vueLogout.aspx")
			.then(ret=>{
				if(ret.code===1){
					// 保存路由，保证登陆后返回desktop
					this.$router.push({"path":"desktop"});
					window.location.reload();
				}else{
					alert("退出登陆时发生错误："+ret.msg);
					this.$router.go(-1);
				}
			})
			.catch(()=>{});
	},
	methods:{

	},
	components:{

	}
}

</script>

<style module lang="scss">
	

</style>
<template>
	<div class='layout'>
		<el-alert 
			title='你没有权限使用感统在线训练系统，请联系管理员。'
			type='error'
			effect='dark'
			v-bind:closable='false'
		>
			
		</el-alert>
	</div>
</template>

<script>
export default{
	name:"Error",
	created:function(){console.log(this.$route.path)}
}
</script>

<style lang='scss'>


</style>
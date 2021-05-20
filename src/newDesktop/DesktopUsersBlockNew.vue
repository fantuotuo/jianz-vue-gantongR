<template>
	<div class='gt-userblock' v-bind:title='"切换至 "+name+" 的量表"'>
		<el-button type='info' v-on:click='$emit("click",uI)'>
			<div class='gt-userblock__name'><i class='el-icon-user'></i>{{name}}</div>
			<div class='gt-userblock__date'>生成日期：{{date}}</div>
		</el-button>
	</div>
	
</template>

<script>
export default{
	name:"DesktopUsersBlockNew",
	props:{
		name:String,
		date:String,
		uI:String
	}



}

</script>

<style lang='scss'>
.gt-userblock{
	width:60%;
	margin:0 auto;
	margin-bottom:15px;

	.el-button{
		width:100%;
		height:66px;
		line-height:1.3;
	}
	.el-icon-user{
		margin-right:5px;
	}
	.gt-userblock__name{
		font-size:1.2rem;
	}
	.gt-userblock__date{
		font-size:0.9rem;
	}
}

</style>
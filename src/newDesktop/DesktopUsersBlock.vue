<template>
	<router-link class='gt-userblock' :to='{path:"/fangan",query:{u_i:uI}}'>
	<!-- <a :class='$style.wrapper' :href='"../fangan?u_i="+uI' title='登陆'> -->
		<div class='gt-userblock__name'>
			{{name}}
		</div>
		<div class='gt-userblock__date'>
			生成日期：{{date}}
		</div>
	</router-link>

</template>

<script>
export default{
	name:"DesktopUsersBlock",
	props:{
		name:String,
		date:String,
		uI:Number
	}
}

</script>

<style lang='scss'>
.gt-userblock{
	display:block;
	width: 60%;
    height: 60px;
    margin: 15px auto 0;
    cursor: pointer;
    position: relative;
    border: 1px solid transparent;
    background-color: #fff;
    color:#212529;
    border-color: #cdcdcd;
    transition: all 0.3s;

    &:hover{
		background-color: #e6e6e6;
		border-color: #adadad;
		color:#212529;
		// box-shadow: 0 3px 3px rgba(0,0,0,0.1);
	}
}
.gt-userblock__name{
	position: absolute;
	top: 5px;
	left: 5px;
	font-size: 1.2rem;
}
.gt-userblock__date{
	position: absolute;
	bottom: 5px;
	right: 5px;
	color: #888;
	font-size: 90%;
}
</style>
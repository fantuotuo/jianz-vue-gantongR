<template>
	<div :class='$style.wrapper'>
		<div :class='$style.layout' class='layout'>
			<div class='btn-group'>
				<button type='button' 
					v-for='item in btns'
					:key='item.i'
					:class='["btn","btn-outline-primary",item.i==step?"active":""]'
					@click='$emit("toggle",item.i)'
				>
					{{item.text}}
				</button>
			</div>
		</div>
	</div>

</template>

<script>
export default{
	name:"BtnsStep",
	props:{
		step:Number,
	},
	data:function(){
		return {
			btns:[
				{i:0,text:"测评报告"},
				{i:1,text:"训练方案"},
				{i:2,text:"训练记录"},
			]
		}
	}
}

</script>

<style module lang='scss'>
.wrapper{

}
div.layout{
	text-align:center;
	margin-bottom:15px;
}

</style>
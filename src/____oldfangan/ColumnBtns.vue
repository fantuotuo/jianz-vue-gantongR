<template>
	<div class='gt-col'>
		<button 
			v-for='item in btns'
			:key='item.i'
			@click='$emit("click",item.score,row)'
			type='button' 
			:class='[$style.btn,item.score==score?"btn-primary":"btn-secondary"]'
			class='btn btn-sm'
			:disabled='getDisabled'>
			{{item.score}}
		</button>
	</div>

</template>

<script>
export default{
	name:"ColumnBtns",
	props:{
		score:Number,
		row:Number,
	},
	computed:{
		getDisabled:function(){
			return this.score>0;
		}
	},
	data:function(){
		return {
			btns:[
				{i:0,score:1},
				{i:1,score:2},
				{i:2,score:3},
				{i:3,score:4},
				{i:4,score:5}
			]
		}
	}

}

</script>

<style module lang='scss'>
.btn:not(:first-child){
	margin-left:8px;
}

</style>
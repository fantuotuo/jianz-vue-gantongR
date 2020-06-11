<template>
	<div class='gt-col gt-col-sm center'>
		<div>
			<div v-for='item in imgs' :key='item.i'>
				<img :src='"/gantong/public/images/"+item.v+".png"' :alt='item.v' :title='item.v' />
				<br />
				<span>{{item.v}}</span>
			</div>
		</div>

	</div>

</template>

<script>
export default{
	name:"ColumnTools",
	props:{
		text:String,
	},
	computed:{
		imgs:function(){
			if(this.text){
				return this.text.split("、").map((v,k)=>{
					return {i:k,v:v};
				});
			}else{
				return [];
			}
		}
	}

}

</script>

<style module lang='scss'>
	

</style>
<template>
	<div :class='["gt-col",getClass,getClassComment,getClassTip]' class='clearFix'>
		<slot></slot>
		{{getText}}
	</div>

</template>

<script>
export default{
	name:"Column",
	props:{
		text:String,
		size:{
			type:Number,
			default:1,
		},
		type:{
			type:String,
			default:"text"
		}
	},
	computed:{
		getText:function(){
			if(this.type=="comment"){
				// 输出量表结果
				let index=parseInt(this.text);
				return ["","正常","轻度偏差","中度偏差","严重偏差"][index];
			}else if(this.type=="tip"){
				// 输出操作说明
				return this.text.replace(/<br>/g,"\n");
			}else{
				return this.text;
			}
		},
		getClassComment:function(){
			if(this.type=="comment"){
				let index=parseInt(this.text);
				let p=["","a","b","c","d"][index];
				return this.$style["comment-"+p];
			}else{
				return "";
			}
		},
		getClassTip:function(){
			if(this.type=="tip"){
				return this.$style.tip;
			}else{
				return "";
			}
		},
		getClass:function(){
			if(this.size==0){
				return "gt-col-sm";
			}else if(this.size==2){
				return "gt-col-db";
			}else if(this.size==3){
				return "gt-col-lg";
			}else{
				return "";
			}
		}
	}
}

</script>

<style module lang='scss'>
.comment-a{
	background-image:url("../assets/comment-bg-a.png");
}
.comment-b{
	background-image:url("../assets/comment-bg-b.png");
}
.comment-c{
	background-image:url("../assets/comment-bg-c.png");
}
.comment-d{
	background-image:url("../assets/comment-bg-d.png");
}
.tip{
	font-size:0.8rem;
}
</style>
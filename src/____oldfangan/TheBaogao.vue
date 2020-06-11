<template>
	<div :class='$style.wrapper'>
		<div :class='$style.layout' class='layout'>
			<div :class='$style.table'>
				<div class='gt-row head bg-secondary text-white'>
					<Column :text='"姓名"' />
					<Column :text='name' />
					<Column :text='"年龄"' />
					<Column :text='age+""' />
				</div>
			</div>
			<div :class='$style.table'>
				<div class='gt-row head bg-secondary text-white'>
					<Column :text='"模块名称"' :size='2' />
					<Column :text='"得分"' :size='2' />
					<Column :text='"能力水平"' :size='2' />
				</div>
				<div
					v-for='(item) in rows'
					:key='item.i'
					class='gt-row'
				>
					<Column :text='names[item.i]' :size='2' />
					<Column :text='item.score+""' :size='2' />
					<Column :text='item.comment+""' :size='2' :type='"comment"' />
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import Column from './Column.vue';

export default{
	name:"TheBaogao",
	props:{
		name:String,
		age:Number,
		rows:Array,
	},
	data:function(){
		return {
			names:[
				"前庭平衡和大脑双侧分化",
				"执行功能与神经抑制功能",
				"触觉敏感性（触觉防御过多、不足）",
				"发展性协调运动功能（运用困难）",
				"视觉空间感知",
				"重力不安症",
				"情绪管理与自我调节"
			]
		};
	},
	components:{
		Column,
	}

}

</script>

<style module lang='scss'>
.wrapper{

}
</style>
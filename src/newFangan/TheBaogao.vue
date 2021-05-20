<template>
	<div>
		<div class='layout'>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='姓名' />
					<Column v-bind:text='name' />
					<Column text='年龄' />
					<Column v-bind:text='age+""' />
				</div>
			</div>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='模块名称' size='double' />
					<Column text='得分' size='double' />
					<Column text='能力水平' size='double' />
				</div>
				<div
					v-for='(item) in rows'
					v-bind:key='item.i'
					class='gt-row'
				>
					<Column v-bind:text='names[item.i]' size='double' />
					<Column v-bind:text='item.score+""' size='double' />
					<Column v-bind:text='item.comment+""' size='double' type='comment' />
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import {mapState} from 'vuex';

export default{
	name:"TheBaogao",
	computed:{
		...mapState({
			name:state=>state.fangan_obj.name,
			age:state=>state.fangan_obj.age,
			rows:state=>state.fangan_obj.score,
		})
	},props:{a:String},
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
		// Column,
	}

}

</script>

<style lang='scss'>



</style>
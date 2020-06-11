<template>
	<div :class='$style.wrapper'>
		<div :class='$style.layout' class='layout'>
			<div class='btn-group'>
				<button 
					v-for='item in dates'
					:key='item.i'
					@click='$emit("clickDate",item.date)'
					:class='date==item.date?"btn-primary":"btn-secondary"'
					class='btn btn-sm'
				>
					{{item.date}}
				</button>
			</div>
			<div :class='$style.table'>
				<div class='gt-row head bg-secondary text-white'>
					<Column :text='"项目名称"' :size='2' />
					<Column :text='"得分"' :size='2' />
					<Column :text='"训练目的"' :size='2' />
				</div>
				<div 
					v-for='item in record'
					:key='item.i'
					class='gt-row'
				>
					<Column :text='item.name' :size='2' />
					<Column :text='item.score' :size='2' />
					<Column :text='item.goal' :size='2' />
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import Column from './Column.vue';

export default{
	name:"TheRecord",
	props:{
		date:String,
		dates:Array,
		record:Array,
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
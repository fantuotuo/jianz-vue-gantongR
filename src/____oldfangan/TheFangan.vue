<template>
	<div :class='$style.wrapper'>
		<div :class='$style.layout' class='layout'>
			<div :class='$style.table'>
				<div class='gt-row head bg-secondary text-white'>
					<Column :text='"姓名"' />
					<Column :text='name' />
					<Column :text='"年龄"' />
					<Column :text='age+""' />
					<Column :text='"已训练天数"' />
					<Column :text='days+""' />
				</div>
			</div>
			<div :class='$style.table'>
				<div class='gt-row head bg-secondary text-white'>
					<Column :text='"项目名称"' :size='0' />
					<Column :text='"训练目的"' :size='0' />
					<Column :text='"训练教具"' :size='0' />
					<Column :text='"训练时间"' :size='0' />
					<Column :text='"操作方法"' :size='3' />
					<Column :text='"操作评分"' />
				</div>
				<div 
					v-for='item in rows'
					:key='item.i'
					class='gt-row'
					:class='item.score>0?"disable":""'
				>
					<Column :text='item.name' :size='0' />
					<Column :text='item.goal' :size='0' />
					<Column :text='""' :size='0' class='center'>
						<div>
							<div
								v-for='(item_son) in getImgs(item.tools)'
								:key='item_son.i'

							>
								<img :src='"/gantong/public/images/"+item_son.v+".png"' :alt='item_son.v' :title='item_son.v' />
								<br />
								<span>{{item_son.v}}</span>
							</div>
						</div>
					</Column>
					<Column :text='item.duration' :size='0' />
					<Column :text='item.tip' :size='3' :type='"tip"' />
					<Column :text='""'>
						<button 
							v-for='(item_son,index) in [1,2,3,4,5]'
							:key='index'
							class='btn btn-sm gt-col__btn'
							:class='item.score===item_son?"btn-primary":"btn-secondary"'
							disabled='item.score>0' 
							@click='clickScore(item.i,item_son)'
						>
							{{item_son}}
						</button>
					</Column>
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import Column from './Column.vue';
// import ColumnTools from './ColumnTools.vue';
// import ColumnBtns from './ColumnBtns.vue';

export default{
	name:'TheFangan',
	props:{
		name:String,
		age:Number,
		days:Number,
		rows:Array,
	},
	methods:{
		getImgs:function(str){
			if(str){
				return str.split("、").map((v,k)=>{
					return {i:k,v:v};
				});
			}else{
				return [];
			}
		},
		clickScore:function(score,row){
			this.$emit("clickScore",score,row);
		}
	},
	components:{
		Column,
		// ColumnTools,
		// ColumnBtns,
	}

}

</script>

<style lang='scss'>
.gt-col__btn:not(:first-child){
	margin-left:8px;
}
</style>
<style module lang='scss'>
.wrapper{

}

</style>
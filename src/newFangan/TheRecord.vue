<template>
	<div class='wrapper-record'>
		<div class='layout'>
			<el-button-group class='btn-group' style='display:block'>
				<!-- <el-button size='small' @click='clickDate("表现曲线")' :type='date==="表现曲线"?"primary":""'>表现曲线</el-button> -->

				<el-button
					v-for='(item,i) in dates'
					v-bind:key='item.i'
					v-on:click='clickDate(item.date)'
					v-bind:type='date==item.date?"primary":""'
					size='mini'
					style='line-height:1.4'
				>
					{{i+1}}<br />
					{{item.date}}
				</el-button>
			</el-button-group>

			<!-- 每日得分详情 -->
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='double' />
					<Column text='得分' size='double' />
					<Column text='训练目的' size='double' />
				</div>
				<div 
					v-for='item in getRecord'
					:key='item.i'
					class='gt-row'
				>
					<Column :text='item.name' size='double' style="justify-content:flex-start" />
					<Column :text='item.score+""' size='double' />
					<Column :text='item.goal' size='double' />
				</div>
			</div>

			<MyChartModule :ui='ui' />
		</div>
	</div>

</template>

<script>
import {mapState} from 'vuex';
import MyChartModule from "../components/MyChartModule.vue";
// import { filter } from 'vue/types/umd';

export default{
	name:"TheRecord",

	data:function(){
		return {
			record:[],
		}
	},
	props:{
		date:{
			type:String,
			default:""
		}
	},
	watch:{
		"date":{
			immediate:true,
			handler(){
				this.dateChange();
			}
		}
	},
	computed:{
		...mapState({
			dates:state=>state.fangan_obj.dates,
			ui:state=>state.fangan_ui
		}),
		getRecord(){
			var record=this.record;
			if(!record) return[];
			// 去重
			return record.filter((item,i)=>{
				return i===getIndex(item);
			});

			function getIndex(item){
				for(var k=0;k<record.length;k++){
					if(record[k].name===item.name){
						return k;
					}
				}
				return -1;
			}
		}
	},
	methods:{
		/**
		 * 点击了某个日期按钮
		 * @param date 日期
		*/
		clickDate(date){
			this.$emit("clickDate",date);
		},
		dateChange(){
			var date=this.date;
			
			this.$http
				.get("./api/vueGetRecord.aspx",{
					date:date,
					u_i:this.ui
				})
				.then(ret=>{
					if(ret && ret.code===1){
						this.record=ret.record;
					}
				})
				.catch(()=>{});
		}
	},
	components:{
		MyChartModule
	}
}


</script>

<style lang='scss'>
.wrapper-record{
	.btn-group{
		margin-bottom:20px;
	}
}
.chart-parent{
	>div{
		float:left;
		&:not(:first-child){
			margin-left:20px;
		}
	}
}

</style>
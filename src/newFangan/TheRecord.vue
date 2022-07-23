<template>
	<div class='wrapper-record'>
		<div class='layout'>
			<el-button-group class='btn-group' style='display:block'>
				<!-- <el-button size='small' @click='clickDate("表现曲线")' :type='date==="表现曲线"?"primary":""'>表现曲线</el-button> -->

				<el-button
					v-for='(item,i) in dates'
					:key='item.i'
					@click='date = item.date'
					:type='date==item.date?"primary":""'
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
					:key='item.courseId'
					class='gt-row'
				>
					<Column text='' size='double' style="justify-content:flex-start">
						<div>
							<i v-if='item.uploadUser!=""' class='el-icon-s-opportunity'></i>
							{{item.enName+" "+item.name}}
						</div>
					</Column>
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
			date:"",
		}
	},
	mounted:function(){
		this.$store.dispatch("fangan_dates_fatch",{ui:this.ui})
			.then((dates)=>{
				if(dates.length>0){
					this.date = dates[dates.length-1].date;
				}
			})
			.catch(error=>{
				error;
				this.$router.push({
					path:"/"
				});
			});
	},
	watch:{
		"date":{
			immediate:false,
			handler(){
				this.dateChange();
			}
		}
	},
	computed:{
		...mapState({
			dates:state=>state.fangan_obj.dates,
			ui:state=>state.fangan_obj.ui
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
					if(record[k].courseId===item.courseId){
						return k;
					}
				}
				return -1;
			}
		},
	},
	methods:{
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
					}else{
						alert(ret)
					}
				})
				.catch(error=>{
					alert(333)
					this.$message({
						text:error,
						type:"error"
					})
				});
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
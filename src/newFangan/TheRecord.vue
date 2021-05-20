<template>
	<div>
		<div class='layout'>
			<el-button-group>
				<el-button size='small' @click='clickDate("表现曲线")' :type='date==="表现曲线"?"primary":""'>表现曲线</el-button>

				<el-button
					v-for='item in dates'
					v-bind:key='item.i'
					v-on:click='clickDate(item.date)'
					v-bind:type='date==item.date?"primary":""'
					size='small'
				>
					{{item.date}}
				</el-button>
			</el-button-group>

			<div v-if='date==="表现曲线"'>
				<div class='chart-parent clear'>
					<div ref='chart1' style='width:487px;height:487px;'></div>
					<div ref='chart2' style='width:487px;height:487px;'></div>
				</div>
			</div>
			<div v-else>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='double' />
					<Column text='得分' size='double' />
					<Column text='训练目的' size='double' />
				</div>
				<div 
					v-for='item in getRecord'
					v-bind:key='item.i'
					class='gt-row'
				>
					<Column v-bind:text='item.name' size='double' />
					<Column v-bind:text='item.score+""' size='double' />
					<Column v-bind:text='item.goal' size='double' />
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import {mapState} from 'vuex';
import * as echarts from "echarts";

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
		"date":"dateChange"
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
		// 获取图表曲线数据
		fetchChartData(){
			var myChart=echarts.init(this.$refs["chart1"]);
			myChart.showLoading();
			myChart.setOption(OPTION_WHOLE);

			var myChart2=echarts.init(this.$refs["chart2"]);
			myChart2.showLoading();
			myChart2.setOption(OPTION_WHOLE);
			myChart2.setOption({title:{text: "模块表现"}});
			this.$http.get("./api/chart/getWhole.aspx",{
				u_i:this.ui
			})
			.then(ret=>{
				myChart.hideLoading();
				myChart.setOption({
					xAxis: {
						data: ret.axis
					},
					series: [{
						name: '总体表现',
						data: ret.series,
						smooth:true
					}]
				});
			})
			.catch(error=>error);

			this.$http.get("./api/chart/getModule.aspx",{
				u_i:this.ui
			})
			.then(ret=>{
				ret.series.forEach(ser=>{
					seriesDataFilter(ser.data);
				});

				myChart2.hideLoading();
				myChart2.setOption({
					title: {text: "模块表现"},
					legend: {
						data: ret.legend,
						left:120
					},
					xAxis: {
						data: ret.axis
					},
					series:ret.series
				});
			})
			.catch(error=>error);
		},
		clickDate(date){
			this.$emit("clickDate",date);
		},
		dateChange(){
			var date=this.date;

			if(date==="表现曲线"){
				this.$nextTick(()=>{
					this.fetchChartData();
				});

				return;
			}

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

}


function seriesDataFilter(scoreArr){
	var s=0;
	for(let k=0;k<scoreArr.length;k++){
		if(scoreArr[k]>0){
			s=scoreArr[k];
			break;
		}
	}
	for(let k=0;k<scoreArr.length;k++){
		if(scoreArr[k]<=0){
			scoreArr[k]=s;
		}else{
			s=scoreArr[k];
		}
	}
}



const OPTION_WHOLE={
	title: {
        text: '总体表现'
    },
    tooltip: {
        trigger: 'axis'
    },
    legend: {
        data:['总体表现']
    },
    grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
    },
    xAxis: {
        type: 'category',
        boundaryGap: false,
        data: []
    },
    yAxis: {
        type: 'value'
    },
    series: [
        {
            name:'总体表现',
            type:'line',
            stack: '总量',
            data:[]
        }
    ]
}

</script>

<style lang='scss'>
.chart-parent{
	>div{
		float:left;
		&:not(:first-child){
			margin-left:20px;
		}
	}
}

</style>
<template>
    <div class="chart-container">
        <div class='chart-pannel'>
            <div ref='echarts1'></div>
        </div>
        <div class='chart-pannel'>
            <div ref='echarts2'></div>
        </div>
    </div>
</template>

<script>
import * as echarts from "echarts";

export default {
    name:"MyChartModule",
    props:{
        ui:Number,
    },
    data:function(){
        return {
            myChart:null,
            myChart2:null,
        }
    },

    mounted(){
        this.initCharts();
        this.fetchData();
    },
    methods:{
        initCharts(){
            this.myChart = echarts.init(this.$refs["echarts1"]);
            this.myChart.showLoading();
            this.myChart.setOption(OPTION_WHOLE);

            this.myChart2 = echarts.init(this.$refs["echarts2"]);
            this.myChart2.showLoading();
            this.myChart2.setOption(OPTION_WHOLE);
            this.myChart2.setOption({title:{text: "模块表现"}});
        },
        fetchData(){
            this.$http.get("./api/chart/getWhole.aspx",{
				u_i:this.ui
			})
			.then(ret=>{
				// 数据综合考虑之前数据的加权
				arrSmoothFilter(ret.series);

				this.myChart.hideLoading();
				this.myChart.setOption({
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
					// 数据综合考虑之前数据的加权
					arrSmoothFilter(ser.data);
					// 防止某一天未进行某个模块的训练，导致数据的空置
					seriesDataFilter(ser.data);
				});

				this.myChart2.hideLoading();
				this.myChart2.setOption({
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
        }
    }

}


/**
 * 模块表现数据格式化（不是每天都有某个模块的数据）
 * @param scoreArr 待格式化数组
*/
function seriesDataFilter(scoreArr){
	// 先找到第一个不为空的score
	let s=0;
	for(let k=0;k<scoreArr.length;k++){
		if(scoreArr[k]>0){
			s=scoreArr[k];
			break;
		}
	}
	// 再填充中间空余的score
	for(let k=0;k<scoreArr.length;k++){
		if(scoreArr[k]<=0){
			scoreArr[k]=s;
		}else{
			s=scoreArr[k];
		}
	}
}
/**
 * 分数数据顺滑格式化，考虑之前训练成绩的加权
 * @param arr 待格式化数组
*/
function arrSmoothFilter(arr){
	var sum=0,
		day=0;
	for(let i=0;i<arr.length;i++){
		if(arr[i]<=0) continue;
		day++;
		sum+=arr[i];
		arr[i]=parseFloat(
			(sum/(day)).toFixed(1)
		);
	}
}

// echart默认配置
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
};
</script>

<style lang="scss">
.chart-container{
    display:grid;
    grid-template-columns:repeat(2,1fr);
}
.chart-pannel{
    box-sizing:border-box;
    box-shadow:rgba(0,0,0,0.1) 0px 0px 20px;
    border-radius: 5px;
    background: #fff;
    overflow: hidden;
    padding: 10px;
    margin:15px 10px 0;

    >div{
        width:280px;
        height:280px;

        width:440px;
        height:440px;
    }
}

</style>
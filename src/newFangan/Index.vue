<template>
	<div>
		<!-- <div class='layout print-hide' style='text-align:right;padding-right:20px;'>
			<el-button type='primary' size='small' @click='onClickPrint'>打印</el-button>
		</div> -->

		<div class='layout' style='margin-bottom:20px;'>
			<el-descriptions title='学员信息' :column='4' size='small' border>
				<template slot='extra'>
					<el-button class='print-hide' type='primary' size='small' @click='onClickPrint'>打印</el-button>
				</template>
				<el-descriptions-item>
					<template slot='label'>姓名</template>
					{{fangan_obj.name}}
				</el-descriptions-item>
				<el-descriptions-item>
					<template slot='label'>年龄</template>
					{{fangan_obj.age}}岁
				</el-descriptions-item>
				<el-descriptions-item>
					<template slot='label'>已训练天数</template>
					{{fangan_obj.dates.length}}
				</el-descriptions-item>
				<el-descriptions-item>
					<template slot='label'>量表生成日期</template>
					{{fangan_obj.date}}
				</el-descriptions-item>
			</el-descriptions>
		</div>
		
		

		<div class='layout center'>
			<el-button-group>
				<el-button 
					v-bind:type='step===0?"primary":""' 
					v-on:click='toggleStep(0)'
					size='small'
				>
					测评报告
				</el-button>
				<el-button
					v-bind:type='step===1?"primary":""' 
					v-on:click='toggleStep(1)'
					size='small'
				>
					训练方案
				</el-button>
				<el-button 
					v-bind:type='step===2?"primary":""' 
					v-on:click='toggleStep(2)'
					size='small'
				>
					训练记录
				</el-button>
			</el-button-group>
		</div>
		<!-- <i class="bi-alarm"></i> -->
		<div>
			<TheBaogao v-if='step==0' />
			<TheFangan v-if='step==1' />
			<TheRecord v-if='step==2' />
		</div>
	</div>
</template>

<script>
import {mapState} from "vuex";
import TheBaogao from "./TheBaogao.vue";
import TheFangan from "./TheFangan.vue";
import TheRecord from "./TheRecord.vue";
import { TipsFilter } from "../plugins/utils.js";

export default{
	name:"Fangan",
	data:function(){
		return {
			step:0,
		}
	},
	computed:{
		...mapState({
			fangan_obj:state=>state.fangan_obj
		})
	},
	watch:{
		"$route":"routeChange"
	},
	// 路由导航切换时会调用
	// 当使用路由参数时，例如从 /user/foo 导航到 /user/bar，原来的组件实例会被复用。
	// 复用组件时，想对路由参数的变化作出响应的话，你可以简单地 watch (监测变化) $route 对象
	created:function(){
		this.routeChange();
	},
	methods:{
		routeChange:function(){
			this.checkQuery();		// 检测query，是否需要自动切换到方案
			let ui = parseInt(this.$route.query.u_i);
			if(isNaN(ui)) ui = 0;
			if(this.fangan_obj.ui===ui){
				// ui未改变，不需要重新获取数据
				return;
			}

			// 重置相关数据值
			this.$store.commit("fangan_init_set",{
				ui:ui,
			});

			let url="./api/vueGetFangan.aspx?u_i="+ui;
			this.$http
				.get(url)
				.then(ret=>{
					if(!ret.fangan || ret.fangan.length<=0){
						alert("量表不存在！");
						this.$router.push({
							path:"/"
						});
						return;
					}
					ret.fangan.forEach(element => {
						element.tip = TipsFilter(element.tip);
						element.tips = element.tip.split("\n").filter(s=>s);
					});
					this.$store.commit("fangan_obj_set",{
						name:ret.name,
						age:ret.age,
						score:ret.score,
						fangan:ret.fangan,
						dates:ret.dates,
						arrayAll:ret.arrayAll,
						date:ret.date,
					});
				})
				.catch(()=>{
					this.$router.push({
						path:"/"
					})
				});
		},
		toggleStep(index){
			this.step=index;
		},
		checkQuery(){
			var query = this.$route.query;
			if(query.openFangan==="1"){
				this.toggleStep(1);
			}
		},
		onClickPrint(){
			window.print();
		}
	},
	components:{
		TheBaogao,
		TheFangan,
		TheRecord,
	}
}

</script>

<style lang='scss'>

</style>
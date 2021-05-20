<template>
	<div>
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
		<div>
			<TheBaogao v-show='step==0' />
			<TheFangan v-show='step==1' />
			<TheRecord v-show='step==2' :date='this.record_date' @clickDate='clickDateRecord' />
		</div>
			
		<TheVideo />
		<!-- <TheVideo :src='"http://jianz.com/zysx/static/videos/0/index.m3u8"' /> -->
		<!-- <TheVideo :src='"../videos/jianzhi_720.mp4"' /> -->
		<!-- <TheLoading :show='loading' /> -->
	</div>
</template>

<script>
import {mapState} from "vuex";
import TheBaogao from "./TheBaogao.vue";
import TheFangan from "./TheFangan.vue";
import TheRecord from "./TheRecord.vue";
import TheVideo from "./TheVideo.vue";

export default{
	name:"Fangan",
	computed:{
		...mapState({
			ui:state=>state.fangan_ui,
		}),
	},
	data:function(){
		return {
			step:0,
			record_date:""
		}
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
			console.log("fangan changed");
			let ui=parseInt(this.$route.query.u_i);
			// 重置相关数据值
			this.$store.commit("fangan_init_set",{
				ui:ui,
				recordfirst:true,
			});

			let url="./api/vueGetFangan.aspx?u_i="+this.ui;
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
					this.$store.commit("fangan_obj_set",{
						name:ret.name,
						age:ret.age,
						score:ret.score,
						fangan:ret.fangan,
						dates:ret.dates,
					});

					if(ret.dates.length>0){
						this.record_date=ret.dates[0].date;
					}
				})
				.catch(()=>{
					this.$router.push({
						path:"/"
					})
				});
		},
		toggleStep:function(index){
			this.step=index;
		},
		clickDateRecord(date){
			this.record_date=date;
		}
	},
	components:{
		TheBaogao,
		TheFangan,
		TheRecord,
		TheVideo,
	}
}

</script>

<style lang='scss'>

</style>
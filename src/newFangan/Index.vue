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
		<transition
			name='transition-step-toggle'
			mode='out-in'
		>
			<TheBaogao 
				v-if='step==0' 
			/>
			<TheFangan 
				v-if='step==1' 
			/>
			<TheRecord 
				v-if='step==2' 
			/>
		</transition>
			
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
			console.log("fangan_changed");
			let ui=this.$route.query.u_i;
			if(ui===undefined){
				// 返回桌面
				// this.$router.push({"path":"desktop",query:{}});
				this.$router.go(-1);
				alert("请选择一个用户登陆");
				return;
			}
			// 重置相关数据值
			this.$store.commit("fangan_init_set",{
				ui:ui,
				recordfirst:true,
			});

			let url="./api/vueGetFangan.aspx?u_i="+this.ui;
			this.axios.get(url)
				.then(response=>{
					let ret=response.data;
					if(ret){
						this.$store.commit("fangan_fangan_set",{
							name:ret.name,
							age:ret.age,
							score:ret.score,
							fangan:ret.fangan,
							dates:ret.dates,
						});
					}
					this.loadingSet(false);
				})
				.catch(error=>{
					alert("发送请求错误："+error);
					this.loadingSet(false);
				});
			this.loadingSet(true);
		},
		toggleStep:function(index){
			this.step=index;
		},
		loadingSet:function(bool){
			this.$store.commit("loading_set",{
				loading:bool
			});
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
<template>
	<div :class='$style.wrapper'>
		<BtnsStep :step='step' @toggle='toggleStep' />
		<transition
			name='transition-step-toggle'
			mode='out-in'
		>
			<TheBaogao v-if='step==0' :name='name' :age='age' :rows='score' />
			<TheFangan v-if='step==1' :name='name' :age='age' :days='dates.length' :rows='fangan' @clickScore='clickScore' />
			<TheRecord v-if='step==2' @clickDate='clickDate' :date='date' :dates='dates' :record='record' />
		</transition>
			
		<TheVideo :src='this.videosrc' :title='"title"' @close='closeVideo' />
		<!-- <TheVideo :src='"http://jianz.com/zysx/static/videos/0/index.m3u8"' /> -->
		<!-- <TheVideo :src='"../videos/jianzhi_720.mp4"' /> -->
		<TheLoading :show='loading' />
	</div>
</template>

<script>
import {mapState} from "vuex";
import TheLoading from "../components/TheLoading.vue";
import BtnsStep from "./BtnsStep.vue";
import TheBaogao from "./TheBaogao.vue";
import TheFangan from "./TheFangan.vue";
import TheRecord from "./TheRecord.vue";
import TheVideo from "./TheVideo.vue";

export default{
	name:"Fangan",
	computed:{
		...mapState({
			loading:state=>state.fangan_loading,
			ui:state=>state.fangan_ui,
			videosrc:state=>state.fangan_videosrc,

			name:state=>state.fangan_name,
			age:state=>state.fangan_age,
			score:state=>state.fangan_score,

			fangan:state=>state.fangan_fangan,

			dates:state=>state.fangan_dates,
			date:state=>state.fangan_date,
			record:state=>state.fangan_record,
			recordfirst:state=>state.fangan_recordfirst,
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
				alert("请选择一个用户登陆");
				// 返回桌面
				// this.$router.push({"path":"desktop",query:{}});
				this.$router.go(-1);
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
		closeVideo:function(){
			this.$store.commit("fangan_videosrc_set",{
				videosrc:""
			});
		},
		toggleStep:function(index){
			this.step=index;
			if(this.step==2 && this.recordfirst){
				if(this.dates.length>0){
					this.clickDate(this.dates[0].date);
				}
			}
		},
		clickScore:function(score,row){
			let url="./api/vueSubmitScore.aspx?row="+row+"&score="+score;
			url+="&u_i="+this.ui;

			this.axios.get(url)
				.then(response=>{
					let ret=response.data;
					if(ret && ret.code===1){
						this.$store.commit("fangan_score_set",{
							row:row,
							score:ret.score
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
		clickDate:function(date){
			let url="./api/vueGetRecord.aspx?date="+date+"&u_i="+this.ui;
			this.axios.get(url)
				.then(response=>{
					let ret=response.data;
					if(ret && ret.code===1){
						let record=ret.record;
						this.$store.commit("fangan_date_set",{
							date:date,
							record:record,
							recordfirst:false
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
		loadingSet:function(bool){
			this.$store.commit("fangan_loading_set",{
				loading:bool
			});
		}
	},
	components:{
		TheLoading,
		BtnsStep,
		TheBaogao,
		TheFangan,
		TheRecord,
		TheVideo,
	}
}

</script>

<style module lang='scss'>
.wrapper{

}

</style>
<template>
	<div>
		<div class='layout'>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='姓名' />
					<Column :text='name' />
					<Column text='年龄' />
					<Column :text='age+""' size='1' />
					<Column text='已训练天数' size='3' />
					<Column :text='days+""' />
				</div>
			</div>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='' />
					<Column text='训练目的' size='' />
					<Column text='训练教具' size='' />
					<Column text='训练时间' size='1' />
					<Column text='操作方法' size='3' />
					<Column text='操作评分' />
				</div>
				<div 
					v-for='item in rows'
					v-bind:key='item.i'
					class='gt-row'
					v-bind:class='item.score>0?"disable":""'
				>
					<Column v-bind:text='item.name' size='' style="justify-content:flex-start" />
					<Column v-bind:text='item.goal' size='' />
					<Column text='' size='' class='center'>
						<div style='width:100%'>
							<MyImgAid 
								v-for='item_son in getImgs(item.tools)'
								:key='item_son.i'
								:cnName='item_son.v'
							/>
						</div>
					</Column>
					<Column v-bind:text='item.duration+"分钟"' size='1' />
					<!-- <Column v-bind:text='item.tip' size='large' type='tip' /> -->
					<Column text='' size='3' type='tip'>
						<el-button :disabled='!item.videoLink' size='mini' @click='onVideoOpen(item.videoLink,item.name)'>
							<i class="el-icon el-icon-film"></i> {{item.videoLink?"查看视频":"暂无视频"}}
						</el-button>

						<el-popover effect="dark" placement='top' width="600" trigger="click">
							<div>
								<p v-for='(tp,iTp) in item.tips' :key='iTp'>{{tp}}</p>
							</div>
							<el-button slot="reference" size='mini'><i class="el-icon el-icon-notebook-2">
								</i> 查看操作方法
							</el-button>
						</el-popover>
					</Column>
					<Column text=''>
						<el-button 
							v-for='(item_son,index) in [1,2,3,4,5]'
							:key='index'
							class='gt-col__btn'
							:type='item.score===item_son?"primary":""'
							:disabled='item.score>0' 
							v-on:click='clickScore(item.i,item_son)'
							size='mini'
						>
							{{item_son}}
						</el-button>
					</Column>
				</div>
			</div>
		</div>

		<myVideo :open='videoOpen' @onClose='onVideoClose' :videoSrc="videoSrc" :videoTitle="videoTitle" />
	</div>

</template>

<script>
import {mapState} from 'vuex';
import myVideo from "../components/myVideo.vue";
import MyImgAid from "../components/MyImgAid.vue";

export default{
	name:'TheFangan',
	data:function(){
		return {
			videoSrc:"",
			videoOpen:false,
			videoTitle:"",
		}
	},
	computed:{
		...mapState({
			name:state=>state.fangan_obj.name,
			age:state=>state.fangan_obj.age,
			days:state=>state.fangan_obj.dates.length,
			rows:state=>state.fangan_obj.fangan,
			ui:state=>state.fangan_ui
		})
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
		clickScore:function(row,score){
			let url="./api/vueSubmitScore.aspx?row="+row+"&score="+score;
			url+="&u_i="+this.ui;

			this.$http
				.get(url)
				.then(ret=>{
					if(ret && ret.code===1){
						this.$store.commit("fangan_score_set",{
							row:row,
							score:ret.score
						});
					}
				})
				.catch(()=>{});
		},
		onVideoOpen:function(link,title){
			this.videoSrc=link;
			this.videoTitle=title;
			this.videoOpen=true;
		},
		onVideoClose:function(){
			this.videoOpen=false;
		},
	},
	components:{
		myVideo,
		MyImgAid
	}
}

</script>

<style lang='scss'>
.gt-col__btn:not(:first-child){
	margin-left:8px;
}
.gt-col__btn.el-button{
	padding:7px;
}
</style>
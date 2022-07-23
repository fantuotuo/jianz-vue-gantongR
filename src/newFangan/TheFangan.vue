<template>
	<div>
		<div class='layout'>
			<!-- <div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='姓名' />
					<Column :text='name' />
					<Column text='年龄' />
					<Column :text='age+""' size='1' />
					<Column text='已训练天数' size='3' />
					<Column :text='days+""' />
				</div>
			</div> -->
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='' />
					<Column text='训练目的' size='' />
					<Column text='训练教具' size='' />
					<Column text='训练时间' size='1' />
					<Column text='操作方法' size='2' />
					<Column text='操作评分' />
					<Column text='更换项目' size='1' />
				</div>
				<div 
					v-for='(item,itemIndex) in rows'
					:key='item.courseId'
					class='gt-row'
					v-bind:class='item.score>0?"disable":""'
				>
					<Column size='' style="justify-content:flex-start">
						<div>
							<i v-if='item.uploadUser!=""' class='el-icon-s-opportunity'></i>
							{{item.enName+" "+item.name}}
						</div>
					</Column>
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
					<Column text='' size='2' type='tip' style='flex-direction:column'>
						<el-button :disabled='!item.videoLink' size='mini' @click='onVideoOpen(item.videoLink,item.name)'>
							<i class="el-icon el-icon-film"></i> {{item.videoLink?"视频":"暂无视频"}}
						</el-button><br />

						<el-popover effect="dark" placement='top' width="600" trigger="click">
							<div>
								<p v-for='(tp,iTp) in item.tips' :key='iTp'>{{tp}}</p>
							</div>
							<el-button slot="reference" size='mini'><i class="el-icon el-icon-notebook-2">
								</i> 文字版
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
							v-on:click='clickScore(item,item_son)'
							size='mini'
						>
							{{item_son}}
						</el-button>
					</Column>
					<Column size='1' style='flex-direction:column'>
						<div>
							<el-button size='mini' class='gt-col__btn' @click='onTouchRowBottom(itemIndex)'><i class='el-icon-bottom'></i></el-button>
							<el-button size='mini' class='gt-col__btn' @click='onTouchRowTop(itemIndex)'><i class='el-icon-top'></i></el-button>
						</div><br/>
						<div>
							<el-button size='mini' class='gt-col__btn' @click='onTouchRowMinus(itemIndex)'><i class='el-icon-minus'></i></el-button>
							<el-button size='mini' class='gt-col__btn' @click='onTouchRowPlus(itemIndex)'><i class='el-icon-plus'></i></el-button>
						</div>
					</Column>
				</div>
			</div>
		</div>

		<myVideo :open='videoOpen' @onClose='onVideoClose' :videoSrc="videoSrc" :videoTitle="videoTitle" />
		<CustomChoose 
			:index='chooseIndex' 
			:visible='chooseOpen' 
			@on-click-close='chooseOpen = false'
			@on-click-choose='onTouchRowsPlusComplete'
		/>
	</div>

</template>

<script>
import {mapState} from 'vuex';
import myVideo from "../components/myVideo.vue";
import MyImgAid from "../components/MyImgAid.vue";
import CustomChoose from "../components/CustomChoose.vue";

export default{
	name:'TheFangan',
	data:function(){
		return {
			videoSrc:"",
			videoOpen:false,
			videoTitle:"",

			chooseIndex:0,
			chooseOpen:false,
		}
	},
	computed:{
		...mapState({
			name:state=>state.fangan_obj.name,
			age:state=>state.fangan_obj.age,
			days:state=>state.fangan_obj.dates.length,
			rows:state=>state.fangan_obj.fangan,
			ui:state=>state.fangan_obj.ui
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
		clickScore:function(item,score){
			var url = "api/vueSubmitScore.aspx";
			url+=`?courseId=${item.courseId}&score=${score}&u_i=${this.ui}`;

			this.$http
				.get(url)
				.then(ret=>{
					if(ret && ret.code===1){
						this.$store.commit("fangan_score_set",{
							courseId:item.courseId,
							// row:row,
							score:ret.score
						});
					}else{
						this.$message({
                            message:ret.msg,
                            type:"error"
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


		onTouchRowTop(index){
			if(index<=0) return;
			var fangan = this.rows.slice();

			var temp = fangan[index];
			fangan[index]=fangan[index-1];
			fangan[index-1] = temp;
			this.$store.commit("fangan_fangan_set",{fangan});
		},
		onTouchRowBottom(index){
			if(index>=this.rows.length-1) return;
			var fangan = this.rows.slice();

			var temp = fangan[index];
			fangan[index]=fangan[index+1];
			fangan[index+1] = temp;
			this.$store.commit("fangan_fangan_set",{fangan});
		},
		onTouchRowPlus(index){
			this.chooseIndex=index;
			this.chooseOpen=true;
		},
		onTouchRowMinus(index){
			this.$confirm("确认删除吗？","提示",{
				confirmButtonText: '确定',
				cancelButtonText: '取消',
				type: 'warning',
				confirmButtonClass:'el-button--danger'
			}).then(()=>{
				var fangan = this.rows.slice();
				fangan.splice(index,1);
				this.$store.commit("fangan_fangan_set",{fangan});
			}).catch(()=>{});
			index;
		},
		onTouchRowsPlusComplete(index,item){
			this.chooseOpen=false;
			
			var fangan = this.rows.slice();
			var itemNew={
				i:-1,
				courseId:item.courseId,
				name:item.name,
				goal:item.goal,
				tools:item.aid,
				duration:item.duration,
				tip:item.tip,
				tips:item.tips,
				score:-1,
				videoLink:item.videoLink,
				enName:item.enName,
				uploadUser:item.uploadUser,
			};
			fangan.splice(index,0,itemNew);
			this.$store.commit("fangan_fangan_set",{fangan});
		}
	},
	components:{
		myVideo,
		MyImgAid,
		CustomChoose,
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
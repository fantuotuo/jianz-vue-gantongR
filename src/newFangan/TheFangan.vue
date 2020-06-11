<template>
	<div>
		<div class='layout'>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='姓名' />
					<Column v-bind:text='name' />
					<Column text='年龄' />
					<Column v-bind:text='age+""' />
					<Column text='已训练天数' />
					<Column v-bind:text='days+""' />
				</div>
			</div>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='small' />
					<Column text='训练目的' size='small' />
					<Column text='训练教具' size='small' />
					<Column text='训练时间' size='small' />
					<Column text='操作方法' size='large' />
					<Column text='操作评分' />
				</div>
				<div 
					v-for='item in rows'
					v-bind:key='item.i'
					class='gt-row'
					v-bind:class='item.score>0?"disable":""'
				>
					<Column v-bind:text='item.name' size='small' />
					<Column v-bind:text='item.goal' size='small' />
					<Column text='' size='small' class='center'>
						<div>
							<div
								v-for='(item_son) in getImgs(item.tools)'
								v-bind:key='item_son.i'
							>
								<img 
									v-bind:src='"/gantong/public/images/"+item_son.v+".png"' 
									v-bind:alt='item_son.v' 
									v-bind:title='item_son.v' />
								<br />
								<span>{{item_son.v}}</span>
							</div>
						</div>
					</Column>
					<Column v-bind:text='item.duration' size='small' />
					<Column v-bind:text='item.tip' size='large' type='tip' />
					<Column text=''>
						<el-button 
							v-for='(item_son,index) in [1,2,3,4,5]'
							v-bind:key='index'
							class='gt-col__btn'
							v-bind:type='item.score===item_son?"primary":""'
							v-bind:disabled='item.score>0' 
							v-on:click='clickScore(item.i,item_son)'
							size='mini'
						>
							{{item_son}}
						</el-button>
					</Column>
				</div>
			</div>
		</div>
	</div>

</template>

<script>
import {mapState} from 'vuex';

export default{
	name:'TheFangan',
	computed:{
		...mapState({
			name:state=>state.fangan_name,
			age:state=>state.fangan_age,
			days:state=>state.fangan_dates.length,
			rows:state=>state.fangan_fangan,
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
		loadingSet:function(bool){
			this.$store.commit("loading_set",{
				loading:bool
			});
		}
	},

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
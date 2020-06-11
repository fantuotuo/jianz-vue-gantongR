<template>
	<div>
		<div class='layout'>
			<el-button-group>
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
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='double' />
					<Column text='得分' size='double' />
					<Column text='训练目的' size='double' />
				</div>
				<div 
					v-for='item in record'
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

export default{
	name:"TheRecord",
	computed:{
		...mapState({
			date:state=>state.fangan_date,
			dates:state=>state.fangan_dates,
			record:state=>state.fangan_record,
			recordfirst:state=>state.fangan_recordfirst,
			ui:state=>state.fangan_ui
		})
	},
	mounted:function(){
		if(this.recordfirst){
			this.dates.length>0 && this.clickDate(this.dates[0].date);
		}
	},
	methods:{
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
			this.$store.commit("loading_set",{
				loading:bool
			});
		}
	},

}

</script>

<style lang='scss'>


</style>
<template>
	<div class='gt-userblock'>
		<div>
			<div style='line-height:1.8;'>
				<span class='gt-userblock__name'>
					<i class='el-icon-user' :class='{female:sex!=="男"}'></i>
					{{name}}
				</span>
				<span class='gt-userblock__date'>生成日期：{{date}}</span>
			</div>
			<div>
				<el-button 
					size='small'
					@click='onClickBaoGao'
				>查看报告</el-button>
				<el-button 
					size='small'
					@click='onClickFangan'
					type='primary'
				>查看方案</el-button>
			</div>
		</div>
	</div>
</template>

<script>
export default{
	name:"DesktopUsersBlockNew",
	props:{
		name:String,
		date:String,
		uI:String,
		sex:String,
	},
	methods:{
		onClickBaoGao(){
			var query = {u_i:this.uI};
			this.$router.push({path:"/fangan",query});
		},
		onClickFangan(){
			var query = {u_i:this.uI,openFangan:"1"};
			this.$router.push({path:"/fangan",query});
		}
	}



}

</script>

<style lang='scss'>
.gt-userblock{
	width:60%;
	margin:0 auto;
	margin-bottom:15px;

	>div{
		line-height: 1;
		white-space: nowrap;
		// cursor: pointer;
		background: #FFFFFF;
		border: 1px solid #DCDFE6;
		border-color: #DCDFE6;
		color: #606266;
		-webkit-appearance: none;
		text-align: center;
		box-sizing: border-box;
		outline: none;
		transition: .1s;
		font-weight: 500;
		-moz-user-select: none;
		-webkit-user-select: none;
		-ms-user-select: none;
		padding: 12px 20px;
		font-size: 14px;
		border-radius: 4px;

		display:flex;
		justify-content:space-between;
	}
	>div{
		color: #909399;
		background: #f4f4f5;
		border-color: #d3d4d6;
	}
    


	>.el-button{
		width:100%;
		height:66px;
		line-height:1.3;
	}
	.gt-userblock__name{
		font-size:1.2rem;
	}
	.gt-userblock__date{
		font-size:0.9rem;
	}
	.el-icon-user{
		margin-right:5px;
		color:#45a9fc;
		font-weight: 600;
		&.female{
			color:#f86363;
		}
	}
}

</style>
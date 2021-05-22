<template>
	<div>
		<div class='layout gt-form'>
			<!-- <div class='alert alert-secondary'>
				请填写你的个人信息，备注项选填
			</div> -->
			<el-form label-width="80px" size='small'>
				<el-form-item label='姓名'>
					<el-input 
						type='text'
						v-model='name'
						maxlength='5'
						v-bind:show-word-limit='true'
					></el-input>
				</el-form-item>
				<el-form-item label='性别'>
					<el-radio-group v-model='sex'>
						<el-radio-button label='男'></el-radio-button>
						<el-radio-button label='女'></el-radio-button>
					</el-radio-group>
				</el-form-item>
				<el-form-item label='年龄'>
					<el-select v-model='age'>
						<el-option label='3岁及以下' value='3'></el-option>
						<el-option label='4岁' value='4'></el-option>
						<el-option label='5岁' value='5'></el-option>
						<el-option label='6岁' value='6'></el-option>
						<el-option label='7岁' value='7'></el-option>
						<el-option label='8岁' value='8'></el-option>
						<el-option label='9岁' value='9'></el-option>
						<el-option label='10岁' value='10'></el-option>
						<el-option label='11岁' value='11'></el-option>
						<el-option label='12岁及以上' value='12'></el-option>
					</el-select>
				</el-form-item>
				<el-form-item label='备注'>
					<el-input 
						type='textarea' 
						rows='3' 
						v-model='remark'
						maxlength='100'
						v-bind:show-word-limit='true'
					></el-input>
				</el-form-item>
				<el-form-item>
					<el-button 
						v-on:click='submit'
						type='primary'
					>
						提交
					</el-button>
				</el-form-item>
			</el-form>
			
		</div>
	</div>

</template>

<script>
export default{
	name:"TheForm",
	data:function(){
		return {
			name:"",
			sex:"男",
			age:"",
			remark:"",
		}
	},
	methods:{
		submit:function(){
			if(this.name==""){
				alert("姓名不能为空");
				return;
			}


			this.$emit('submit',{
				name:this.name,
				sex:this.sex,
				age:this.age,
				remark:this.remark
			});
		}
	}
}

</script>

<style lang='scss'>
.gt-form{
	width:600px!important;
}
.pad{
	padding-top: calc(.375rem + 1px);
}
.col-form-label{
	text-align:right;
}
</style>
<template>
	<div :class='$style.wrapper'>
		<div :class='$style.layout' class='layout'>
			<div class='alert alert-secondary'>
				请填写你的个人信息，备注项选填
			</div>
			<form>
				<div class='form-group row'>
					<label class='col-4 col-form-label' for='input-name'>姓名</label>
					<div class='col-3'>
						<input v-model='name' type='text' class='form-control' id='input-name' />
					</div>
				</div>
				<div class='form-group row'>
					<label class='col-4 col-form-label' for='input-sex'>性别</label>
					<div class='col-8' :class="$style.pad">
						<div class='form-check form-check-inline'>
							<input v-model='sex' type='radio' class='form-check-input' id='input-sex-0' value='男' />
							<label class='form-check-label' for='input-sex-0'>男</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='sex' type='radio' class='form-check-input' id='input-sex-1' value='女' />
							<label class='form-check-label' for='input-sex-1'>女</label>
						</div>
					</div>
					
				</div>
				<div class='form-group row'>
					<label class='col-4 col-form-label' for='input-age'>年龄</label>
					<div class='col-8' :class="$style.pad">
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-3' value='3' />
							<label class='form-check-label' for='input-age-3'>3岁及以下</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-4' value='4' />
							<label class='form-check-label' for='input-age-4'>4</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-5' value='5' />
							<label class='form-check-label' for='input-age-5'>5</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-6' value='6' />
							<label class='form-check-label' for='input-age-6'>6</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-7' value='7' />
							<label class='form-check-label' for='input-age-7'>7</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-8' value='8' />
							<label class='form-check-label' for='input-age-8'>8</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-9' value='9' />
							<label class='form-check-label' for='input-age-9'>9</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-10' value='10' />
							<label class='form-check-label' for='input-age-10'>10</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-11' value='11' />
							<label class='form-check-label' for='input-age-11'>11</label>
						</div>
						<div class='form-check form-check-inline'>
							<input v-model='age' type='radio' class='form-check-input' id='input-age-12' value='12' />
							<label class='form-check-label' for='input-age-12'>12岁及以上</label>
						</div>
					</div>
				</div>
				<div class='form-group row'>
					<label class='col-4 col-form-label' for='input-remark'>备注</label>
					<div class='col-3'>
						<textarea v-model='remark' class='form-control' id='input-remark' rows='3'></textarea>
					</div>
				</div>
				<div class='form-group row'>
					<div class='col-4'>
						
					</div>
					<div class='col-3'>
						<button type="button" class="btn btn-primary" @click='submit'>
							提交
						</button>
					</div>
				</div>
			</form>
		</div>
	</div>

</template>

<script>
export default{
	name:"TheForm",
	computed:{
		name:{
			get(){
				return this.$store.state.login_name;
			},
			set(value){
				this.$store.commit("login_name_set",{
					name:value
				});
			}
		},
		sex:{
			get(){
				return this.$store.state.login_sex;
			},
			set(value){
				this.$store.commit("login_sex_set",{
					sex:value
				});
			}
		},
		age:{
			get(){
				return this.$store.state.login_age;
			},
			set(value){
				this.$store.commit("login_age_set",{
					age:value
				});
			}
		},
		remark:{
			get(){
				return this.$store.state.login_remark;
			},
			set(value){
				this.$store.commit("login_remark_set",{
					remark:value
				});
			}
		}
	},
	methods:{
		submit:function(){
			if(this.name==""){
				alert("姓名不能为空");
				return;
			}
			this.$emit('submit');
		}
	}
}

</script>

<style module lang='scss'>
.wrapper{

}
.pad{
	padding-top: calc(.375rem + 1px);
}
</style>
<style>
.col-form-label{
	text-align:right;
}
/*input[type=radio]:not(:first-child){
	margin-left:10px;
}*/
</style>
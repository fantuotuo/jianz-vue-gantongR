<template>
	<div :class='$style.table'>
		<!-- head -->
		<div 
			:class='[$style.rowhead,$style.row]' 
			class='bg-secondary text-white'
			@click='closed=!closed'
			title='点击打开或关闭详细选项'
		>
			<div :class='$style.col1' class='clearFix'>
				<span>{{title}}</span>
				<!-- 计算总分 -->
				<div :class='$style.score' class='input-group'>
					<div class='input-group-prepend'>
						<span class='input-group-text'>模块得分</span>
					</div>
					<input 
						type='text' 
						class='form-control' 
						aria-label='score' 
						aria-describedby='score-addon'
						v-model='inputValue'
						:disabled='!manual' 
						@input='inputChange'
						@click.stop=''
					/>
					<div class='input-group-append'>
						<span class='input-group-text' id="score-addon">分</span>
					</div>
				</div>
			</div>
			<div :class='$style.col2' class='clearFix'>
				<div 
					v-for='item in labels'
					:key='item.i'
					:class='$style.label'
				>
					{{item.text}}
				</div>
			</div>
		</div>
		<!-- body -->
		<div 
			:class='$style.body'
			:style='{height:heightBody+"px"}'>
		<div 
			:class='$style.row'
			v-for='(item,index) in rows'
			:key='index'>
			<div :class='$style.col1' class='clearFix'>
				{{item.ques}}
			</div>
			<div :class='$style.col2' class='clearFix'>
				<div>
					<label
						v-for='(itemRadio,index) in radios'
						:key='index'
						:class='$style.label'>
						<input 
							v-model='item.score' 
							type='radio' 
							name='' 
							:value='itemRadio.score'
							@change='radioChange'/>
					</label>
				</div>
			</div>
		</div>
		</div>

	</div>

</template>

<script>
export default{
	name:"TheLiangbiaoTable",
	props:{
		manual:Boolean,
		title:String,
		scoreAll:Number,	// 这个只在初始创建的时候用到，使用data属性inputValue单向传值保持和父组件值一致
		rows:Array,
		i:Number,
	},
	created:function(){
		this.inputValue=this.scoreAll;
	},
	computed:{
		heightBody:function(){
			return this.closed?0:40*this.rows.length;
		}
	},
	methods:{
		inputChange:function(){
			this.inputValue=parseInt(this.inputValue);
			if(isNaN(this.inputValue)) this.inputValue=0;
			this.emitScore();
		},
		radioChange:function(){
			let sum=this.rows.reduce((a,b)=>a+b.score,0);
			this.inputValue=sum;
			this.emitScore();
		},
		emitScore:function(){
			this.$emit('scoreAllChange',this.inputValue,this.i);
		}
	},
	data:function(){
		return {
			labels:[
				{i:0,text:"没有"},
				{i:1,text:"很少"},
				{i:2,text:"偶尔"},
				{i:3,text:"经常"},
				{i:4,text:"总是"},
			],
			radios:[
				{i:0,score:1},
				{i:1,score:2},
				{i:2,score:3},
				{i:3,score:4},
				{i:4,score:5},
			],
			inputValue:-1,
			closed:false,
		}
	}
}

</script>

<style module lang='scss'>
$h-col1-head:47px;
$p-col1-head:5px;
$h-col1-line-head:$h-col1-head - 2*$p-col1-head - 1px;

$h-col1:40px;
$p-col1:5px;
$h-col1-line:$h-col1 - 2*$p-col1 - 1px;

$w-col1:800px;
$w-col2:1024px - 30px - $w-col1 - 1px;
$c-border:#cdcdcd;
.rowhead{
	// width:100%;
	border-top:1px solid $c-border;
}
.body{
	overflow: hidden;
	transition: all .3s ease;
}
.col1 .score{
	width:180px;
	float:right;
}
.label{
	width: 20px;
	margin-right: 8px;
	float:left;
	line-height:1.3;
	margin-bottom:0;
}
.rowhead{
	font-weight: 600;
	cursor: pointer;
	.col1{
		height:$h-col1-head;
		line-height: $h-col1-line-head;
	}
}
.row{
	white-space: nowrap;
	display: -ms-inline-flexbox;
	display: inline-flex;
	border-left:1px solid $c-border;
	&:not(.rowhead){
		&:hover{
			background:#eeeeee;
		}
		.label{
			padding-top:6px;
			input[type='radio']{
				zoom:1.4;
			}
		}
	}
}

.col1{
	width:$w-col1;
	height:$h-col1;
	overflow:hidden;
	line-height:$h-col1-line;
	padding:$p-col1;
	padding-left:10px;
	white-space: normal;
	word-break: break-word;

	border-right:1px solid $c-border;
	border-bottom:1px solid $c-border;

}
.col2{
	width:$w-col2;
	min-height:$h-col1;
	padding:$p-col1;
	padding-left:20px;
	white-space: normal;

	border-right:1px solid $c-border;
	border-bottom:1px solid $c-border;
	// display:flex;
	// justify-content:center;
	// align-items:center;
}

</style>
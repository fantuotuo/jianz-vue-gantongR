<template>
	<div>
		<div class='layout'>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='姓名' size='2' />
					<Column :text='name' size='2' />
					<Column text='年龄' size='2' />
					<Column :text='age+""' size='2' />
					<Column text='生成日期' size='2' />
					<Column :text='date' size='2' />
				</div>
			</div>
			<div>
				<div class='gt-row head bg-secondary text-white'>
					<Column text='模块名称' size='3' />
					<Column text='原始分' size='3' />
					<Column text='年龄T分' size='3' />
					<Column text='能力水平' size='3' />
				</div>
				<div
					v-for='(item) in rows'
					:key='item.i'
					class='gt-row'
				>
					<Column :text='names[item.i]' size='3' />
					<Column :text='item.score+""' size='3' />
					<Column :text='item.scoreT+""' size='3' />
					<Column :text='item.comment+""' size='3' type='comment' />
				</div>
			</div>

			<el-alert
				type='info'
				title='查阅T分表把原始分转换为T分(如下图)，再根据T分查看评估结果。'
				effect='dark'
				v-bind:closable='false'
				style='margin-top:20px'
			></el-alert>
			<div class='wj-demo clearfix'>
				<div class='wj-comment'><img src='../assets/bg_comment-3.png'><span>严重偏差</span></div>
				<div class='wj-comment'><img src='../assets/bg_comment-2.png'><span>中度偏差</span></div>
				<div class='wj-comment'><img src='../assets/bg_comment-1.png'><span>轻度偏差</span></div>
				<div class='wj-comment'><img src='../assets/bg_comment-0.png'><span>正常</span></div>
			</div>
			<!-- 查询T分对应表格 -->
			<el-table
				v-bind:data='getTable2'
				class='el-table-t'
				border>
				<el-table-column label='前庭平衡和大脑双侧分化'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c0'>
								<img v-bind:src='scope.row.src0' />
								<span>{{scope.row.p00}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c0'>
								<img v-bind:src='scope.row.src0' />
								<span>{{scope.row.p01}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
				<el-table-column label='执行功能与神经抑制功能'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c1'>
								<img v-bind:src='scope.row.src1' />
								<span>{{scope.row.p10}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c1'>
								<img v-bind:src='scope.row.src1' />
								<span>{{scope.row.p11}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
				<el-table-column label='触觉敏感性（触觉防御过多、不足）'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c2'>
								<img v-bind:src='scope.row.src2' />
								<span>{{scope.row.p20}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c2'>
								<img v-bind:src='scope.row.src2' />
								<span>{{scope.row.p21}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
				<el-table-column label='发展性协调运动功能（运用困难）'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c3'>
								<img v-bind:src='scope.row.src3' />
								<span>{{scope.row.p30}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c3'>
								<img v-bind:src='scope.row.src3' />
								<span>{{scope.row.p31}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
				<el-table-column label='视觉空间感知'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c4'>
								<img v-bind:src='scope.row.src4' />
								<span>{{scope.row.p40}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c4'>
								<img v-bind:src='scope.row.src4' />
								<span>{{scope.row.p41}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
				<el-table-column label='重力不安全感'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c5'>
								<img v-bind:src='scope.row.src5' />
								<span>{{scope.row.p50}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c5'>
								<img v-bind:src='scope.row.src5' />
								<span>{{scope.row.p51}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
				<el-table-column label='情绪管理与自我调节'>
					<el-table-column
						label='原始分'
						width='70'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c6'>
								<img v-bind:src='scope.row.src6' />
								<span>{{scope.row.p60}}</span>
							</div>
						</template>
					</el-table-column>
					<el-table-column
						label='年龄T分'
					>
						<template slot-scope='scope'>
							<div v-bind:class='scope.row.c6'>
								<img v-bind:src='scope.row.src6' />
								<span>{{scope.row.p61}}</span>
							</div>
						</template>
					</el-table-column>
				</el-table-column>
			</el-table>
		</div>
	</div>

</template>

<script>
import {mapState} from 'vuex';
import img0 from "../assets/bg_comment-0.png";
import img1 from "../assets/bg_comment-1.png";
import img2 from "../assets/bg_comment-2.png";
import img3 from "../assets/bg_comment-3.png";

export default{
	name:"TheBaogao",
	computed:{
		...mapState({
			name:state=>state.fangan_obj.name,
			age:state=>state.fangan_obj.age,
			rows:state=>state.fangan_obj.score,
			arrayAllAge:state=>state.fangan_obj.arrayAll,
			date:state=>state.fangan_obj.date
		}),
		getTable2(){
			let arrayAll=this.arrayAllAge;
			var table=this.rows.map(o=>{
				return {
					score:parseInt(o.score),
					scoreT:parseInt(o.scoreT),
				}
			});
			if(arrayAll.length<=0) return [];

			var borderIndexArr=[
				this.getBorderIndex(0,table),
				this.getBorderIndex(1,table),
				this.getBorderIndex(2,table),
				this.getBorderIndex(3,table),
				this.getBorderIndex(4,table),
				this.getBorderIndex(5,table),
				this.getBorderIndex(6,table),
			];
			let ret=arrayAll[0].map((arr0,index)=>{
				let obj0={
					p00:arr0[0],
					p01:arr0[1],
					c0:this.getComment(arr0[1])+
						(borderIndexArr[0]===index?" wj-border":""),
					src0:this.getImgSrc(arr0[1])
				};
				let obj1=arrayAll[1][index]?{
					p10:arrayAll[1][index][0],
					p11:arrayAll[1][index][1],
					c1:this.getComment(arrayAll[1][index][1])+
						(borderIndexArr[1]===index?" wj-border":""),
					src1:this.getImgSrc(arrayAll[1][index][1])
				}:{p10:" ",p11:" ",c1:this.getComment(0)};
				let obj2=arrayAll[2][index]?{
					p20:arrayAll[2][index][0],
					p21:arrayAll[2][index][1],
					c2:this.getComment(arrayAll[2][index][1])+
						(borderIndexArr[2]===index?" wj-border":""),
					src2:this.getImgSrc(arrayAll[2][index][1])
				}:{p20:" ",p21:" ",c2:this.getComment(0)};
				let obj3=arrayAll[3][index]?{
					p30:arrayAll[3][index][0],
					p31:arrayAll[3][index][1],
					c3:this.getComment(arrayAll[3][index][1])+
						(borderIndexArr[3]===index?" wj-border":""),
					src3:this.getImgSrc(arrayAll[3][index][1])
				}:{p30:" ",p31:" ",c3:this.getComment(0)};
				let obj4=arrayAll[4][index]?{
					p40:arrayAll[4][index][0],
					p41:arrayAll[4][index][1],
					c4:this.getComment(arrayAll[4][index][1])+
						(borderIndexArr[4]===index?" wj-border":""),
					src4:this.getImgSrc(arrayAll[4][index][1])
				}:{p40:" ",p41:" ",c4:this.getComment(0)};
				let obj5=arrayAll[5][index]?{
					p50:arrayAll[5][index][0],
					p51:arrayAll[5][index][1],
					c5:this.getComment(arrayAll[5][index][1])+
						(borderIndexArr[5]===index?" wj-border":""),
					src5:this.getImgSrc(arrayAll[5][index][1])
				}:{p50:" ",p51:" ",c5:this.getComment(0)};
				let obj6=arrayAll[6][index]?{
					p60:arrayAll[6][index][0],
					p61:arrayAll[6][index][1],
					c6:this.getComment(arrayAll[6][index][1])+
						(borderIndexArr[6]===index?" wj-border":""),
					src6:this.getImgSrc(arrayAll[6][index][1])
				}:{p60:" ",p61:" ",c6:this.getComment(0)};

				return{
					...obj0,
					...obj1,
					...obj2,
					...obj3,
					...obj4,
					...obj5,
					...obj6
				}
			});


			return ret;
		}
	},
	props:{a:String},
	data:function(){
		return {
			names:[
				"前庭平衡和大脑双侧分化",
				"执行功能与神经抑制功能",
				"触觉敏感性（触觉防御过多、不足）",
				"发展性协调运动功能（运用困难）",
				"视觉空间感知",
				"重力不安症",
				"情绪管理与自我调节"
			]
		};
	},
	methods:{
		getBorderIndex:function(index,table){
			var score=table[index]?table[index].score:-1,
				scoreT=table[index]?table[index].scoreT:-1;
			var arrGroup=this.arrayAllAge[index];
			if(!arrGroup) return -1;
			var indexArr=[];
			arrGroup.forEach((arr,i)=>{
				if(arr[1]===scoreT){
					indexArr.push(i);
				}
			});
			var ret=-1;
			for(var i=0;i<indexArr.length;i++){
				var index2=indexArr[i];
				// 对应于getScoreT的算法
				// 需要和getScoreT内容保持同步
				// 有可能score比arrGroup[0][0]还小，所以加index2===0的判断
				if(index2===0 || score>=arrGroup[index2][0]){
					// 循环后ret取到了尽可能大的那个
					ret=index2;
				}
			}
			return ret;
		},
		getImgSrc:function(score){
			if(score>=50){
				return img0;
			}else if(score>=40){
				return img1;
			}else if(score>=30){
				return img2;
			}else{
				return img3;
			}
		},
		getComment:function(){
			return "wj-comment";
		},
	},

	components:{
		// Column,
	}

}

</script>

<style lang='scss'>
.el-table.el-table-t{
	td{
		padding:0;
	}
	td:first-child .cell{
		padding:0;
	}
	.cell{
		padding:0;
		>div{
			padding:2px;
		}
	}
}
$c:#888;
.wj-demo{
	&>div{
		// box-sizing: border-box;
		float:right;
		width:90px;
		text-align: center;
		padding:5px;
		border-top:1px solid $c;
		border-bottom:1px solid $c;
		border-right:1px solid $c;
		&:last-child{
			border-left:1px solid $c;
		}
	}
}


.wj-comment{
	position:relative;
	img{
		position:absolute;
		left:0;
		top:0;
		width:100%;
		height:100%;
		z-index:0;
	}
	span{
		display:block;
		box-sizing:border-box;
		height:18px;
		line-height:18px;
		position:relative;
		z-index:1;
	}
	&.wj-border span{
		border:2px solid red;
	}
}


</style>
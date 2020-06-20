<template>
	<div>
		<div class='layout'>
			<!-- <div class='alert alert-secondary'>
				请根据小孩实际情况填写量表，也可以直接切换到手动输入直接录入分数
			</div> -->
			<div class='gt-btns'>
				<transition 
					name='transition-btn-toggle'
					mode='out-in'
				>
					<el-button 
						v-if='manual'
						v-on:click='manual=false' 
						size='small'
					>
						切换到逐题输入
					</el-button>
					<el-button 
						v-else 
						v-on:click='manual=true'
						size='small'
					>
						切换到手动输入
					</el-button>
				</transition>
			</div>
			<!-- 量表table -->
			<div>
				<TheLiangbiaoTable 
					v-for='(item,index) in tbs'
					v-bind:key='item.head.name'
					v-bind:i='index'
					v-bind:title='item.head.title'
					v-bind:score-all='item.head.score'
					v-bind:rows='item.rows'
					v-bind:manual='manual'
					v-on:scoreAllChange='scoreAllChange'
				/>
			</div>
			<div class='gt-btns'>
				<el-button 
					type='primary'
					size='small'
					v-on:click='submitLiangbiao'
				>
					提交量表
				</el-button>
			</div>
		</div>
	</div>

</template>

<script>
import TheLiangbiaoTable from './TheLiangbiaoTable.vue';

export default{
	name:"TheLiangbiao",
	data:function(){
		return {
			manual:false,
			tbs:[
				{
					head:{name:"1",title:"一、前庭平衡和大脑双侧分化",score:0},
					rows:[
						{ques:"1、儿童特别爱玩旋转圆凳,玩公园中旋转地球或飞转设施,不觉晕。",name:"",score:0},
						{ques:"2、儿童看来正常、健康，有正常智慧，但学习阅读或做算术特难。",name:"",score:0},
						{ques:"3、在眼看得见情况下，屡碰撞桌椅、杯子或旁人，方向和距离感差。",name:"",score:0},
						{ques:"4、手舞足蹈，吃饭、写字、打鼓时双手或双脚配合不良，常忘另一边。",name:"",score:0},
						{ques:"5、表面上左撇子，左右手都用，或尚未固定偏好使用哪一只手。  ",name:"",score:0},
						{ques:"6、大动作笨拙，容易跌倒，并不会用手支撑保护自己；拉他时显得笨重。",name:"",score:0},
						{ques:"7、语音不清晰，组合句子或编组故事困难。",name:"",score:0},
						{ques:"8、看书眼睛会累，却可以长时间看电视。 ",name:"",score:0},
						{ques:"9、俯卧地板床上时，无法把头、颈、胸、手脚举高离地（如飞机状）。",name:"",score:0},
						{ques:"10、喜欢听故事，不喜欢看书，听的容易记住，看的却容易忘记。",name:"",score:0},
						{ques:"11、走路跑跳常碰撞东西，不善与同伴投球和传球，排队和游戏有困难。",name:"",score:0},
					],
					head_close:false,
				},{
					head:{name:"2",title:"二、执行功能与神经抑制功能",score:0},
					rows:[
						{ques:"12、注意力分散，不专心，小动作多，或上课左顾右盼。",name:"",score:0},
						{ques:"13、偏食或挑食：不吃水果、软皮的食物、肉类、蛋类，只吃白饭、奶等。",name:"",score:0},
						{ques:"14、害羞，见到陌生人赶紧躲避或紧张捻衣角，皱眉头，口吃说不出话。",name:"",score:0},
						{ques:"15、看电视电影，很容易激动；高兴时又叫又跳，恐怖片不敢看。",name:"",score:0},
						{ques:"16、严重怕黑，到暗处要有人陪，晚上拒绝出去，不喜欢到空屋子去。",name:"",score:0},
						{ques:"17、换床睡不着，换枕头或被子睡不好，出外总是对睡眠空间担心。",name:"",score:0},
						{ques:"18、别人为他用棉棒清洁鼻子和耳朵时，他往往觉得不舒服。",name:"",score:0},
						{ques:"19、喜欢往亲人的身上挨靠或搂抱，象被宠坏或被溺爱的孩子。",name:"",score:0},
						{ques:"20、睡觉时总爱触摸被角，抱棉被，衣物或玩具，否则会出现不安或睡不好。",name:"",score:0},
					],
					head_close:false,
				},{
					head:{name:"3",title:"三、触觉敏感性（触觉防御过多、不足）",score:0},
					rows:[
						{ques:"21、脾气不好，对亲人特别暴躁，常常为琐事无故发脾气，遇事会强词夺理。",name:"",score:0},
						{ques:"22、到新的场合或人多的地方不久，就要求离开或自己跑掉。",name:"",score:0},
						{ques:"23、轻微病后多次向人表示不喜欢去幼儿园；没原因或为小事对幼儿园产生恐惧。",name:"",score:0},
						{ques:"24、常吮舔手指头或咬指甲，不喜欢别人帮剪指甲。",name:"",score:0},
						{ques:"25、不喜欢脸被别人碰和帮他洗脸。洗头或理发为最痛苦的事。",name:"",score:0},
						{ques:"26、成人帮他拉袖口和袜子，或协助穿衣服而碰他皮肤时会引起他的反感。",name:"",score:0},
						{ques:"27、游戏中或玩玩具时，担心别人从后面逼近，为此而引起苦恼。",name:"",score:0},
						{ques:"28、到处碰、触摸不停，但又避免触碰毛毯和编织玩具的表面。",name:"",score:0},
						{ques:"29、常常喜欢穿宽松的长袖衣衫，不冷也常喜欢穿毛线衫或夹克。",name:"",score:0},
						{ques:"30、爱谈天或做无接触的交往，但很不愿意跟朋友搭肩或做肌肤接触。",name:"",score:0},
						{ques:"31、对某些布料很敏感，不喜欢那类布料所做的衣服。",name:"",score:0},
						{ques:"32、对自己的事物很敏感，很容易动情，计划或结果改变时不能容忍。",name:"",score:0},
						{ques:"33、对无所谓的瘀伤，小肿块，小刀伤等，总觉得很痛而诉怨不止。",name:"",score:0},
						{ques:"34、顽固偏执不合作，一直坚持依自己的方式办事，对事没有灵活性。",name:"",score:0},
					],
					head_close:false,
				},{
					head:{name:"4",title:"四、发展性协调运动功能（运用困难）",score:0},
					rows:[
						{ques:"35、四岁尚不会洗手，上厕所不会自行擦屁股。",name:"",score:0},
						{ques:"36、四岁尚不会使用筷子，或一直用汤勺吃饭，不会拿笔。",name:"",score:0},
						{ques:"37、五岁不会玩骑上、爬下或钻进去等的大玩具。",name:"",score:0},
						{ques:"38、六岁不会站起来用脚荡秋千，不会攀绳网或爬竹竿。",name:"",score:0},
						{ques:"39、穿脱袜子、衣服、扣纽扣、系鞋带等动作，向来非常慢，或做不来。",name:"",score:0},
						{ques:"40、入学后尚不会自己洗澡；单脚跳，跳绳等都做不好也学不好。",name:"",score:0},
						{ques:"41、入学后对拿笔写字，剪贴作业，着色等做得不好或非常慢。",name:"",score:0},
						{ques:"42、饭桌上经常弄得很脏，成人要求他收拾好书桌或玩具很困难。",name:"",score:0},
						{ques:"43、做手工、做家务事很笨拙，使用工具抓握动作很不顺手。",name:"",score:0},
						{ques:"44、动作懒散，行动迟缓不积极；做事非常没效率。",name:"",score:0},
						{ques:"45、常惹事，如弄翻碗盘，弄洒牛奶，从车上跌落等，需家长特别保护。",name:"",score:0},
					],
					head_close:false,
				},{
					head:{name:"5",title:"五、视觉空间感知",score:0},
					rows:[
						{ques:"46、在年幼时，玩积木总比别人差。",name:"",score:0},
						{ques:"47、外出或远行时常达不到目的地，很容易迷失，不喜欢到陌生的地方。",name:"",score:0},
						{ques:"48、蜡笔着色和铅笔写字都不好，比别人慢，常超出轮廓或方格之外。",name:"",score:0},
						{ques:"49、拼图总比别人差；对模型或图样的异同辨别常有困难。",name:"",score:0},
						{ques:"50、混淆背景中的特定图形，不易看出或认出。",name:"",score:0},
					],
					head_close:false,
				},{
					head:{name:"6",title:"六、重力不安症",score:0},
					rows:[
						{ques:"51、内向，不喜出去玩，朋友少，沉默寡言，喜欢独处或帮家里做事。",name:"",score:0},
						{ques:"52、上下阶梯或过马路多迟疑；登高会觉得头重脚轻不敢向别处看或走动。",name:"",score:0},
						{ques:"53、被抱起举高时，很焦虑的要把脚着地，经可信赖人的帮助会安心配合。",name:"",score:0},
						{ques:"54、避免从高处跳到低处，对高地或有跌落危险时，表现非常害怕。",name:"",score:0},
						{ques:"55、不喜欢把头脚倒置：如避免翻筋斗，打滚，或参加室内打斗游戏活动。",name:"",score:0},
						{ques:"56、对游乐设施不感兴趣，不喜欢移动性玩具。",name:"",score:0},
						{ques:"57、对不寻常移动（如上下车，前座移到后座，走不平地面）动作缓慢。",name:"",score:0},
						{ques:"58、上下楼梯很慢，紧紧的抓住栏杆；双手可抓紧的简单攀登，都尽量避免。",name:"",score:0},
						{ques:"59、旋转时，很容易感到失去平衡。车行进中，转弯太快也会吓坏自己。",name:"",score:0},
						{ques:"60、不喜欢在凸起的地面上走，总会抱怨或心中感到太高。",name:"",score:0},
					],
					head_close:false,
				},{
					head:{name:"7",title:"七、情绪管理与自我调节",score:0},
					rows:[
						{ques:"61、情绪起伏不定，瞬间变好很大",name:"",score:0},
						{ques:"62、因为小事而发脾气",name:"",score:0},
						{ques:"63、每次发脾气后，持续时间较长，难以平复情绪",name:"",score:0},
						{ques:"64、容易紧张，因小事而紧张",name:"",score:0},
						{ques:"65、自控能力差，容易冲动",name:"",score:0},
						{ques:"66、平时兴奋度低，无精打采，但遇到喜欢的人、活动或者事物时，却有强烈的反应",name:"",score:0},
						{ques:"67、在家中表现自如，但在不熟悉环境或陌生人面前却表现得十分紧张或者退缩",name:"",score:0},
						{ques:"68、只顾满足自己的愿望，不理会环境要求，常常忘记原本要做的事情。",name:"",score:0},
						{ques:"69、玩游戏或看电视时，在没有预告的情况下，需要转移下一个活动时，会发脾气或者哭闹。",name:"",score:0},
						{ques:"70、做作业或活动时被打扰后难以重新投入之前进行的活动。",name:"",score:0},
						{ques:"71、在新活动或者面对挑战时，会躲起来或者把脸转向一边等退缩行为，不愿意参与新活动或者尝试。",name:"",score:0},
						{ques:"72、面对新环境或者挑战时，会变得紧张，动作僵硬或脸红冒汗。",name:"",score:0},
					],
					head_close:false,
				}
			],
		}
	},
	methods:{
		scoreAllChange:function(score,i){
			this.tbs[i].head.score=score;
		},
		submitLiangbiao:function(){
			let tbs=this.tbs;

			let ok=true;
			let str="";
			tbs.map((tb,k)=>{
				tb.rows.map((row)=>{
					if(!(row.score>0)){
						ok=false;
					}
					return null;
				});

				str+=k>0?"b":"";
				str+=isNaN(tb.head.score)?0:tb.head.score;
				return null;
			});

			console.log(str)
			if(!ok && !this.manual){
				alert("请完整填写问卷！");
				return;
			}

			this.$store.commit("login_str_set",{
				str:str
			});
			this.$emit('submit');
		}
	},
	computed:{

	},
	components:{
		TheLiangbiaoTable,
	}
}

</script>

<style lang='scss'>
.gt-btns{
	width:100%;
	text-align:center;
	overflow:hidden;
}
.transition-btn-toggle-enter-active,.transition-btn-toggle-leave-active{
	transition: all .3s ease;
}
.transition-btn-toggle-enter,.transition-btn-toggle-leave-to{
	transform: translateX(500px);
	opacity:0;
}
</style>
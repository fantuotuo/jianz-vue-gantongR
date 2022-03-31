<template>
    <div>
        <div class="layout">
            <div>
				<div class='container-tags row'>
					<div class='col-1' style="font-size:0.9rem;line-height:32px;">模块分类</div>
					<div class='col-11'>
						<!-- <el-button-group> -->
							<el-button 
								size='small'
								:type='""===currentTag1?"primary":""' 
								@click='currentTag1=""'
							>全部</el-button>
							<el-button 
								v-for='tag in tags1' 
								:key='tag.id' 
								size='small'
								:type='tag.id===currentTag1?"primary":""' 
								@click='currentTag1=tag.id'
							>{{tag.cnName}}</el-button>
						<!-- </el-button-group> -->
					</div>
				</div>
				<div class='container-tags row'>
					<div class="col-1" style="font-size:0.9rem;line-height:32px;">工具分类</div>
					<div class='col-11'>
						<!-- <el-button-group> -->
							<el-button 
								size='small'
								:type='""===currentTag2?"primary":""' 
								@click='currentTag2=""'
							>全部</el-button>
							<el-button 
								v-for='tag in tags2' 
								:key='tag.id' 
								size='small'
								:type='tag.id===currentTag2?"primary":""' 
								@click='currentTag2=tag.id'
							>{{tag.cnName}}</el-button>
						<!-- </el-button-group> -->
					</div>
				</div>

				<div class='container-tags row form-group'>
					<label for="keySearch" class="col-1 col-form-label">搜索</label>

					<div class='input-group col-3'>
						<input 
							v-model="currentKey" 
							@keydown.prevent.enter='onTouchSearch' 
							type="text" 
							class="form-control" 
							id="keySearch" 
							placeholder="请输入搜索词" 
							aria-label="关键词" 
							aria-describedby="button-addon2"
						>
						<div class="input-group-append">
							<button 
								@click='onTouchSearchClear' 
								class="btn btn-outline-primary" 
								type="button" 
								id="button-addon2"
							>清空</button>
						</div>
					</div>
				</div>
            </div>

				<div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='2' />
					<Column text='训练目的' size='2' />
					<Column text='训练教具' size='2' />
					<Column text='训练时间' size='2' />
					<Column text='操作方法' size='4' />
				</div>
				<div 
					v-for='(item,i) in getListCurrent'
					:key='item.enName+" "+item.name+i'
					class='gt-row'
				>
					<Column v-bind:text='item.enName+" "+item.name' size='2' style="justify-content:flex-start" />
					<Column v-bind:text='item.goal' size='2' />
					<Column text='' size='2' class='center'>
						<div style='width:100%'>
							<MyImgAid 
								v-for='item_son in getImgs(item.aid)'
								:key='item_son.i'
								:cnName='item_son.v'
							/>
						</div>
					</Column>
					<Column v-bind:text='item.duration+"分钟"' size='2' />
					<!-- <Column v-bind:text='item.tip' size='large' type='tip' /> -->
					<Column text='' size='4' type='tip'>
						<el-button :disabled='!item.videoLink' size='mini' @click='onVideoOpen(item.videoLink,item.enName+" "+item.name)'>
							<i class="el-icon el-icon-film"></i> {{item.videoLink?"查看视频":"暂无视频"}}
						</el-button>

						<el-popover effect="dark" placement='top' width="600" trigger="click">
							<div>
								<p v-for='(tp,iTp) in item.tips' :key='iTp'>{{tp}}</p>
							</div>
							<el-button slot="reference" size='mini'>
								<i class="el-icon el-icon-notebook-2"></i> 查看操作方法
							</el-button>
						</el-popover>
					</Column>
				</div>
		</div>

        <myVideo :open='videoOpen' @onClose='onVideoClose' :videoSrc="videoSrc" :videoTitle="videoTitle" />
    </div>
</template>

<script>
import myVideo from "../components/myVideo.vue";
import MyImgAid from "../components/MyImgAid.vue";
const TAGS1 = [
    new Tag("1", "前庭平衡"),
    new Tag("2", "重力安适"),
    new Tag("3", "触觉调和"),
    new Tag("4", "协调运动"),
    new Tag("5", "综合训练"),
],
    TAGS2 = [
        new Tag("O", "O课堂说明"),
		new Tag("A", "A开场游戏"),
		new Tag("B", "B无教具&被动类"),
		new Tag("C", "C钻 & 滚"),
		new Tag("D", "D爬行类"),
		new Tag("E", "E跳跃类"),
		new Tag("F", "F走 & 跑"),
		new Tag("G", "G大龙球类"),
		new Tag("H", "H花生球 & 羊角球"),
		new Tag("J", "J平衡台项目"),
		new Tag("K", "K独脚凳项目"),
		new Tag("L", "L大陀螺项目"),
		new Tag("M", "M大A字架项目"),
		new Tag("P", "P滑板车项目"),
		new Tag("Q", "Q蹦床项目"),
		new Tag("R", "R彩虹桥"),
		new Tag("S", "S¼圈项目"),
		new Tag("T", "T万象组合项目"),
		new Tag("U", "U小球类"),
		new Tag("V", "V小教具 & 组合操作"),
		new Tag("W", "W攀爬架/综合游戏设计"),
    ];

export default {
    name:"GaiLan",

    data:function(){
        return {
            list:[],
			tags1:TAGS1,
            tags2:TAGS2,
			currentKey:"",
			currentTag1: "",
            currentTag2: "",

            videoSrc:"",
			videoOpen:false,
			videoTitle:"",
        }
    },
	computed:{
		getListCurrent:function(){
			var currentTag1 = this.currentTag1,
                currentTag2 = this.currentTag2,
				currentKey = this.currentKey;
			return this.list.filter(element=>{
				var match = true;
				if (currentTag1 !== "" && match) {
					match = element.tags.filter(function (tag) { return tag.id === currentTag1 }).length > 0;
				}
				if (currentTag2 !== "" && match) {
					match = element.tags.filter(function (tag) { return tag.id === currentTag2 }).length > 0;
				}
				if(currentKey !=="" && match){
					match = element.enName.match(new RegExp(currentKey,"i")) || 
						element.name.match(new RegExp(currentKey,"i")) ||
						element.aid.match(new RegExp(currentKey,"i"));
					match = Boolean(match);
				}
				return match;
			});
		}
	},
    mounted:function(){
        this.fetchList();
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
        onVideoOpen:function(link,title){
			this.videoSrc=link;
			this.videoTitle=title;
			this.videoOpen=true;
		},
		onVideoClose:function(){
			this.videoOpen=false;
		},


        fetchList:function(){
            let url="./api/gailan/getCourses.aspx";
			this.$http.get(url)
				.then(ret=>{
					if(!ret.code){
						alert("发生错误！");
						this.$router.push({
							path:"/"
						});
						return;
					}
                    ret.list.forEach(element => {
						element.tip = element.tip.replace(/([\d]+、)/g,"<br>$1");
						element.tip = element.tip.replace(/([；。])([\d]+)/g,"$1<br>$2");
						element.tip = element.tip.replace(/[<br>]+/g,"\n");
						element.tips = element.tip.split("\n").filter(s=>s);
						
						element.tags = [getTag1(element.mId), getTag2(element.enName)];
					});
					ret.list.sort((a, b) => {
						return a.enName > b.enName ? 1 : -1;
					});

                    this.list = ret.list;
				})
				.catch(()=>{
					this.$router.push({
						path:"/"
					})
				});
        },


		onTouchSearch:function(){
			
        },
        onTouchSearchClear:function(){
            this.currentKey="";
        }
    },
    components:{
        myVideo,
		MyImgAid
    }





}





function getTag1(mId){
	mId = mId + "";
	var tag = null;
	TAGS1.forEach(ta => {
		if(ta.id === mId){
			tag = ta;
		}
	});
	if(!tag) tag = TAGS1[0];
	return tag;
}
function getTag2(enName){
	var tag = null;
	TAGS2.forEach(ta => {
		if(enName.match(new RegExp("^" + ta.id))){
			tag = ta;
		}
	});
	if(!tag) tag = TAGS2[TAGS2.length - 1];
	return tag;
}

/**
 * Tag标签对象构造类
 * @param id 标签id
 * @param cnName 标签中文名
*/
function Tag(id, cnName) {
    this.id = id;
    this.cnName = cnName;
}
</script>

<style lang="scss">
.container-tags{
	margin-bottom:15px;

	.el-button +.el-button{
		margin-left:0;
	}
	.el-button:not(:last-child){
		margin-right:10px;
		margin-bottom:5px;
	}
}


</style>
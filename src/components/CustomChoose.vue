<template>
    <div class='custom-choose-wrapper'>
        <el-dialog :visible='visible' title='配置个性化项目' @close='onClickClose' width='1024px'>
            <div>
				<el-alert
					title='为学员配置个性化项目，点击项目后面的【选择】按钮可将项目添加到训练方案'
					type="info"
					style='margin-bottom:15px'
					:closable='false'>
				</el-alert>

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

                <div class='gt-row head bg-secondary text-white'>
					<Column text='项目名称' size='2' />
					<Column text='训练目的' size='2' />
					<Column text='训练教具' size='2' />
					<Column text='训练时间' size='2' />
					<Column text='操作方法' size='3' />
                    <Column text='选择' size='1'/>
				</div>
                <div 
					v-for='(item) in getListCurrent'
					:key='item.courseId'
					class='gt-row'
				>
					<Column size='2' style="justify-content:flex-start">
						<div>
							<i v-if='item.uploadUser!=""' class='el-icon-s-opportunity'></i>
							{{item.enName+" "+item.name}}
						</div>
					</Column>
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
					<Column text='' size='3' type='tip'>
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
                    <Column size='1'>
                        <el-button @click='onClickChoose(item)' size='mini' type='primary'>选择</el-button>
                    </Column>
				</div>

            </div>


            <div slot='footer' class='dialog-footer'>
                <el-button size='small' @click='onClickClose'>取消</el-button>
                <!-- <el-button size='small' type='primary' @click='onClickSubmit'>提交</el-button> -->
            </div>

            <myVideo :open='videoOpen' @onClose='onVideoClose' :videoSrc="videoSrc" :videoTitle="videoTitle" />
        </el-dialog>
    </div>
</template>




<script>
import {mapState} from "vuex";
import {TAGS1,TAGS2,getCoursesAll} from "../plugins/utils.js";
import myVideo from "./myVideo.vue";
import MyImgAid from "./MyImgAid.vue";

export default {
    name:"CustomChoose",

    data:function(){
        return {
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
    props:{
        visible:Boolean,
        index:Number,
    },
    computed:{
        ...mapState({
            list:state=>state.courses_all
        }),
        getListCurrent:function(){
			var currentTag1 = this.currentTag1,
                currentTag2 = this.currentTag2,
				currentKey = this.currentKey;
			return this.list.filter(element=>{
				var match = true;
				if (currentTag1 !== "" && match) {
					// 模块分类
					match = element.tags.filter(function (tag) { 
							return tag.id === currentTag1 
						}).length > 0;
				}
				if (currentTag2 !== "" && match) {
					// 工具分类
					match = element.tags.filter(function (tag) { 
							return tag.id === currentTag2 
						}).length > 0;
				}
				if(currentKey !=="" && match){
					// 搜索词
					match = element.enName.match(new RegExp(currentKey,"i")) || 
						element.name.match(new RegExp(currentKey,"i")) ||
						element.aid.match(new RegExp(currentKey,"i"));
					match = Boolean(match);
				}
				return match;
			});
		},
    },
    mounted:function(){
        if(!this.list || this.list.length<=0) this.fetchList();
    },


    methods:{
        fetchList:function(){
            getCoursesAll().then(list=>{
                this.$store.commit("courses_all_set",{courses_all:list});
            }).catch(error=>{
                alert(error);
            });
        },

        onClickClose:function(){
            this.$emit("on-click-close");
        },
        onClickChoose:function(item){
            this.$emit("on-click-choose",this.index,item);
        },


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
        onTouchSearch:function(){
			
        },
        onTouchSearchClear:function(){
            this.currentKey="";
        }
    },
    components:{
        myVideo,
        MyImgAid,
    }

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
.custom-choose-wrapper{
    .gt-row{
        cursor:pointer;
    }
}

</style>
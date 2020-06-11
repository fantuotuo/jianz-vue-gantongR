<template>
	<div 
		v-bind:class='["gt-wrapper--video",src==""?"hide":""]'
	>
		<div 
			class='gt-wrapper--video__bg' 
			v-on:click='closeVideo' 
			title='关闭视频'
		>
		</div>
		<div
			class='gt-wrapper--video__player' 
			v-bind:style='{
				width:playerWidth+"px",
				transform:"translateX("+(-playerWidth/2)+"px)"
			}'
		>
			<!-- <video crossorigin='anonymous' id='video' class='video-js vjs-default-skin vjs-fluid video'></video> -->
			<video id='video' class='video-js vjs-default-skin vjs-fluid video'></video>
		</div>
		<div class='gt-wrapper--video__title'>
			{{title}}

			<button 
				type='button' 
				class='gt-wrapper--video__title__close close' 
				v-on:click='closeVideo'
				title='关闭视频'
				aria-label='关闭'
			>
				<span aria-hidden='true'>&times;</span>
			</button>
		</div>
	</div>
</template>

<script>
import {mapState} from "vuex";
import videojs from "video.js";
import "video.js/dist/video-js.css";

export default{
	name:"TheVideo",
	computed:{
		...mapState({
			src:state=>state.fangan_videosrc,
			title:state=>state.fangan_videotitle
		}),
		playerWidth:function(){
			return Math.min(this.screenHeight/0.5625,this.screenWidth);
		}
	},
	data:function(){
		return {
			player:null,
			screenHeight:window?window.innerHeight:0,
			screenWidth:window?window.innerWidth:0,
		}
	},
	methods:{
		closeVideo:function(){
			this.$store.commit("fangan_videosrc_set",{
				videosrc:""
			});
		},
		changeVideoSrc(){
			if(!this.player) return;
			if(this.src===""){
				this.player.pause();
				return;
			}
			this.player.src(this.src);
			this.player.load();
			this.player.play();
		}
	},
	watch:{
		src:function(){
			this.changeVideoSrc();
		}
	},
	mounted:function(){
		window.onresize=()=>{
			this.screenHeight=window.innerHeight;
			this.screenWidth=window.innerWidth;
		}

		this.player = videojs("video");
		this.player.controls(true);
		this.changeVideoSrc();
	}
}

</script>

<style lang='scss'>
.gt-wrapper--video{
	position:fixed;
    top:0;
    left:0;
    width:100%;
    height:100%;
    z-index:10001;
}
.gt-wrapper--video__bg{
	background:rgba(20,20,20,0.5);
	position:absolute;
	top:0;
	left:0;
	width:100%;
	height:100%;
}
.gt-wrapper--video__player{
	width:1170px;
	margin:0 auto;
	position:absolute;
	bottom:0;
	left:50%;
}
$w-x:40px;
$pos-x:5px;
$h-title:$w-x + 2*$pos-x;
.gt-wrapper--video__title{
	font-size:2rem;
	// background:rgba(20,20,20,0.3);
	background:rgba(230,230,230,0.5);
	padding:0 10px;
	// color:#fff;
	height:$h-title;
	line-height:$h-title;

	position:absolute;
	top:0;
	left:0;
	width:100%;
}
.gt-wrapper--video__title__close.close{
	font-size:2.7rem;
	text-shadow:none;
}
</style>
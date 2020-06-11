<template>
	<div :class='[$style.wrapper,src==""?"hide":""]'>
		<div :class='$style.bg' @click='closeVideo' title='关闭视频'>
		</div>
		<div :class='$style.player' :style='{width:playerWidth+"px",transform:"translateX("+(-playerWidth/2)+"px)"}'>
			<!-- <video crossorigin='anonymous' id='video' class='video-js vjs-default-skin vjs-fluid video'></video> -->
			<video id='video' class='video-js vjs-default-skin vjs-fluid video'></video>
		</div>
		<div :class='$style.title'>
			{{title}}

			<button 
				type='button' 
				:class='$style.close' 
				class='close' 
				@click='closeVideo'
				title='关闭视频'
				aria-label='关闭'>
				<span aria-hidden='true'>&times;</span>
			</button>
		</div>
	</div>
</template>

<script>
import videojs from "video.js";
import "video.js/dist/video-js.css";

export default{
	name:"TheVideo",
	props:{
		src:String,
		title:{
			type:String,
			default:"title"
		},
	},
	computed:{
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
			this.$emit("close");
		},
		changeVideoSrc(){
			if(!this.player) return;
			if(this.src==""){
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

<style module lang='scss'>
.wrapper{
	position:fixed;
    top:0;
    left:0;
    width:100%;
    height:100%;
    z-index:10001;
}
.bg{
	background:rgba(20,20,20,0.5);
	position:absolute;
	top:0;
	left:0;
	width:100%;
	height:100%;
}
.player{
	width:1170px;
	margin:0 auto;
	position:absolute;
	bottom:0;
	left:50%;
}
$w-x:40px;
$pos-x:5px;
$h-title:$w-x + 2*$pos-x;
.title{
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
.close{
	font-size:2.7rem;
	text-shadow:none;
}
</style>
<template>
    <div class="wrapper" v-show='open'>
        <div class="bg" @click='onClose' title="关闭视频">

        </div>
        <!-- 中间容器 -->
        <div class="content-container">
            <!-- 标题与关闭按钮 -->
            <div class="close-container">
                <div class="title-container">
                    【{{videoTitle}}】操作演示视频
                </div>

                <!-- <el-button type='text' size='small' icon="el-icon-close" @click='onClose'>
                </el-button> -->
                <el-link icon='el-icon-close' type='danger' :underline="false" @click='onClose'></el-link>
            </div>
            <!-- 主视频容器 -->
            <div class="video-container" v-show='isVideoValid'>
                <div id='myVideoDiv' ref='myVideoDiv'>
                    
                </div>
                <!-- <video-player  
                    class="video-player vjs-custom-skin"
                    ref="videoPlayer"
                    :playsinline="true"
                    :options="playerOptions"
                ></video-player> -->
            </div>
            <div v-show='!isVideoValid' class="empty">
                暂无操作视频，您可以查看文字版操作方法
            </div>
        </div>
    </div>
</template>

<script>

export default {
    name:"myVideo",
    props:{
        videoSrc:String,
        videoTitle:String,
        open:Boolean,
    },
    updated:function(){
        this.isVideoValid = !!this.videoSrc;
        if(this.open){
            this.changeVideo(this.videoSrc,true);
        }else{
            this.myVideo.pause();
        }
    },
    mounted:function(){
        this.initVideo();
        document.querySelector("video").oncontextmenu=function(){ return false; }
    },
    destroyed:function(){
        this.myVideo && this.myVideo.dispose();
    },
    data:function(){
        return {
            isVideoValid:false,
            myVideo:null,
        }
    },
    methods:{
        /**
         * 使用mp4格式的视频时，在手机端播放器会被拦截替换，同时有下载按钮
         * 使用m3u8格式则不会
        */
        initVideo:function(){
            var myVideoDiv = this.$refs["myVideoDiv"];
            myVideoDiv.innerHTML=`
                    <video 
                        id='myVideo' 
                        style='width:100%;height:400px;' 
                        class="video-js vjs-default-skin vjs-big-play-centered" 
                        controls 
                        preload="auto" 
                        data-setup='{}'
                    ></video>
            `;
            var options={
                bigPlayButton: true,
                textTrackDisplay: false,
                posterImage: false,
                errorDisplay: false,
                playbackRates: [0.5, 1, 1.5, 2],
                controlBar: { //底部的控制栏组件
                    currentTimeDisplay: true, //当前播放时间
                    timeDivider: true, //当前播放时间与总时间的斜杆分隔符
                    durationDisplay: true, //是否显示总时间
                    remainingTimeDisplay: false, //是否显示剩下的时间，该选项与是否显示总时间二选一
                    // pictureInPictureToggle:false
                }
            };
            this.myVideo = this.$video("myVideo", options);
        },
        changeVideo:function(src,play){
            this.myVideo.src(src);
            this.myVideo.load();
            play && this.myVideo.play();
        },
        onClose(){
            this.myVideo && this.myVideo.pause();
            this.$emit("onClose");
        },
    },
}
</script>

<style lang="scss" scoped>
.wrapper{
    z-index:100;
    position:fixed;
    top:0;
    left:0;
    right:0;
    bottom:0;
    display:flex;
    justify-content: center;
    align-items:center;
    .bg{
        background:rgba(0,0,0,0.6);
        backdrop-filter: blur(4px);
        position:absolute;
        top:0;
        left:0;
        right:0;
        bottom:0;
        z-index:0;
    }
    .content-container{
        display:flex;
        justify-content: center;
        align-items:center;
        flex-direction: column;
        width:90%;
        position:relative;
        z-index:1;
        box-shadow: 0px 2px 6px #0000008a;
        >*{
            width:100%;
        }
    }
}
.empty{
    background:#222;
    height:300px;
    border-radius:4px;
    border-top-left-radius: 0;
    border-top-right-radius: 0;
    color:#fff;
    display:flex;
    justify-content: center;
    align-items:center;
}
.close-container{
    font-weight:bold;

    position:relative;
    height:50px;
    background:#222;
    color:#fff;
    display: flex;
    align-items: center;
    justify-content: space-between;

    .title-container{
        border-top-left-radius: 3px;
        border-top-right-radius: 3px;
        font-size:18px;
    }
    button,.el-link{
        // position:absolute;
        // top:0;
        // right:0;
        margin-right:8px;
        padding:3px 3px;
        font-size:2.8rem;
        text-decoration: none;
    }

}
</style>
<style lang="scss">
body .video-js{  
    // & .vjs-time-control,& .vjs-current-time,& .vjs-duration{
    //     display:block;
    // }
    // & .vjs-remaining-time{
    //     display: none;
    // }
    .vjs-picture-in-picture-control{
        display: none;
    }
}
.vjs-fullscreen .vjs-tech{
	width:100%!important;
	height:100%!important;
}
.video-container .video-js{
    // background-color:transparent!important;
}
@media (max-width:1024px){
    .video-container .vjs-custom-skin > .video-js .vjs-control-bar{
        font-size:10px;
    }
}
</style>
<template>
    <div>
        <div class='bg' :class='{"hide":imgStatus===1}'>

		</div>
        <div :class='{"hide":imgStatus===2}'>
            <img 
                @load='imgLoad'
                @error='imgLoadError'
                :src='"/gt/public/images/"+cnName+".png"' 
                :alt='cnName' 
                :title='cnName' />
        </div>
        <span>{{cnName}}</span>
    </div>
</template>

<script>
export default {
    name:"MyImgAid",
    props:{
        cnName:String,
    },
    data:function(){
        return {
            imgStatus:0
        }
    },
    watch:{
        src:{
            immediate:true,
            handler(){
                this.imgStatus=0;
            }
        }
    },
    methods:{
        imgLoad(){
            this.imgStatus=1;
        },
        imgLoadError(){
            this.imgStatus=2;
        }
    }
}
</script>

<style lang="scss" scoped>
.bg{
	height:90px * 154/200;
	background-image:url("../assets/baby-mobile.png");
	background-size:100% 100%;
	background-repeat: no-repeat;
	// background:red;
}

</style>
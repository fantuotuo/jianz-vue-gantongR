<template>
	<div :class='$style.wrapper'>
		<DesktopUsers @loading='loadingSet' />
		<TheLoading :show='loading' />
	</div>
</template>

<script>
import {mapState} from "vuex";
import TheLoading from "../components/TheLoading.vue";
import DesktopUsers from "./DesktopUsers.vue";

export default{
	name:"Desktop",
	computed:{
		...mapState({
			loading:state=>state.desktop_loading,
		}),
	},
	methods:{
		loadingSet:function(bool){
			this.$store.commit("desktop_loading_set",{
				loading:bool
			});
		}
	},
	components:{
		TheLoading,
		DesktopUsers,
	}

}

</script>

<style module lang='scss'>
.wrapper{
	
}
</style>
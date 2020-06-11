<template>
	<div :class="[$style.wrapper,show?'':'hide']">
		<div :class="$style.bg">
		</div>
		<div :class="$style.txt">
			<div class="rounded-lg">
				loading...
			</div>
		</div>
	</div>
</template>

<script>
export default{
	name:"TheLoading",
	props:{
		show:Boolean
	}

}

</script>

<style module lang="scss">
.wrapper{
	position:fixed;
	top:0;
	left:0;
	width:100%;
	height:100%;
	z-index:10000;
	>div{
		position:absolute;
		top:0;
		left:0;
		width:100%;
		height:100%;
	}
	.bg{
		background:rgba(20,20,20,0.5);
	}
	$s-txt:60px;
	.txt{
		display:flex;
		justify-content:center;
		align-items:center;

		>div{
			width:$s-txt;
			height:$s-txt;
			line-height:$s-txt;
			background:rgba(20,20,20,0.9);
			color:#fff;
			text-align:center;
		}
	}
}
</style>
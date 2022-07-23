<template>
    <div>
        
        <el-dialog
            title="创建自定义项目"
            :visible="visible"
            @open='onOpen'
            @close='onClickClose'
        >
            <el-alert
                :title='alertTitle'
                type="info"
                style='margin-bottom:15px'
                :closable='false'>
            </el-alert>
            <el-form :model="form" :rules='rules' label-width="80px" size='small' ref='addForm'>
                <el-form-item label="项目名称" prop="c_name">
                    <el-input v-model="form.c_name" maxlength="20" show-word-limit></el-input>
                </el-form-item>
                <el-form-item label="模块分类" prop="c_groupName">
                    <el-select v-model='form.c_groupName'>
						<el-option label='前庭平衡' value='ZA'></el-option>
						<el-option label='重力安适' value='ZB'></el-option>
						<el-option label='触觉调和' value='ZC'></el-option>
						<el-option label='协调运动' value='ZD'></el-option>
                        <el-option label='综合训练' value='ZE'></el-option>
					</el-select>
                </el-form-item>
                <el-form-item label="训练时间" prop="c_duration">
                    <el-input-number v-model="form.c_duration" :min="1" :max="100" label=""></el-input-number>
                </el-form-item>
                <el-form-item label="操作方法" prop="c_tip">
                    <el-input v-model="form.c_tip" maxlength="200" show-word-limit></el-input>
                </el-form-item>
            </el-form>

            <div slot="footer" class="dialog-footer">
                <el-button @click="onClickClose" size='small'>取消</el-button>
                <el-button type="primary" @click="onClickSubmit" size='small'>提交</el-button>
            </div>
        </el-dialog>

    </div>
</template>


<script>
export default {
    name:"CustomCourse",

    props:{
        visible:Boolean,
        customNum:{
            type:Number,
            default:0
        }
    },
    data:function(){
        return {
            form:{
                c_name:"",
                c_groupName:"ZA",
                // c_aid:"",
                c_duration:"5",
                c_tip:"",       // 操作方法
                // c_remark:"",
            },
            rules:{
                c_name:[
                    {required:true,message:"请输入项目名称"}
                ]
            }
        }
    },
    computed:{
        alertTitle:function(){
            var max = 50;
            var left = max - this.customNum;
            return `每个账户最多可以创建${max}个自定义项目，已创建${this.customNum}个，还可创建${left}个。`;
        }
    },


    methods:{
        onOpen(){
            
        },
        onClickClose(){
            this.$emit("on-click-close");
        },
        onClickSubmit(){
            this.$refs["addForm"].validate(valid=>{
                if(!valid){
                    alert("请填写完整！");
                    return;
                }
                this.submitForm();
            });
        },
        submitForm(){
            var url = "api/custom/createCourse.aspx";
            this.$http.post(url,{
                c_name:this.form.c_name,
                c_groupName:this.form.c_groupName,
                c_duration:this.form.c_duration,
                c_tip:this.form.c_tip
            })
                .then(ret=>{
                    if(ret.code>0){
                        this.$message({
                            message:ret.msg,
                            type:"success"
                        });
                        // 数据初始化，防止创建重复的项目名称造成混淆
                        this.form.c_name="";
                        this.$nextTick(()=>{
                            // 清除表单的校验结果，防止一打开就校验
                            this.$refs["addForm"].clearValidate();
                        });

                        this.$emit("on-submit-done");
                    }else{
                        this.$message({
                            message:ret.msg,
                            type:"error"
                        });
                    }
                })
                .catch(()=>{});
        }

    }

}
</script>


<style lang="scss">


</style>
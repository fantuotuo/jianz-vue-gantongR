import 'es6-promise/auto'
import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex);

const store=new Vuex.Store({
	// getters:{
	// 	desktop_users_groups:state=>{
	// 		let users=state.desktop_users;

	// 		let groups=[];
	// 		users.map(user=>{
	// 			let month=user.date.replace(/\-\d*$/g,"");

	// 			let find=false;
	// 			groups.map(group=>{
	// 				if(group.month===month){
	// 					find=true;
	// 					group.users.push(user);
	// 					console.log(month);
	// 				}
	// 			})
	// 		})

	// 	}
	// },
	state:{
		desktop_zhanghao:"",
		desktop_users:[
			// {i:0,u_i:0,name:"name",date:"2020-05-05"}
		],
		desktop_liangbiao_times:-1,

		login_name:"",
		login_sex:"",
		login_age:"",
		login_remark:"",
		login_str:"",

		// u_i:window.initData.u_i,
		fangan_ui:-1,
		// video_src:"http://jianz.com/upload/jianzhi_720.mp4",
		// http://jianz.com/zysx/static/videos/0/index.m3u8
		fangan_videosrc:"",
		fangan_videotitle:"title",

		fangan_name:"",
		fangan_age:3,
		fangan_score:[
			// {i:0,score:"20",comment:"1"}
		],

		fangan_fangan:[
			// {i:0,name:"BB02 前滚翻",goal:"综合训练",tools:"tools",duration:"10",tip:"dasd<br>asddas",score:-1}
		],

		fangan_dates:[
			// {i:0,date:"2020-03-03"},
			// {i:1,date:"2020-03-05"},
		],
		fangan_date:"",
		fangan_record:[
			// {i:0,name:"C02 万象组合：过山洞",score:3,goal:"goal",time:""}
		],
		fangan_recordfirst:true,


		loading:false,
	},
	mutations:{
		desktop_user_load(state,payload){
			state.desktop_zhanghao=payload.user;
			state.desktop_users=payload.users;
			state.desktop_liangbiao_times=payload.liangbiao_times;
		},

		login_init_set(state,payload){
			state.login_name=payload.name;
			state.login_sex=payload.sex;
			state.login_age=payload.age;
			state.login_remark=payload.remark;
			state.login_str=payload.str;
		},
		login_name_set(state,payload){
			state.login_name=payload.name;
		},
		login_sex_set(state,payload){
			state.login_sex=payload.sex;
		},
		login_age_set(state,payload){
			state.login_age=payload.age;
		},
		login_remark_set(state,payload){
			state.login_remark=payload.remark;
		},
		login_str_set(state,payload){
			state.login_str=payload.str;
		},

		fangan_init_set(state,payload){
			state.fangan_ui=payload.ui;
			state.fangan_recordfirst=payload.recordfirst;
		},
		fangan_videosrc_set(state,payload){
			state.fangan_videosrc=payload.videosrc;
		},
		fangan_score_set(state,payload){
			state.fangan_fangan[payload.row].score=payload.score;
		},
		fangan_date_set(state,payload){
			state.fangan_date=payload.date;
			state.fangan_record=payload.record;
			state.fangan_recordfirst=payload.recordfirst;
		},
		fangan_fangan_set(state,payload){
			state.fangan_name=payload.name;
			state.fangan_age=payload.age;
			state.fangan_score=payload.score;
			state.fangan_fangan=payload.fangan;
			state.fangan_dates=payload.dates;
		},

		loading_set(state,payload){
			state.loading=payload.loading;
		}
	}
});

export default store;
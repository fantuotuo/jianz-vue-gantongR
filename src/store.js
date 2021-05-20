import 'es6-promise/auto'
import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex);

const store = new Vuex.Store({
	state:{
		user_valid:{
			fetched:false,
			valid:false,
		},

		desktop_obj: {
			user: "",
			users: [
				// {i:0,u_i:0,name:"name",date:"2020-05-05"}
			],
			liangbiao_times: -1,
		},


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


		fangan_obj: {
			name: "",
			age: 3,
			score: [],
			fangan: [],
			dates:[]
		},


		loading:false,
	},
	mutations:{
		user_valid_set(state,payload){
			state.user_valid.fetched=payload.fetched;
			state.user_valid.valid=payload.valid;
		},

		desktop_obj_set(state,payload) {
			state.desktop_obj.user = payload.user;
			state.desktop_obj.users = payload.users;
			state.desktop_obj.liangbiao_times = payload.liangbiao_times;
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
		fangan_score_set(state, payload) {
			var i = payload.row,
				item;
			for (var k = 0; k < state.fangan_fangan.length; k++) {
				if (state.fangan_fangan[k].i === i) {
					item = state.fangan_fangan[k];
					break;
				}
			}
			if (item) {
				item.score = payload.score;
			}
		},
		fangan_obj_set(state, payload) {
			state.fangan_obj.name = payload.name;
			state.fangan_obj.age = payload.age;
			state.fangan_obj.score = payload.score;
			state.fangan_obj.fangan = payload.fangan;
			state.fangan_obj.dates = payload.dates;
		},

		loading_set(state,payload){
			state.loading=payload.loading;
		}
	}
});

export default store;
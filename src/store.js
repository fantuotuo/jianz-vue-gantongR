import 'es6-promise/auto'
import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex);
import http from "./http.js";


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


		// u_i:window.initData.u_i,
		// fangan_ui:-1,
		// video_src:"http://jianz.com/upload/jianzhi_720.mp4",
		// http://jianz.com/zysx/static/videos/0/index.m3u8


		fangan_obj: {
			ui: -1,
			name: "",
			age: 3,
			score: [],
			fangan: [],
			dates: [],
			date: "",
			arrayAll: [],
		},



		// 所有课程
		courses_all: [],


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

		



		// 2022.05.13 提交分数逻辑改变，提交的是courseId
		fangan_score_set(state, payload) { 
			var score = payload.score,
				courseId = payload.courseId;
			var fangan = state.fangan_obj.fangan;
			fangan.forEach(item => {
				if (item.courseId === courseId) {
					item.score = score;
				}
			});
		},
		fangan_fangan_set(state, payload) { 
			var fangan = payload.fangan;
			state.fangan_obj.fangan = fangan;
		},
		fangan_init_set(state, payload) {
			state.fangan_obj.ui = payload.ui;
		},
		// fangan_score_set(state, payload) {
		// 	var i = payload.row,
		// 		item;
		// 	var fangan = state.fangan_obj.fangan;
		// 	for (var k = 0; k < fangan.length; k++) {
		// 		if (fangan[k].i === i) {
		// 			item = fangan[k];
		// 			break;
		// 		}
		// 	}
		// 	if (item) {
		// 		item.score = payload.score;
		// 	}
		// },
		fangan_obj_set(state, payload) {
			state.fangan_obj.name = payload.name;
			state.fangan_obj.age = payload.age;
			state.fangan_obj.score = payload.score;
			state.fangan_obj.fangan = payload.fangan;
			state.fangan_obj.dates = payload.dates;
			state.fangan_obj.arrayAll = payload.arrayAll;
			state.fangan_obj.date = payload.date;		// 注册时间
		},
		fangan_dates_set(state, payload) { 
			// 更新方案用户的成绩记录【天数】
			state.fangan_obj.dates = payload.dates;
		},




		courses_all_set(state, payload) { 
			state.courses_all = payload.courses_all;
		},

		

		loading_set(state,payload){
			state.loading = payload.loading;
		}
	},


	actions: {
		// 获取当前某个用户的训练记录dates
		fangan_dates_fatch: function (context, payload) { 
			return new Promise((resolve, reject) => {
				http.get("./api/vueGetRecordDates.aspx?u_i=" + payload.ui)
					.then(ret => {
						if (ret && ret.code == 1) {
							context.commit("fangan_dates_set", { dates: ret.dates });
							resolve(ret.dates);
						} else {
							reject("请求错误");
						}
					})
					.catch(error => reject(error));
			});
		}
	}
});

export default store;
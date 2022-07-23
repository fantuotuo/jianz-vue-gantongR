import axios from 'axios';
import store from './store.js';

const loadingSet=function(bool){
	store.commit("loading_set",{loading:bool});
}
const http={
	get: function (url, data) {
		data = data || {};
		data.tstamp = new Date().getTime();		// 防止get请求缓存

		return new Promise((resolve,reject)=>{
			loadingSet(true);
			axios.get(url,{params:data})
				.then(response=>{
					let ret=response.data;
					if(ret.unLogin===true){
						alert("登陆超时，请重新登陆");
						location.href="/gt/";
						reject("登陆超时，请重新登陆");
					}else{
						resolve(ret);
					}
					loadingSet(false);
				})
				.catch(error=>{
					alert("发送请求过程错误 - "+error);
					reject(error);
					loadingSet(false);
				});
		});
	},
	post:function(url,data){
		// 序列化参数
		var params;
		if (window.URLSearchParams) { 
			params = new window.URLSearchParams();
			for(let key in data){
				params.append(key,data[key]);
			}
		} else {
			// ie兼容
			var ret = '';
			for (var it in data) {
				// 如果要发送中文 编码
				ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
			}
			ret = ret.substring(0, ret.length - 1);
			params = ret;
		}
		// let params = new URLSearchParams();
		// for(let key in data){
		// 	params.append(key,data[key]);
		// }
		return new Promise((resolve,reject)=>{
			loadingSet(true);
			axios({
				url:url,
				method:"post",
				data:params,
				headers:{
					"content-Type":"application/x-www-form-urlencoded"
				}
			})
				.then(response=>{
					let ret=response.data;
					if(ret.unLogin===true){
						alert("登陆超时，请重新登陆");
						location.href="/gt/";
						reject("登陆超时，请重新登陆");
					}else{
						resolve(ret);
					}
					loadingSet(false);
				})
				.catch(error=>{
					alert("发送请求过程错误 - "+error);
					reject(error);
					loadingSet(false);
				})
		});
	}
}

export default http;
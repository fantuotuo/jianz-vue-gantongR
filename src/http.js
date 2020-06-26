import axios from 'axios';
import store from './store.js';

const loadingSet=function(bool){
	store.commit("loading_set",{loading:bool});
}
const http={
	get:function(url,data){
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
		let params=new URLSearchParams();
		for(let key in data){
			params.append(key,data[key]);
		}
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
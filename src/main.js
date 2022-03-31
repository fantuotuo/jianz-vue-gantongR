import Vue from 'vue'
import http from './http.js'
Vue.prototype.$http=http;
Vue.config.productionTip = false;
// Vue.use(VueAxios,axios);			// 安装axios
import store from './store.js';		// vuex
import router from './router.js';	// 路由
import './plugins/element.js';		// 引入element-ui
import MyComponents from './components/MyComponents.js';
Vue.use(MyComponents);				// 引入自定义组件

// videojs相关处理
import Video from 'video.js';
import 'video.js/dist/video-js.css';
Vue.prototype.$video = Video;


import App from './App.vue'

new Vue({
  render: h => h(App),
  store,
  router,
}).$mount('#app');

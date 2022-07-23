module.exports={
	publicPath:process.env.NODE_ENV === 'production'? './': '/',
	assetsDir:"static_vue",

	devServer: {
        port: 9080, // 端口
        https: false, // 启用https
        proxy: {
          '/api': {
            target: 'http://localhost:80/gt',
            changeOrigin: true,
            secure: false,
            // pathRewrite: {
            //   '^/api': '/api'
            // }
          },
          "/m": {
            target: 'http://localhost:80',
            changeOrigin: true,
            secure:false
          },
          "/images": {
            target: 'http://localhost:80',
            changeOrigin: true,
            secure:false
          }
        }
    }
}
const { createProxyMiddleware } = require('http-proxy-middleware')

module.exports = function(app) {
  app.use('/localBitcoins/*', 
    createProxyMiddleware({
      target: 'https://localbitcoins.com',
      changeOrigin: true,
      pathRewrite: {
          '^/localBitcoins/': ''
      }
    })
  );
};

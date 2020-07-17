# AcFun OBS 直播推流地址获取

## 目前已知流程
1. `https://id.app.acfun.cn/rest/app/token/get` 获取token，包括`acfun.midground.api_st`和`ssecurity`
2. 构造[StartPushRequest](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunOBS/live.proto)
3. 用`ssecurity`生成HMAC-SHA256签名，内容为[StartPushRequest](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunOBS/live.proto)和其他query参数
4. `https://api.kuaishouzt.com/rest/zt/live/startPush`，MultipartFormData内容包括`videoPushReq`，`caption`，`bizCustomData`和`cover`
5. 因为没有直播权限，到此为止
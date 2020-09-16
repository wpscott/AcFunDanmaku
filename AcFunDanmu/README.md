# AcfunDanmu AcFun直播弹幕工具

Source: [7.js](https://cdnfile.aixifan.com/static/js/7.05b952e7.js)

*[Im.proto](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDanmu/protos/Im.proto)为主站websocket，主要负责私信、推送之类的。*

## AcFun直播websocket数据结构

| 起始位置，偏移量  |  结构 |  说明 |
|---|---|---|
|  0, 12 |  ABCD 0001 FFFF FFFF FFFF FFFF |  ABCD 0001为Magic Number， 第一组FFFF FFFF为头数据长度，第二组FFFF FFFF为具体数据长度（AES IV长度（通常为16） + AES加密后的数据长度） |
|  12, 头数据长度 | [PacketHeader.proto](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDanmu/protos/PacketHeader.proto) |  具体数据结构请查看[PacketHeader.proto](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDanmu/protos/PacketHeader.proto) |
|  12 + 头数据长度, 16 |  FFFF FFFF FFFF FFFF FFFF FFFF FFFF FFFF |  4组int32作为AES IV，加解密用 |
|  28 + 头数据长度, 具体数据长度 - 16 | AES加密的[UpstreamPayload.proto](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDanmu/protos/UpstreamPayload.proto)或[DownstreamPayload.proto](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDanmu/protos/DownstreamPayload.proto) | 密钥为SecurityKey或SessionKey（由[PacketHeader.proto](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDanmu/protos/PacketHeader.proto)中的encryptionMode指定）。根据command选择对应的protobuf进行进一步的payload解析 |

## AcFun直播websocket流程
### 前置流程
 1. 请求`https://live.acfun.cn`获取`_did`Cookies
 2. 获取`userId`、`acSecurity`和`acfun.api.visitor_st`
    * 未登录/匿名用户发送AJAX
    POST请求`https://id.app.acfun.cn/rest/app/visitor/login`，表单数据为`sid=acfun.api.visitor`
    * 已登录用户发送AJAX
    POST请求`https://id.app.acfun.cn/rest/web/token/get`，表单数据为`sid=acfun.midground.api`
3. 获取`availableTickets`、`liveId`和`enterRoomAttach`

    发送AJAX POST请求`https://api.kuaishouzt.com/rest/zt/live/web/startPlay?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId=[userId]&did=[_did]&acfun.api.visitor_st=[acfun.api.visitor_st/acfun.midground.api_st]`，表单数据为`authorId=[主播Id]`，
 * 获取礼物列表
 
    发送AJAX POST请求`https://api.kuaishouzt.com/rest/zt/live/web/gift/list?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId=[userId]&did=[_did]&acfun.midground.api_st=[acfun.api.visitor_st/acfun.midground.api_st]`，表单数据为`visitorId=[userId]&liveId=[liveId]`
 * 获取在线观众
 
    发送AJAX POST请求`https://api.kuaishouzt.com/rest/zt/live/web/watchingList?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId=[userId]&did=[_did]&acfun.midground.api_st=[acfun.api.visitor_st/acfun.midground.api_st]`，表单数据为`visitorId=[userId]&liveId=[liveId]`
### 正式流程
1. 建立websocket链接`wss://link.xiatou.com/`
2. 发送RegisterRequest（SeqId加1），`encryptionMode`为`KEncryptionServiceToken`，加密密钥为`acSecurity`
3. 接收RegisterResponse，获取`instanceId`和`sessKey`。后续`encryptionMode`为`KEncryptionSessionKey`，加密密钥为`sessKey`
4. 发送KeepAliveRequest（SeqId加1）
5. 发送zt.live.interactive.ZtLiveCsCmd，payload为ZtLiveEnterRoom（SeqId加1）
6. 接收zt.live.interactive.ZtLiveCsCmd，payload为ZtLiveEnterRoomAck，根据`HeartbeatIntervalMs`，创建zt.live.interactive.ZtLiveCsHeartbeat定时器
7. 发送zt.live.interactive.ZtLiveCsHeartbeat（SeqId加1）及KeepAliveRequest，接收zt.live.interactive.ZtLiveCsHeartbeatAck和KeepAliveResponse
8. 发送/接收弹幕及礼物，具体请查看zt.live.interactive.proto。一般只需处理Push.ZtLiveInteractive.Message中的ZtLiveScActionSignal和ZtLiveScStateSignal
9. 接收Push.ZtLiveInteractive.Message，发送空的Push.ZtLiveInteractive.Message（发送的PacketHeader的SeqId为接收到的PacketHeader的SeqId）
10. 接收Push.ZtLiveInteractive.Message，MessageType为ZtLiveScStatusChanged，如果`Type`为LiveClosed则直播结束，发送UnregisterRequest，退出直播

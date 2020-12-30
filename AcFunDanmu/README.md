

# AcfunDanmu AcFun直播弹幕工具

Source: [9.js](https://ali-imgs.acfun.cn/kos/nlav10360/static/js/9.bba02d82.js)

*[Im.proto](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Im.proto)为主站websocket，主要负责私信、推送之类的。*

## AcFun直播websocket数据结构

| 起始位置，偏移量  |  结构 |  说明 |
|---|---|---|
|  0, 12 |  ABCD 0001 FFFF FFFF FFFF FFFF |  ABCD 0001为Magic Number， 第一组FFFF FFFF为头数据长度，第二组FFFF FFFF为AES IV长度（通常为16） + AES加密后的数据长度 |
|  12, 头数据长度 | [PacketHeader](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/PacketHeader.proto) |  具体数据结构请查看[PacketHeader](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/PacketHeader.proto) |
|  12 + 头数据长度, 16 |  FFFF FFFF FFFF FFFF FFFF FFFF FFFF FFFF |  AES IV，加解密用 |
|  28 + 头数据长度, 具体数据长度 - 16 | AES加密的[UpstreamPayload](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/UpstreamPayload.proto)或[DownstreamPayload](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/DownstreamPayload.proto) | 密钥为SecurityKey或SessionKey（由[PacketHeader](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/PacketHeader.proto)中的`encryptionMode`指定） |

## AcFun直播websocket流程
<details>
  <summary><b>前置流程</b></summary>

 1. 请求`https://live.acfun.cn`获取`_did`Cookies
 2. 获取`userId`、`acSecurity`和`acfun.api.visitor_st`
    * 未登录/匿名用户发送
    POST application/x-www-form-urlencoded请求`https://id.app.acfun.cn/rest/app/visitor/login`，表单数据为`sid=acfun.api.visitor`
    * 已登录用户发送
    POST application/x-www-form-urlencoded请求`https://id.app.acfun.cn/rest/web/token/get`，表单数据为`sid=acfun.midground.api`
3. 获取`availableTickets`、`liveId`和`enterRoomAttach`

    发送POST application/x-www-form-urlencoded请求`https://api.kuaishouzt.com/rest/zt/live/web/startPlay?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId=[userId]&did=[_did]&acfun.api.visitor_st=[acfun.api.visitor_st/acfun.midground.api_st]`，表单数据为`authorId=[主播Id]`
    
<details>
  <summary>其他请求</summary>
 
  * 获取礼物列表
  
    发送POST application/x-www-form-urlencoded请求`https://api.kuaishouzt.com/rest/zt/live/web/gift/list?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId=[userId]&did=[_did]&acfun.midground.api_st=[acfun.api.visitor_st/acfun.midground.api_st]`，表单数据为`visitorId=[userId]&liveId=[liveId]`
  * 获取在线观众
  
    发送POST application/x-www-form-urlencoded请求`https://api.kuaishouzt.com/rest/zt/live/web/watchingList?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId=[userId]&did=[_did]&acfun.midground.api_st=[acfun.api.visitor_st/acfun.midground.api_st]`，表单数据为`visitorId=[userId]&liveId=[liveId]`

  </details>
</details>

### 正式流程
1. 建立websocket链接`wss://link.xiatou.com/`
2. 发送[RegisterRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Register.proto#L13)（SeqId加1），`encryptionMode`为`KEncryptionServiceToken`，加密密钥为`acSecurity`
3. 接收[DownStreamPayload](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/DownstreamPayload.proto#L5)，根据[Command](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/DownstreamPayload.proto#L6)进行对应的处理
	- [Basic.Register](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Register.proto#L38)
      1. 保存[instanceId](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Register.proto#L41)及[sessKey](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Register.proto#L40)，后续所有发送的`encryptionMode`为`KEncryptionSessionKey`，加密密钥为`sessKey`
      2. 发送[KeepAliveRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/KeepAlive.proto#L9)
      3. SeqId + 1
      4. 发送[EnterRoomRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsEnterRoom.proto#L5)
      5. SeqId + 1
	- [Basic.Unregister](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Unregister.proto#L7)
      1. 停止Heartbeat定时器
      2. 关闭WebSocket连接
	- [Basic.KeepAlive](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/KeepAlive.proto#L17)  
    目前无需处理，忽略即可
	- [Basic.Ping](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Ping.proto#L15)  
    目前无需处理，忽略即可
	- [Global.ZtLiveInteractive.CsCmd](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsCmd.proto#L12)  
      根据[CmdAckType](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsCmd.proto#L13)进行对应的处理
      - [ZtLiveCsEnterRoomAck](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsEnterRoom.proto#L13)
          1. 保存[HeartbeatIntervalMs](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsEnterRoom.proto#L14)（目前默认10秒）
          2. 启动Heartbeat定时器
             1. 发送[HeartbeatRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsHeartbeat.proto#L5)
             2. SeqId + 1
             3. HeartbeatSeqId + 1
             4. 如果`HeartbeatSeqId % 5 == 4`，即启动Heartbeat定时器后每50秒，发送[KeepAliveRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/KeepAlive.proto#L9)，SeqId + 1
      - [ZtLiveCsHeartbeatAck](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsHeartbeat.proto#L10)  
         目前无需处理，忽略即可
      - [ZtLiveCsUserExitAck](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsUserExit.proto#L7)  
         目前无需处理，忽略即可
	- [Push.ZtLiveInteractive.Message](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScMessage.proto#L5)
      1. 发送空的Push.ZtLiveInteractive.Message（发送的PacketHeader的SeqId为接收到的PacketHeader的SeqId）
      2. 根据[CompressionType](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScMessage.proto#L7)，决定是否需要Gzip解压缩
      3. 根据[MessageType](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScMessage.proto#L6)进行对应的处理  
          - **[ZtLiveScActionSignal](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScActionSignal.proto#L7)**  
        遍历[Item](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveActionSignalItem.proto#L5)，根据[SignalType](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveActionSignalItem.proto#L6)进行对应处理  
            - [CommonActionSignalComment](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonActionSignalComment.proto#L7)
            - [CommonActionSignalLike](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonActionSignalLike.proto#L7)
            - [CommonActionSignalUserEnterRoom](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonActionSignalUserEnterRoom.proto#L7)
            - [CommonActionSignalUserFollowAuthor](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonActionSignalUserFollowAuthor.proto#L7)
            - [AcfunActionSignalThrowBanana](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/acfun.live.proto#L10)（可忽略）
            - [CommonActionSignalGift](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonActionSignalGift.proto#L8)
            - [CommonActionSignalRichText](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonActionSignalRichText.proto#L8)
            - [AcfunActionSignalJoinClub](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/acfun.live.proto#L20)
          - **[ZtLiveScStateSignal](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScStateSignal.proto#L7)**  
        遍历[Item](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveStateSignalItem.proto#L5)，根据[SignalType](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveStateSignalItem.proto#L6)进行对应的处理
            - [AcfunStateSignalDisplayInfo](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/acfun.live.proto#L16)
            - [CommonStateSignalDisplayInfo](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalDisplayInfo.proto#L5)
            - [CommonStateSignalTopUsers](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalTopUsers.proto#L7)
            - [CommonStateSignalRecentComment](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalRecentComment.proto#L7)
            - [CommonStateSignalChatCall](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalChatCall.proto#L5)
            - [CommonStateSignalChatAccept](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalChatAccept.proto#L7)
            - [CommonStateSignalChatReady](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalChatReady.proto#L8)
            - [CommonStateSignalChatEnd](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalChatEnd.proto#L5)
            - [CommonStateSignalCurrentRedpackList](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalCurrentRedpackList.proto#L7)
            - [CommonStateSignalAuthorChatCall](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalAuthorChatCall.proto#L7)
            - [CommonStateSignalAuthorChatAccept](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalAuthorChatAccept.proto#L5)
            - [CommonStateSignalAuthorChatReady](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalAuthorChatReady.proto#L7)
            - [CommonStateSignalAuthorChatEnd](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalAuthorChatEnd.proto#L5)
            - [CommonStateSignalAuthorChatChangeSoundConfig](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalAuthorChatChangeSoundConfig.proto#L5)
            - [CommonStateSignalPKAccept](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalPKAccept.proto#L5)
            - [CommonStateSignalPKInvitation](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalPKInvitation.proto#L7)
            - [CommonStateSignalPKReady](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalPKReady.proto#L7)
            - [CommonStateSignalPKSoundConfigChanged](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalPKSoundConfigChanged.proto#L5)
            - [CommonStateSignalPkEnd](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalPkEnd.proto#L5)
            - [CommonStateSignalPkStatistic](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalPkStatistic.proto#L9)
            - [CommonStateSignalWishSheetCurrentState](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonStateSignalWishSheetCurrentState.proto#L5)
          - **[ZtLiveScNotifySignal](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScNotifySignal.proto#L7)**  
      遍历[Item](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveNotifySignalItem.proto#L5)，根据[SignalType](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveNotifySignalItem.proto#L6)进行对应的处理
            - [CommonNotifySignalKickedOut](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonNotifySignalKickedOut.proto#L5)
            - [CommonNotifySignalViolationAlert](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonNotifySignalViolationAlert.proto#L5)
            - [CommonNotifySignalLiveManagerState](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/CommonNotifySignalLiveManagerState.proto#L5)
          - [ZtLiveScStatusChanged](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScStatusChanged.proto#L5)
            - 如果[Type](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScStatusChanged.proto#L6)为[LiveClosed](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScStatusChanged.proto#L12)或[LiveBanned](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScStatusChanged.proto#L15)，发送[UnregisterRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/im.basic/Unregister.proto#L5)
               1. 停止Heartbeat定时器
               2. 关闭WebSocket连接
          - [ZtLiveScTicketInvalid](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveScTicketInvalid.proto#L5)
            1. 选取之前获取的`availableTickets`中的下一个
            2. 发送[EnterRoomRequest](https://github.com/wpscott/AcFunDanmaku/blob/e8aaeea0598210ec641bfc0b31ce808a582dacf6/AcFunDanmu/protos/zt.live.interactive/ZtLiveCsEnterRoom.proto#L5)
            3. SeqId + 1

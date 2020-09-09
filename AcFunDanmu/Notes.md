# AcFun Live Channel
POST`https://api-new.app.acfun.cn/rest/app/live/channel`

POST Data

`count=[Count]&filters=[{"filterType":1, "filterId":[FilterId]}]&pcursor=`
* Count &le; 50
* [FilterId](https://api-new.acfunchina.com/rest/pc-client/live/type/list?kpf=WINDOWS_PC&appver=1.4.0.145)
  * 1 = 游戏直播
  * 2 = 手游直播
  * 3 = 娱乐直播
  * 4 = 虚拟偶像

# AcFun Live Info
POST`https://api-new.app.acfun.cn/rest/app/live/info`

POST Data

`authorId=[AuthorId]`

# AcFun Follow Feed V2
GET`https://api-new.app.acfun.cn/rest/app/feed/followFeedV2?pcursor=`

Get Cookies
* auth_key = UserId
* acPasstoken

---

# HOST `//api.kuaishouzt.com`
**params**
```
{subBiz, kpn, kpf, userId, did, token}
```

## startPlay
`/rest/zt/live/web/startPlay`
```
{authorId, pullStreamType}
```

## stopPlay
`/rest/zt/live/web/stopPlay`
```
{visitorId, liveId}
```

## postComment
`/rest/zt/live/web/audience/action/comment`
```
{visitorId, liveId, content}
```

## postLike
`/rest/zt/live/web/audience/action/like`
```
{visitorId, liveId, count, durationMs}
```

## getPlayUrls
`/rest/zt/live/web/getPlayUrls`
```
{visitorId, liveId, pullStreamType}
```

## getWatchingList
`/rest/zt/live/web/watchingList`
```
{visitorId, liveId}
```

## getGiftList
`/rest/zt/live/web/gift/list`
```
{visitorId, liveId, giftListHash}
```

## getWalletBalance
`/rest/zt/live/web/pay/wallet/balance`
```
{visitorId}
```

## sendGift
`/rest/zt/live/web/gift/send`
```
{visitorId, liveId, giftId, batchSize, comboKey}
```

## getEndSummary
`/rest/zt/live/web/endSummary`
```
{visitorId, liveId}
```

## getSimpleUsrInfo
`/rest/zt/live/web/user/info/simple`
```
{visitorId, queryUserId}
```

## getManager
`/rest/zt/live/web/author/action/manager/list`
```
{visitorId, liveId}
```

## managerKickAudience
`/rest/zt/live/web/manager/kick`
```
{visitorId, liveId, kickedUserId}
```

## authorKickAudience
`/rest/zt/live/web/author/action/kick`
```
{visitorId, liveId, kickedUserId}
```

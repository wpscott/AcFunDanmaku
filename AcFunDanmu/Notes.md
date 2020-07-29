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
GET`https://api-new.app.acfun.cn/rest/app/feed/followFeedV2`

Get Cookies
* auth_key = UserId
* acPasstoken

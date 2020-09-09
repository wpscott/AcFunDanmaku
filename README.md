

# AcFunDanmaku
AcFunDanmaku是用C# 和 .Net Core 3.1编写的AcFun直播弹幕工具。
该项目分为13个子项目。
**不提供32位程序。**

## AcFunDanmu 
AcFun直播弹幕解析工具。**开发中**

在AcFunDanmu文件夹中运行`generate.bat`或`generate.sh csharp`生成C# Protobuf文件。需安装[Google Protocol Buffers](https://github.com/protocolbuffers/protobuf/releases/tag/v3.13.0)。

测试数据来源于`m.acfun.cn.har`，可以在Chrome或Firefox中导入并查看websockets。

### 使用方式
1. 复制/添加/导入AcFunDanmu文件夹到你的解决方案
2. 添加AcFunDanmu的项目引用
3. 添加以下代码片段
```
Using AcFunDanmu;
...
三选一
1. var client = new Client(用户ID, ServiceToken, SecurityKey, Tickets, EnteryRoomAttach, LiveId);
2. var client = new Client(); await client.Initialize(主播ID);
3. var client = new Client(); await client.Login(用户名, 密码, 主播ID);

client.Handler = (你的自定义函数);
await client.Start();
```
*具体请参考AcFunDanmuConsole*

## AcFunDanmuConsole
使用AcFunDanmu的控制台AcFun直播弹幕输出工具。
### 使用方式
1. 编译
2. 运行`AcFunDanmuConsole.exe 用户ID（如：69065、23682490或156843）`
3. 查看弹幕

## AcFunDanmuLottery
使用AcFunDanmu的AcFun直播弹幕抽奖工具
### 使用方式
1. 编译
2. 运行AcFunDanmuLottery.exe
3. 输入主播ID，点击连接
4. 输入弹幕关键词，点击开始
5. 点击结束，输入抽选数量，点击抽！
6. 查看抽奖结果

## AcFunCommentControl
AcFun评论组件
### 使用方式
*请参考AcFunCommentLottery及AcFunMomentLottery*

## AcFunCommentLottery
AcFun视频及文章评论区抽奖工具
### 使用方式
1. 编译
2. 运行AcFunCommentLottery.exe
3. 输入ac号，点击获取
4. （可选）输入评论关键词，点击筛选
5. 输入抽选数量，点击抽！
6. 查看抽奖结果

## AcFunDanmuSongRequest
使用AcFunDanmu的AcFun直播点歌工具。**开发中**
### 使用方式
1. 复制/添加/导入AcFunDanmuSongRequest文件夹到你的解决方案
2. 添加AcFunDanmuSongRequest的项目引用
3. 添加以下代码片段
```
using AcFunDanmuSongRequest;
...
DGJ.ExitEvent += (你的自定义函数);
DGJ.AddSongEvent += (你的自定义函数);
await DGJ.Initialize();
```
*具体请参考AcFunDGJ*

## AcFunDGJ
使用AcFunDanmuSongRequest的AcFun直播点歌工具。**开发中**
### 使用方式
1. 编译
2. 新建config.json并和AcFunDGJ.exe放在一起
```
{
  "version": 1,  //请勿修改
  "独立运行": true, //需为true，同时需设置主播ID
  "音乐平台": "网易云音乐", //代码支持网易云音乐和QQ音乐，仅测试了网易云音乐
  "主播ID": 如：69065、23682490或156843, //监听指定主播ID的直播弹幕
  "播放列表": "", //未实现
  "点歌格式": "^点歌 (.*?)$", //默认格式为：点歌 歌名 或 点歌 歌名 歌手
  "显示歌词": false //未实现
}
```
3. 运行AcFunDGJ.exe
4. 等待点歌，点歌后会自动播放

## AcFunMomentLottery
AcFun动态评论区抽奖工具
### 使用方式
1. 编译
2. 运行AcFunMomentLottery.exe
3. 输入动态ID，点击获取
4. （可选）输入评论关键词，点击筛选
5. 输入抽选数量，点击抽！
6. 查看抽奖结果

## AcFunCommentLottery
AcFun视频弹幕抽奖工具
**因使用的API限制，弹幕上限为1000条**
### 使用方式
1. 编译
2. 运行AcFunVideoDanmuLottery.exe
3. 输入ac号，点击获取
4. （可选）输入弹幕关键词，点击筛选
5. 输入抽选数量，点击抽！
6. 查看抽奖结果

## AcFunBlackBoxGuess
AcFun直播黑箱盲猜工具。**开发中**
### 使用方式
1. 编译
2. 运行AcFunBlackBoxGuess.exe
3. 输入主播ID，点击连接
4. 输入答案（注意隐藏），点击开始
5. 根据弹幕提问（格式为`【问题】`或`[问题]`）选择是、否、盲猜或已回答
6. 若盲猜正确或已回答100道提问，游戏结束

## AcFunDMJ
AcFun直播弹幕姬。**开发中（本项目仅用于试验Blazor功能）**

[chatbox.css](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDMJ/wwwroot/chatbox.css)基于[【教程】关于obs捕获浏览器窗口弹幕](https://www.acfun.cn/a/ac16690082)
### 使用方式
1. 编译
2. 运行AcFunDMJ.exe
3. 打开浏览器，访问http://localhost:5000/[主播ID]
4. 查看弹幕

## AcFunDMJ-WASM
AcFun直播弹幕姬服务器版。**开发中（本项目仅用于试验Blazor Webassembly功能）**

分为3个子项目
* AcFunDMJ-WASM.Client为客户端代码
* AcFunDMJ-WASM.Server为服务器代码
* AcFunDMJ-WASM.Shared为两者通用代码

[chatbox.css](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDMJ/wwwroot/chatbox.css)基于[【教程】关于obs捕获浏览器窗口弹幕](https://www.acfun.cn/a/ac16690082)
### 使用方式
1. 编译AcFunDMJ-WASM.Server
2. 运行AcFunDMJ-WASM.Server.exe
3. 打开浏览器，访问http://localhost:5000/[主播ID（需在appsettings.json的AVUP列表中）]
4. 查看弹幕

## AcFunDMJ.Vanilla
AcFun直播弹幕姬OBS版。**开发中**

[chatbox.css](https://github.com/wpscott/AcFunDanmaku/blob/master/AcFunDMJ/wwwroot/chatbox.css)基于[【教程】关于obs捕获浏览器窗口弹幕](https://www.acfun.cn/a/ac16690082)
### 使用方式
1. 编译
2. 运行AcFunDMJ.Vanilla.exe
3. 打开OBS，添加浏览器，地址为http://localhost:5000/[主播ID]
4. 查看弹幕
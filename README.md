
# AcFunDanmaku
AcFunDanmaku是用C# 和 .Net Core 3.1编写的AcFun直播弹幕工具。
该项目分为4个子项目。
**不提供32位程序。**

## AcFunDanmu 
AcFun直播弹幕解析工具。**开发中**

在AcFunDanmu文件夹中运行`protoc -I .\protos --csharp_out=.\Models [文件名].proto`生成C# Protobuf文件。需安装[Google Protocol Buffers](https://github.com/protocolbuffers/protobuf/releases/tag/v3.12.2)。

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

## AcFunCommentLottery
AcFun视频及文章评论区抽奖工具
### 使用方式
1. 编译
2. 运行AcFunCommentLottery.exe
3. 输入ac号，点击获取
4. （可选）输入评论关键词，点击筛选
5. 输入抽选数量，点击抽！
6. 查看抽奖结果

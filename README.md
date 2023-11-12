# AcFunDanmaku
AcFunDanmaku是用C# 和 .Net 6编写的AcFun直播弹幕工具。

## AcFunDanmu 
AcFun直播弹幕解析工具。

在AcFunDanmu文件夹中运行`generate.bat`或`generate.sh csharp`生成C# Protobuf文件。需安装[Google Protocol Buffers](https://github.com/protocolbuffers/protobuf/releases/tag/v25.0)。

测试数据来源于`m.acfun.cn.har`，可以在Chrome或Firefox中导入并查看websockets。

### 使用方式
1. 在Visual Studio中使用**NuGet Package Manager**搜索并添加[AcFunDanmu](https://www.nuget.org/packages/AcFunDanmu)或在控制台/终端中输入`dotnet add package AcFunDanmu`
2. 添加以下代码片段
```
Using AcFunDanmu;
...
var client = new Client();

client.Handler += (你的自定义函数);
client.OnEnd += (你的自定义函数);

client.Start(主播ID);
```
*具体请参考[AcFunDanmuConsole](https://github.com/wpscott/AcFunDanmaku/tree/master/AcFunDanmuConsole)*

## AcFunDanmuConsole
使用AcFunDanmu的控制台AcFun直播弹幕输出工具。
### 使用方式
1. 编译
2. 运行`AcFunDanmuConsole.exe 用户ID（如：69065、23682490或156843）`
3. 查看弹幕

# 有问题？
1. 提交Issue
2. 搜索QQ频道“AcFun开源⑨课”

本项目已被[《学园构想家》](https://store.steampowered.com/app/1937500)采用，添加了 AcFun 直播互动功能，让主播在游戏中与观众实时互动。

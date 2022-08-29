![image](https://user-images.githubusercontent.com/6392170/187136698-3f92ad37-a79e-49fe-a25c-678adadd1cef.png)

以此为例
* `abcd 0001`为快手定义的magic number
* `0000 001d`为PacketHeader的长度
* `0000 00b0`为IV + Downstream/UpstreamPayload的长度
* 红色代表的是PacketHeader的具体内容，长度为29 (0x1d)
* 橙色为IV，长度固定为16
* 紫色是Downstream/UpstreamPayload，长度为160 (0xb0 = 176 = 160 + 16)

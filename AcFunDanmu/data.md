![image](https://user-images.githubusercontent.com/6392170/187055753-0491d018-3ff1-46dc-90aa-bfa058f8cae5.png)

以此为例
* `abcd 0001`为快手定义的magic number
* `0000 002d`为PacketHeader的长度
* `0000 0060`为IV + Downstream/UpstreamPayload的长度
* 红色下划线代表的是PacketHeader的具体内容，长度为45 (0x2d)
* 黄色下划线为IV，长度固定为8
* 绿色下划线是Downstream/UpstreamPayload，长度为88 (88 + 8 = 96 = 0x60)

# AcFun弹幕姬规范提案

本提案是关于规范AcFun弹幕姬前后端通讯的方式及方法，简化前后端开发难度

1. 前后端通讯接口地址：`ws://localhost:22386`（22386源自手机九宫格的AcFun）
2. 前后端通讯以JSON为数据格式
3. 连接直播间方式
    1. 客户端通过访问`ws://localhost:22386/:id`的方式连接直播间
    2. 客户端访问`ws://localhost:22386`后，发送`{cmd:0, uid: number}`的方式连接直播间
4. 客户端接收的基础消息格式
    1. 弹幕
        ```
        {
            type: 1,
            content: string,
            user: {
                id: number,
                name: string,
                avatar: string,
                badge: {
                    name: string,
                    level: number
                }
            },
            timestamp: number
        }
        ```
    2. 礼物
        ```
        {
            type: 2,
            user: {
                id: number,
                name: string,
                avatar: string,
                badge: {
                    name: string,
                    level: number
                }
            },
            gift: {
                id: number,
                name: string,
                value: number,
                count: number,
                combo: number,
                comboId: string,
                pic: string
            },
            timestamp: number
        }
        ```
    3. 点赞
        ```
        {
            type: 3,
            user: {
                id: number,
                name: string,
                avatar: string,
                badge: {
                    name: string,
                    level: number
                }
            },
            timestamp: number
        }
        ```
    4. 关注
        ```
        {
            type: 4,
            user: {
                id: number,
                name: string,
                avatar: string,
                badge: {
                    name: string,
                    level: number
                }
            },
            timestamp: number
        }
        ```
    5. 进入直播间
        ```
        {
            type: 5,
            user: {
                id: number,
                name: string,
                avatar: string,
                badge: {
                    name: string,
                    level: number
                }
            },
            timestamp: number
        }
        ```
    6. 点赞及观众数
        ```
        {
            type: 6,
            likes: number
            audience: number
        }
        ```
    7. 礼物图片
        ```
        {
            type: 7,
            pic: string
        }
        ```
5. （可选）客户端发送的基础消息格式
    1. 获取礼物图片
        ```
        {
            cmd: 1,
            id: number
        }
        ```
6. （可选）客户端接收的高级消息格式
    1. 关注数
        ```
        {
            type: 100,
            followers: number
        }
        ```
    2. 更新OBS浏览器源的CSS样式
        ```
        {
            type: 101,
            css: string
        }
        ```
7. （可选）客户端发送的高级消息格式 
    **TODO**

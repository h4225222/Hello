using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unity.mgobe;

public class NetManager : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        GameInfoPara gameInfo = new GameInfoPara
        {
            GameId = "obg-lvsgzyuo",
            OpenId = "openid_123_test",
            SecretKey = "7458edf96efb6108bfc43a5e88cdfc5b1b22b347"
        };

        ConfigPara config = new ConfigPara
        {
            Url = "lvsgzyuo.wxlagame.com",
            ReconnectMaxTimes = 5,
            ReconnectInterval = 1000,
            ResendInterval = 1000,
            ResendTimeout = 10000
        };

        Listener.Init(gameInfo, config, (req) =>
        {
            if(req.Code == 0)
            {
                Debug.Log("初始化成功");
                var room = new Room(null);
                room.GetRoomDetail((ResponseEvent e) => { 
                    if(e.Code != 0 && e.Code != 20011)
                    {
                        Debug.Log(e.Msg);
                    }
                    Debug.Log("查询成功");
                    if(e.Code == 20011)
                    {
                        Debug.Log("玩家不在房间内");
                    }
                    else
                    {
                        var res = (GetRoomByRoomIdRsp)e.Data;
                        Debug.LogFormat("房间名 {0}", res.RoomInfo.Name);
                    }
                });
            }
            else
            {
                Debug.Log(req.Msg);
            }
        });
    }
}

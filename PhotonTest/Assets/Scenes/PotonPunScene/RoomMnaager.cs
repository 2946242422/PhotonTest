using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject roomNamePrefab;//房间显示的预制体
    public Transform gridLayout;//预制体的父物体

    /// <summary>
    /// 房间更新的函数，每次房间消失或者增加，就会进行调用，所以要对消失的时候进行特殊处理
    /// </summary>
    /// <param name="roomList">返回的房间参数</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("roomlist:" + roomList.Count);

        for (int i = 0; i < gridLayout.childCount; i++)//遍历父物体下的子物体
        {
            if (gridLayout.GetChild(i).gameObject.GetComponentInChildren<Text>().text == roomList[i].Name)//有相同的物体
            {
                Destroy(gridLayout.GetChild(i).gameObject);//销毁
                if (roomList[i].PlayerCount == 0)//如果房间玩家为0
                {
                    roomList.Remove(roomList[i]);//移除该房间
                }
            }
        }
        foreach (var room in roomList)//遍历生成房间的显示
        {

            GameObject newRoom = Instantiate(roomNamePrefab, gridLayout.position, Quaternion.identity, gridLayout);//生成房间按钮
            newRoom.GetComponent<RoomBtn>().roomName = room.Name;//设置房间名字
            newRoom.GetComponentInChildren<Text>().text = room.Name + "(" + room.PlayerCount + "/4)";//显示参数

        }
    }
}


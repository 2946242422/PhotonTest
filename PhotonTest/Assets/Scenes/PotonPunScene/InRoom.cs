using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRoom : MonoBehaviourPunCallbacks
{
    public GameObject playerInfoPrefab;//玩家按钮预制体
    public Transform conlayout;//父物体
    List<GameObject> mplayers = new List<GameObject>();
    Player[] allplayers;

    bool ready = false;

    public InputField contentInput;//聊天输入框
    public GameObject textPrefab;//文本预制体
    public Transform layoutContent;//父物体
    void Start()
    {

    }
    /// <summary>
    /// 当加入房间时
    /// </summary>
    public override void OnJoinedRoom()
    {

        allplayers = PhotonNetwork.PlayerList;
        foreach (var item in allplayers)
        {
            GameObject obj = Instantiate(playerInfoPrefab, conlayout);

            m_PlayerInfo _PlayerInfo = obj.GetComponent<m_PlayerInfo>();
            _PlayerInfo.playerName = item.NickName;
            obj.GetComponentInChildren<Text>().text = item.NickName + "(" + "未准备" + ")";
            mplayers.Add(obj);
        }
    }

    /// <summary>
    /// 当有玩家进入时，更新玩家列表
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject obj = Instantiate(playerInfoPrefab, conlayout);
        m_PlayerInfo _PlayerInfo = obj.GetComponent<m_PlayerInfo>();
        _PlayerInfo.playerName = newPlayer.NickName;
        obj.GetComponentInChildren<Text>().text = newPlayer.NickName + "(" + "未准备" + ")";
        mplayers.Add(obj);
    }
    /// <summary>
    /// 当有玩家离开时，更新玩家列表
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach (var item in mplayers)
        {
            if (item.GetComponentInChildren<Text>().text.Contains(otherPlayer.NickName))
            {

                Destroy(item);
                break;
            }
        }
    }
    /// <summary>
    /// 当离开房间时，如果房间内还有人，则设置其他人为房主
    /// </summary>
    public override void OnLeftRoom()
    {
        foreach (var item in mplayers)
        {
            if (item.GetComponentInChildren<Text>().text.Contains(PhotonNetwork.LocalPlayer.NickName))
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    foreach (var p in allplayers)
                    {
                        if (p != PhotonNetwork.LocalPlayer)
                        {
                            PhotonNetwork.CurrentRoom.SetMasterClient(p);
                            break;
                        }
                    }

                }

                Destroy(item);
                break;
            }
        }
    }


    Dictionary<string, bool> IsPlayerReady = new Dictionary<string, bool>();//使用字典来进行数据广播，广播字典
    /// <summary>
    /// 准备按钮事件
    /// </summary>
    public void SetReadyBtn()
    {
        foreach (var item in mplayers)
        {
            m_PlayerInfo playerinfo = item.GetComponent<m_PlayerInfo>();
            if (!IsPlayerReady.ContainsKey(playerinfo.playerName))
            {
                IsPlayerReady.Add(playerinfo.playerName, false);
            }
        }
        ready = !ready;
        foreach (var item in IsPlayerReady)
        {
            if (item.Key == PhotonNetwork.LocalPlayer.NickName)
            {
                IsPlayerReady[item.Key] = ready;
                break;
            }
        }
        photonView.RPC("ReadyBtn", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, IsPlayerReady);
    }

    [PunRPC]
    void ReadyBtn(string xname, Dictionary<string, bool> keyValuePairs)
    {

        foreach (var item in keyValuePairs)
        {
            if (item.Value == true)
            {
                foreach (var p in mplayers)
                {
                    if (p.GetComponentInChildren<Text>().text.Contains(item.Key))
                    {
                        p.GetComponentInChildren<Text>().text = item.Key + "(" + "已准备" + ")";
                        break;
                    }
                }
            }
            else
            {
                foreach (var p in mplayers)
                {
                    if (p.GetComponentInChildren<Text>().text.Contains(item.Key))
                    {
                        p.GetComponentInChildren<Text>().text = item.Key + "(" + "未准备" + ")";
                        break;
                    }
                }
            }
        }

        //foreach (var item in mplayers)
        //{
        //    Debug.Log(item.GetComponentInChildren<Text>().text);
        //    if (item.GetComponentInChildren<Text>().text.Contains(xname))
        //    {
        //        if(ready)
        //        {
        //          item.GetComponentInChildren<Text>().text= xname + "(" + "已准备" + ")";
        //        }
        //        else
        //        {
        //            item.GetComponentInChildren<Text>().text = xname + "(" + "未准备" + ")";
        //        }
        //        break;
        //    }
        //}
    }


    //发送消息
    public void SendMessInfoBtn()
    {
        string info = PhotonNetwork.LocalPlayer.NickName + " :" + contentInput.text;
        photonView.RPC("SendMess", RpcTarget.All, info);
    }

    [PunRPC]
    void SendMess(string mess)
    {
        GameObject textobj = Instantiate(textPrefab, layoutContent);
        textobj.GetComponentInChildren<Text>().text = mess;
    }

    //开始游戏
    public void StartGameButton()
    {

        photonView.RPC("LoadGameScene", RpcTarget.All);
    }


    [PunRPC]
    void LoadGameScene()
    {
        PhotonNetwork.LoadLevel(1);
    }
}


using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;
using static System.Net.Mime.MediaTypeNames;

public class Test : MonoBehaviourPunCallbacks
{
    public InputField inputField;
    public UnityEngine.UI.Text text;
    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ�������
        Player localPlayer = PhotonNetwork.LocalPlayer;

        // ��ȡ������ҵ� ActorNumber��Ψһ��ʶ����
        int actorNumber = localPlayer.ActorNumber;

        // ��ȡ������ҵ��ǳ�
        string nickname = localPlayer.NickName;
        Debug.Log("Online player1: " + actorNumber+nickname);
        text = GameObject.Find("zText").GetComponent<UnityEngine.UI.Text>();
        // ����Ƿ����ӵ��� Photon ������
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // ��ȡ��ǰ�����е��������
            Player[] players = PhotonNetwork.PlayerList;
            Debug.Log("Online player: " + players.Length);
            // ��������б�
            foreach (Player player in players)
            {
                // ��ȡ��ҵ��ǳ�
                string playerName = player.NickName;

                // �������ǳƵ�����̨
                Debug.Log("Online player: " + playerName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ������Ϣ�ķ�����������������ϱ�����
    [PunRPC]
    private void ReceiveMessage(string message, PhotonMessageInfo info)
    {
        // ��ȡ������Ϣ����ҵ���Ϣ
        string senderName = info.Sender.NickName;

        // �ڿ���̨�����Ϣ
        Debug.Log("Received message from " + senderName + ": " + message);

        // �ڴ˴�ִ����������...
        text.text = message;
    }

}

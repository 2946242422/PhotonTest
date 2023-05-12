using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public string playerName;
    public InputField roomName;
    public bool coonectToMaster;
    private bool joinRoom;
    public List<GameObject> playerlist=new List<GameObject>();
    public Text text;
    public void ConnectToMaster()
    {
        //������������
        PhotonNetwork.ConnectUsingSettings();
        //�汾��
        PhotonNetwork.GameVersion = "1";
    }
    public void CreateRoom()
    {
        print("cjain");
        if (!coonectToMaster || joinRoom) return;
        
           //�������� ������� λ����һ����������
           print(coonectToMaster);
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { MaxPlayers = 15 },TypedLobby.Default);
          
        
    }
    public void JoinRoom()
    {

        if (!coonectToMaster || joinRoom)
        return;
       
     
            PhotonNetwork.JoinRoom(roomName.text);
            
        
      
     

    }
    private void Update()
    {
        // ���磬���¿ո��������Ϣ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���÷�����Ϣ�ķ���
            SendMessageToPlayer(roomName.text, playerlist[0] );
        }
    }
    /// <summary>
    /// ���ӳɹ��ص�
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        coonectToMaster = true;
        print(coonectToMaster);
    }
    /// <summary>
    /// ��������ص�
    /// </summary>
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("�����ɹ�");
        text.text = "�����ɹ�";
        joinRoom = true;
    }
    /// <summary>
    ///���뷿��ص�
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("���뷿��");
        text.text = "���뷿��";
       //GameObject play= PhotonNetwork.Instantiate(playerName,Vector3.zero,Quaternion.identity);
       // playerlist.Add(play);
        // ����ǰ��ҹ�����������
        GameObject playerObject = InstantiatePlayerObject();
        AssignPlayerToGameObject(playerObject);
    }
    private void SendMessageToPlayer(string message, GameObject player)
    {
        // ��ȡ������Ϣ��ҵ� PhotonView ���
        PhotonView playerPhotonView = player.GetComponent<PhotonView>();

        // ͨ��RPC���ý�����Ϣ�ķ���������Ϣ���͸�ָ�������
        playerPhotonView.RPC("ReceiveMessage", playerPhotonView.Owner, message);
    }

    // ������Ϣ�ķ���������ָ������ϱ�����
    [PunRPC]
    private void ReceiveMessage(string message, PhotonMessageInfo info)
    {
        // ��ȡ������Ϣ����ҵ���Ϣ
        string senderName = info.Sender.NickName;

        // �ڿ���̨�����Ϣ
        Debug.Log("Received message from " + senderName + ": " + message);
        text.text = message;
        // �ڴ˴�ִ����������...
    }
    // ʵ������Ҷ���
    private GameObject InstantiatePlayerObject()
    {
        // ���ʵ���λ�úͳ���ʵ������Ҷ���
        return PhotonNetwork.Instantiate(playerName, Vector3.zero, Quaternion.identity, 0);
    }

    // ����ҹ�����������
    private void AssignPlayerToGameObject(GameObject playerObject)
    {
        // ��ȡ��ǰ��ҵ� PhotonPlayer ����
        //Player currentPlayer = PhotonNetwork.LocalPlayer;

        // ����ǰ��ҹ�����������
        //playerObject.GetComponent<PlayerController>().SetPlayer(currentPlayer);
    }
}

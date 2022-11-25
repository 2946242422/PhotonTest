using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mPlayer : MonoBehaviourPunCallbacks
{
    void Update()
    {
        if (photonView.IsMine)
        {
            MoveController();
        }
    }
    public void MoveController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * 10 * Time.deltaTime);
    }
}


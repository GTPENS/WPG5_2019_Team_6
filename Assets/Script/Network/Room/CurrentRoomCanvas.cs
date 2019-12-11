using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClickStartSync() {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.LoadLevel(3);
    }
}

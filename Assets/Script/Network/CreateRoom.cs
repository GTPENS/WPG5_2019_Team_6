using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private Text _roomName;
    private Text RoomName {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom() {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };

        if(PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default)) {
            print("Create room successfully sent");
        }
        else {
            print("Create room failed sent");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage) {
        print("Create room failed: " + codeAndMessage[1]);
    }

    private void OnCreatedRoom() {
        print("Room created successfully");
    }
}

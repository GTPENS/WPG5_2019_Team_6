using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerListingPrefab;
    private GameObject PlayerListingPrefab {
        get { return _playerListingPrefab; }
    }

    private List<PlayerListing> _playerListing = new List<PlayerListing>();
    private List<PlayerListing> PlayerListing {
        get { return _playerListing; }
    }

    private void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
        PhotonNetwork.LeaveRoom();
    }

    private void OnJoinedRoom() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

        MainCanvasManager.Instance.CurrentRoomCanvaz.transform.SetAsLastSibling();

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
        for(int i = 0; i < photonPlayers.Length; i++) {
            PlayerJoinedRoom(photonPlayers[i]);
        }
    }

    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer) {
        PlayerJoinedRoom(photonPlayer);
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer) {
        PlayerLeftRoom(photonPlayer);
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer) {
        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);

        GameObject playerListingObj = Instantiate(PlayerListingPrefab);
        playerListingObj.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        PlayerListing.Add(playerListing);
    }

    private void PlayerLeftRoom(PhotonPlayer photonPlayer) {
        int index = PlayerListing.FindIndex(x => x.PhotonPlayer == photonPlayer);

        if(index != -1) {
            Destroy(PlayerListing[index].gameObject);
            PlayerListing.RemoveAt(index);
        }
    }

    public void OnClickRoomState() {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
        PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
    }

    public void OnClickLeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }
}

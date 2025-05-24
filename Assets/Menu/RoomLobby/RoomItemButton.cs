using UnityEngine;

public class RoomItemButton : MonoBehaviour
{
    public string RoomName { get; set; }
    
    public void OnButtonPressed()
    {
        RoomList.Instance.JoinRoomByName(RoomName);
    }
}

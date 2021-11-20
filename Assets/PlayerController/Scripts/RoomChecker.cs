using UnityEngine;

namespace PlayerController.Scripts
{
    public class RoomChecker : MonoBehaviour
    {
        public string CurrentRoomName { get; private set; }
        public Room.Room CurrentRoom { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Equals("Room")) return;
            CurrentRoomName = other.name;
            CurrentRoom = other.GetComponent<Room.Room>();
        }
    }
}

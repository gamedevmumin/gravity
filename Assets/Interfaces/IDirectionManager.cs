using UnityEngine;

namespace Interfaces
{
    public interface IDirectionManager 
    {
        void ManageDirection(Vector2 input);
        void ToggleSide();
    }
}

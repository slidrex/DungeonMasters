using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public void SetPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    
}

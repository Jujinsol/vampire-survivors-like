using UnityEngine;

public class AreaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            CameraController._inst.MoveToFar();
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    float h, v, _speed = 7.0f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    float minX = -23f, maxX = 23f, minY = -15f, maxY = 15f;

    GameObject background;

    private void Start()
    {
        background = GameObject.Find("Grid").gameObject;
    }

    private void LateUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-h, -v, 0f) * _speed * Time.deltaTime;

        // 이동 적용
        background.transform.position += move;

        // 카메라 화면 사이즈 고려해서 Clamp
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Camera.main.aspect;

        float clampedX = Mathf.Clamp(
            background.transform.position.x,
            minX + horzExtent,
            maxX - horzExtent
        );

        float clampedY = Mathf.Clamp(
            background.transform.position.y,
            minY + vertExtent,
            maxY - vertExtent
        );

        background.transform.position = new Vector3(clampedX, clampedY, background.transform.position.z);
    }
}
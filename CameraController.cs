using UnityEngine;

public class CameraController : MonoBehaviour
{
    float h, v, _speed = 7.0f;
    float minX = -23f, maxX = 23f, minY = -15f, maxY = 15f;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(h, v, 0f) * _speed * Time.deltaTime;

        // 이동 적용
        transform.position += move;

        MoveToFar();
    }

    void MoveToFar()
    {
        if (transform.position.x > 10f && GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Right)
        {
            transform.position = new Vector3(-9f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -10f && GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Left)
        {
            transform.position = new Vector3(9f, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > 13f && GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Up)
        {
            transform.position = new Vector3(transform.position.x, -12f, transform.position.z);
        }
        else if (transform.position.y < -13f && GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Down)
        {
            transform.position = new Vector3(transform.position.x, 12f, transform.position.z);
        }
    }
}
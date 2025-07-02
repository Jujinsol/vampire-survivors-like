using System.Threading;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public static CatController _inst;

    private GameObject _player;
    private float _speed = 3.0f;
    private bool _isStopped = false;

    private Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.Find("Player").gameObject;
        cameraTransform = Camera.main.transform;
        _inst = this;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isStopped = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isStopped) return;
        Moving();
    }

    private void Moving()
    {
        Vector3 direction = cameraTransform.position - transform.position;
        direction.z = 0f;

        float distance = direction.magnitude; // 거리 계산

        if (distance <= 1f) return; // 거리가 1 이하면 멈춤

        direction = direction.normalized;
        transform.position += direction * _speed * Time.deltaTime;

        if (0 - transform.position.x > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    public void MoveToFar()
    {
        if (_player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Left)
        {
            transform.position = new Vector3(_player.transform.position.x-transform.position.x, transform.position.y, transform.position.z);
        }
        else if (_player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Right)
        {
            transform.position = new Vector3(_player.transform.position.x- transform.position.x, transform.position.y, transform.position.z);
        }
        else if (_player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Up)
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y - transform.position.y, transform.position.z);
        }
        else if (_player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Down)
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y - transform.position.y, transform.position.z);
        }
    }
}

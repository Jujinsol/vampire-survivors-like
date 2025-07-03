using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterController : PoolAble
{
    public IObjectPool<GameObject> Pool { get; set; }
    public static MonsterController _inst;
    public int _hp, power;
    public float _speed = 3.0f;
    private bool _isStopped = false;

    public int Hp
    {
        get => _hp;
        private set => _hp = Mathf.Clamp(value, 0, _hp);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _inst = this;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isStopped = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isStopped = false;
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
        //Vector3 target = GameManager._inst.virtualPlayerPos;
        Vector3 direction = GameManager._inst.player.transform.position - transform.position;
        direction.z = 0f;

        float distance = direction.magnitude; 

        if (distance <= 1f) return;

        direction = direction.normalized;
        transform.position += direction * _speed * Time.deltaTime;

        if (GameManager._inst.player.transform.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    public void MoveToFar()
    {
        if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Left)
        {
            transform.position = new Vector3(GameManager._inst.player.transform.position.x - transform.position.x, transform.position.y, transform.position.z);
        }
        else if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Right)
        {
            transform.position = new Vector3(GameManager._inst.player.transform.position.x - transform.position.x, transform.position.y, transform.position.z);
        }
        else if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Up)
        {
            transform.position = new Vector3(transform.position.x, GameManager._inst.player.transform.position.y - transform.position.y, transform.position.z);
        }
        else if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Down)
        {
            transform.position = new Vector3(transform.position.x, GameManager._inst.player.transform.position.y - transform.position.y, transform.position.z);
        }
    }

    public void Die()
    {
        var coin = ObjectPoolManager.instance.GetGo("coin");
        coin.transform.position = transform.position;

        ReleaseObject();
    }
}

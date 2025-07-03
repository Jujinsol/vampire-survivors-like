using UnityEngine;
using UnityEngine.Pool;

public class Coin : PoolAble
{
    public IObjectPool<GameObject> Pool { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ReleaseObject();
            GameManager._inst.GetCoin();
        }
    }
}

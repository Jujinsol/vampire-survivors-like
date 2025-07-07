using UnityEngine;
using UnityEngine.Pool;

public class Arrow : PoolAble
{
    public IObjectPool<GameObject> Pool { get; set; }
    float speed = 5f, deleteTime;

    void Update()
    {
        deleteTime += Time.deltaTime;
        if (deleteTime > GameManager._inst.arrowTime)
        {
            ReleaseObject();
            deleteTime = 0f;
        }

        this.transform.Translate(Vector3.up * this.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Attack(collision.gameObject, GameManager._inst.arrowPower);
        }
    }

    void Attack(GameObject obj, int power)
    {
        obj.GetComponent<MonsterController>()._hp -= power;
        if (obj.GetComponent<MonsterController>()._hp < 0)
        {
            obj.GetComponent<MonsterController>().Die();
        }
    }
}

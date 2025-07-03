using UnityEngine;
using UnityEngine.Pool;

public class Arrow : PoolAble
{
    public IObjectPool<GameObject> Pool { get; set; }
    float speed = 5f, deleteTime;
    bool isReleased = false;
    int check = 0;

    void Update()
    {
        deleteTime += Time.deltaTime;
        if (!isReleased && deleteTime > GameManager._inst.arrowTime)
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
            ReleaseObject();
            isReleased = true;

            Attack(collision.gameObject, GameManager._inst.arrowPower);
        }
    }

    void Attack(GameObject obj, int power)
    {
        Debug.Log("Attack " + check);
        check++;

        obj.GetComponent<MonsterController>()._hp -= power;
        if (obj.GetComponent<MonsterController>()._hp < 0)
        {
            obj.GetComponent<MonsterController>().Die();
        }
    }
}

using System.Collections;
using UnityEngine;

public class GarlicManager : MonoBehaviour
{
    public static GarlicManager _inst;
    GameObject Garlic;

    private bool isAttacking = false; // 공격 중인지 체크

    void Awake()
    {
        _inst = this;
        Garlic = GameObject.Find("Weapon").transform.Find("Garlic").gameObject;
    }

    void Update()
    {
        // Garlic은 플레이어 위치에 항상 따라다님
        transform.position = GameManager._inst.player.transform.position + new Vector3(0, -0.2f, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && !isAttacking)
        {
            StartCoroutine(GarlicAttack(collision.gameObject));
        }
    }

    IEnumerator GarlicAttack(GameObject monster)
    {
        isAttacking = true;

        while (monster != null && monster.activeInHierarchy)
        {
            // 공격
            Attack(monster, GameManager._inst.garlicPower);

            // 2초마다 공격
            yield return new WaitForSeconds(2f);
        }

        isAttacking = false;
    }

    void Attack(GameObject obj, int power)
    {
        if (obj.TryGetComponent(out MonsterController monster))
        {
            monster._hp -= power;
            if (monster._hp <= 0)
            {
                monster.Die();
            }
        }
    }
}
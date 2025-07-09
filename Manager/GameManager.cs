using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _inst;
    public Vector3 virtualPlayerPos = Vector3.zero;
    public GameObject player;
    public float catInitTime = 2.0f, arrowTime = 2.0f;
    public float arrowPower = 3, garlicPower = 3;
    public float level = 1, currentExp = 0, maxExp = 10;
    Slider sliderEXP;

    private void Awake()
    {
        _inst = this;
        player = GameObject.Find("Player").gameObject;
        sliderEXP = GameObject.Find("Canvas").transform.Find("SliderEXP").gameObject.GetComponent<Slider>();
        sliderEXP.maxValue = maxExp;
        sliderEXP.value = currentExp;

        for (int i = 0; i < 20; i++)
        {
            SpawnCat("Cat1");
        }

        StartCoroutine(SpawnCat1Routine("Cat1"));
    }

    private void SpawnCat(string catName)
    {
        for (int i = 0; i < level; i++)
        {
            var newCat = ObjectPoolManager.instance.GetGo(catName);

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(5f, 20f);

            float randX = player.transform.position.x + Mathf.Cos(angle) * distance;
            float randY = player.transform.position.y + Mathf.Sin(angle) * distance;

            newCat.transform.position = new Vector3(randX, randY, 0f);
        }
    }

    private IEnumerator SpawnCat1Routine(string catName)
    {
        while (true)
        {
            yield return new WaitForSeconds(catInitTime);
            SpawnCat(catName);
        }
    }

    private IEnumerator SpawnCat2Routine(string catName)
    {
        while (true)
        {
            yield return new WaitForSeconds(catInitTime);
            SpawnCat(catName);
        }
    }

    public void GetCoin()
    {
        currentExp += 5;
        sliderEXP.value = currentExp;

        if (currentExp >= maxExp)
            LevelUp();
    }

    public void Cat2Start()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnCat("Cat2");
        }

        StartCoroutine(SpawnCat2Routine("Cat2"));
    }

    void LevelUp()
    {
        level += 1;
        Debug.Log(level);
        if (level == 2)
            Cat2Start();
        LevelUpManager._inst.LevelUpUI();
        currentExp = 0;
        maxExp = 50 + (level - 1) * 25;

        sliderEXP.maxValue = maxExp;
        sliderEXP.value = currentExp;
    }
}
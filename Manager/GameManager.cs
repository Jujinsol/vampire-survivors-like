using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _inst;
    public Vector3 virtualPlayerPos = Vector3.zero;
    public GameObject player;
    public float catInitTime = 2.0f, arrowTime = 4.0f;
    public int arrowPower = 3, garlicPower = 3;

    public float level = 1, currentExp = 0, maxExp = 10;
    Slider sliderEXP;

    private void Awake()
    {
        _inst = this;
        player = GameObject.Find("Player").gameObject;
        sliderEXP = GameObject.Find("Canvas").transform.Find("SliderEXP").gameObject.GetComponent<Slider>();
        sliderEXP.maxValue = maxExp;
        sliderEXP.value = currentExp;

        for (int i = 0; i < 10; i++)
        {
            SpawnCat();
        }

        StartCoroutine(SpawnCatRoutine());
    }

    private void SpawnCat()
    {
        for (int i = 0; i < level; i++)
        {
            var newCat = ObjectPoolManager.instance.GetGo("Cat1");

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(5f, 20f);

            float randX = player.transform.position.x + Mathf.Cos(angle) * distance;
            float randY = player.transform.position.y + Mathf.Sin(angle) * distance;

            newCat.transform.position = new Vector3(randX, randY, 0f);
        }
    }

    private System.Collections.IEnumerator SpawnCatRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(catInitTime);
            SpawnCat();
        }
    }

    public void GetCoin()
    {
        currentExp += 5;
        sliderEXP.value = currentExp;

        if (currentExp >= maxExp)
            LevelUp();
    }

    void LevelUp()
    {
        LevelUpManager._inst.LevelUpUI();
        level += 1;
        currentExp = 0;
        maxExp = 50 + (level - 1) * 25;

        sliderEXP.maxValue = maxExp;
        sliderEXP.value = currentExp;
    }
}
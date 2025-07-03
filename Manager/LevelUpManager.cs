using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LevelUpManager;
using Random = UnityEngine.Random;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager _inst;
    GameObject LevelUPUI;
    Button BtnLevel1, BtnLevel2;

    public class Skill
    {
        public string name;
        public Sprite icon; // 버튼에 이미지 표시할 경우
        public System.Action effect; // 스킬 효과 함수
    }

    public List<Skill> skills = new List<Skill>();

    private void Start()
    {
        _inst = this;
        LevelUPUI = GameObject.Find("Canvas").transform.Find("imgLevelUP").gameObject;
        BtnLevel1 = LevelUPUI.transform.Find("BtnLevelUP1").GetComponent<Button>();
        BtnLevel2 = LevelUPUI.transform.Find("BtnLevelUP2").GetComponent<Button>();

        skills.Add(new Skill
        {
            name = "Speed Up",
            effect = () => GameManager._inst.player.GetComponent<PlayerController>()._speed += 1f
        });
        skills.Add(new Skill
        {
            name = "Power Up",
            effect = () => GameManager._inst.arrowPower += 1
        });
        skills.Add(new Skill
        {
            name = "Arrow Level Up",
            effect = () => GameManager._inst.arrowTime -= 0.5f
        });
    }

    public void LevelUpUI()
    {
        Time.timeScale = 0f;
        LevelUPUI.SetActive(true);

        List<Skill> randomSkills = skills.OrderBy(x => Random.value).Take(2).ToList();
        BtnLevel1.transform.Find("txt1").GetComponent<TextMeshProUGUI>().text = randomSkills[0].name;
        BtnLevel1.onClick.RemoveAllListeners();
        BtnLevel1.onClick.AddListener(() =>
        {
            randomSkills[0].effect.Invoke();
            Time.timeScale = 1f;
            LevelUPUI.SetActive(false);
        });

        BtnLevel2.transform.Find("txt2").GetComponent<TextMeshProUGUI>().text = randomSkills[1].name;
        BtnLevel2.onClick.RemoveAllListeners();
        BtnLevel2.onClick.AddListener(() =>
        {
            randomSkills[1].effect.Invoke();
            Time.timeScale = 1f;
            LevelUPUI.SetActive(false);
        });
    }
}

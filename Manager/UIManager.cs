using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI timerText;

    private float elapsedTime = 0f;

    void Start()
    {
        timerText = GameObject.Find("Canvas").transform.Find("txtTime").gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void GoStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}

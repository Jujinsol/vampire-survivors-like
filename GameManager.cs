using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _inst;
    public GameObject player;

    private void Awake()
    {
        _inst = this;
    }
}

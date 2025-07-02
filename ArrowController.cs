using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }
}

using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraController : MonoBehaviour
{
    public static CameraController _inst;
    GameObject background;
    float h, v, _speed = 7.0f;

    private void Start()
    {
        background = GameObject.Find("Grid").gameObject;
        _inst = this;
    }

    private void LateUpdate()
    {
        /*
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(h, v, 0f) * _speed * Time.deltaTime;
        
        background.transform.position -= move;
        GameManager._inst.virtualPlayerPos += move;
        */
        Camera.main.transform.position = new Vector3(GameManager._inst.player.transform.position.x, GameManager._inst.player.transform.position.y, -10);
    }

    public void MoveToFar()
    {
        if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Right)
        {
            background.transform.position += new Vector3(18f, 0, 0);
        }
        else if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Left)
        {
            background.transform.position += new Vector3(-18f, 0, 0);
        }
        else if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Up)
        {
            background.transform.position += new Vector3(0, 24f, 0);
        }
        else if (GameManager._inst.player.GetComponent<PlayerController>()._finalDir == Define.FinalDir.Down)
        {
            background.transform.position += new Vector3(0, -24f, 0);
        }
        //GameManager._inst.cat.transform.position = new Vector3(transform.position.x - GameManager._inst.cat.transform.position.x, transform.position.y - GameManager._inst.cat.transform.position.y, GameManager._inst.cat.transform.position.y);
    }
}
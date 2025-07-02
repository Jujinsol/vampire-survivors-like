using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private float _speed = 5.0f, shootCoolTime, attackTime = 3f;
    
    public Vector3 offset = new Vector3(0f, 0f, 20f);
    public FinalDir _finalDir;
    private MoveDir _dir;
    private GameObject _arrow;

    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;

            switch (value)
            {
                case MoveDir.Left:
                    _animator.SetTrigger("walk-side");
                    _animator.SetBool("idle", false);
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    _finalDir = FinalDir.Left;
                    break;
                case MoveDir.Right:
                    _animator.SetTrigger("walk-side");
                    _animator.SetBool("idle", false);
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    _finalDir = FinalDir.Right;
                    break;
                case MoveDir.Up:
                    _animator.SetTrigger("walk-back");
                    _animator.SetBool("idle", false);
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    _finalDir = FinalDir.Up;
                    break;
                case MoveDir.Down:
                    _animator.SetTrigger("walk-front");
                    _animator.SetBool("idle", false);
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    _finalDir = FinalDir.Down;
                    break;
                case MoveDir.None:
                    if (_finalDir == FinalDir.Left)
                    {
                        _animator.SetBool("idle", true);
                        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("/Sprites/hero/idle/hero-idle-side");
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    else if (_finalDir == FinalDir.Right)
                    {
                        _animator.SetBool("idle", true);
                        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("/Sprites/hero/idle/hero-idle-side");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_finalDir == FinalDir.Up)
                    {
                        _animator.SetBool("idle", true);
                        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("/Sprites/hero/idle/hero-idle-back");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        _animator.SetBool("idle", true);
                        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("/Sprites/hero/idle/hero-idle-front");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    break;
            }
            _dir = value;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _arrow = Resources.Load<GameObject>("Prefabs/arrow");
        shootCoolTime = 0.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        shootCoolTime += Time.deltaTime;
        Vector3 camPos = Camera.main.transform.position;
        transform.position = new Vector3(camPos.x, camPos.y, 0f);
        Moving();
    }

    private void Moving()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
        }
        else
        {
            Dir = MoveDir.None;
            _animator.SetTrigger("idle");
        }

        if (shootCoolTime > attackTime)
        {
            switch (_finalDir)
            {
                case FinalDir.Left:
                    _animator.SetTrigger("attack-side");
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case FinalDir.Right:
                    _animator.SetTrigger("attack-side");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case FinalDir.Up:
                    _animator.SetTrigger("attack-back");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case FinalDir.Down:
                    _animator.SetTrigger("attack-front");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
            }
            Shoot();
            shootCoolTime = 0.0f;
        }
    }

    void Shoot()
    {
        GameObject newArrow = null;
        Vector3 spawnOffset = Vector3.zero;
        float rotationZ = 0f;
        Vector2 forceDir = Vector2.zero;

        switch (_finalDir)
        {
            case FinalDir.Left:
                spawnOffset = new Vector3(-0.5f, -0.2f, 0f);
                rotationZ = 90f;
                forceDir = Vector2.left;
                break;
            case FinalDir.Right:
                spawnOffset = new Vector3(0.5f, -0.2f, 0f);
                rotationZ = -90f;
                forceDir = Vector2.right;
                break;
            case FinalDir.Up:
                spawnOffset = new Vector3(0f, 0.5f, 0f);
                rotationZ = 0f;
                forceDir = Vector2.up;
                break;
            case FinalDir.Down:
                spawnOffset = new Vector3(0f, -1f, 0f);
                rotationZ = 180f;
                forceDir = Vector2.down;
                break;
        }

        newArrow = Instantiate(_arrow, transform.position + spawnOffset, Quaternion.Euler(0, 0, rotationZ));
        newArrow.GetComponent<Rigidbody2D>().AddForce(forceDir * 1000.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}

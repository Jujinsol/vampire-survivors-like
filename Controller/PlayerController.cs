using UnityEngine;
using UnityEngine.UI;
using static Define;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    public float _speed = 5.0f, shootCoolTime, hurtTime, attackTime = 4.0f;
    
    public Vector3 offset = new Vector3(0f, 0f, 20f);
    public FinalDir _finalDir;
    private MoveDir _dir;

    public float currentHp = 100, maxHp = 100;
    Slider sliderHP;

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
        sliderHP = GameObject.Find("Canvas").transform.Find("SliderHP").gameObject.GetComponent<Slider>();
        sliderHP.maxValue = maxHp;
        sliderHP.value = currentHp;

        shootCoolTime = 0.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        shootCoolTime += Time.deltaTime;
        hurtTime += Time.deltaTime;

        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.7f, 0));
        sliderHP.transform.position = _hpBarPos;

        Moving();
    }

    private void Moving()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left;
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
            transform.position += Vector3.up * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
            transform.position += Vector3.down * Time.deltaTime * _speed;
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

        var newArrow = ObjectPoolManager.instance.GetGo("arrow");

        newArrow.transform.position = transform.position + spawnOffset;
        newArrow.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        var rb = newArrow.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(forceDir * 1000.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
            Hurt();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            CameraController._inst.MoveToFar();
    }

    void Hurt()
    {
        if (hurtTime > 3f)
        {
            currentHp -= 5;
            sliderHP.value = currentHp;
            hurtTime = 0f;
        }
    }
}

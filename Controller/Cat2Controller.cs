using UnityEngine;

public class Cat2Controller : MonsterController
{
    protected override void Start()
    {
        base.Start();

        _hp = 3.0f;
        _speed = 1.5f;
        _power = 4.0f;
    }
}

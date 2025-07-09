
public class Cat1Controller : MonsterController
{
    protected override void Start()
    {
        base.Start();

        _hp = 3.0f;
        _speed = 1.0f;
        _power = 3.0f;
    }
}

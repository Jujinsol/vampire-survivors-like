using System.Threading;
using UnityEngine;

public class CatController : MonsterController
{
    protected override void Start()
    {
        base.Start();

        _hp = 3;
    }
}

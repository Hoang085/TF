using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float Health { get; }
    void OnDamageTaken(float damage);
    void OnDead();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FroggerBaseState
{
    void Enter(Frogger frogger);
    void Exit();
    void Update();
    void OnTriggerEnter2D(Collider2D other);
}
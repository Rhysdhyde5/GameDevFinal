using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity 
    {
        Transform transform { get;}
        void Death();
        void AdvanceRow();
    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour, ITargetable
{
    public Transform Transform => transform;
}

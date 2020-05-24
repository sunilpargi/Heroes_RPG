using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Sphericalvector spherical_Vector_Data = new Sphericalvector(0, 0, 1);

    protected virtual void Update()
    {
        transform.position = spherical_Vector_Data.Position;
    }
}

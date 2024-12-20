using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public float RdmPercent()
    {
        return Random.Range(0f,100f);
    }

    public float Rolld(float nbSides)
    {
        return Random.Range(1f, nbSides);
    }
}

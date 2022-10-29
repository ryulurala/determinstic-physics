using System.Collections;
using System.Collections.Generic;
using Deterministic;
using UnityEngine;

public class CubeEx : MonoBehaviour
{
    public DCube DCube { get; set; }

    void Start()
    {

    }

    void Update()
    {
        if (DCube != null)
            DCube.DTransform.Snap();
    }
}

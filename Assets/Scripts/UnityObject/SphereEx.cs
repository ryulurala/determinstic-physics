using System.Collections;
using System.Collections.Generic;
using Deterministic;
using UnityEngine;

public class SphereEx : MonoBehaviour
{
    public DSphere DSphere { get; set; }

    void Start()
    {

    }

    void Update()
    {
        if (DSphere != null)
            DSphere.DTransform.Snap();
    }
}

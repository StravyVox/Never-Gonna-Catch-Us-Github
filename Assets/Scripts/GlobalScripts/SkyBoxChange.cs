using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChange : MonoBehaviour
{
    [SerializeField] Light lightObject;
    [SerializeField] Gradient lightAmbientGradient;
    [SerializeField] Gradient lightGradient;
    [SerializeField] Gradient HorizontLineGradient;
    [SerializeField] Gradient SkyTopGradient;
    [SerializeField] Gradient SkyBottomGradient;
    private Material skybox;
    [Range(0, 1f)]
    [SerializeField] float _start = 0;
    [Range(0.0001f, 0.1f)]
    [SerializeField] float _speed;
    void Awake()
    {
        skybox = RenderSettings.skybox;
        float _start = 0;
        lightObject.color = lightGradient.Evaluate(_start);
        RenderSettings.ambientLight = lightAmbientGradient.Evaluate(_start);

        skybox.SetColor("_HorizonLineColor", HorizontLineGradient.Evaluate(_start));

        skybox.SetColor("_SkyGradientTop", SkyTopGradient.Evaluate(_start));

        skybox.SetColor("_SkyGradientBottom", SkyBottomGradient.Evaluate(_start));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _start += _speed;
        lightObject.color = lightGradient.Evaluate(_start);
        RenderSettings.ambientLight = lightAmbientGradient.Evaluate(_start);
        //lightAmbientObject.color = lightGradient.Evaluate(_start);
        skybox.SetColor("_HorizonLineColor", HorizontLineGradient.Evaluate(_start));


        skybox.SetColor("_SkyGradientTop", SkyTopGradient.Evaluate(_start));

        skybox.SetColor("_SkyGradientBottom", SkyBottomGradient.Evaluate(_start));

        if (_start > 1)
        {
            _start = 0;
        }
    }
}

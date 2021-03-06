﻿using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour
{
    public Transform _bar;
    private float _scale = 1.0f;

    // Set the hp bar length(0f~1f)
    private void SetScale(float scale)
    {
        if (scale < 0)
            _scale = 0;
        else
            _scale = scale;
        _bar.localScale = new Vector3(_scale, 1f, 1f);
    }

    public float Scale
    {
        set { SetScale(value); }
        get { return _scale; }
    }
}

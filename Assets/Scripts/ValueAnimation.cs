using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueAnimation
{
    public float Value => curve.Evaluate(time / duration);
    public bool IsEnded => time == duration;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private float time;

    public void Start()
    {
        time = 0;
    }

    public void Update()
    {
        if (IsEnded)
            return;

        time = Mathf.Clamp(time + Time.deltaTime, 0, duration);
    }
}

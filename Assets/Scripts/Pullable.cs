using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pullable : MonoBehaviour
{
    private float lerpT = 0;
    public Vector3 startPos;
    public Vector3 endPos;

    public void SetLerp(int _t)
    {
        lerpT = _t;
        transform.position = Vector3.Lerp(startPos, endPos, lerpT);
    }
}

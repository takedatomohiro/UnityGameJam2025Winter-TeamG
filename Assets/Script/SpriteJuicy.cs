using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteJuicy : MonoBehaviour
{
    public Rigidbody2D parentRb;
    public Bowl parentBowl;
    public float rotateSmooth = 6f;     // 回転の遅れ

    float visualRotation;
    Vector3 baseScale;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (parentRb == null || parentBowl == null) return;
        if (!parentBowl.isDropped) return; // ★ 落とす前は回さない

        float angle = parentRb.angularVelocity * rotateSmooth * Time.deltaTime;
        transform.Rotate(0, 0, angle);
    }
}

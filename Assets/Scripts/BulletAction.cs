using UnityEngine;
using System.Collections;

public class BulletAction {
    public int timer;
    public bool relative;
    public float speed;
    public float angle;
    public float acceleration;
    public float maxSpeed;
    public float angularVelocity;

    // Most basic
    public BulletAction(int t, bool rel, float spd, float ang)
    {
        timer = t;
        relative = rel;
        speed = spd;
        angle = ang;
        acceleration = 0;
        maxSpeed = 0;
        angularVelocity = 0;
    }

    // Slightly more advanced
    public BulletAction(int t, bool rel, float spd, float ang, float acc, float max, float angv)
    {
        timer = t;
        relative = rel;
        speed = spd;
        angle = ang;
        acceleration = acc;
        maxSpeed = max;
        angularVelocity = angv;
    }
}

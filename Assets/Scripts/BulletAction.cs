using UnityEngine;
using System.Collections;

public class BulletAction {
    public int timer;
    public int type;
    public bool relative;
    public float speed;
    public float angle;
    public float acceleration;
    public float maxSpeed;
    public float angularVelocity;
    public BulletType newGraphic;

    // Most basic
    public BulletAction(int t, bool rel, float spd, float ang)
    {
        timer = t;
        type = 0;
        relative = rel;
        speed = spd;
        angle = ang;
    }

    // Slightly more advanced
    public BulletAction(int t, bool rel, float spd, float ang, float acc, float max, float angv)
    {
        timer = t;
        type = 1;
        relative = rel;
        speed = spd;
        angle = ang;
        acceleration = acc;
        maxSpeed = max;
        angularVelocity = angv;
    }

    // Change graphic
    public BulletAction(int t, BulletType bullet)
    {
        timer = t;
        newGraphic = bullet;
    }

}

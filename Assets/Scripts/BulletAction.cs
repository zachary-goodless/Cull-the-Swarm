using UnityEngine;
using System.Collections;

public class BulletAction : MonoBehaviour {
    public int timer;
    public bool relative;
    public float speed;
    public float angle;

    // Most basic
    public BulletAction(int t, bool rel, float spd, float ang)
    {
        timer = t;
        relative = rel;
        speed = spd;
        angle = ang;
    }
}

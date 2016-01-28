using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    public GameObject bulletPrefab;
    public int poolSize = 1000;
    public static PoolSystem bulletPool;
    public static GameObject shotID;

	// Use this for initialization
	void Start ()
    {
        gameObject.AddComponent<PoolSystem>();
        bulletPool = gameObject.GetComponent<PoolSystem>();
        bulletPool.Initialize(poolSize, bulletPrefab);
    }

    // Shoots a bullet from the specified position, with a certain speed and angle. The bullet is stored for subsequent AddActions.
    public static void ShootBullet(Vector2 position, float speed, float angle)
    {
        GameObject bt = bulletPool.GetRecycledObject();
        bt.SetActive(true);
        bt.GetComponent<Bullet>().Init(position, speed, angle);
        shotID = bt;
    }

    // Shoots a bullet with a few more parameters. The bullet is stored for subsequent AddActions.
    public static void ShootBullet(Vector2 position, float speed, float angle, float acc, float max, float angv)
    {
        GameObject bt = bulletPool.GetRecycledObject();
        bt.SetActive(true);
        bt.GetComponent<Bullet>().Init(position, speed, angle, acc, max, angv);
        shotID = bt;
    }

    // Adds an action to the queue of the last shot bullet.
    public static void AddAction(BulletAction action)
    {
        shotID.GetComponent<Bullet>().AddAction(action);
    }

    // Adds a list of action to the queue of the last shot bullet.
    public static void AddAction(List<BulletAction> actions)
    {
        foreach(BulletAction action in actions)
        {
            shotID.GetComponent<Bullet>().AddAction(action);
        }
    }

    // Adds an action to the queue of a specified bullet.
    public static void AddAction(GameObject shot, BulletAction action)
    {
        shot.GetComponent<Bullet>().AddAction(action);
    }

    // Adds a list of action to the queue of a specified bullet.
    public static void AddAction(GameObject shot, List<BulletAction> actions)
    {
        foreach (BulletAction action in actions)
        {
            shot.GetComponent<Bullet>().AddAction(action);
        }
    }

    // Deletes the specified bullet.
    public static void DeleteBullet(GameObject shot)
    {
        shot.GetComponent<Bullet>().Reset();
        bulletPool.Recycle(shot);
    }
}

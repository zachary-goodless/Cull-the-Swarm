using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    public GameObject bulletPrefab;
    PoolSystem bulletPool;

	// Use this for initialization
	void Start () {
        bulletPool = new PoolSystem(500, bulletPrefab);
	}

    public GameObject ShootBullet(Vector2 position, float speed, float angle)
    {
        GameObject bt = bulletPool.GetRecycledObject();
        bt.SetActive(true);
        bt.GetComponent<Bullet>().Init(position, speed, angle);
        return bt;
    }

    public void DeleteBullet(GameObject bt)
    {
        bt.GetComponent<Bullet>().Reset();
        bulletPool.Recycle(bt);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

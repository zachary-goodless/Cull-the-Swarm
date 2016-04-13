using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

    List<BulletAction> actionQueue;
    float speed = 0;
    float angle = 0;
    float acceleration = 0;
    float maxSpeed = 0;
    float angularVelocity = 0;
    bool stillSpawning = false;

    // Update is called once per frame
    void LateUpdate()
    {
		if(Time.timeScale != 1f) return;

        if (!stillSpawning) {

            // Pull the next action and decrement its timer. If the timer is 0, execute the action.
            if (actionQueue != null)
            {
                BulletAction currentAction = actionQueue[0];
                currentAction.timer--;

                if (currentAction.timer == 0)
                {
                    ExecuteAction(currentAction);
                    actionQueue.RemoveAt(0);

                    // Remove the queue when there's no actions left.
                    if (actionQueue.Count == 0)
                    {
                        actionQueue = null;
                    }

                }
            }

            // Moves the bullet and applies accelerations.
            if(acceleration > 0){
                speed = Mathf.Min(speed + acceleration, maxSpeed);
            } else if (acceleration < 0) {
                speed = Mathf.Max(speed + acceleration, maxSpeed);
            }
            angle += angularVelocity;

            transform.Translate(Mathf.Cos(Mathf.Deg2Rad * angle) * speed, Mathf.Sin(Mathf.Deg2Rad * angle) * speed, 0, Space.World);
        }

        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(currentEuler.x, currentEuler.y, angle));
        if (transform.position.x < -1000 || transform.position.x > 1000 || transform.position.y < -600 || transform.position.y > 600)
        {
            BulletManager.DeleteBullet(gameObject);
        }

    }

    // Apply the changes as specified in the action. If the action is relative, add the action to the existing values rather than replacing.
    public void ExecuteAction(BulletAction action) {
        if(action.type == 0)
        {
            if (action.relative)
            {
                speed += action.speed;
                angle += action.angle;
            } else
            {
                speed = action.speed;
                angle = action.angle;
            }
        }
        else if (action.type == 1)
        {
            if (action.relative)
            {
                speed += action.speed;
                angle += action.angle;
                acceleration += action.acceleration;
                maxSpeed += action.maxSpeed;
                angularVelocity += action.angularVelocity;
            }
            else
            {
                speed = action.speed;
                angle = action.angle;
                acceleration = action.acceleration;
                maxSpeed = action.maxSpeed;
                angularVelocity = action.angularVelocity;
            }
        } else if (action.type == 2)
        {
            SetGraphic(BulletManager.propertyList[action.newGraphic]);
        }
    }

    public void Init(Vector2 position, float spd, float ang)
    {
        transform.position = position;
        speed = spd;
        angle = ang;
        acceleration = 0;
        maxSpeed = 0;
        angularVelocity = 0;
        StartCoroutine(SpawnEffect());
    }

    public void Init(Vector2 position, float spd, float ang, float acc, float max, float angv)
    {
        transform.position = position;
        speed = spd;
        angle = ang;
        acceleration = acc;
        maxSpeed = max;
        angularVelocity = angv;
        StartCoroutine(SpawnEffect());
    }

    // Resets all variables so this bullet can be reused.
    public void Reset()
    {
        speed = 0;
        angle = 0;
        acceleration = 0;
        maxSpeed = 0;
        angularVelocity = 0;
        actionQueue = null;
    }

    IEnumerator SpawnEffect() {
        stillSpawning = true;
        CircleCollider2D hitbox = GetComponent<CircleCollider2D>();
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        hitbox.enabled = false;
        for(int i = 0; i < 5; i++) {
            transform.localScale = new Vector3(1f + i / 2f, 1f + i / 2f, 1);
            rend.material.color = new Color(1,1,1, i / 5f);
            yield return null;
        }
        transform.localScale = new Vector3(1,1,1);
        rend.material.color = new Color(1, 1, 1, 1);
        hitbox.enabled = true;
        stillSpawning = false;
    }

    // Add an action to the action queue.
    public void AddAction(BulletAction action)
    {
        if (action.timer <= 0)
        {
            ExecuteAction(action);
            return;
        }

        if (actionQueue == null)
        {
            actionQueue = new List<BulletAction>();
        }

        actionQueue.Add(action);
    }

    public void SetGraphic(BulletTypeProperties t) {

        GetComponent<CircleCollider2D>().radius = t.radius;
        GetComponent<SpriteRenderer>().sprite = t.graphicIndex;
        if (t.isAddBlend) {
            GetComponent<SpriteRenderer>().material = BulletManager.bulletMats[1];
        } else {
            GetComponent<SpriteRenderer>().material = BulletManager.bulletMats[0];
        }
    }
}
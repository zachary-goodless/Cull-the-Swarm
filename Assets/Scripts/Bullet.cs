using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

    List<BulletAction> actionQueue;
    float speed;
    float angle;
    float acceleration;
    float maxSpeed;
    float angularVelocity;

    // Update is called once per frame
    void Update()
    {
        // Moves the bullet and applies accelerations.
        transform.Translate(Mathf.Cos(angle)*speed,Mathf.Sin(angle)*speed,0);

        if (transform.position.x < -1000 || transform.position.x > 1000 || transform.position.y < -600 || transform.position.y > 600)
        {
            GameObject.FindGameObjectWithTag("BulletManager").GetComponent<BulletManager>().DeleteBullet(gameObject);
        }

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
    }

    // Apply the changes as specified in the action. If the action is relative, add the action to the existing values rather than replacing.
    void ExecuteAction(BulletAction action)
    {
        if (action.relative)
        {
            speed += action.speed;
            angle += action.angle;
        } else {
            speed = action.speed;
            angle = action.angle;
        }
    }

    public void Init(Vector2 position, float spd, float ang)
    {
        transform.position = position;
        speed = spd;
        angle = ang;
    }

    // Resets all variables so this bullet can be reused.
    public void Reset()
    {
        speed = 0;
        angle = 0;
        acceleration = 0;
        maxSpeed = 0;
        angularVelocity = 0;
    }

    // Add an action to the action queue.
    public void AddAction(BulletAction action)
    {
        if(action.timer == 0)
        {
            ExecuteAction(action);
        }

        if (actionQueue == null)
        {
            actionQueue = new List<BulletAction>();
        }

        actionQueue.Add(action);
    }
}
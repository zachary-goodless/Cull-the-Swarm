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
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(currentEuler.x, currentEuler.y, angle));
        if (transform.position.x < -1000 || transform.position.x > 1000 || transform.position.y < -600 || transform.position.y > 600)
        {
            BulletManager.DeleteBullet(gameObject);
        }

    }

    // Apply the changes as specified in the action. If the action is relative, add the action to the existing values rather than replacing.
    public void ExecuteAction(BulletAction action)
    {
        if (action.relative)
        {
            speed += action.speed;
            angle += action.angle;
            acceleration += action.acceleration;
            maxSpeed += action.maxSpeed;
            angularVelocity += action.angularVelocity;
        } else {
            speed = action.speed;
            angle = action.angle;
            acceleration = action.acceleration;
            maxSpeed = action.maxSpeed;
            angularVelocity = action.angularVelocity;
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
    }

    public void Init(Vector2 position, float spd, float ang, float acc, float max, float angv)
    {
        transform.position = position;
        speed = spd;
        angle = ang;
        acceleration = acc;
        maxSpeed = max;
        angularVelocity = angv;
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

    // Add an action to the action queue.
    public void AddAction(BulletAction action)
    {
        if (action.timer == 0)
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
}
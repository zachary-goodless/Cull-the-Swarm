using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueenPatterns : MonoBehaviour {

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

	// Use this for initialization
	void Start ()
    {
        b = GetComponent<Boss>();
        b.tiltEnabled = false;
        b.isQueen = true;
        attackQueue = new List<int>();
        StartCoroutine(FlyIn());
    }
	
	// Update is called once per frame
	void Update () {
        if(b.phase != currentPhase)
        {
            phaseJustChanged = true;
            currentPhase = b.phase;
        }
	}

    IEnumerator FlyIn()
    {
        for(int i = 0; i < 480; i++)
        {
            transform.Translate(new Vector3(0, 1000f / 480f, 0));
            yield return null;
        }
        ChooseRandomPattern();
    }

    void ChooseRandomPattern()
    {
        if(b.health < 0) { return; }
        int chosenAttack;

        if (attackQueue.Count == 0 || phaseJustChanged) {
            attackQueue.Clear();
            Debug.Log("Refreshing attack queue for phase " + currentPhase);
            if (currentPhase == 0){
                attackQueue.Add(1);
            } else if (currentPhase == 1){
                attackQueue.Add(1);
            }
            else if (currentPhase == 2){
                attackQueue.Add(1);
            }
            else if (currentPhase == 3){
                attackQueue.Add(1);
            }
        }

        Debug.Log("Queue contents:");
        foreach(int num in attackQueue)
        {
            Debug.Log(num);
        }

        if (phaseJustChanged)   // Always do the new attack first when phase changes
        {
            chosenAttack = attackQueue[attackQueue.Count-1];
            phaseJustChanged = false;
        } else {
            chosenAttack = attackQueue[Random.Range(0, attackQueue.Count)];
        }

        Debug.Log("Chose " + chosenAttack);

        attackQueue.Remove(chosenAttack);

        switch (chosenAttack)
        {
            case 1:
                StartCoroutine(Pattern1());
                break;
        }

    }

    // Shoots waving pattern of green dots from two sides
    IEnumerator Pattern1()
    {

        for(int i = 0; i < 3; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            b.MoveRandomly();

            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(1f);
        ChooseRandomPattern();
        yield break;
    }
}

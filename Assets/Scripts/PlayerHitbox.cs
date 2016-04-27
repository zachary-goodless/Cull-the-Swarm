using UnityEngine;
using System.Collections;

public class PlayerHitbox : MonoBehaviour {
    
    float visibleRatio = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Precision") || Input.GetButton("XBOX_LB")) {
            if(visibleRatio < 0.98f) {
                visibleRatio = visibleRatio * 0.8f + 0.2f;
            } else {
                visibleRatio = 1;
            }
        } else {
            if (visibleRatio > 0.02f) {
                visibleRatio = visibleRatio * 0.8f;
            } else {
                visibleRatio = 0;
            }
        }

        GetComponent<SpriteRenderer>().color = new Color(1,1,1, visibleRatio);
        transform.Rotate(new Vector3(0, 0, 6));
        transform.localScale = new Vector3(3.2f - visibleRatio * 2f, 3.2f - visibleRatio * 2f, 1);
	}


}

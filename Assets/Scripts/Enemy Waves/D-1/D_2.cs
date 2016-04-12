using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class D_2 : MonoBehaviour
{

    public Waves waves;
    public ScreenFade sf;
    public LevelCompleteHandler finished;
    public GameObject drone;
    public GameObject missile;
    public GameObject turret;

    void Start()
    {
        StartCoroutine("LevelLayout2");
        sf = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
    }

    IEnumerator LevelLayout2()
    {
        // Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
        yield return new WaitForSeconds(5f);
        waves.StartSpawnOsc(drone, 3, 0, 300, 200, false, true, 200, 0, -1, 50, -500, waves.upScreen + 150, Quaternion.identity, 500, 0, "d23");

        Debug.Log(waves.leftScreen + " " + waves.upScreen);
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        waves.StartSpawnOsc(turret, 2, 0, 100, 200, false, false, 200, 0, -1, 50, -250, waves.upScreen, Quaternion.identity, 500, 0, "t2");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(drone, 5, 1, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0, "d1");
        yield return new WaitForSeconds(9f);
        waves.StartLR6Alternate(turret);
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, 1, 0, 50, -1000, 0, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, -1, 0, 50, 1000, 0, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(6.5f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(8f);
        waves.StartSpawnLinear(turret, 2, 1.5f, 160, 0, -1, 60, -300, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        waves.StartSpawnLinear(drone, 3, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d4");
        waves.StartSpawnLinear(turret, 2, 1.5f, 160, 0, -1, 60, 300, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        yield return new WaitForSeconds(10f);
        waves.StartFourFromBelow(drone);
        yield return new WaitForSeconds(9f);
        waves.StartSpawnDive(turret, 5, 3, 300, 10, 100, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 0, 200, "t1");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(turret, 2, 4, 350, -1, 1, 50, waves.rightScreen, waves.downScreen, Quaternion.identity, 0, 0, "t2");
        waves.StartSpawnLinear(turret, 2, 4, 350, 1, 1, 50, waves.leftScreen, waves.downScreen, Quaternion.identity, 0, 0, "t3");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(drone, 4, 4, 350, -1, -1, 50, waves.rightScreen, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnLinear(drone, 4, 4, 350, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(30f);
        sf.Fade();
        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}

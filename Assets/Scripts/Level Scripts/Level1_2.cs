using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1_2 : MonoBehaviour
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
        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d1");
       yield return new WaitForSeconds(2f);
        waves.StartSpawnLCornerCircle(turret, 0, 0, 250, 1, 50, "t3");
        waves.StartSpawnRCornerCircle(turret, 0, 0, 250, 1, 50, "t3");
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        waves.StartSpawnOsc(turret, 2, 0, 100, 200, false, false, 200, 0, -1, 50, -250, waves.upScreen, Quaternion.identity, 500, 0, "t2");
        waves.StartSpawnLinear(drone, 3, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d23");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d23");
        yield return new WaitForSeconds(9f);
        waves.StartLR6Alternate(turret);
        waves.StartSpawnLinear(turret, 3, 2, 180, 0, -1, 60, -500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        waves.StartSpawnLinear(turret, 3, 2, 180, 0, -1, 60, 500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, 1, 0, 50, -1000, 0, Quaternion.identity, 0, 0, "d3");
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, -1, 0, 50, 1000, 0, Quaternion.identity, 0, 0, "d3");
        yield return new WaitForSeconds(6.5f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(8f);
        waves.StartSpawnLinear(turret, 2, 1.5f, 160, 0, -1, 60, -400, waves.upScreen, Quaternion.identity, 0, 0, "t2");
        waves.StartSpawnLinear(drone, 3, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d4");
        waves.StartSpawnLinear(turret, 2, 1.5f, 160, 0, -1, 60, 400, waves.upScreen, Quaternion.identity, 0, 0, "t2");
        yield return new WaitForSeconds(10f);
        waves.StartFourFromBelow(drone);
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, 1, 0, 50, -1000, 0, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, -1, 0, 50, 1000, 0, Quaternion.identity, 0, 0, "d5");
        waves.FiveWormsTop();
        yield return new WaitForSeconds(10f);
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, -1, 0, 50, 1200, 0, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, 1, 0, 50, -1200, 0, Quaternion.identity, 0, 0, "d5");
        yield return new WaitForSeconds(9f);
        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d6");
        yield return new WaitForSeconds(3f);
        waves.StartSpawnInnerCircle(turret, 0, 0, 180, 1, 50, "t3");
        yield return new WaitForSeconds(3f);
        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d1");
        waves.StartSpawnLCornerCircle(turret, 0, 5, 200, 1, 50, "t1");
        yield return new WaitForSeconds(10f);
        waves.StartDoubleWorms();
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d23");
        waves.StartBigW(drone);
        waves.StartFlanktasticFour(drone);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(turret, 0, 0, 200, 1, 50, "d4");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d4");
        waves.StartLR6Alternate(turret);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d1");
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d1");
        yield return new WaitForSeconds(2f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d23");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d23");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d6");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d6");
        waves.StartSpawnFollow(drone, 5, 1.5f, .05f, 0, 200, 1, 50, 600, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, -300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnLinear(drone, 5, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, 300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnFollow(drone, 5, 1.5f, -.05f, 0, 200, 1, 50, -600, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d1");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d1");
        waves.StartSpawnFollow(drone, 5, 1.5f, .05f, 0, 200, 1, 50, 600, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, -300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnLinear(drone, 5, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, 300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnFollow(drone, 5, 1.5f, -.05f, 0, 200, 1, 50, -600, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(20f);
        sf.Fade();
        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}

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
        waves.StartFlanktasticFour(drone);
        Debug.Log(waves.leftScreen + " " + waves.upScreen);
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(drone, 5, 1, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0);
        yield return new WaitForSeconds(9f);
        waves.StartLR6Alternate(turret);
        yield return new WaitForSeconds(6.5f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(8f);
        waves.TwoWormsBelow();
        waves.StartDoubleWorms();
        yield return new WaitForSeconds(10f);
        waves.StartFourFromBelow(drone);
        yield return new WaitForSeconds(9f);
        waves.StartSpawnDive(turret, 5, 3, 300, 10, 100, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 0, 200);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(turret, 2, 4, 350, -1, 1, 50, waves.rightScreen, waves.downScreen, Quaternion.identity, 0, 0);
        waves.StartSpawnLinear(turret, 2, 4, 350, 1, 1, 50, waves.leftScreen, waves.downScreen, Quaternion.identity, 0, 0);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(drone, 4, 4, 350, -1, -1, 50, waves.rightScreen, waves.upScreen, Quaternion.identity, 0, 0);
        waves.StartSpawnLinear(drone, 4, 4, 350, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 0, 0);
        yield return new WaitForSeconds(30f);
        sf.Fade();
        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}

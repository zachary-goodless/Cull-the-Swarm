using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class D_1 : MonoBehaviour
{

    public Waves waves;
    public ScreenFade sf;
    public LevelCompleteHandler finished;
    public GameObject drone;
    public GameObject missile;
    public GameObject turret;

    void Start()
    {
        StartCoroutine ("LevelLayout2");
        sf = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
    }

    IEnumerator LevelLayout2()
    {
      // Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
        yield return new WaitForSeconds(5f);
        waves.StartSpawnLinear(drone, 5, 2, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0);
        waves.StartSpawnLinear(drone, 2, 2, 200, -1, -1, 50, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0);
        Debug.Log(waves.leftScreen + " " + waves.upScreen);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnWorms(3, 5, 3, .5f, 30, 200, 1, 50, waves.leftScreen, 100, Quaternion.Euler(0, 0, 90), 0, -200);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnFollow(turret, 3, 4, .5f, 30, 150, -1, 200, 5, waves.downScreen, Quaternion.identity, 0, 0);
        yield return new WaitForSeconds(5f);
        waves.StartLR6Alternate(drone);
        yield return new WaitForSeconds(6.5f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(8f);
        waves.StartSpawnFromBackground(drone, 3, 10f, 2f, Vector3.down, 10f, 1, 1, 200, waves.leftScreen, waves.downScreen, Quaternion.identity, 200, 0);
        waves.TwoWormsBelow();
        yield return new WaitForSeconds(4f);
        waves.StartSpawnSideToBottom(turret, 5, 2f, 2f, 4f, 100, -100, 200, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0);
        waves.StartSpawnSideToBottom(turret, 5, 3f, 4f, 5f, -100, -100, 200, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0);
        yield return new WaitForSeconds(6f);
        waves.StartSpawnSideToBottom(turret, 5, 2f, 3f, 5f, -100, -100, 200, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0);
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        yield return new WaitForSeconds(9f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(10f);
        waves.FiveWormsTop();
        yield return new WaitForSeconds(20f);
        waves.StartLR6Alternate(turret);
        waves.StartDoubleWorms();
        sf.Fade();
        yield return new WaitForSeconds(2f);
        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}

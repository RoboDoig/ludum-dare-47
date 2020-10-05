using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<TimeLoopObject> timeLoopObjects;

    public float totalLevelTime;

    private float levelTimer;
    public string nextLevel;

    void Start() {
        levelTimer = totalLevelTime;
    }

    void Update() {
        levelTimer -= Time.deltaTime;

        if (levelTimer <= 0) {
            levelTimer = totalLevelTime;

            foreach (TimeLoopObject tObject in timeLoopObjects) {
                tObject.Reset();
            }
        }
    }

    public float GetLevelTimer() {
        return levelTimer;
    }

    public void LevelEnd() {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}

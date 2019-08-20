using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameModeIntEvent : UnityEvent<int>
{ }
public class GameMode : MonoBehaviour
{
    public static GameMode instance;
    public GameObject ballPrefab;
    public int spawnBallForEveryBrickDestroyed = 3;

    public int winSceneIndex;
    public int loseSceneIndex;

    private int ballsInPlay;
    private int starsInPlay;

    public GameModeIntEvent onBallsChanged;
    public GameModeIntEvent onStarsChanged;
    private void Awake()
    {
        //Assert.IsNotNull(instance, "GameMode instance already exists");
        //Assert.IsNull(ballPrefab, "No ball prefab assigneed");
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    public void OnBallAdded()
    {
        ballsInPlay++;
        onBallsChanged.Invoke(ballsInPlay);
    }

    public void OnBallRemoved()
    {
        ballsInPlay--;
        onBallsChanged.Invoke(ballsInPlay);
        if (ballsInPlay <= 0)
        {
            SceneManager.LoadScene(loseSceneIndex);
        }
    }

    public void OnStarsAdded()
    {
        starsInPlay++;
        onStarsChanged.Invoke(starsInPlay);
    }

    public void OnStarsRemoved()
    {
        starsInPlay--;
        onStarsChanged.Invoke(starsInPlay);
        if (starsInPlay <= 0)
        {
            SceneManager.LoadScene(winSceneIndex);
        }
    }
}

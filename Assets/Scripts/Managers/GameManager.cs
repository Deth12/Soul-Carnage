using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public PlayableDirector InitialCutscene;

    public bool CanEnemyMove = false;
    public bool IsStartCutsceneFinished = false;
    public bool IsGameStarted = false;
    
    public Action OnCutsceneStart;
    public Action OnGameStart;

    public Transform Player { get; private set; }

    public void PlayInitialCutscene()
    {
        CanEnemyMove = true;
        InitialCutscene.Play();
        OnCutsceneStart?.Invoke();
    }
    
    public void StartGame()
    {
        OnGameStart?.Invoke();
        IsStartCutsceneFinished = true;
        IsGameStarted = true;
        UIManager.Instance.ShowGameOverlay(true);;
        InputManager.Instance.StartGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager.Instance.HidePauseMenu(IsStartCutsceneFinished);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowPauseMenu();
    }

    public void RestartGame()
    {
        IsGameStarted = false;
        Time.timeScale = 1;
        Fader.Instance.FadeIn(0.5f, delegate { SceneManager.LoadScene(1); });
    }
    
    [Header("Score spawn")] 
    public int minScoreSpawnAmount = 0;
    public int maxScoreSpawnAmount = 3;
    public float scoreDisappearDistance = 180f;
    
}

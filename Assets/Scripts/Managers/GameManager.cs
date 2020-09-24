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
        AudioManager.Instance.SwitchMainClip(AudioManager.Instance.gameTheme, 1.4f);

    }
    
    public void StartGame()
    {
        OnGameStart?.Invoke();
        IsStartCutsceneFinished = true;
        IsGameStarted = true;
        WeatherSystem.Instance.Activate();
        UIManager.Instance.ShowGameOverlay(true);;
        InputManager.Instance.StartGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager.Instance.HidePauseMenu(IsStartCutsceneFinished);
        AudioManager.Instance.ResumeMainClip();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowPauseMenu();
        AudioManager.Instance.PauseMainClip();
    }

    public void RestartGame()
    {
        IsGameStarted = false;
        Fader.Instance.FadeIn(0.5f, delegate 
        { 
            SceneManager.LoadScene(1); 
            AudioManager.Instance.SwitchMainClip(AudioManager.Instance.mainMenuTheme, 0.8f);
            GameProfile.Score = 0;
            Time.timeScale = 1;
        });
    }
    
    [Header("Score spawn")] 
    public int minScoreSpawnAmount = 0;
    public int maxScoreSpawnAmount = 3;
}

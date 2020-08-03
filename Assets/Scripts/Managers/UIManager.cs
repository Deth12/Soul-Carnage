using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
    }

    // TODO: CREATE AUDIO MANAGER
    public AudioClip buttonClickSound;

    [Header("Panels")]
    [SerializeField] UI_Panel startPanel = null;
    [SerializeField] UI_Panel pausePanel = null;
    [SerializeField] UI_Panel bottomPanel = null;
    [SerializeField] UI_Panel topPanel = null;
    [SerializeField] UI_Panel settingsPanel = null;
    [SerializeField] UI_Panel shopPanel = null;
    [SerializeField] UI_Panel endScreen = null;

    [Header("Buttons")] 
    [SerializeField] UI_StartButton startButton = null;
    
    [SerializeField] GameObject middlePanel;
    [SerializeField] Text scoreCounter = null;
    [SerializeField] Text endScreenScoreCounter = null;
    [SerializeField] Text totalScore = null;
    [SerializeField] Text soulsCounter = null;

    [SerializeField] Image soulsBarMask = null;

    [SerializeField] Text title = null;
    
    private void OnEnable()
    {
        GameProfile.OnScoreChange += UpdateScore;
        GameProfile.OnSoulsChange += UpdateSouls;
        UpdateTotalScore(GameProfile.TotalKills);
        UpdateSouls(GameProfile.Souls);
    }
    
    private void Start()
    {
        LeanTween
            .alphaText(title.rectTransform, 1f, 1f)
            .setFrom(0f)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                Fader.Instance.FadeOut(1f);
            });
    }

    public void HideTitle()
    {
        LeanTween.alphaText(title.rectTransform, 0f, 0.3f).setFrom(1f).setIgnoreTimeScale(true);
    }

    public void ShowTitle()
    {
        LeanTween.alphaText(title.rectTransform, 1f, 0.3f).setFrom(0f).setIgnoreTimeScale(true);
    }

    public void ShowStartPanel()
    {
        settingsPanel.IsHidden = true;
        if (!shopPanel.IsHidden)
        {
            CameraManager.instance.ChangeCameraMode(CameraMode.mode_default);
            shopPanel.IsHidden = true;
        }
        if (!GameManager.Instance.IsGameStarted)
        {
            ShowTitle();
            startPanel.IsHidden = false;
        }
        else
            ShowPauseMenu();
        startButton.Show();
    }
    
    public void HideStartPanel()
    {
        HideTitle();
        startPanel.IsHidden = true;
    }
    
    public void ShowGameOverlay(bool showControlButtons)
    {
        topPanel.IsHidden = false;
        if (showControlButtons)
            bottomPanel.IsHidden = false;
    }

    public void ShowPauseMenu()
    {
        bottomPanel.IsHidden = true;
        topPanel.IsHidden = true;
        pausePanel.IsHidden = false;
    }

    public void HidePauseMenu(bool showControlButtons)
    {
        pausePanel.IsHidden = true;
        topPanel.IsHidden = false;
        if (showControlButtons)
            bottomPanel.IsHidden = false;
    }
    
    public void ShowEndScreen()
    {
        topPanel.IsHidden = true;
        bottomPanel.IsHidden = true;
        endScreen.IsHidden = false;
    }

    public void OpenSettings()
    {
        startPanel.IsHidden = true;
        pausePanel.IsHidden = true;

        if(!GameManager.Instance.IsGameStarted)
            HideTitle();
        
        startButton.Hide();
        settingsPanel.IsHidden = false;
    }

    public void OpenShop()
    {
        startPanel.IsHidden = true;
        CameraManager.instance.ChangeCameraMode(CameraMode.mode_inShop);

        if(!GameManager.Instance.IsGameStarted)
            HideTitle();
        
        startButton.Hide();
        shopPanel.IsHidden = false;
    }
    
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
    
    #region Counters Updaters
    
    public void UpdateScore(int value)
    {
        scoreCounter.text = value.ToString();
        endScreenScoreCounter.text = value.ToString();
    }
    
    public void UpdateSouls(int value)
    {
        soulsCounter.text = value.ToString();
    }

    public void UpdateTotalScore(int value)
    {
        totalScore.text = value.ToString();
    }
    
    public void UpdateSoulBar(float normalizedValue)
    {
        soulsBarMask.fillAmount = normalizedValue;
    }
    
    #endregion

    #region Buttons
    
    public void ButtonHold(Animator anim)
    {
        anim.SetBool("isPressed", true);
    }

    public void ButtonRelease(Animator anim)
    {
        anim.SetBool("isPressed", false);
    }

    public void ButtonClick(Animator anim)
    {
        anim.SetTrigger("isClicked");
    }

    public void ButtonClickSound()
    {
        if(buttonClickSound != null)
            AudioManager.Instance.PlayOneShot(buttonClickSound, 0.4f);
    }
    
    #endregion

    private void OnDisable()
    {
        GameProfile.OnScoreChange -= UpdateScore;
        GameProfile.OnSoulsChange -= UpdateSouls;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public enum StartButtonStates
{
    menu, minimized_game, menu_pause
}

public class UI_StartButton : MonoBehaviour, IPointerClickHandler //IPointerDownHandler, IPointerUpHandler, 
{
    private StartButtonStates state;
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    private void StartGame()
    {
        state = StartButtonStates.minimized_game;
        anim.SetTrigger("Minimize");
        UIManager.Instance.HideStartPanel();
        GameManager.Instance.PlayInitialCutscene();
    }
    
    private void ResumeGame()
    {
        state = StartButtonStates.minimized_game;
        anim.SetTrigger("Minimize");
        GameManager.Instance.ResumeGame();
    }

    public void PauseGame()
    {
        state = StartButtonStates.menu_pause;
        anim.SetTrigger("Maximize");
        GameManager.Instance.PauseGame();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (state)
        {
            case StartButtonStates.menu:
                StartGame();
                break;
            case StartButtonStates.minimized_game:
                PauseGame();
                break;
            case  StartButtonStates.menu_pause:
                ResumeGame();
                break;
        }
    }

    public void Hide()
    {
        anim.SetBool("isHidden", true);
    }

    public void Show()
    {
        anim.SetBool("isHidden", false);
    }
}

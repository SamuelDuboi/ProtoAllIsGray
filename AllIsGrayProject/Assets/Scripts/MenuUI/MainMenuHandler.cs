using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuHandler : MonoBehaviour
{
    enum MenuState { StartScreen, MainMenu, PlayerJoin, StageSelection, GameRules, Settings};

    [SerializeField, ReadOnly]
    MenuState _currentMenuState;
    MenuState CurrentMenuState
    {
        get { return _currentMenuState; }
        set
        {
            switch (value)
            {
                case MenuState.StartScreen:
                    InitStartScreenState();
                    break;
                case MenuState.MainMenu:
                    InitMainMenuState();
                    break;
                case MenuState.PlayerJoin:
                    break;
                case MenuState.StageSelection:
                    break;
                case MenuState.GameRules:
                    break;
                case MenuState.Settings:
                    break;
                default:
                    break;
            }

            _currentMenuState = value;
        }
    }

    DefaultController controls;
    [SerializeField]
    EventSystem eventSystem;

    [SerializeField]
    StartScreen startScreenElements;
    [SerializeField]
    MainMenu mainMenuElements;


    private void Awake()
    {
        controls = new DefaultController();
        controls.UIControls.Enable();
        CurrentMenuState = MenuState.StartScreen;
        eventSystem.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        controls.UIControls.Disable();
    }

    #region StartScreen
    private void InitStartScreenState()
    {
        controls.UIControls.Start.performed += StartScreenTranstionBegin;
        startScreenElements.GlowingText();
    }

    public void StartScreenTranstionBegin(InputAction.CallbackContext context)
    {
        controls.UIControls.Start.performed -= StartScreenTranstionBegin;
        startScreenElements.TransitionEnds += StartScreenTranstionEnd;
        startScreenElements.Transition();

    }

    public void StartScreenTranstionEnd()
    {
        startScreenElements.TransitionEnds -= StartScreenTranstionEnd;
        CurrentMenuState = MenuState.MainMenu;

    }
    #endregion

    #region MainMenu
    private void InitMainMenuState()
    {
        mainMenuElements.AppearAnimation();
        mainMenuElements.AppearEnds += EnableMainMenu;
    }

    public void EnableMainMenu()
    {
        mainMenuElements.AppearEnds -= EnableMainMenu;
        eventSystem.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(mainMenuElements.ReturnFirstButton());
    }

    public void OnNewGameCliked()
    {
        print("Start a new game");
    }

    public void OnSettingsClicked()
    {
        print("Go to settings");
    }

    public void OnQuitClicked()
    {
        print("Quit");
        Application.Quit();
    }

    #endregion

}

[Serializable]
class StartScreen
{
    [SerializeField]
    RectTransform startScreenRoot;
    [SerializeField]
    Image leftPanel;
    [SerializeField]
    Image rightPanel;
    [SerializeField]
    Image titleImage;
    [SerializeField]
    TextMeshProUGUI indicationText;

    [Space]
    [SerializeField]
    RectTransform leftPanelTarget;
    [SerializeField]
    RectTransform rightPanelTarget;

    [Space]
    [SerializeField]
    float glowLength;
    [SerializeField]
    AnimationCurve glowCurve;

    [Space]
    [SerializeField]
    float transitionDuration;
    [SerializeField]
    AnimationCurve panelsProgressionCurve;
    [SerializeField]
    AnimationCurve titleFadeCurve;

    public Action TransitionEnds;

    public void GlowingText()
    {
        var transparent = indicationText.color;
        transparent.a = 0;

        indicationText.DOColor(transparent, glowLength)
            .SetEase(glowCurve)
            .SetLoops(-1, LoopType.Yoyo);

        indicationText.transform.DOScale(Vector3.one * 0.80f, glowLength)
            .SetEase(glowCurve)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Transition()
    {
        indicationText.DOKill();
        indicationText.gameObject.SetActive(false);

        var titleTransparent = titleImage.color;
        titleTransparent.a = 0;

        titleImage.DOColor(titleTransparent, transitionDuration / 2)
            .SetEase(titleFadeCurve);

        leftPanel.rectTransform.DOMove(leftPanelTarget.position, transitionDuration)
            .SetEase(panelsProgressionCurve);
            
        rightPanel.rectTransform.DOMove(rightPanelTarget.position, transitionDuration)
            .SetEase(panelsProgressionCurve)
            .OnComplete(CallTransitionEnd);
    }

    void CallTransitionEnd()
    {
        TransitionEnds?.Invoke();
        startScreenRoot.gameObject.SetActive(false);
    }
    
}

[Serializable]
class MainMenu
{
    [SerializeField]
    RectTransform mainMenuRoot;

    [SerializeField]
    Image title;
    [SerializeField]
    GameObject newGameButton;
    [SerializeField]
    GameObject settingsButton;
    [SerializeField]
    GameObject quitButton;

    [Space]
    [SerializeField]
    RectTransform titleTarget;
    [SerializeField]
    RectTransform newGameButtonTarget;
    [SerializeField]
    RectTransform settingsButtonTarget;
    [SerializeField]
    RectTransform quitButtonTarget;

    Vector2 titleBasePos; 
    Vector2 newGameButtonBasePos; 
    Vector2 settingsButtonBasePos; 
    Vector2 quitButtonBasePos; 

    [Space]
    [SerializeField]
    float appearDuration;
    [SerializeField]
    AnimationCurve appearCurve;
    [SerializeField]
    float buttonAppearDelay;

    public Action AppearEnds;

    public GameObject ReturnFirstButton()
    {
        return newGameButton.gameObject;
    }

    public void AppearAnimation()
    {
        titleBasePos = title.rectTransform.position;
        newGameButtonBasePos = newGameButton.transform.position;
        settingsButtonBasePos = settingsButton.transform.position;
        quitButtonBasePos = quitButton.transform.position;

        mainMenuRoot.gameObject.SetActive(true);

        title.transform.DOMove(titleTarget.position, appearDuration)
            .SetEase(appearCurve);

        newGameButton.transform.DOMove(newGameButtonTarget.position, appearDuration)
            .SetEase(appearCurve)
            .SetDelay(buttonAppearDelay * 0f);

        settingsButton.transform.DOMove(settingsButtonTarget.position, appearDuration)
            .SetEase(appearCurve)
            .SetDelay(buttonAppearDelay * 1f);

        quitButton.transform.DOMove(quitButtonTarget.position, appearDuration)
            .SetEase(appearCurve)
            .SetDelay(buttonAppearDelay * 2f)
            .OnComplete(OnAppearEnds);
    }

    public void OnAppearEnds() => AppearEnds?.Invoke();

}
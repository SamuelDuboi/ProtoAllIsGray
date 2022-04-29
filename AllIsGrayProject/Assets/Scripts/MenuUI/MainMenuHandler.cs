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
    enum MenuState { StartScreen, MainMenu, PlayerJoin, StageSelection, GameRules, Settings };

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
                    InitPlayerJoinState();
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
    [SerializeField]
    PlayerJoin playerJoinElements;


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
        eventSystem.gameObject.SetActive(false);
        mainMenuElements.DisappearAnimation();
        mainMenuElements.DisappearEnds += () => 
        {
            CurrentMenuState = MenuState.PlayerJoin;
            mainMenuElements.DisappearEnds = null;
        };
    }

    public void OnSettingsClicked()
    {
        eventSystem.gameObject.SetActive(false);
        mainMenuElements.DisappearAnimation();
        mainMenuElements.DisappearEnds += () => print("Go to settings"); ;
    }

    public void OnTutoClicked()
    {
        eventSystem.gameObject.SetActive(false);
        mainMenuElements.DisappearAnimation();
        mainMenuElements.DisappearEnds += () => print("Go to tutorial"); ;
    }

    public void OnQuitClicked()
    {
        print("Quit");
        Application.Quit();
    }

    #endregion

    #region PlayerJoin

    private void InitPlayerJoinState()
    {
        playerJoinElements.AppearEnds += EnablePlayerJoin;
        playerJoinElements.Appear();
    }

    public void EnablePlayerJoin()
    {
        playerJoinElements.AppearEnds -= EnablePlayerJoin;

        controls.UIControls.MenuValidate.performed += PlayerJoinValidate;
        controls.UIControls.MenuReturn.performed += PlayerJoinReturn;
    }

    public void PlayerJoinValidate(InputAction.CallbackContext context)
    {
        ref var usedDevices = ref GameManager.Instance.playerDevices;

        if (!usedDevices.Contains(context.control.device) && usedDevices.Count < 5)
        {
            usedDevices.Add(context.control.device);
            playerJoinElements.AddNewPlayer(context.control.device);
            UpdateGoToStage();

        }
        else if(usedDevices.Contains(context.control.device))
        {
            playerJoinElements.SetPlayerReady(context.control.device);
            UpdateGoToStage();
        }
    }

    public void PlayerJoinReturn(InputAction.CallbackContext context)
    {
        playerJoinElements.Disappear();
        playerJoinElements.DisappearEnds += () =>
        {
            CurrentMenuState = MenuState.MainMenu;
            playerJoinElements.DisappearEnds = null;
        };
    }

    public void UpdateGoToStage()
    {
        if (playerJoinElements.CanGoToStage())
        {
            controls.UIControls.Start.performed += GoToStageSelection;
            playerJoinElements.UpdateGoToStage(true);
        }
        else
        {
            controls.UIControls.Start.performed -= GoToStageSelection;
            playerJoinElements.UpdateGoToStage(false);
        }
    }

    public void GoToStageSelection(InputAction.CallbackContext context)
    {
        playerJoinElements.Disappear();
        playerJoinElements.DisappearEnds += () =>
        {
            print("Go To Stage Select");
            playerJoinElements.DisappearEnds = null;
        };
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
    GameObject tutoButton;
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
    RectTransform tutoButtonTarget;
    [SerializeField]
    RectTransform quitButtonTarget;

    Vector2 titleBasePos;
    Vector2 newGameButtonBasePos;
    Vector2 settingsButtonBasePos;
    Vector2 tutoButtonBasePos;
    Vector2 quitButtonBasePos;

    [Space]
    [SerializeField]
    float appearDuration;
    [SerializeField]
    AnimationCurve appearCurve;
    [SerializeField]
    AnimationCurve disappearCurve;
    [SerializeField]
    float buttonAppearDelay;

    public Action AppearEnds;
    public Action DisappearEnds;

    public GameObject ReturnFirstButton()
    {
        return newGameButton.gameObject;
    }

    public void AppearAnimation()
    {
        titleBasePos = title.rectTransform.position;
        newGameButtonBasePos = newGameButton.transform.position;
        settingsButtonBasePos = settingsButton.transform.position;
        tutoButtonBasePos = tutoButton.transform.position;
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

        tutoButton.transform.DOMove(tutoButtonTarget.position, appearDuration)
            .SetEase(appearCurve)
            .SetDelay(buttonAppearDelay * 2f);

        quitButton.transform.DOMove(quitButtonTarget.position, appearDuration)
            .SetEase(appearCurve)
            .SetDelay(buttonAppearDelay * 3f)
            .OnComplete(OnAppearEnds);
    }

    public void DisappearAnimation()
    {
        title.transform.DOMove(titleBasePos, appearDuration)
            .SetEase(disappearCurve);

        newGameButton.transform.DOMove(newGameButtonBasePos, appearDuration)
            .SetEase(disappearCurve)
            .SetDelay(buttonAppearDelay * 3f)
            .OnComplete(OnDisappearEnds);

        settingsButton.transform.DOMove(settingsButtonBasePos, appearDuration)
            .SetEase(disappearCurve)
            .SetDelay(buttonAppearDelay * 2f);

        tutoButton.transform.DOMove(tutoButtonBasePos, appearDuration)
            .SetEase(disappearCurve)
            .SetDelay(buttonAppearDelay * 1f);

        quitButton.transform.DOMove(quitButtonBasePos, appearDuration)
            .SetEase(disappearCurve)
            .SetDelay(buttonAppearDelay * 0f);
    }

    public void OnAppearEnds() => AppearEnds?.Invoke();
    public void OnDisappearEnds()
    {
        DisappearEnds?.Invoke();
        mainMenuRoot.gameObject.SetActive(false);
    }

}

[Serializable]
class PlayerJoin
{
    [SerializeField]
    RectTransform playerJoinRoot;
    [SerializeField]
    RectTransform portraitLayout;

    [Space]
    [SerializeField]
    List<PlayerJoinElement> playerPortraits;

    Dictionary<InputDevice, PlayerJoinElement> connectedPlayerPortraits = new Dictionary<InputDevice, PlayerJoinElement>();
    int playerCount = 0;
    int readyCount = 0;

    [Space]
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    RectTransform goStageButton;
    [SerializeField]
    RectTransform goBackButton;
    [SerializeField]
    TextMeshProUGUI waitingText;
    [SerializeField]
    TextMeshProUGUI goStageText;
    [SerializeField]
    Image background;
    [SerializeField]
    RectTransform newPlayerBox;

    [Space]
    [SerializeField]
    float appearDuration;
    [SerializeField]
    AnimationCurve appearCurve;
    [SerializeField]
    AnimationCurve disappearCurve;

    [Space]
    [SerializeField]
    float elementAppearDuration;
    [SerializeField]
    AnimationCurve elementAppearCurve;

    public Action AppearEnds;
    public Action DisappearEnds;

    public void Appear()
    {
        playerJoinRoot.gameObject.SetActive(true);

        foreach (PlayerJoinElement portait in playerPortraits)
            portait.gameObject.SetActive(false);
        
        goStageText.gameObject.SetActive(false);
        waitingText.gameObject.SetActive(true);

        background.rectTransform.localScale = Vector3.one * 0;
        background.rectTransform.DOScale(Vector3.one, appearDuration)
            .SetEase(appearCurve)
            .OnComplete(OnAppearEnds);
    }

    public void Disappear()
    {
        background.rectTransform.DOScale(Vector3.one * 0, appearDuration)
            .SetEase(disappearCurve)
            .OnComplete(OnDisappearEnds);
    }

    public void OnDisappearEnds()
    {
        DisappearEnds?.Invoke();
        playerJoinRoot.gameObject.SetActive(false);
    }

    public void AddNewPlayer(InputDevice device)
    {
        var newElement = playerPortraits[playerCount];
        connectedPlayerPortraits.Add(device, newElement);
        newElement.gameObject.SetActive(true);
        newElement.InitElement(playerCount,elementAppearDuration, elementAppearCurve);
        playerCount++;

        if(playerCount == 4)
            newPlayerBox.gameObject.SetActive(false);
    }

    IEnumerator UnityMadeMeDoThis(PlayerJoinElement obj)
    {
        yield return new WaitForEndOfFrame();
        obj.gameObject.SetActive(true);
        obj.StartAnimation();

    }

    public void SetPlayerReady(InputDevice device)
    {
        readyCount++;
        connectedPlayerPortraits[device].SetReady(true);
    }

    public bool CanGoToStage()
    {
        return playerCount > 1 && readyCount == playerCount;
    }

    public void UpdateGoToStage(bool state)
    {
        if (state)
        {
            goStageText.gameObject.SetActive(true);
            waitingText.gameObject.SetActive(false);
        }
        if (!state)
        {
            goStageText.gameObject.SetActive(false);
            waitingText.gameObject.SetActive(true);
        }
    }

    private void OnAppearEnds() => AppearEnds?.Invoke();

}
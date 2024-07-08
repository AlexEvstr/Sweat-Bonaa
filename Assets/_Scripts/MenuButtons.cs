using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Animator _animatorBananaSpin;
    [SerializeField] private Button _buttonBanaSpin;
    [SerializeField] private Animator _animatorDoorsSecret;
    [SerializeField] private Button _buttonDoors;
    [SerializeField] private Animator _animatorCardsSecret;
    [SerializeField] private Button _buttonCards;
    [SerializeField] private Animator _animatorShop;
    [SerializeField] private Button _buttonShop;
    [SerializeField] private Animator _animatorCloseShop;
    [SerializeField] private Button _buttonCloseShop;
    [SerializeField] private Animator _animatorDailyBonusClose;
    [SerializeField] private Button _buttonDailyBonusClose;
    [SerializeField] private Animator _animatorSettingsClose;
    [SerializeField] private Button _buttonSettingsClose;
    [SerializeField] private Animator _animatorSettingsOpen;
    [SerializeField] private Button _buttonSettingsOpen;
    [SerializeField] private Animator _animatorExitGame;
    [SerializeField] private Button _buttonExitGame;
    [SerializeField] private GameObject _shopWindow;
    [SerializeField] private GameObject _mainMenuWindow;
    [SerializeField] private GameObject _dailyBonusWindow;
    [SerializeField] private GameObject _settingsWindow;


    void Start()
    {
        _buttonBanaSpin.onClick.AddListener(BananaButton);
        _buttonDoors.onClick.AddListener(DoorsButton);
        _buttonCards.onClick.AddListener(SecretButton);
        _buttonShop.onClick.AddListener(OpenShopButton);
        _buttonCloseShop.onClick.AddListener(CloseShopButton);
        _buttonDailyBonusClose.onClick.AddListener(CloseDailyBonusWindow);
        _buttonSettingsClose.onClick.AddListener(CloseSettingsWindow);
        _buttonSettingsOpen.onClick.AddListener(OpenSettingsButton);
        _buttonExitGame.onClick.AddListener(ExitGame);
    }

    public void BananaButton()
    {
        StartCoroutine(WaitForAnimation("BananaSpin", _animatorBananaSpin));
    }

    public void DoorsButton()
    {
        StartCoroutine(WaitForAnimation("DoorsSecret", _animatorDoorsSecret));
    }

    public void SecretButton()
    {
        StartCoroutine(WaitForAnimation("CardsSecret", _animatorCardsSecret));
    }

    public void OpenShopButton()
    {
        StartCoroutine(OpenNewWindow(_mainMenuWindow, _shopWindow, _animatorShop));
    }

    public void CloseShopButton()
    {
        StartCoroutine(OpenNewWindow(_shopWindow, _mainMenuWindow, _animatorCloseShop));
    }

    public void CloseDailyBonusWindow()
    {
        StartCoroutine(OpenNewWindow(_dailyBonusWindow, _mainMenuWindow, _animatorDailyBonusClose));
    }

    public void OpenSettingsButton()
    {
        StartCoroutine(OpenNewWindow(_mainMenuWindow, _settingsWindow, _animatorSettingsOpen));
    }

    public void CloseSettingsWindow()
    {
        StartCoroutine(OpenNewWindow(_settingsWindow, _mainMenuWindow, _animatorSettingsClose));
    }

    public void ExitGame()
    {
        StartCoroutine(ExitGameBtn());
    }

    private IEnumerator ExitGameBtn()
    {
        _animatorExitGame.SetTrigger("Pressed");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    private IEnumerator OpenNewWindow(GameObject disabledWindow,GameObject enabledWindow, Animator animator)
    {
        animator.SetTrigger("Pressed");
        yield return new WaitForSeconds(0.5f);
        disabledWindow.SetActive(false);
        enabledWindow.SetActive(true);
    }

    private IEnumerator WaitForAnimation(string sceneName, Animator animator)
    {
        animator.SetTrigger("Pressed");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    } 
}
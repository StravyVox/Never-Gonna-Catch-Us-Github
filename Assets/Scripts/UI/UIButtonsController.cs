using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonsController : MonoBehaviour
{

    public Action OnSaveSettings;
    private Camera _mainCamera;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _settingsUI;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _startbutton;
    [SerializeField] private List<GameObject> _shutdownObjectsOnOpenMenu;
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private Slider _settingsSlider;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _maxScore;
    [SerializeField] List<GameObject> _startObjectsToOff;
    private bool _menuOpened;
    private float _gameSpeed;

    private void Start()
    {
        _menuOpened = false;
        _mainCamera = Camera.main;
        
        _audioSlider.value = AudioListener.volume;
        _settingsSlider.value = QualitySettings.GetQualityLevel();
    }
    public void FirstStart()
    {
        _startbutton.SetActive(false);
        foreach(GameObject obj in _startObjectsToOff)
        {
            obj.SetActive(false);
        }
        _menuUI.SetActive(true);  
    }
    public void StartGame()
    {

        GlobalVariables.instance.SaveToPrefs();
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Menu");
    }
    public void OpenCloseMenu()
    {
        if (!_menuOpened)
        {
            Time.timeScale = 0;
            _menuUI?.SetActive(true);
            _menuOpened = true;
        }
        else
        {

            Time.timeScale = 1f;
            _menuUI?.SetActive(false);
            _menuOpened = false;
        }
    }
    public void ChangeAudio() {
        AudioListener.volume = _audioSlider.value;
        GlobalVariables.instance.SoundVolume = AudioListener.volume;

    }
    public void ChangeSettings()
    {
        QualitySettings.SetQualityLevel((int)_settingsSlider.value, true);
        GlobalVariables.instance.Settings = QualitySettings.GetQualityLevel();
        if(_settingsSlider.value > 2)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 30;
        }
    }
    
    public void ExitToStartScreen()
    {

        Time.timeScale = 1f;
        GlobalVariables.instance.SaveToPrefs();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("SampleScene");
    }
    public void OpenUpgradeMenu()
    {
        _upgradeMenu?.SetActive(true);
        _menuUI.SetActive(false);
    }
    public void CloseUpgradeMenu()
    {

        GlobalVariables.instance.SaveToPrefs();
        _upgradeMenu?.SetActive(false);
        _menuUI.SetActive(true);
    }
    public void OpenSettings()
    {
        _settingsUI.SetActive(true);
        _menuUI.SetActive(false);

    }
    public void ReturnToMenu()
    {
        GlobalVariables.instance.SaveToPrefs();
        _settingsUI.SetActive(false);
        _menuUI.SetActive(true);
        OnSaveSettings?.Invoke();

    }
    public void OpenDeathMenu()
    {
        Time.timeScale = 0;
        GlobalVariables.instance.SaveSessionResults();
        _currentScore.text = GlobalVariables.instance.CurrentScore.ToString();
        _maxScore.text = GlobalVariables.instance.MaxScore.ToString();
        GlobalVariables.instance.SaveToPrefs();
        _deathMenu.SetActive(true);
    }
}

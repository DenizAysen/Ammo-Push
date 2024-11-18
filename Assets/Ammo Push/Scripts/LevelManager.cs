using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelManagerSo levelManagerSo;

    private int _bossSceneIndex;
    #region Singleton
    public static LevelManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion
    private void OnEnable()
    {
        Stopwatch.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(LoadScene());
    }
    private void OnDisable()
    {
        Stopwatch.onGameOver -= OnGameOver;
    }
    void Start()
    {
        _bossSceneIndex = 3;
    }
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.5f);
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_bossSceneIndex);
    }
    public LevelDataSo GetLevelData()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        return levelManagerSo.Levels[currentLevel];
    }
}

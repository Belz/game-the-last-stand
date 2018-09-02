﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    [Header("General")]
    [Space]
    [SerializeField]
    private float buildTimer = 30f;

    [Space]
    [SerializeField]
    private int startingMoney = 0;

    [HideInInspector]
    public int currentMoney = 0;

    [HideInInspector]
    public bool isPaused;

    [HideInInspector]
    public bool buildMode = true;
    private float baseTimer;

    private UIManagerScript uIManager;
    private WaveManagerScript waveManager;

    private void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        waveManager = gameController.GetComponentInChildren<WaveManagerScript>();
        uIManager = gameController.GetComponentInChildren<UIManagerScript>();
    }

    private void Start()
    {
        ToggleBuildMode(true);
        UpdateMoney(startingMoney);
    }

    private void FixedUpdate()
    {
        if (buildMode)
        {
            if (Time.time > baseTimer) ToggleBuildMode(false);
            else uIManager.UpdateBuildModeTimer(Mathf.CeilToInt(baseTimer - Time.time));
        }
    }

    public void ToggleBuildMode(bool toggle)
    {
        buildMode = toggle;
        uIManager.ToggleBuildModeUI(toggle);

        if (buildMode) baseTimer = buildTimer + Time.time;
        else
        {
            uIManager.CollapseTrapMenu();
            waveManager.StartWave();
        }
    }

    public void TogglePause(bool toggle)
    {
        isPaused = toggle;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void UpdateMoney(int money)
    {
        currentMoney += money;
        uIManager.UpdateMoneyText(currentMoney, money, money > 0);
    }

    public void GameOver()
    {
        Debug.LogWarning("Unfinished Script");
        //*******************
    }
    public void GameWin()
    {
        Debug.LogWarning("Unfinished Script");
        //*******************
    }
}
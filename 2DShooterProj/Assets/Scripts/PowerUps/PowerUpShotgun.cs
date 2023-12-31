using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpShotgun : MonoBehaviour
{
    [Header("PowerUp Info")]
    [SerializeField] private Button button;
    [SerializeField] private new string name;
    [SerializeField] private Sprite icon;

    [Header("Prefab Child Components")]
    [SerializeField] private TextMeshProUGUI powerUpNameText;
    [SerializeField] private Image powerUpIcon;

    [Header("Weapons Info")]
    [SerializeField] private GameObject weaponAnchor;
    [SerializeField] private GameObject weaponAnchorShotgun;
    [SerializeField] private GameObject powerUpPistol;


    void Awake()
    {
        button.onClick.AddListener(SwitchToShotgun);
        powerUpIcon.sprite = icon;
        powerUpNameText.text = name;
    }

    private void SwitchToShotgun()
    {
        weaponAnchor.SetActive(false);
        weaponAnchorShotgun.SetActive(true);
        Time.timeScale = 1;
        UIManager.instance.SetPowerUpContainer(false);
        gameObject.SetActive(false);
        powerUpPistol.gameObject.SetActive(true);
    }
}

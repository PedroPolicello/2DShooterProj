using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private int lifes = 3;

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private int initialLives;
    private bool isHurt;
    public bool isMoving;

    private int playerLevel;
    private int currentPlayerXP;
    private int toLevelUpXP = 10;

    private bool recoil;

    private bool canDash = true;
    private bool powerUpDash = false;

    [SerializeField] private GameObject powerupDash;
    [SerializeField] private GameObject powerupShotgun;
    [SerializeField] private GameObject powerupPistol;
    [SerializeField] private GameObject weaponAnchor;
    [SerializeField] private GameObject weaponAnchorShotgun;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        initialLives = lifes;
        playerTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.instance.SetPlayerLife(lifes);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LifeHandler(-1);
        }
    }

    public void LifeHandler(int value)
    {
        if (value > 0)
        {
            lifes += value;
        }
        else
        {
            isHurt = true;
            lifes += value;

            if (lifes <= 0)
            {
                playerTransform.position = new Vector3(0, 0, 0);
                lifes = initialLives;
                playerLevel = 0; currentPlayerXP = 0;
                toLevelUpXP = 10;
                GameManager.instance.gameScore = 0;
                UIManager.instance.SetScoreText(currentPlayerXP);
                UIManager.instance.SetXPInfoText(currentPlayerXP, toLevelUpXP);
                UIManager.instance.SetPlayerLevelText(playerLevel);
                powerupDash.gameObject.SetActive(true);
                powerupShotgun.gameObject.SetActive(true);
                powerupPistol.gameObject.SetActive(false);
                weaponAnchor.gameObject.SetActive(true);
                weaponAnchorShotgun.gameObject.SetActive(false);
            }
        }
        GameManager.instance.SetPlayerLife(lifes);
    }

    public bool CheckPlayerMove()
    {
        return isMoving;
    }
    private void CheckLevelUp()
    {
        if (currentPlayerXP >= toLevelUpXP)
        {
            playerLevel++;
            currentPlayerXP -= toLevelUpXP;
            toLevelUpXP += 5;
            GameManager.instance.OnLevelUp();
        }

        GameManager.instance.SetLevelInfo(playerLevel, currentPlayerXP, toLevelUpXP);
    }

    //PLAYER
    public Vector2 GetPlayerPosition()
    {
        return playerTransform.position;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }


    public bool CheckPlayerHurt()
    {
        return isHurt;
    }

    public void SetPlayerHurt(bool hurt)
    {
        isHurt = hurt;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }

    //XP
    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public void SetCurrentXP(int xpToAdd)
    {
        currentPlayerXP += xpToAdd;
        CheckLevelUp();
    }

    //RECOIL

    public void SetRecoil(bool Recoil)
    {
        recoil = Recoil;
    }
    public bool CheckRecoil()
    {
        return recoil;
    }
}

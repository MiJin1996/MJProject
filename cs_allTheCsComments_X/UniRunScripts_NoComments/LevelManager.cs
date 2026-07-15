using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public  AudioSource levelUpSound;
    public GameObject levelUpEffectPrefab;

    public static LevelManager instance;

    public TextMeshProUGUI levelText;

    public int level = 1;
    public int coinsForLevelUp = 5;

    private int currentCoinCnt = 0;

    void Awake()
    {

        instance = this;
    }

    public void AddCoin(int amount)
    {
        currentCoinCnt += amount;

        if (currentCoinCnt >= coinsForLevelUp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentCoinCnt = 0;

        if (levelText != null)
        {
            levelText.text = "LV. " + level;
            levelUpSound.Play();

             GameObject destryEffect =Instantiate(levelUpEffectPrefab, transform.position, Quaternion.identity);
            Destroy(destryEffect, 0.5f);
        }

        Debug.Log("레벨업! 현재 레벨: " + level);

    }

}

using UnityEngine;
using TMPro;
using BlackMassSoftware.FloatingTextEngine.Lite;
using BlackMassSoftware.FloatingTextEngine.Lite.Behaviors;

public class Coin : MonoBehaviour {

    private bool stepped = false;
    public GameObject[] coins;
    public AudioSource coinAudioSource;
    public AudioClip coinSound;

    public CoinScoreEffect coinScoreEffect;

    private static int coinCnt = 0;

        private void OnEnable()
        {
            stepped = false;
            for (int i = 0; i < coins.Length; i++)
            {
                coins[i].SetActive(true);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("코인 충돌됨");

            if (other.tag == "Player" && !stepped)
            {
                stepped = true;

                coinScoreEffect.Play(coinCnt++);
                GameManager.instance.AddScore(1);
                if (LevelManager.instance != null)
                {
                    LevelManager.instance.AddCoin(1);
                }

               AudioSource.PlayClipAtPoint(coinSound, transform.position);

                gameObject.SetActive(false);
            }
        }

    }


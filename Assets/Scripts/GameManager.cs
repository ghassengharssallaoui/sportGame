using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    SlidersController slidersController;
    public event Action<int> OnGoalHit;
    public event Action<int> OnOverHit;
    public event Action<int, BongsController> OnBongHit;
    public event Action<int, int> OnStarsHit;
    public event Action<int> OnKeeperHit;
    public event Action<int> OnPlayerHit;
    public event Action OnGameStart;
    public event Action OnGameEnd;
    public static GameManager Instance;
    [SerializeField] float gameDuration = 180f;
    private float gameTime = 0f;
    private bool isPaused = false;
    [SerializeField] private StarController[] playerOneStars; // Stars belonging to Player 1
    [SerializeField] private StarController[] playerTwoStars; // Stars belonging to Player 2

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Ensure only one instance exists.
        }
    }
    private void Start()
    {
        slidersController.LoadSettings();
        OnGameStart?.Invoke();
    }
    public void NotifyGoalHit(int scoringPlayer)
    {
        OnGoalHit?.Invoke(scoringPlayer);
    }
    public void NotifyOverHit(int scoringPlayer)
    {
        OnOverHit?.Invoke(scoringPlayer);
    }

    public void NotifyBongHit(int scoringPlayer, BongsController bong)
    {
        OnBongHit?.Invoke(scoringPlayer, bong);
    }

    public void NotifyStarsHit(int scoringPlayer, int index)
    {
        OnStarsHit?.Invoke(scoringPlayer, index);
    }
    public void NotifyKeeperHit(int playersKeeper)
    {
        OnKeeperHit?.Invoke(playersKeeper);
    }
    public void NotifyPlayerHit(int player)
    {
        OnPlayerHit?.Invoke(player);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        gameTime += Time.deltaTime;
        // Stop the game at 3 minutes
        if (gameTime >= gameDuration)
        {
            OnGameEnd?.Invoke();
            Time.timeScale = 0f;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void CheckAllStarsLit(int player)
    {
        StarController[] stars = player == 1 ? playerOneStars : playerTwoStars;

        bool allLit = true;
        foreach (StarController star in stars)
        {
            if (!star.GetIsLit())
            {
                allLit = false;
                break;
            }
        }

        if (allLit)
        {
            HandleAllStarsLit(player);
        }
    }


    private void HandleAllStarsLit(int player)
    {
        StarController[] stars = player == 1 ? playerOneStars : playerTwoStars;
        bool allLit = true;
        foreach (StarController star in stars)
        {
            if (!star.GetIsLit())
            {
                allLit = false;
                break;
            }
        }

        if (allLit)
        {
            ResetAllStars(stars);
        }

    }
    private void ResetAllStars(StarController[] stars)
    {
        foreach (StarController star in stars)
        {
            star.ResetStar();
        }
    }
    public StarController[] GetPlayerOneStars() => playerOneStars;
    public StarController[] GetPlayerTwoStars() => playerTwoStars;
    /* public void StarHit(bool isPlayerOneStar, GameObject starHit)
 {

     if (starHit.GetComponent<SpriteRenderer>().sprite == litStar)
     {
         starHit.GetComponent<SpriteRenderer>().sprite = unlitStar;
         // UpdateScore(isPlayerOneStar, -2);
         if (isPlayerOneStar)
         {
             playerOneLitStarsNumber--;
             playerOneStars--;
         }
         else
         {
             playerTwoLitStarsNumber--;
             playerTwoStars--;
         }
     }
     else
     {
         starHit.GetComponent<SpriteRenderer>().sprite = litStar;
         //   UpdateScore(isPlayerOneStar, 2);
         if (isPlayerOneStar)
         {
             playerOneLitStarsNumber++;


             playerOneStars++;




             // Debug.Log("playerOneLitStarsNumber" + playerOneLitStarsNumber);
             if (playerOneLitStarsNumber % 5 == 0)
             {
                 foreach (SpriteRenderer sprite in PlayerTwoStars)
                 {
                     sprite.sprite = unlitStar;
                 }
             }
         }
         else
         {
             playerTwoLitStarsNumber++;

             playerTwoStars++;
             //  Debug.Log("playerTwoLitStarsNumber" + playerTwoLitStarsNumber);
             if (playerTwoLitStarsNumber % 5 == 0)
             {
                 foreach (SpriteRenderer sprite in PlayerOneStars)
                 {

                     sprite.sprite = unlitStar;
                 }
             }
         }
     }
 }
*/

}

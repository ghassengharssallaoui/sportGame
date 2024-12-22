using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    // Track the number of times each type of goal is scored
    private int playerOneGoalHits = 0;
    private int playerTwoGoalHits = 0;
    private int playerOneOverHits = 0;
    private int playerTwoOverHits = 0;
    private int playerOneBongHits = 0;
    private int playerTwoBongHits = 0;
    private int playerOneStarHits = 0;
    private int playerTwoStarHits = 0;
    private int playerOneTotalStarHits = 0;
    private int playerTwoTotalStarHits = 0;
    private int playerOnePossession = 0;
    private int playerTwoPossession = 0;
    private int playerOneSaves = 0;
    private int playerTwoSaves = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGoalHit += AddGoalScore;
        GameManager.Instance.OnOverHit += AddOverScore;
        GameManager.Instance.OnBongHit += AddBongScore;
        GameManager.Instance.OnStarsHit += AddStarScore;
        GameManager.Instance.OnKeeperHit += AddKeeperStatistics;
        GameManager.Instance.OnPlayerHit += AddPlayerStatistics;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGoalHit -= AddGoalScore;
        GameManager.Instance.OnOverHit -= AddOverScore;
        GameManager.Instance.OnBongHit -= AddBongScore;
        GameManager.Instance.OnStarsHit -= AddStarScore;
        GameManager.Instance.OnKeeperHit -= AddKeeperStatistics;

    }

    // Add score for a goal hit
    private void AddGoalScore(int scoringPlayer)
    {
        if (scoringPlayer == 1)
        {
            playerOneScore += 6;
            playerOneGoalHits++; // Track goal hit count for player 1
        }
        else if (scoringPlayer == 2)
        {
            playerTwoScore += 6;
            playerTwoGoalHits++; // Track goal hit count for player 2
        }
    }

    // Add score for an over hit
    private void AddOverScore(int scoringPlayer)
    {
        if (scoringPlayer == 1)
        {
            playerOneScore += 3;
            playerOneOverHits++; // Track over hit count for player 1
        }
        else if (scoringPlayer == 2)
        {
            playerTwoScore += 3;
            playerTwoOverHits++; // Track over hit count for player 2
        }
    }

    // Add score for a bong hit
    private void AddBongScore(int scoringPlayer, BongsController bong)
    {
        if (scoringPlayer == 1)
        {
            playerOneScore += 1;
            playerOneBongHits++; // Track bong hit count for player 1
        }
        else if (scoringPlayer == 2)
        {
            playerTwoScore += 1;
            playerTwoBongHits++; // Track bong hit count for player 2
        }
    }

    // Add score for a star hit
    private void AddStarScore(int scoringPlayer, int index)
    {
        StarController[] stars = (scoringPlayer == 1) ? GameManager.Instance.GetPlayerOneStars() : GameManager.Instance.GetPlayerTwoStars();
        StarController star = stars[index];
        if (scoringPlayer == 1)
        {
            playerOneTotalStarHits++;
        }
        else if (scoringPlayer == 2)
        {
            playerTwoTotalStarHits++;
        }
        if (star.GetIsLit()) // If the star is lit
        {
            if (scoringPlayer == 1)
            {
                playerOneScore -= 2;  // Subtract score for Player 1
                playerOneStarHits--;   // Track star hit count for Player 1
            }
            else if (scoringPlayer == 2)
            {
                playerTwoScore -= 2;  // Subtract score for Player 2
                playerTwoStarHits--;   // Track star hit count for Player 2
            }
        }
        else // If the star is unlit
        {
            // Lit the star and add score
            star.GetIsLit();
            if (scoringPlayer == 1)
            {
                playerOneScore += 2;  // Add score for Player 1
                playerOneStarHits++;   // Track star hit count for Player 1
            }
            else if (scoringPlayer == 2)
            {
                playerTwoScore += 2;  // Add score for Player 2
                playerTwoStarHits++;   // Track star hit count for Player 2
            }
        }
    }

    private void AddKeeperStatistics(int playersKeeper)
    {
        if (playersKeeper == 1)
        {
            playerOneSaves++;
            playerOnePossession++;
        }
        else if (playersKeeper == 2)
        {
            playerTwoSaves++;
            playerTwoPossession++;
        }
    }
    private void AddPlayerStatistics(int player)
    {
        if (player == 1)
        {
            playerOnePossession++;
        }
        else if (player == 2)
        {
            playerTwoPossession++;
        }
    }
    // Methods to get the score of each player
    public int GetPlayerOneScore() => playerOneScore;
    public int GetPlayerTwoScore() => playerTwoScore;

    // Methods to get the number of times each goal type was hit
    public int GetPlayerOneGoalHits() => playerOneGoalHits;
    public int GetPlayerTwoGoalHits() => playerTwoGoalHits;

    public int GetPlayerOneOverHits() => playerOneOverHits;
    public int GetPlayerTwoOverHits() => playerTwoOverHits;

    public int GetPlayerOneBongHits() => playerOneBongHits;
    public int GetPlayerTwoBongHits() => playerTwoBongHits;

    public int GetPlayerOneStarHits() => playerOneStarHits;
    public int GetPlayerTwoStarHits() => playerTwoStarHits;
    public int GetPlayerOneTotalStarHits() => playerOneTotalStarHits;
    public int GetPlayerTwoTotalStarHits() => playerTwoTotalStarHits;

    public int GetPlayerOnePossession() => playerOnePossession;
    public int GetPlayerTwoPossession() => playerTwoPossession;

    public int GetPlayerOneSaves() => playerOneSaves;
    public int GetPlayerTwoSaves() => playerTwoSaves;

}

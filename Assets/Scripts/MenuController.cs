using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Image playerOne, playerTwo;

    [SerializeField]
    private Sprite[] skins;

    private int playerOneSkinIndex = 0;
    private int playerTwoSkinIndex = 0;

    void Start()
    {
        playerOne.sprite = skins[playerOneSkinIndex];
        playerTwo.sprite = skins[playerTwoSkinIndex];
    }

    public void NextSkin(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            playerOneSkinIndex = (playerOneSkinIndex + 1) % skins.Length;
            playerOne.sprite = skins[playerOneSkinIndex];
        }
        else
        {
            playerTwoSkinIndex = (playerTwoSkinIndex + 1) % skins.Length;
            playerTwo.sprite = skins[playerTwoSkinIndex];
        }
    }

    public void PreviousSkin(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            playerOneSkinIndex = (playerOneSkinIndex - 1 + skins.Length) % skins.Length;
            playerOne.sprite = skins[playerOneSkinIndex];
        }
        else
        {
            playerTwoSkinIndex = (playerTwoSkinIndex - 1 + skins.Length) % skins.Length;
            playerTwo.sprite = skins[playerTwoSkinIndex];
        }
    }

    public void Play()
    {
        // Save the selected skin indices
        SkinManager.PlayerOneSkinIndex = playerOneSkinIndex;
        SkinManager.PlayerTwoSkinIndex = playerTwoSkinIndex;

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

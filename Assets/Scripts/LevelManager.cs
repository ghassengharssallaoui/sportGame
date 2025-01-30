using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject postprossesing, playerOneOver, playerTwoOver, playerOneGoal, playerTwoGoal, ball, playerOneKeeper, playerTwoKeeper;

    private void Start()
    {
        if (MenuController.Instance.isNeon)
        {
            postprossesing.SetActive(true);
            playerOneOver.GetComponent<SpriteShapeRenderer>().color = new Color(0f, 0.85f, 0.96f);
            playerOneGoal.GetComponentInChildren<SpriteRenderer>().color = new Color(0.945f, 0.290f, 0.792f);
            // playerOneKeeper.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.584f, 0.192f);

            playerTwoOver.GetComponent<SpriteShapeRenderer>().color = new Color(0f, 0.85f, 0.96f);
            playerTwoGoal.GetComponentInChildren<SpriteRenderer>().color = new Color(0.945f, 0.290f, 0.792f);
            // playerTwoKeeper.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.584f, 0.192f);



        }
        else
        {
            // postprossesing.SetActive(false);
        }
    }
}

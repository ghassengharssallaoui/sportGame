using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatdiumSelector : MonoBehaviour
{
    public Sprite[] stadiums;
    public Sprite[] balls;
    public SpriteRenderer ballSprite;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = stadiums[MenuController.Instance.currentStadiumTextIndex];
        ballSprite.sprite = balls[MenuController.Instance.currentBallTextIndex];
    }


}

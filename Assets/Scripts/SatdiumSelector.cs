using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatdiumSelector : MonoBehaviour
{
    public Sprite[] stadiums;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = stadiums[MenuController.currentStadiumTextIndex - 1];
    }


}

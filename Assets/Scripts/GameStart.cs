using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        Instantiate(gameManager);
    }
}

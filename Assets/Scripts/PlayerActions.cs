using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour 
{


    private SpriteRenderer[] _spriteRenderers;

    public GameObject Player;

    private PlayerScript currentPlayer;

	void Start () 
    {
        currentPlayer = Player.GetComponent<PlayerScript>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}
	
	void Update () 
    {
        for (int i = 0; i < 3; i++)
        {
            if (currentPlayer.getActionsLeft() > i)
            {
                _spriteRenderers[i].enabled = true;
            }
            else if (currentPlayer.getActionsLeft() < i + 1 )
            {
                _spriteRenderers[i].enabled = false;
            }
        }
	}
}

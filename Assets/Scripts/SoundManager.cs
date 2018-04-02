using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource _audioSource;
    public AudioClip attack, changeDirection, explosion, jump, stunned;



	void Start () 
    {
        _audioSource = GetComponent<AudioSource>();
	}

    public void PlaySound(string sound)
    {
        switch(sound)
        {
            case "attack":
                _audioSource.PlayOneShot(attack);
            break;
            case "changeDirection":
                _audioSource.PlayOneShot(changeDirection);
                break;
            case "explosion":
                _audioSource.PlayOneShot(explosion);
                break;
            case "jump":
                _audioSource.PlayOneShot(jump);
                break;
            case "stunned":
                _audioSource.PlayOneShot(stunned);
                break;

        }

    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}

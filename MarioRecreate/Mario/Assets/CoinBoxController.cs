using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBoxController : MonoBehaviour 
{

    private Animator anim;

    public GameObject coin;
    public int SpawnAmount;
    private bool active; 

    public void use()
    {
        if (active) 
        {
            anim.SetBool("EmptyCoinBox", true);
            Debug.Log("UseThis");

            active = false;

            // Instantiate(coin, transform.position + Vector3.up * 1.1f, transform.rotation);
            for (int i = 0; i < SpawnAmount; i++)
            {
                //Instantiate(coin, transform.position + new Vector3(0, .5f, Random.Range(-.2f, 2f)), transform.rotation);
                Instantiate(coin, transform.position + new Vector3(Random.Range(-.2f, 2f), Random.Range(.5f, .1f), 0f), transform.rotation);
            }

            //Insan = create the coin in game
            //Instantiate(coin, NewTransform);
        }
    }

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        active = true;

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
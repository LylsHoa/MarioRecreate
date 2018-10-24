using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

    private Animator anim;

    public float speed;

    //Everything on ground set on edior 
    public LayerMask isGround;

    public LayerMask isMario;

    //Child of Goomba
    public Transform wallHitBox;

    public Transform marioHitBox;

    //true when wall hits
    private bool wallHit;

    private bool marioHit;

    //edit in inspector
    public float wallHitHeight;

    public float wallHitWidth;

    public float marioHitHeight;

    public float marioHitWidth;
    
    public bool IsDead;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("goombaWalk", true);
        anim.SetBool("deadGoomba", false);
        marioHit = false;
        IsDead = false;

    }
	
    public void Kill()
    {
        IsDead = true;
        anim.SetBool("deadGoomba", true);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (IsDead)
        {
            return;
        }

        transform.Translate(speed * Time.deltaTime, 0, 0);

        //wall
        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(marioHitWidth, marioHitHeight, 1));


    }


}

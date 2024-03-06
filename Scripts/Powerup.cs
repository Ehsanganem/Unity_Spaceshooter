using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float speed = 3f;
    // Start is called before the first frame update

    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

     

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip, transform.position);
                switch (powerupID)
                {
                    case 0:
                        player.activiate_triple_shot();

                        break;
                    case 1:
                        player.activiate_speed_Up();
                        break;
                    case 2:
                        player.activate_shield();
                        break;
                    
                }
                
            }
            Destroy(this.gameObject);
        }
    }

}

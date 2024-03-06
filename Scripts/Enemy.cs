using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    [SerializeField]
    private GameObject _enemylaser;

    private Animator _anim;

    private AudioSource _audioSource;

    private float _firerate=3.0f;

    private float _canfire=-1f;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("player is null");
        }

       
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        calculate_movement();
        if (Time.time > _canfire)
        {
            _firerate = Random.Range(3.0f, 7f);
            _canfire = Time.time + _firerate;

            //GameObject enemylasers = Instantiate(_enemylaser, transform.position, Quaternion.identity);
            /* Laser[] lasers = enemylasers.GetComponentsInChildren<Laser>();

             for(int i = 0; i < lasers.Length; i++)
             {

                 lasers[i].applyenemylaser();
             }
         }
        */

        }
    }

    void calculate_movement()
    {
        float rand = Random.Range(-8f, 8f);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(rand, 7, 0);
        }
        float randfire = Random.Range(4, 20);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.damage();
            }
            _anim.SetTrigger("onPlayerdeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);

        }
        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.scoreadder(Random.Range(5,10));
            }
            _anim.SetTrigger("onPlayerdeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
           
        }
    }
          

        
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private GameManager _gamemanager;

    [SerializeField]
    private GameObject _rightwing;
    [SerializeField]
    private GameObject _leftwing;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _tripleshot;
    [SerializeField]
    private GameObject _shieldvisual;
    [SerializeField]
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip _lasersound;
    [SerializeField]
    private AudioClip _explosion_sound;
    [SerializeField]
    private float speed = 10000000000000000000000000f;
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private float _firerate = 0.5f;
    private float _canfire = -1f;
    private SpawnManager _spawnmanager;
    private UI_manager ui;

   
    private bool _tripleshotactive = false;
   
    private bool _speed_up_powerup = false;

    private bool _shieldactive = false;
    // Start is called before the first frame update

    private void Start()
    {
        _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        ui = GameObject.Find("Canvas").GetComponent<UI_manager>();
        transform.position = new Vector3(0f, 0f, 0f);
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnmanager == null)
        {
            Debug.LogError("spawn manager is null");
        }
        _audiosource = GetComponent<AudioSource>();
        if (_audiosource == null)
        {
            Debug.Log("audio source is null");
        }
        else
        {
            _audiosource.clip = _lasersound;
        }

        if (_gamemanager._iscoopmode == false)
        {
            transform.position=new Vector3(0,0,0);
        }
    }


    // Update is called once per frame
    void Update()
    {

        movementcalc();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time>_canfire)
        {

            firemachinegun();
        }
    }

    void movementcalc()
    {
        
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        if (!_speed_up_powerup)
        {
            transform.Translate(Vector3.right * horizontal_input * speed * Time.deltaTime);
            transform.Translate(Vector3.up * vertical_input * speed * Time.deltaTime);
        }
        if (_speed_up_powerup)
        {
            transform.Translate(Vector3.right * horizontal_input * speed* 2* Time.deltaTime);
            transform.Translate(Vector3.up * vertical_input * speed*2 * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x
            , Mathf.Clamp(transform.position.y, -7f,7f), 0);


        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    private void firemachinegun()
    {

        _canfire = Time.time + _firerate;
        if (!_tripleshotactive)
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleshot, transform.position, Quaternion.identity);
        }
        _audiosource.Play();
        
    }

    public void damage()
    {
        if (_shieldactive)
        {
            _shieldactive = false;
            _shieldvisual.SetActive(false);
            return;
        }
            health--;
            ui.set_livesimg(health);
        if (health == 2)
        {
            _rightwing.SetActive(true);
        }
        if (health == 1)
        {
            _leftwing.SetActive(true);
        }

        if (health < 1) {

            Debug.Log("game over will");
            _spawnmanager.onplayerdeath();

            ExplosionAudioClipChange();
          

            Destroy(this.gameObject);
        }

    }

    public void activiate_triple_shot()
    {
        _tripleshotactive = true;
        speed*=2;
        StartCoroutine(tripleshotspwanroutine());
    }
    public void activiate_speed_Up()
    {
        _speed_up_powerup = true;
        StartCoroutine(speedupspawnroutine());
    }

    public void activate_shield()
    {
        _shieldactive = true;
        _shieldvisual.SetActive(true);
       
    }

   
    IEnumerator tripleshotspwanroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleshotactive = false;

    }

    IEnumerator speedupspawnroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed_up_powerup = false;
        speed /=2;
        
    }

    public void scoreadder(int points)
    {
        this._score += points;
        ui.set_score(this._score);
    }

    public void ExplosionAudioClipChange()
    {
        _audiosource.clip = _explosion_sound;
        _audiosource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour

{
    private Player _player;
    [SerializeField]
    private GameObject _explosion;
   
    

    [SerializeField]
    private float _rotate_speed = 3.0f;
    // Start is called before the first frame update

    [SerializeField]
    private SpawnManager _spawnmanager;
    void Start()
    {
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotate_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
           
            Destroy(other.gameObject);
            _spawnmanager.startSpawn();
            
            Destroy(this.gameObject,0.25f);
            
        }
        
    }
}

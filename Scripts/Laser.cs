using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour

{
    [SerializeField]
    private float speed = 8.0f;

    private bool _isenemylaser = false;

    [SerializeField]
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        if (_isenemylaser == false)
        {
            moveup();
        }
       // else if(_isenemylaser==true)
       // {
         //   movedown();
        //}
        
    }

    void movedown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);

        }
    }

    void moveup()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);

        }
    }

    public void applyenemylaser()
    {
        this._isenemylaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isenemylaser==true)
        {
            if (_player != null)
            {
                _player.damage();
            }
        }
    }
    
        
    
}

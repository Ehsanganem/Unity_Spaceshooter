using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    
    [SerializeField]
    private GameObject _enemycontainer;

   


    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopspawning=false;
    void Start()
    {
       
        
    }

    public void startSpawn()
    {
       
        StartCoroutine(enemyspawnroutine());
        StartCoroutine(powerupspawnroutine());
    }
    // Update is called once per frame
    void Update()
    {
        
       
        
    }


    IEnumerator powerupspawnroutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopspawning)
        {
            Vector3 spawnpos = new Vector3(Random.Range(3, 8), 7, 0);
            int rand = Random.Range(0, 3);
            GameObject power_up_obj = Instantiate(_powerups[rand],spawnpos
                   , Quaternion.identity);


            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }

 

    IEnumerator enemyspawnroutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopspawning)
        {
            
            GameObject newobj= Instantiate(_enemyprefab, transform.position+new Vector3(Random.Range(-5.0f,5.0f),7f,0)
                , Quaternion.identity);

            newobj.transform.parent = _enemycontainer.transform;

            yield return  new WaitForSeconds(3.0f);
           

        }
        if (_stopspawning)
        {
            Destroy(this.gameObject);
        }

    }

    public void onplayerdeath()
    {
        _stopspawning = true;
    }
}

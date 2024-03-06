using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_manager : MonoBehaviour
{
    [SerializeField]
    private Text _scorefield;

    [SerializeField]
    private Image _img_lives;
    [SerializeField]
    private Sprite[] _livesprites;

    [SerializeField]
    private Text _gameovertext;

    [SerializeField]
    private Text _restart_text;
    [SerializeField]
    private GameManager _gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_score(int text)
    {
        _scorefield.text = "score :" + text;
    }

    public void set_livesimg(int lives)
    {
        this._img_lives.sprite = _livesprites[lives];
        
        if (lives == 0)
        {
            gameoversequence();
        }
        


    }

    public void gameoversequence()
    {
        _gamemanager.gameover();
        _gameovertext.gameObject.SetActive(true);
        _restart_text.gameObject.SetActive(true);
        StartCoroutine(gameoverflicker());
    }




    IEnumerator gameoverflicker()
    {
        while (true)
        {
            _gameovertext.text = "GAME OVER";
 
            yield return new WaitForSeconds(0.5f);
            _gameovertext.text = "";
            yield return new WaitForSeconds(0.5f);

        }

    }
   
  
}

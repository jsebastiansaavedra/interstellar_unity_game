using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoheteController : MonoBehaviour {

    public GameObject gameController;
    public int speed = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameController.GetComponent<GameController>().end = true;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Bonus")
        {
            gameController.GetComponent<GameController>().filaBonus.Enqueue(collision.gameObject.GetComponent<BonusController>().tipo);
            if (gameController.GetComponent<GameController>().filaBonus.Count == 1)
            {
                gameController.GetComponent<GameController>().BonusenPantalla();
            }
            Debug.Log(gameController.GetComponent<GameController>().filaBonus.Peek());
            Destroy(collision.gameObject);
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
        }
    }
}

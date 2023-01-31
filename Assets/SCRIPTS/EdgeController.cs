using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {

    public GameObject gameController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameController.GetComponent<GameController>().objetosEnemigos.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "DISPARO")
        {
            Destroy(collision.gameObject);
        }
    }
}

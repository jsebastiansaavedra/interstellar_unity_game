using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<GameObject> objetosEnemigos = new List<GameObject>();
    public List<int> bonusHechos = new List<int>();
    public bool play = false;
    public bool end = false;
    public float timerCreatorEnemy = 2f;
    public float timerCreatorBonus = 5f;
    public GameObject enemy;
    public GameObject bonus1;
    public GameObject bonus2;
    public GameObject bonus3;
    public GameObject bonus1Estatico;
    public GameObject bonus2Estatico;
    public GameObject bonus3Estatico;
    public GameObject disparo;
    public GameObject disparo2;
    public Queue<int> filaBonus = new Queue<int>();
    private int contador = 0;
    public Text textoPuntaje;
    public int puntos = 0;
    private GameObject enPantalla;
    public Canvas canvasInicial, canvasFinal, canvasSkin, canvas0;
    public Text textoFinal;
    public Dictionary<int, GameObject> diccionarioSkins = new Dictionary<int, GameObject>();
    public GameObject skin1, skin2, skin3, skin4;


	// Use this for initialization
	void Start () {
        diccionarioSkins.Add(1, skin1);
        diccionarioSkins.Add(2, skin2);
        diccionarioSkins.Add(3, skin3);
        diccionarioSkins.Add(4, skin4);
    }
	
	// Update is called once per frame
	void Update () {
        if (end == false && play==true)
        {
            timerCreatorEnemy -= Time.deltaTime;
            timerCreatorBonus -= Time.deltaTime;
            if (timerCreatorEnemy <= 0)
            {
                GameObject objectTemporary = Instantiate(enemy, new Vector3(Random.Range(-179, 134), 144f, 0f), Quaternion.identity);
                objetosEnemigos.Add(objectTemporary);
                contador++;
                timerCreatorEnemy = 2f / contador + 0.2f;
            }
            if (timerCreatorBonus <= 0)
            {
                int bonus = Random.Range(1, 4);
                if (bonus == 1)
                {
                    Instantiate(bonus1, new Vector3(Random.Range(-179, 134), 144f, 0f), Quaternion.identity);
                }
                if (bonus == 2)
                {
                    Instantiate(bonus2, new Vector3(Random.Range(-179, 134), 144f, 0f), Quaternion.identity);
                }
                if (bonus == 3)
                {
                    Instantiate(bonus3, new Vector3(Random.Range(-179, 134), 144f, 0f), Quaternion.identity);
                }
                timerCreatorBonus = 5f;
            }

            textoPuntaje.text = "Puntaje\n\n" + puntos;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (filaBonus.Count != 0)
                {
                    int bonusTemporary = filaBonus.Dequeue();
                    bonusHechos.Add(bonusTemporary);
                    DispararBonus(bonusTemporary);
                }
            }
        }
        else if (end == true)
        {
            Final();
        }
        
	}

    public void BonusenPantalla()
    {
        if(filaBonus.Peek() == 1)
        {
            Destroy(enPantalla);
            enPantalla = Instantiate(bonus1Estatico, new Vector3(172f, 64f, 0f), Quaternion.identity);
        }
        if (filaBonus.Peek() == 2)
        {
            Destroy(enPantalla);
            enPantalla = Instantiate(bonus2Estatico, new Vector3(172f, 64f, 0f), Quaternion.identity);
        }
        if (filaBonus.Peek() == 3)
        {
            Destroy(enPantalla);
            enPantalla = Instantiate(bonus3Estatico, new Vector3(172f, 64f, 0f), Quaternion.identity);
        }
    }

    public void DispararBonus(int bonus)
    {
        if (bonus == 1)
        {
            puntos += objetosEnemigos.Count;
            for(int i=0; i<objetosEnemigos.Count; i++)
            {
                Destroy(objetosEnemigos[i]);
            }
            objetosEnemigos.Clear();
            
        }

        if (bonus == 2)
        {
            Instantiate(disparo, FindObjectOfType<CoheteController>().GetComponent<CoheteController>().transform.position, Quaternion.identity);
        }
        if (bonus == 3)
        {
            Instantiate(disparo2, FindObjectOfType<CoheteController>().GetComponent<CoheteController>().transform.position, Quaternion.identity);
        }

        if (filaBonus.Count == 0)
        {
            Destroy(enPantalla);
        }
        else
        {
            BonusenPantalla();
        }
    }

    void Final()
    {
        canvasInicial.gameObject.SetActive(false);
        canvasFinal.gameObject.SetActive(true);
        for (int i = 0; i < objetosEnemigos.Count; i++)
        {
            Destroy(objetosEnemigos[i]);
        }
        objetosEnemigos.Clear();

        for (int i = 0; i < bonusHechos.Count; i++)
        {
            int aux = bonusHechos[i];
            int j = i - 1;
            while (j >= 0 && bonusHechos[j] > aux)
            {
                bonusHechos[j + 1] = bonusHechos[j];
                j--;
            }
            bonusHechos[j + 1] = aux;
        }
        int puntosExtra = 0;
        for (int i = 1; i <= 3; i++)
        {
            int contador = 0;
            for (int j = 0; j < bonusHechos.Count; j++)
            {
                if (i == bonusHechos[j])
                {
                    contador++;
                }
            }
            puntosExtra = puntosExtra + (contador * contador);
        }

        textoFinal.text = "Puntaje: " + puntos + "\nPuntos extra: " + puntosExtra + "\nTotal: " + (puntos + puntosExtra);
    }

    public void RecargarScene()
    {
        SceneManager.LoadScene("PRINCIPAL");
    }

    public void PlayGame()
    {
        play = true;
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);

    }

    public void CanvasSkin()
    {
        canvas0.gameObject.SetActive(false);
        canvasSkin.gameObject.SetActive(true);
    }

    public void SetSkin(int skin)
    {
        
        for(int i=1; i <= 4; i++)
        {
            if (i != skin)
            {
                diccionarioSkins[i].gameObject.SetActive(false);
            }
            else
            {
                diccionarioSkins[i].gameObject.SetActive(true);
            }
        }
        canvasSkin.gameObject.SetActive(false);
        canvas0.gameObject.SetActive(true);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //Defino una variable que contega la referencia al prefab de la explosion del asteroide y la llamo "explosion". 
    public GameObject explosion;
    //Defino la referencia para almacenar la referencia al prefab del jugador, con el nombre de "playerExplosion". 
    public GameObject playerExplosion;

    //Defino una variable entera que contenga la puntuacion que va a dar el enemigo al ser destruido por el jugador.   
    public int scoreValue;
    //Defino una variable privada que hace referencia al GameController y la llamo "gameController". 
    private GameController gameController;

    void Start()
    {
        //Defino una variable GameObject como "gameControllerObject" para guardar la referencia al objeto que tiene el tag "GameController".  
        GameObject gameControllerObject = GameObject.FindWithTag("GameController"); 
        //Y luego con la referencia al objeto llamo al GetComponent. 
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    //Uso OnTriggerEnter para saber cuando el laser entra dentro del collider del asteroide. 
    void OnTriggerEnter(Collider other)
    {
        //Si el objeto tiene el tag "Boundary" entonces (other.CompareTag("Boundary") valdra verdadero. 
        //return hara que termine la ejecucion del OnTriggerEnter y asi lo posterior no se ejecutara. 
        //No quiero que haga nada cuando tambien el disparo colisione con un objeto que tenga el tag "Enemy1".
        //Y asi marcando los propios disparos de los enemigos, los asteroides y las naves enemigas con el tag "Enemy1", ocurrira un return y no habra ni explosiones, ni se añadiran puntos, ni se destruira ningun objeto.
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy1")) return;
        {
            //Si "explosion" es distinto de null, este se instanciara. 
            if (explosion != null)
            //Si "explosion" no vale nada, no ejecutara el Instantiate.  
            {
                //Instanceo la explosion: la referencia a la explosion,la posicion del asteroide donde aparecera tambien la explosion y la rotacion, estas dos ultimas las obtengo del transform.  
                Instantiate(explosion, transform.position, transform.rotation);
            }
            //Se instanciara solo cuando el disparo del enemigo colisione con el objeto del jugador. 
            if (other.CompareTag("Player"))
            {
                //Instanceo la explosion del jugador: su referencia a la explosion, la posicion del jugador donde aparecera la explosion y la rotacion. 
                //Estas dos ultimas las obtengo del transform, y "other", objeto que pertenece al collider (nave del jugador).
                //Y de esta manera, solo se instanciara la explosion del jugador, cuando sea él, el que colisione. 
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                //Con la referencia al "gameController" de este script, lo uso para llamar al "GameOver". 
                gameController.GameOver();
            }
        }
        //Ya con el "gameController", justo antes de ser destruido el objeto, mando a llamar la variable que referencia al "gameController" con AddScore y cuanto se va a incrementar el valor "scoreValue" que valga el objeto que acaba de ser destruido.   
        //Se va a ejecutar esta linea pero al valer 0, no se va a incrementar la puntuacion. 
        gameController.AddScore(scoreValue);
        //Se destruye el objeto con el que colisione el disparo. 
        Destroy(other.gameObject);
        //Y se destruya el disparo de la nave enemiga.  
        Destroy(gameObject);
    }
}

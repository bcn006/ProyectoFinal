using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Añado "UnityEngine.SceneManagement" para poder utilizar el "SceneManager". 
using UnityEngine.SceneManagement;
//Añado "UnityEngine.UI" para no tener errores en la variable Text. 
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //array tipo GameObject, de refencia a todos los prefabs a generar 
    //Defino la variable publica "hazards" (peligros) como un array de tipo GameObject, teniendo la referencia a todos los prefabs que quiera generar. 
    public GameObject[] hazards;
    //Defino la variable de tipo Vector3 y la llamo "spawnValues" (generador de valores). 
    public Vector3 spawnValues;
    //Defino una variable entera que la nombro "hazardCount" (contador o total de peligros). 
    public int hazardCount;
    //Defino una variable de tipo entero que llamo "spawnWait" (la espera entre el generador de asteroides).
    public float spawnWait;
    //Defino una variable tipo float y llamo "startWait" (espera inicial). 
    public float startWait;
    //Defino una variable tipo float y lo llamo "waveWait" (ola de espera).
    public float waveWait;

    //Defino una variable privada, ya que la puntuacion no la manejare desde ningun otro script y tampoco la quiero establecer desde el inspector, de tipo entero que tenga la puntuacion y la llamo "score".
    private int score;
    //Defino una variable publica de tipo Text y la llamo "scoreText" (Texto de puntuacion). 
    public Text scoreText;

    //Obtengo la referencia al GameObject como "restartGameObject". 
    public GameObject restartGameObject;
    //Defino una variable privada (ya que el uso sera interno en el script) de tipo logico (bool) y la llamo "restart" (reiniciar). 
    private bool restart;
    //Defino una variable publica de tipo Text y lo llamo "gameOverText" (Texto de juego terminado").
    public Text gameOverText;
    //Defino una variable privada de tipo logico (bool) y la llamo "gameOver" (Juego Terminado).   
    private bool gameOver; 

    void Start()
    {
        //"restart" empezara valiendo falso.  
        restart = false;
        //"restartGameObject" que hace referencia al GameObject del "restart", (.gameObject) que hace referencia al objeto y (.SetActive) y lo desactivo usando "false".
        restartGameObject.gameObject.SetActive(false);
        //Tambien "gameOver" empezara valiendo falso. 
        gameOver = false;
        //"gameOverText" que hace referencia al componente Texto del "gameOver", (.gameObject) que hace referencia al objeto y (.SetActive) y lo desactivo usando "false". 
        gameOverText.gameObject.SetActive(false); 
        //Pongo la puntuacion a 0. 
        score = 0;
        //Cada vez que cambie el valor de la puntuacion tendre que llamar al metodo "UpdateScore" para que actualice la cadena de texto que est mostrando el objeto "Score".  
        UpdateScore();
        //Llamo la corrutina usando "StartCoroutine" y dentro asi hago la llamada del metodo. 
        StartCoroutine(SpawnWaves());
    }
    //Uso "Update" para saber que teclas se estan pulsando en cada fotograma. 
    void Update()
    {
        //Si "restart" es verdadero (true) y se sabe que se ha pulsado la tecla "R".  
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            //Mando a llamar al metodo "Restart". 
            Restart();
        }
    }

    //Creo un metodo publico para poder establecerlo en el boton y lo llamo "Restart". 
    public void Restart()
    {
        //Se recargara la misma escena del inicio, en valor entero como 0.
        SceneManager.LoadScene(0);
    }

    //Creo un metodo que se encargara de instanciar el asteroide y lo llamo "SpawnWaves"(Generador de asteroides).
    //Convierto el metodo void en una corrutina tipo IEnumerator. Una corrutina es un metodo en el cual, dentro del codigo de ese metodo, puedo aplazar su ejecucion hasta el siguiente fotograma o hasta dentro de un tiempo concreto. 
    IEnumerator SpawnWaves()
    {
        //Hago la espera incial al principio, conforme al tiempo de "spawnWait". 
        yield return new WaitForSeconds(spawnWait);
        //Uso una orden while, que mientras este bucle infinito que siempre sera verdadero y de ahi no va a salir. Esto lanzara 10 olas e inmediatamente despues otras 10, y asi continuamente. 
        while (true)
        {
            //Por medio de un bucle con una variable "i" que empieza desde 0, el bucle se ejecute mientras "i" sea menor que "hazardCount" y ya al final de cada bucle incremento el valor de "i".
            for (int i = 0; i < hazardCount; i++)
            //De esta forma se van a instanciar tantos asteroides como valor tengamos establecido en la variable "hazardCount". 
            {
                //Declaro una variable tipo GameObject con el nombre de "hazard". Accedo al array, selecciono el indice de manera aleatoria usando Random.Range, seleccionara un numero aleatorio entre 0 y 2, ya que nunca selecciona el ultimo valor. 
                //Hago referencia a la variable "hazards",".Length" que devuelve el total de elementos que tiene ese array.                                      
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                //Defino una variable local Vector3 con el nombre de "spawnPosition". Uso "spawnValues" para los vectores (x,y,z) requeridos. 
                //Sustituyo el valor de la "x" que va a ser un valor aleatorio (Random.Range) entre "spawnValues" como valor negativo y positivo. 
                //Asi que la "x" de este nuevo Vector3, que sera la posicion donde se va instanciar los asteroides, va a estar entre el valor negativo "x" de spawnValues y su valor positivo. 
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                //Despues instancio: con la referencia al "hazard" aleatorio, la posicion(aqui uso una variable "spawnPosition") y la rotacion que quiero que sea 0 por lo que uso "Quaternion.identity". 
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                //Uso la variable "spawnWait" en esta instruccion, con "yield return" empezara cuando se inicie la corrutina, se iniciara el bucle, se instanciara el primer asteroide y al llegar a esta parte, se pospondra su ejecucion (en espera), hasta que pase el tiempo de "spawnWait".      
                yield return new WaitForSeconds(spawnWait);
                //Y cuando pase ese tiempo, ya hara la siguiente vuelta del bucle y volvera a instanciar el siguiente asteroide.
            }
            //Con esto esperara, se lanzara una ola de 10 asteroides, esperara otra vez, lanzara de nuevo otra ola y asi consecutivamente.
            yield return new WaitForSeconds(waveWait);

            //Si la variable "gameOver" se ha activado. 
            if (gameOver)
            {
                //"restartGameObject" que hace referencia al GameObject del "restart", (.gameObject) que hace referencia al objeto y (.SetActive) y lo activo usando "true". Entonces se mostrara el boton para reiniciar. 
                restartGameObject.gameObject.SetActive(true);
                //Pongo la variable "restart" a verdadero para activar el modo de empezar a ver si el usuario pulsa la tecla "R" o el boton de "restart".
                restart = true;
                //Y se saldra de el bucle infinito "while (true)" mediante "break". Y la corrutina terminaria su trabajo, ya que ya no habria mas asteroides que instanciar. 
                break; 
            }
        }
    }

    //Creo otro metodo publico y lo llamo "AddScore", y mas el valor que se va a incrementar a la variable "score". 
    public void AddScore(int value)
    {
        //Agrego "score" e incremento ese valor. Aqui se ctualiza la variable numerica.  
        score += value;
        //Cada vez que se cambie el "score", se tendra que llamar al metodo "UpdateScore" para que se actualice el texto de "score". 
        UpdateScore(); 
    }

    //Llamo al metodo "UpdateScore" (Actualizar Puntuacion), que asigna la cadena de texto (Score) con el valor actualizado. 
    void UpdateScore()
    {
        //Pongo la variable que representa al texto, su texto, le asigno la cadena de texto "Score: " junto con el valor que tuviera la variable "score" en el momento de asignarse.   
        scoreText.text = "Score: " + score;
    }

    //Creo un metodo publico (para que este accesible para otros scripts) que se llama "GameOver".
    public void GameOver()
    {
        //"gameOverText" que hace referencia al componente Texto del "gameOver", (.gameObject) que hace referencia al objeto y (.SetActive) y lo activo usando "true".
        gameOverText.gameObject.SetActive(true);
        //A "gameOver" le pongo verdadero para indicar que ya se esta en "Game Over". 
        gameOver = true; 
    }
}

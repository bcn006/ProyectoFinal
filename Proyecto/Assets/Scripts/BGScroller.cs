using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    //Defino una variable publica que defina la velocidad con la que quiero que se mueva y la llamo "scrollSpeed" (velocidad del scroll).
    public float scrollSpeed;

    //Defino una variable privada de tipo Vector3 (para guardar su posicion) y la llamo "startPosition" (posicion inicial).
    private Vector3 startPosition;
    //Defino una variable privada de tipo real (con numeros decimales) y la guardo como "tileSize" (tamaño de la altura del background). 
    private float tileSize;

    //Inicio con Start los siguientes dos valores.
    void Start()
    {
        //La posicion inicial que esta colocado en el objeto "Background", ya que quiero alamcenarla para luego poder restablecerlo.      
        startPosition = transform.position;
        //El "tileSize" lo tomo del transform de la escala local del "y", ya que este valor me va a dar cuanto de alto. 
        tileSize = transform.localScale.y; 
    }

    // Update is called once per frame
    void Update()
    {
        //Defino un float que va a contener la distancia que tendra que desplazarse de la posicion inicial. 
        //Llamo al metodo "Mathf.Repeat"(haciendo lo mismo que el modulo pero con numeros decimales), con los segundos actuales y lo multiplico por "scrollSpeed" (tiene un valor inferior a 1), mas la longitud ("tileSize") hasta donde quiero que llegue ese valor antes volver a empezar de nuevo.  
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        //"transform.position" es igual a la posicion inicial que tenga, sumandole un vector con "forward"(que referencia a la z) y lo multiplico por el valor de "newPosition". 
        //Haciendo que "z" valga "newPosition". Y teniendo ese offset, se le agregara a la posicion inicial. 
        transform.position = startPosition + Vector3.forward * newPosition; 
    }
}

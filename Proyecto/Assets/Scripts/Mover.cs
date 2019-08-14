using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Defino una variable publica de tipo real que se llama speed (velocidad). 
    public float speed;

    //Defino una variable privada Rigidbody como "rig". 
    private Rigidbody rig;
    //Para asignarle el valor uso Awake. 
    void Awake()
    {
        //Obtengo la referencia con GetComponent del Rigidbody.  
        rig = GetComponent<Rigidbody>();
    }
    //Uso Start para que se le aplique la velocidad en cuanto el objeto se inicie en la escena. 
    void Start()
    {
        //Uso la referencia al Rigidbody para aplicarle la velocidad. Accedo al transform del objeto, con el eje (forward). 
        //De esta forma se tendria siempre el vector velocidad, apuntando con una velocidad de una unidad por segundo, ya que el vector "forward" esta normalizado. 
        //Multiplico el vector de direccion (transform.forward) por la velocidad (speed).
        rig.velocity = transform.forward * speed;
        //Y asi, como el vector de direccion siempre mide una unidad, al multiplicarlo por la velocidad, si la velocidad es "5", pues el vector de direccion medira "5", siendo una velocidad de "5" unidades por segundo. 
    }

}

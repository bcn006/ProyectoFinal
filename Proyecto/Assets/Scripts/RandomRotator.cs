using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    //Defino una variable publica para establecer la velocidad de rotacion desde el inspector. La nombro "tumble" como velocidad de giro. 
    public float tumble;
    //Defino una variable privada Rigidbody como "rig".
    private Rigidbody rig;

    //Uso Awake para obtener las referencias de los componentes. 
    void Awake()
    {
        //Obtengo la referencia al Rigidbody.
        rig = GetComponent<Rigidbody>();  
    }

    //Uso Start para darle una fuerza de empuje, es decir una velocidad de giro angular inicial. 
    void Start()
    {
        //Con la referencia al Rigidbody, establezco la velocidad angular (angularVelocity) que es la velocidad de giro que va a afectar solo a la rotacion del objeto. 
        //Uso Random.insideUnitSphere ya que obtiene un punto aleatorio dentro de una esfera de radio 1, pero el valor no estara normalizado.Y lo multiplico por "tumble" para que asi tenga una magnitud equivalente a el valor que tenga "tumble". 
        rig.angularVelocity = Random.insideUnitSphere * tumble; 
        //Y asi cada asteroide tiene su propia velocidad de giro lo cual sera mas realista. 
    }

    
}

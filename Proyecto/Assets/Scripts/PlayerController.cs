using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hago el tipo "Boundary" serializable y asi aparecera la variable publica (boundary) dentro del inspector.  
[System.Serializable]
//Defino otra clase publica, nombrada como "Boundary"(Limite). Va a servir como un tipo que va a contener las variables de abajo (maximo y minimo de "x" y "z").  
public class Boundary
{
    //Defino estas variables, todas agrupadas en "Boundary".  
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    //Añado una cabecera a este grupo de variables, con el nombre de "Movement" (Movimiento).  
    [Header("Movement")]
    //Defino la variable publica para velocidad("speed") y asi la puedo establecer desde el inspector. 
    public float speed;
    //Defino una variable que se llama "tilt"(inclinacion). 
    public float tilt;
    //Player Controller va a usar una variable de tipo "Boundary", llamada "boundary". 
    public Boundary boundary;
    //Defino una variable privada Rigidbody como "rig".
    private Rigidbody rig;

    //A este otro grupo de variables, de igual forma le asigno una cabecera, que se va a referir al disparo, con el nombre de "Shooting" (Disparo).
    [Header("Shooting")]
    //Defino la variable publica para el prefab, de tipo GameObject y lo llamo "shot" (disparo). 
    public GameObject shot;
    //Defino una variable publica de tipo Transform y la llamo "shotSpawn" (Generador de disparo). 
    public Transform shotSpawn;
    //Defino una variable real y la llamo "fireRate" (tasa de disparo): cada cuanto tiempo (minimo), se va a poder disparar. 
    public float fireRate;
    //Defino una variable privada que va a indicar en cuantos segundos de iniciado el juego vamos a poder volver a disparar. Y la nombro "nextFire", que inicialmente valdra 0.  
    private float nextFire;

    //Defino una variable privada de tipo AudioSource. Con el nombre de "audioSource" (como fuente de audio). 
    private AudioSource audioSource; 

    //Uso Awake para asignar valores. 
    void Awake ()
    {
        //Obtengo la referencia con GetComponent del Rigidbody.  
        rig = GetComponent<Rigidbody>();
        //Obtengo la referencia del AudioSource dentro de Awake. 
        audioSource = GetComponent<AudioSource>();
    }
    //Uso Update ya que el disparo no tiene nada que ver con el sistema de fisicas. 
    void Update()
    {
        //En cada fotograma del juego voy a comprobar si el usuario ha pulsado una tecla en concreto, mediante un if y la clase Input.GetButton, entre parentesis y comillas ya que tengo darle un string. 
        //Se pone "Fire1", que eso es un eje que esta definido dentro de la seccion Input de Unity. 
        //Cuando esto de verdadero, se instanciara por medio la instruccion Instantiate. Si ademas de pulsar el boton de disparo para disparar y que el tiempo actual sea mayor que el tiempo programado al que se tiene que llegar para poder volver a disparar.
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            //Cambio el valor de "nextFire" para que en el siguiente fotograma, la parte posterior no se cumpla de nuevo. Y al tiempo actual sumandole la tasa de disparo (fireRate). 
            //De esta forma si en el Time.time son las 10:00 y el "fireRate" es un segundo, nextFire hara que no se pueda volver a disparar hasta que sean las 10:01.
            nextFire = Time.time + fireRate;
            //Instantiate requiere una referencia al objeto que quiero que copie ("shot"), la posicion ("shotSpawn", como esta referencia es Transform puedo acceder a su posicion) y la rotacion siempre sera "0,0,0" mediante "Quaternion.identity".
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
            //Justo despues de instanciar, le solicito al audioSource que reproduzca el sonido. 
            audioSource.Play();
        }
    }
    //Uso FixedUpdate para la fisica.
    void FixedUpdate()
    {
        //Defino la variable que va a obtener el valor usando Input.GetAxis, para mover al personaje de forma horizontal.  
        float moveHorizontal = Input.GetAxis("Horizontal");
        //De igual forma, pero para moverlo verticalmente usando las flechas hacia arriba y hacia abajo.  
        float moveVertical = Input.GetAxis("Vertical");

        //Defino la variable Vector3 que va a obtener el valor de la velocidad en cada uno de los ejes que se va a aplicar al Rigidbody. 
        //La nave solo se movera en "x" y "z", por lo que mando a llamar los valores que obtenemos al movernos en vertical y horizontal. Y en la "y" no quiero que se mueva asi que le asigno el 0.
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        //Gracias a su referencia accedo a su velocidad y le asigno la velocidad que he calculado aqui arriba. Los valores de Horizontal y Vertical o sea el Vector3, los multiplico por la velocidad.  
        rig.velocity = movement * speed;
        //Aqui establezco su posicion usando su Rigidbody. La posicion en la "y" ya se sabe que es 0, ya que no cambiara. En la "x" siempre estara dentro de una posicion minima y maxima, y para eso usare la libreia Mathf. 
        //Usare un metodo llamado Clamp (asegura que el valor que le demos como primer parametro, esta entre un minimo y un maximo, mientras este dentro estos dos valores, esta funcion va a devolver el mismo valor).
        //En la posicion "x", se va obtener la posicion x actual del Rigidbody, y lo mismo para "z". Uso "boundary", para el acceso a "Boundary" usando el operador punto. 
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        //Asi como arriba cambie su posicion, aqui establecere su rotacion. Y para las rotaciones de Unity desde codigo, funcionan con Quaternion. En .Euler pongo los grados que quiero que este girado en x, y, z. 
        //Pero solo quiero que gire en z, asi que asigno 0° a "x" y "y". Y la "z" va a tener tanto de giro en base a la velocidad en x del Rigidbody. Por lo que accedo al rig.velocity.x y la nave se girara un maximo de 10° tanto para un lado como para otro.
        //Y lo multiplico por la variable "-tilt". 
        rig.rotation = Quaternion.Euler(0f, 0f, rig.velocity.x * -tilt);
    }
}

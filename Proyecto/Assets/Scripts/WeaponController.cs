using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Defino una variable de tipo GameObject para referenciar al prefab del disparo. Lo llamo "shot" (disparo). 
    public GameObject shot;
    //Defino una variable de tipo Transform que va a tener la posicion y rotacion donde quiero poner el disparo. Y lo llamo "shotSpawn".  
    public Transform shotSpawn;
    //Defino una variable publica con el nombre de "delay" (espera inicial), en cuanto la nave enemiga aparezca en escena, se esperara un cierto tiempo y pasado ese tiempo va a disparar. 
    public float delay;
    //Defino una variable publica con el nombre de "fireRate" (espera entre llamadas a "Fire"). 
    //Una vez que dispare, va a esperar un cierto tiempo, que estara guardado en "fireRate", que sera el tiempo que indique cada cuanto va a volver a disparar.   
    public float fireRate;

    //Agrego la referencia al AudioSource. 
    private AudioSource audioSource;

    //Para obtener la referencia lo hago en el Awake.  
    private void Awake()
    {
        //Añado el componente AudioSource con la referencia al sonido del disparo, para que asi, "GetComponent<AudioSource>" lo pueda obtener.   
        audioSource = GetComponent<AudioSource>(); 
    }

    //Llamo un metodo desde el Start, que en cuanto la nave enemiga aparezca en escena iniciada, se ejecutara InvokeRepeating. 
    void Start()
    {
        //InvokeRepeating lo que hace es programar una serie de llamadas a un metodo de forma automatica. 
        //Como primer parametro requiere el nombre del metodo que quiero que se ejecute ("Fire"), luego el tiempo inicial que va a tener que esperar antes de hacer la primera llamada al metodo "Fire" ("delay"), y luego la tasa de repeticion, es decir, cuanto tiempo tiene que esperar entre una llamada a "Fire" y la siguiente (fireRate). 
        //Y asi sera infinitamente mientras el enemigo este activo en la escena. 
        InvokeRepeating("Fire", delay, fireRate); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creo un metodo nuevo que llamo "Fire" (Disparar), que al llamarse hara lo siguiente. 
    void Fire()
    {
        //Instancio el prefab de "shot", en la posicion del objeto "shotSpawn" y con su rotacion.
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //Y luego hare que suene el sonido al disparar en la nave enemiga, referenciando al componente y luego llamando a su metodo Play. 
        audioSource.Play();
    }
}

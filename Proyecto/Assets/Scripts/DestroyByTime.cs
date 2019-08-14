using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    //Defino una variable tipo float, que indique el tiempo. Con el nombre de "lifeTime" (como tiempo de vida).
    public float lifeTime;

    void Start()
    {
        //Llamo a destruir el gameObject que contendra la referencia al objeto en donde este colocado el script "DestroyByTime", siendo el objeto de la explosion. 
        //Ademas de asestar un gameObject, asesta tambien un tiempo, que es el tiempo en el cual, es donde se programa para destruir el objeto. 
        Destroy(gameObject, lifeTime);
        //Y por ende, el objeto se destruira pasado el tiempo de "lifeTime".
    }

}

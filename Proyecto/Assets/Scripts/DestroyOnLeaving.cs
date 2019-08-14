using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLeaving : MonoBehaviour
{
    //Uso OnTriggerExit para que el collider deje de estar en contacto. Tambien obtengo la referencia al collider other (otro objeto), el cual va a salir. 
    //De esta forma OnTriggerExit avisara si el objeto collider se acaba de escapar.  
    void OnTriggerExit(Collider other)
    {
        //Y al momento de que el objeto se escape, este lo destruira.  
        Destroy(other.gameObject); 
    }
}

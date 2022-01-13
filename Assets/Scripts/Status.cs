using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [HideInInspector]
    public int Vida;
    public int VidaInicial = 100;
    public float Velocidade = 5;
    public float tempoPosicoesAleatoria = 4;



    void Awake(){

        Vida = VidaInicial;
    }
}

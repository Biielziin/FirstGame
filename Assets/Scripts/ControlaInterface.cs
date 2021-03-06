using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour 
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public Status statusJogador;
    

    void Start () {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        // SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {

    private Rigidbody rigidbodyJogador;
    Vector3 direcao;
    void Awake()
    {
        rigidbodyJogador = GetComponent<Rigidbody>();
    }

    public void Movimentar (Vector3 direcao, float velocidade)
    {
        rigidbodyJogador.MovePosition (rigidbodyJogador.position + (direcao.normalized * velocidade * Time.deltaTime));
    }

    public void Rotacionar (Vector3 direcao)
{
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyJogador.MoveRotation(novaRotacao);
}
}

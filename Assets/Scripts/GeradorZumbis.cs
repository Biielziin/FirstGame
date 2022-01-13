using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;


    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }

	void Update () {

        if(Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao)
        {
            contadorTempo += Time.deltaTime;

            if(contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZombie());
                contadorTempo = 0;
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZombie()
    {
        Vector3 posicaoDeCriacao = PosicaoNascerAleatorio();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoDeCriacao = PosicaoNascerAleatorio();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi); 
            yield return null;
        }

        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 PosicaoNascerAleatorio()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    public GameObject Jogador;
    private Animator animatorInimigo;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomDaMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;

    private int DistanciaAleatorizarPosicao = 10;

	
	void Start () {
        Jogador = GameObject.FindWithTag("Jogador");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
        animatorInimigo = GetComponent<Animator>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<Status>();
    }
	

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            MoverEAtacar();
        }
        else
        {
            animacaoInimigo.Atacar(true);
        }
    }

    void MoverEAtacar()
    {
        direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        animacaoInimigo.Atacar(false);
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <=0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += statusInimigo.tempoPosicoesAleatoria;
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if(ficouPertoOSuficiente == false)
        {
        direcao = posicaoAleatoria - transform.position;
        movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * DistanciaAleatorizarPosicao;
        posicao += transform.position;
        posicao.y = transform.position.y;
        
        return posicao;
    }

    void AtacaJogador ()
    {
        int Dano = Random.Range(15, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(Dano);
        
    }


    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if(statusInimigo.Vida <= 0)
        {
            Morrer();
        }

    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDaMorte);

    }
}

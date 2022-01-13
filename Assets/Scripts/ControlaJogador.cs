using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel
{

    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public bool Vivo = true;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador MovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    private void Start()
    {
        Time.timeScale = 1;
        MovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }
  
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        animacaoJogador.Movimentar(direcao.magnitude);

        if(statusJogador.Vida <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
        MovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);
        
        MovimentoJogador.RotacionarJogador(MascaraChao);
    }
    


       public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;

        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        
        scriptControlaInterface.AtualizarSliderVidaJogador();
        if(statusJogador.Vida <= 0)
        {
            Morrer();
        } 
    }

    public void Morrer()
    {
        Time.timeScale = 0;
        TextoGameOver.SetActive(true);
    }
    
}

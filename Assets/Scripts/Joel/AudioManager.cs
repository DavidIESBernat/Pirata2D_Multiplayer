using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource backgroundMusic;       
    public AudioSource sfxSource;         
    public AudioSource dialogueSource;
    public AudioSource ambientSource;
    public AudioSource uiSource;
    public AudioClip[] audioClips;
    public AudioClip[] audioUI;


    /* CREAR UN NUEVO ARRAY SOLO PARA LOS CLIPS DE LOS DIALOGOS. EL OTRO SOLO PARA EFECTOS DE INTERACCION, ETC */

    private AudioClip backgroundClip;
    private AudioClip sfxClip;
    private AudioClip uiClip;


    // VARIABLES SONIDO BARBOSSA
    public List<AudioClip> audioClipsBarbossa;
    private AudioClip currentClip;
    public AudioSource source;
    public float minWaitBetweenPlays = 1f;
    public float maxWaitBetweenPlays = 30f;
    public float waitTimeCountdown = -1f;



    public AudioSource audioSourceEnemigo;
    public AudioClip dañoEnemigo;

    public AudioSource jackEmpezarSourceSent;
    public AudioClip jackEmpezarSound;
    
    public AudioClip barbossasFinalPhrase;

    public AudioClip finalBossFightClip;
    //public Health health;

    public void ReproduceBarbossasSoundEffects()
    {
        if (!source.isPlaying)
        {
            if (waitTimeCountdown < 0f)
            {
                currentClip = audioClipsBarbossa[Random.Range(0, audioClipsBarbossa.Count)];
                source.clip = currentClip;
                source.Play();
                waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
            }
            else
            {
                waitTimeCountdown -= Time.deltaTime;
            }
        }
    }

    public void ReproduceBarbossasDyingPhraseSoundEffects()
    {
        source.clip = barbossasFinalPhrase;
        source.Play();
    }

    public void ReproduceEnemigoDaño(AudioSource enemySourceSent)
    {
        enemySourceSent.clip = dañoEnemigo;
        enemySourceSent.Play();
        //Debug.Log("REPRODUCIDO!");
    }

    public void ReproduceFinalFightSoundEffect(AudioSource finalFightSourceSent)
    {
        finalFightSourceSent.clip = finalBossFightClip;
        finalFightSourceSent.Play();
        //Debug.Log("MUSICA BATALLA FINAL!");
    }

    public void ReproduceSonidoJackEmpezar()
    {
        jackEmpezarSourceSent.clip = jackEmpezarSound;
        jackEmpezarSourceSent.Play();
        //Debug.Log("REPRODUCIDO!");
    }

    // ------------------------


    /* public Dialogue dialogue;
    public PlayerMovement playerMovement; */
    //private void Awake()
    //{
    //    // Configuración del Singleton
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);  // Mantiene el AudioManager entre escenas
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    void Start()
    {
        //PlayBackgroundMusic();
    }

    // Musica de fondo
    public void PlayBackgroundMusic()
    {
        foreach (var audio in audioClips)
        {
            if (audio.name == "backgroundClip")
            {
                backgroundClip = audio;
                backgroundMusic.clip = backgroundClip;
                backgroundMusic.loop = true;
                backgroundMusic.Play();
                break; 
            }
        }
    }

    // Efectos de sonido (monedas, ron, manzana, etc)
    public void PlaySfx(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            sfxClip = audioClips[index];
            sfxSource.PlayOneShot(sfxClip);
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango: " + index);
        }
    }

    // Efectos de sonido de la UI (Interfaz)
    public void PlayUI(int index)
    {
        if (index >= 0 && index < audioUI.Length)
        {
            uiClip = audioUI[index];
            uiSource.PlayOneShot(uiClip);
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango: " + index);
        }
    }

    // Dialogo de los tripulantes
    public void PlayDialogue(AudioClip[] list, int index)
    {
        AudioSource dialogueSfx = gameObject.GetComponent<AudioSource>();
        
        if (index >= 0 && index < list.Length) 
        {
            if(index == 0)
            {
                dialogueSfx.PlayOneShot(list[index]);
            }
            else if(index == 1)
            {
                if(list[0] != null)
                {
                    dialogueSfx.Stop();
                }
                dialogueSfx.PlayOneShot(list[index]);
            }
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango: " + index);
        }
    }

    /* public void StopCrewMemberDialogue()
    {
        AudioSource dialogueSfx = gameObject.GetComponent<AudioSource>();
        if(dialogueSfx.isPlaying)
        {
            dialogueSfx.Stop();
            dialogue.panel.SetActive(false);
            playerMovement.enabled = !playerMovement.enabled;
        }
    } */

    public void PlayBusinessmanDialogue(AudioClip[] list, int index)
    {
        AudioSource dialogueSfx = gameObject.GetComponent<AudioSource>();
        Debug.Log("NOMBRE DEL OBJETO: " + gameObject.name);   
        if (index >= 0 && index < list.Length) 
        {
            for (int i = 0; i < index; i++)
            {
                if(list[i] != null)
                {
                    dialogueSfx.Stop();
                }
            }
            dialogueSfx.PlayOneShot(list[index]);
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango: " + index);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EfectosSonido : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    private AudioSource controlAudio;

    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }
    
    public void SeleccionarSonido(int index, float volumen)
    {
        controlAudio.PlayOneShot(audios[index], volumen);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips; // Lista pública de clipes de áudio
    private Dictionary<string, AudioSource> audioSources; // Dicionário para armazenar AudioSources
    public string loopClipName; // Nome do clipe de áudio que deve ficar em loop
    public float loopVolume = 0.5f; // Volume normal do áudio em loop
    public float reducedVolume = 0.2f; // Volume reduzido do áudio em loop

    private AudioSource loopAudioSource; // AudioSource para o áudio em loop
    //private AudioSource welcomeAudioSource;

    public string welcomeClipName;

    private void Awake()
    {
        // Inicializa o dicionário
        audioSources = new Dictionary<string, AudioSource>();

        // Adiciona um AudioSource para cada AudioClip na lista
        foreach (AudioClip clip in audioClips)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.playOnAwake = false;
            audioSources[clip.name] = source;

            // Configura o áudio em loop se for o clipe designado
            if (clip.name == loopClipName)
            {
                loopAudioSource = source;
                loopAudioSource.loop = true;
                loopAudioSource.volume = loopVolume;
                loopAudioSource.Play();
                loopAudioSource.playOnAwake = true;
            }
            /*
            if(clip.name == welcomeClipName)
            {
                welcomeAudioSource = source;
                welcomeAudioSource.Play();
                welcomeAudioSource.playOnAwake = true;
            }
            */
        }
    }

    // Método para tocar um áudio pelo nome do clipe
    public void PlayAudio(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
            if (audioSources[clipName] != loopAudioSource)
            {
                ReduceLoopVolume();
            }

            audioSources[clipName].Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }

    // Método para parar um áudio pelo nome do clipe
    public void StopAudio(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].Stop();
            CheckActiveAudioSources();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }

    // Método para parar todos os áudios
    public void StopAllAudio()
    {
        foreach (var source in audioSources.Values)
        {
            source.Stop();
        }
        ResetLoopVolume();
    }

    // Método para reduzir o volume do áudio em loop
    private void ReduceLoopVolume()
    {
        if (loopAudioSource != null)
        {
            loopAudioSource.volume = reducedVolume;
        }
    }

    // Método para resetar o volume do áudio em loop
    private void ResetLoopVolume()
    {
        if (loopAudioSource != null)
        {
            loopAudioSource.volume = loopVolume;
        }
    }

    // Verifica se há algum áudio ativo além do loop
    private void CheckActiveAudioSources()
    {
        foreach (var source in audioSources.Values)
        {
            if (source.isPlaying && source != loopAudioSource)
            {
                ReduceLoopVolume();
                return;
            }
        }
        ResetLoopVolume();
    }
}

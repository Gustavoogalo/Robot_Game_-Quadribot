using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips; // Lista p�blica de clipes de �udio
    private Dictionary<string, AudioSource> audioSources; // Dicion�rio para armazenar AudioSources
    public string loopClipName; // Nome do clipe de �udio que deve ficar em loop
    public float loopVolume = 0.5f; // Volume normal do �udio em loop
    public float reducedVolume = 0.2f; // Volume reduzido do �udio em loop

    private AudioSource loopAudioSource; // AudioSource para o �udio em loop
    //private AudioSource welcomeAudioSource;

    public string welcomeClipName;

    private void Awake()
    {
        // Inicializa o dicion�rio
        audioSources = new Dictionary<string, AudioSource>();

        // Adiciona um AudioSource para cada AudioClip na lista
        foreach (AudioClip clip in audioClips)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.playOnAwake = false;
            audioSources[clip.name] = source;

            // Configura o �udio em loop se for o clipe designado
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

    // M�todo para tocar um �udio pelo nome do clipe
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

    // M�todo para parar um �udio pelo nome do clipe
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

    // M�todo para parar todos os �udios
    public void StopAllAudio()
    {
        foreach (var source in audioSources.Values)
        {
            source.Stop();
        }
        ResetLoopVolume();
    }

    // M�todo para reduzir o volume do �udio em loop
    private void ReduceLoopVolume()
    {
        if (loopAudioSource != null)
        {
            loopAudioSource.volume = reducedVolume;
        }
    }

    // M�todo para resetar o volume do �udio em loop
    private void ResetLoopVolume()
    {
        if (loopAudioSource != null)
        {
            loopAudioSource.volume = loopVolume;
        }
    }

    // Verifica se h� algum �udio ativo al�m do loop
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

    // 音源
    private AudioSource audioSource;
    // 字典容器
    private Dictionary<string, AudioClip> dictAudio;

    [Tooltip("播放许可")]
    public bool canPlay = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 初始化变量
        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }
    
    /// <summary>
    /// 辅助函数：加载音频，需要确保音频文件的路径在Resources文件夹下
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);
    }

    /// <summary>
    /// 获取音频，并且将其缓存在dictAudio中，避免重复加载
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private AudioClip GetAudio(string path)
    {
        if (!dictAudio.ContainsKey(path)) 
        {
            dictAudio[path] = LoadAudio(name);
        }
        return dictAudio[path];
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="name"></param>
    /// <param name="volume"></param>
    public void PlayBGM(string name, float volume = 1.5f)
    {
        if(canPlay)
        {
            audioSource.Stop();
            audioSource.clip = GetAudio(name);
            audioSource.Play();
        }
    }

    /// <summary>
    /// 暂停BGM
    /// </summary>
    public void StopBGM()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path"></param>
    /// <param name="volume"></param>
    public void PlaySound(string path, float volume = 1f)
    {
        if(canPlay)
        {
            // PlayOneShot可以叠加播放
            this.audioSource.PlayOneShot(LoadAudio(path), volume);
        }
    }
}

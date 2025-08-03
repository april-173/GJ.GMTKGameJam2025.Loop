using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

    // ��Դ
    private AudioSource audioSource;
    // �ֵ�����
    private Dictionary<string, AudioClip> dictAudio;

    [Tooltip("�������")]
    public bool canPlay = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // ��ʼ������
        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }
    
    /// <summary>
    /// ����������������Ƶ����Ҫȷ����Ƶ�ļ���·����Resources�ļ�����
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);
    }

    /// <summary>
    /// ��ȡ��Ƶ�����ҽ��仺����dictAudio�У������ظ�����
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
    /// ����BGM
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
    /// ��ͣBGM
    /// </summary>
    public void StopBGM()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="path"></param>
    /// <param name="volume"></param>
    public void PlaySound(string path, float volume = 1f)
    {
        if(canPlay)
        {
            // PlayOneShot���Ե��Ӳ���
            this.audioSource.PlayOneShot(LoadAudio(path), volume);
        }
    }
}

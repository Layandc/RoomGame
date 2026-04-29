using UnityEngine;

public class ChangeSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sounds;
    private int index = 0;

    public void ChangeMusic()
    {
        index++;

        if (index >= sounds.Length)
            index = 0;

        audioSource.clip = sounds[index];
        audioSource.Play();
    }
}
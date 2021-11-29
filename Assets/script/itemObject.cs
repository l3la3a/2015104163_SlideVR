using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObject : MonoBehaviour
{
    Animator ani;
    public GameObject child;
    public GameObject effect;

    public AudioClip effectSound;
    AudioSource _audio;
    void Start()
    {
        ani = child.gameObject.GetComponent<Animator>();
        _audio = this.gameObject.AddComponent<AudioSource>();
        _audio.clip = effectSound;
        _audio.loop = false;
        _audio.playOnAwake = false;
    }

    public void getCollision() {
        ani.SetBool("delete",true);
        _audio.Play();
        effect.SetActive(true);
        StartCoroutine(setCount());
    }

    IEnumerator setCount() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

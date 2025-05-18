using System.Collections;
using Core.DI;
using Core.DI.Contexts;
using Core.Storages;
using Session;
using UnityEngine;
namespace Music
{
  public class MusicController : MonoBehaviour
  {
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _clip;

    private void Awake()
    {
      if (ProjectContext.Container.HasBind(GetType())) {
        Destroy(gameObject);

        return;
      }

      ProjectContext.Container.Bind(this, BindType.Singleton);
      DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
      _audioSource.clip = _clip;
      SetVolume(ProjectContext.Container.Resolve<Storage<SettingsSaveData>>().Data.MusicVolume);
      _audioSource.Play();
    }

    public void ChangeVolume (float volume)
    {
      _audioSource.volume = volume;
    }

    private void SetVolume (float volume)
    {
      _audioSource.volume = 0;
      StartCoroutine(IncreaseVolumeCoroutine(volume));
    }

    private IEnumerator IncreaseVolumeCoroutine (float volume)
    {
      WaitForSeconds wait = new WaitForSeconds(0.1f);

      while (_audioSource.volume < volume) {
        yield return wait;
        _audioSource.volume += volume / 10f;
      }
    }
  }
}
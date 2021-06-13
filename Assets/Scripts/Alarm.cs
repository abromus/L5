using System.Collections;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
	[SerializeField] private float _fadeTime = 4f;

	private AudioSource _audio;
	private readonly float _maxVolume = 1f;
	private readonly float _minVolume = 0f;
	private Coroutine _startAlarm;

	private void Start()
	{
		_audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Thief>(out _))
		{
			Debug.Log(0);
			_startAlarm = StartCoroutine(StartAlarm());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Thief>(out _))
		{
			StartCoroutine(EndAlarm());
		}
	}

	private IEnumerator StartAlarm()
	{
		float reduce = -1;
		float increase = 1;
		float direction = increase;

		Debug.LogWarning($"Enter");

		_audio.volume = _maxVolume;
		_audio.Play();

		while(true)
		{
			if(_audio.volume >= _maxVolume || _audio.volume <= _minVolume)
			{
				direction = direction == increase ? reduce : increase;
			}

			_audio.volume += direction*Time.deltaTime / _fadeTime;

			Debug.Log($"Volume: {_audio.volume}");
			yield return null;
		}
	}

	private IEnumerator EndAlarm()
	{
		Debug.LogWarning($"Exit");

		if(_startAlarm != null)
		{
			StopCoroutine(_startAlarm);
			_startAlarm = null;
		}

		while(true)
		{
			if(_audio.volume >= _maxVolume || _audio.volume <= _minVolume)
			{
				_audio.Stop();
				break;
			}

			_audio.volume -= Time.deltaTime / _fadeTime;

			Debug.Log($"Volume: {_audio.volume}");
			yield return null;
		}
	}
}

using System.Collections;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
	[SerializeField] private float _fadeTime = 4f;

	private AudioSource _audio;
	private readonly float _maxVolume = 1f;
	private readonly float _minVolume = 0f;
	private bool _isAlarm;

	private void Start()
	{
		_audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Thief>(out _))
		{
			_isAlarm = true;
			StartCoroutine(RunAlarm());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Thief>(out _))
		{
			_isAlarm = false;
		}
	}

	private IEnumerator RunAlarm()
	{
		float reduce = -1;
		float increase = 1;
		float direction = increase;

		_audio.volume = _maxVolume;
		_audio.Play();

		while(true)
		{
			if(_isAlarm == false)
			{
				if(direction != reduce)
				{
					direction = reduce;
				}

				if(_audio.volume <= 0)
				{
					_audio.Stop();
					break;
				}
			}

			if(_audio.volume >= _maxVolume || _audio.volume <= _minVolume)
			{
				direction = direction == increase ? reduce : increase;
			}

			_audio.volume += direction * Time.deltaTime / _fadeTime;

			yield return null;
		}
	}
}

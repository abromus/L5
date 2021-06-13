using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private float _speed;

	private Vector3 _direction;
	private Vector3 _newPosition;

	private void Start()
	{
		_newPosition = transform.position;
	}

	private void Update()
	{
		_newPosition = transform.position + GetDirection();
		transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.deltaTime);
	}

	private Vector3 GetDirection()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
			_direction = Vector3.left;
		else if(Input.GetKey(KeyCode.RightArrow))
			_direction = Vector3.right;
		else
			_direction = Vector3.zero;

		return _direction;
	}
}

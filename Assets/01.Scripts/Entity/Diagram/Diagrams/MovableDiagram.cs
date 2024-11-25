using UnityEngine;

public class MovableDiagram : Entity
{
	[Header("Movement")]
	[SerializeField] protected float _speed = 1f;
	[SerializeField] protected float _rotateSpeed = 5f;
	protected float _moveAngle;
	protected GameObject _targetObj;
	protected Vector3 _targetPos;
	protected Bounds _moveBounds;

	[Header("Target")]
	[SerializeField] protected LayerMask _cellLayer;
	[SerializeField] protected float _targetFindRadius = 1f;

	private void Start()
	{
		_moveBounds = Camera.main.GetBounds();
	}

	private void Update()
	{
		if (_targetObj == null)
		{
			bool foundTarget = FindTarget(); // 타깃이 없으면 타깃을 찾는다.
			if (!foundTarget && Vector3.Distance(transform.position, _targetPos) < _speed) // 타겟을 못찾음
			{
				SetTarget(null); // 무작위 목표지점에 도달했다면 다른 무작위 지점으로 이동
			}
		}

		Move();
	}

    public void MoveTo(Vector3 pos)
	{
		_targetPos = pos;
	}

	private void Move()
	{
		Vector3 targetDir = _targetPos - transform.position;
		float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x);// * Mathf.Rad2Deg;

		float distanceFactor = Vector2.Distance(transform.position, _targetPos);
		distanceFactor = Mathf.Clamp(distanceFactor, 0.01f, 1f);

		_moveAngle = Mathf.LerpAngle(_moveAngle * Mathf.Rad2Deg, targetAngle * Mathf.Rad2Deg, _rotateSpeed * Time.deltaTime / distanceFactor);
		transform.rotation = Quaternion.Euler(0, 0, _moveAngle - 90f);
		_moveAngle *= Mathf.Deg2Rad;

		Vector3 dir = new Vector3(Mathf.Cos(_moveAngle), Mathf.Sin(_moveAngle));
		transform.position += dir * _speed * Time.deltaTime;
	}

	private bool FindTarget()
	{
		Collider2D col = Physics2D.OverlapCircle(transform.position, _targetFindRadius, _cellLayer);
		if (col == null)
			return false;

		SetTarget(col.gameObject);
		return true;
	}

	private void SetTarget(GameObject target)
	{
		_targetObj = target;

		if (_targetObj == null)
		{
			_targetObj = null;
			_targetPos = _moveBounds.GetRandomPoint();
			return;
		}

		_targetPos = _targetObj.transform.position;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Cell"))
		{
			Destroy(collision.gameObject);
			CellSpawner.Instance.ModifyCellCount(-1);

			if (_targetObj != null && collision.gameObject == _targetObj)
			{
				_targetObj = null;
				if (!FindTarget())
				{
					SetTarget(null);
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_targetPos, 0.3f);
		Vector3 dir = new Vector3(Mathf.Cos(_moveAngle), Mathf.Sin(_moveAngle));
		Gizmos.DrawLine(transform.position, transform.position + dir);
		Gizmos.color = Color.white;
	}
}

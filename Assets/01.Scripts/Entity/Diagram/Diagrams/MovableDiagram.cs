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

	private HitTrigger _hitTrigger;

	private void Start()
	{
        _hitTrigger = GetComponent<HitTrigger>();
        _hitTrigger.OnHit += OnHitAction;

        _moveBounds = Camera.main.GetBounds();
    }

    private void OnHitAction(Collider2D obj)
    {
		if (_targetObj != null && obj.gameObject == _targetObj)
		{
			_targetObj = null;
			if (!FindTarget())
			{
				SetTarget(null);
			}
		}
	}

    private void Update()
	{
		if (_targetObj == null)
		{
			bool foundTarget = FindTarget(); // Ÿ���� ������ Ÿ���� ã�´�.
			if (!foundTarget && Vector3.Distance(transform.position, _targetPos) < _speed) // Ÿ���� ��ã��
			{
				SetTarget(null); // ������ ��ǥ������ �����ߴٸ� �ٸ� ������ �������� �̵�
			}
		}

		Move();
	}

    public void SetTargetPos(Vector3 pos)
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_targetPos, 0.3f);
		Vector3 dir = new Vector3(Mathf.Cos(_moveAngle), Mathf.Sin(_moveAngle));
		Gizmos.DrawLine(transform.position, transform.position + dir);
		Gizmos.color = Color.white;
	}
}

using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Rigidbody2D rb;

    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float _weaponrange = 10f;
    [SerializeField] private Animator _mussleFlashAnimator;

    [SerializeField] public float _timeBetweenShots = 0.5f;
    public bool LookAtMouseEnabled = true;
    private float _timeSinceLastShot = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LookAtMouse();
        Move();
        Shoot();
    }

    private void LookAtMouse()
    {
        if (PauseMenu.gamePaused || !LookAtMouseEnabled)
        {
            return;
        }

        Vector2 mopusePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mopusePos - new Vector2(transform.position.x, transform.position.y);
    }

    private void Move()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = input.normalized * moveSpeed;
    }

    private void Shoot()
    {
        if (PauseMenu.gamePaused || !LookAtMouseEnabled)
        {
            return;
        }

        if (Input.GetMouseButton(0) && _timeSinceLastShot >= _timeBetweenShots)
        {
            _mussleFlashAnimator.SetTrigger("Shoot");
            _timeSinceLastShot = 0f;
            if (_timeBetweenShots >= 0)
            {
                AudioManager.Instance.PlayRandomShoot();
            }

            var hit = Physics2D.Raycast(_gunPoint.position, transform.up, _weaponrange);
            var trail = Instantiate(_bulletTrail, _gunPoint.position, transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
                var hittable = hit.collider.GetComponent<IHitable>();

                if (hittable != null)
                {
                    hittable.ReceiveHit(hit);
                }
            }
            else
            {
                var endposition = _gunPoint.position + transform.up * _weaponrange;
                trailScript.SetTargetPosition(endposition);
            }
        }
        _timeSinceLastShot += Time.deltaTime;
    }
}

using System.Collections;
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
    private bool _firstShot = true;
    [SerializeField] public GameManager gameManager;


    public float dashDistance = 5f; // Dystans, na który gracz siê przesunie podczas dashu
    public float dashDuration = 0.5f; // Czas trwania dashu
    private bool isDashing = false;
    public bool isDashUpgradeIsActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LookAtMouse();
        Move();
        Shoot();
        DashEffect();
    }
    private void DashEffect()
    {
        if (isDashUpgradeIsActive && Input.GetKeyDown(KeyCode.Tab) && !isDashing)
        {
            StartCoroutine(Dash());
        }
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, input.normalized, input.magnitude * moveSpeed * Time.deltaTime);

        if (hit.collider == null || !hit.collider.CompareTag("Obstacle"))
        {
            rb.velocity = input.normalized * moveSpeed;
        }
    }
    private void Shoot()
    {
        if (PauseMenu.gamePaused || !LookAtMouseEnabled)
        {
            return;
        }

        if (Input.GetMouseButton(0) && (_firstShot || _timeSinceLastShot >= _timeBetweenShots))
        {
            _mussleFlashAnimator.SetTrigger("Shoot");
            if (_firstShot)
            {
                _firstShot = false;
            }
            else
            {
                _timeSinceLastShot = 0f;
            }

            AudioManager.Instance.PlayRandomShoot();


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
                    gameManager.numKills++; // zwiêksza wartoœæ licznika zabójstw o 1, jeœli przeciwnik zosta³ zabity

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

    private IEnumerator Dash()
    {
        isDashing = true;
        Vector2 dashDirection = transform.up; // Kierunek, w którym gracz siê przesunie podczas dashu
        float dashTime = 0f;
        Vector3 startingPos = transform.position;

        while (dashTime < dashDuration)
        {
            // Oblicz pozycjê docelow¹ na podstawie kierunku dashDirection i dystansu dashDistance
            Vector3 targetPos = startingPos + (Vector3)(dashDirection * dashDistance);

            // U¿yj Lerp do p³ynnego przesuwania gracza z pozycji pocz¹tkowej do pozycji docelowej w czasie dashDuration
            transform.position = Vector3.Lerp(startingPos, targetPos, dashTime / dashDuration);

            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}

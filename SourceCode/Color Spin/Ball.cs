using UnityEngine;
using UnityEngine.Tilemaps; // Tilemaps ���ӽ����̽� �߰�

public class Ball : MonoBehaviour
{
    public float bounceForce = 500f;
    public float gravityTime = 3f; // �߷��� �����ϴ� �ð� ����
    public float gravityAmount = 1f; // �߷��� �����ϴ� ��
    public float minHeight = 5f; // �ּ� ƨ�� ����
    private Vector2 lastPosition;

    private Rigidbody2D rb;
    private Vector2 initialPosition = new Vector2(960, 540); // �ʱ� ��ġ ����
    private float initialGravityScale;
    private float upAmount;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private bool hasCollided; // �浹 ���� ����
    private bool isMoving; // ���� �����̰� �ִ��� Ȯ��
    public AudioClip collisionSound; // �浹 ȿ����

    private Color[] colors = new Color[]
    {
        new Color(160/255f, 0/255f, 255/255f), // (160,0,255)
        new Color(255/255f, 0/255f, 0/255f),   // (255,0,0)
        new Color(0/255f, 255/255f, 0/255f),   // (0,255,0)
        new Color(0/255f, 0/255f, 255/255f),   // (0,0,255)
        new Color(90/255f, 90/255f, 90/255f),  // (90,90,90)
        new Color(255/255f, 255/255f, 255/255f) // (255,255,255)
    };

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>(); // GameManager ��ũ��Ʈ ����
        initialGravityScale = rb.gravityScale; // �ʱ� �߷� ������ ����
    }

    void Start()
    {
        lastPosition = transform.position = initialPosition; // �ʱ� ��ġ ����
        upAmount = 0f;
        hasCollided = false;
        isMoving = false; // �ʱ� ���¿��� ���� �������� ����

        // �ʱ� ���� ����
        spriteRenderer.color = GetRandomColor();
    }

    void Update()
    {
        if (gameManager.isGameStarted && isMoving)
        {
            float deltaTime = Time.deltaTime; // Time.deltaTime�� �ѹ��� ȣ��
            // �ð��� ������ �߷� ����
            upAmount += deltaTime;
            if (upAmount >= gravityTime)
            {
                rb.gravityScale += gravityAmount;
                upAmount = 0f;
            }

            // �� ������ ���� ���� ��ġ�� ����
            lastPosition = transform.position;
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.gravityScale = initialGravityScale; // �߷� ������ �ʱ�ȭ
        spriteRenderer.color = GetRandomColor();
        upAmount = 0f;
        hasCollided = false;
        isMoving = false; // ���� �ʱ� ���¿��� �������� ����
    }

    public void StartMoving()
    {
        isMoving = true; // ���� �����̱� ������
    }

    private void ChangeColor()
    {
        spriteRenderer.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Tilemap"))
        {
            // ȿ���� ���
            if (collisionSound != null && AudioManager.instance != null)
            {
                AudioManager.instance.PlayEffect(collisionSound);
            }

            // ���� ������ ��ġ�� �ǵ���
            transform.position = lastPosition;

            // �������� ƨ��� �������� �����ϰ�, �ּ� ���� ����
            Vector2 bounceDirection = Vector2.up;
            rb.velocity = Vector2.zero; // ���� �ӵ��� ����
            rb.AddForce(bounceDirection * bounceForce);

            // �ּ� �ӵ��� �����Ͽ� �ּ� ���̱��� ƨ�⵵�� ����
            float minVelocity = Mathf.Sqrt(2 * rb.gravityScale * minHeight);
            if (rb.velocity.y < minVelocity)
            {
                rb.velocity = new Vector2(rb.velocity.x, minVelocity);
            }

            // ���� ����� Ÿ�ϸ��� ������ ���Ͽ� ���� ���� �Ǵ� ���� ����
            Tilemap tilemap = collision.collider.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    Vector3 hitPosition = hit.point - (Vector2)(0.01f * hit.normal);
                    Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
                    TileBase tile = tilemap.GetTile(tilePosition);
                    if (tile != null)
                    {
                        Color tileColor = tilemap.GetColor(tilePosition);
                        if (spriteRenderer.color == tileColor)
                        {
                            gameManager.AddScore(1); // ���� 1 ����
                        }
                        else
                        {
                            gameManager.GameOver(); // ���� ����
                            return; // ���� �ڵ带 �������� �ʵ��� ����
                        }
                    }
                }
            }

            // �浹 ���� ����
            hasCollided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Tilemap") && hasCollided)
        {
            // �浹�� ������ �� ���� ����
            ChangeColor();
            hasCollided = false;
        }
    }
}

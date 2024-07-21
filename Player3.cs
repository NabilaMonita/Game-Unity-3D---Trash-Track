using UnityEngine;
using UnityEngine.UI;

public class Player3 : MonoBehaviour
{
    public float runSpeed = 10f;
    public float rotationSpeed = 720f;
    private int currentLives = 3;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode forwardKey = KeyCode.UpArrow;
    public KeyCode backwardKey = KeyCode.DownArrow;

    private Animator animator;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private bool isMoving = false;

    private int botolCount = 0; // Inisialisasi variabel botol
    private int donatCount = 0; // Inisialisasi variabel donat
    public Text botolText; // Asumsikan botolText adalah UI Text element untuk menampilkan jumlah botol
    public Text donatText; // Asumsikan donatText adalah UI Text element untuk menampilkan jumlah donat
    public UIManager1 uiManager; // Referensi ke UIManager
    private BoxCollider boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the Rigidbody from rotating
        animator = GetComponent<Animator>();
        animator.Play("Start"); // Memulai dengan animasi start
        uiManager = FindObjectOfType<UIManager1>();
        boxCollider = GetComponent<BoxCollider>();
        UpdateBotolText(); // Update UI botol pada awal permainan
        UpdateDonatText(); // Update UI donat pada awal permainan
    }

    void Update()
    {
        HandleMovementInput();
        AnimateCharacter();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void HandleMovementInput()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(leftKey))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(rightKey))
        {
            moveDirection += Vector3.right;
        }
        if (Input.GetKey(forwardKey))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(backwardKey))
        {
            moveDirection += Vector3.back;
        }

        moveDirection = moveDirection.normalized * runSpeed;
        isMoving = moveDirection != Vector3.zero;
    }

    void MoveCharacter()
    {
        if (isMoving)
        {
            Vector3 newVelocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            rb.velocity = newVelocity;

            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void AnimateCharacter()
    {
        if (isMoving)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
            {
                animator.Play("Start");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Botol"))
        {
            GetBotol(other);
        }
        else if (other.CompareTag("Donat"))
        {
            GetDonat(other);
        }
        else if (other.CompareTag("Obstacle")) // Periksa apakah objek bersentuhan adalah obstacle
        {
            HitObstacle(); // Panggil metode HitObstacle jika bertabrakan dengan obstacle
        }
    }

    void GetBotol(Collider other)
    {
        botolCount++;
        UpdateBotolText(); // Update UI setiap kali mendapatkan botol
        other.gameObject.SetActive(false);
    }

    void GetDonat(Collider other)
    {
        donatCount++;
        UpdateDonatText(); // Update UI setiap kali mendapatkan donat
        other.gameObject.SetActive(false);
    }

    void UpdateBotolText()
    {
        if (botolText != null)
        {
            botolText.text = botolCount.ToString();
        }
    }

    void UpdateDonatText()
    {
        if (donatText != null)
        {
            donatText.text = donatCount.ToString();
        }
    }
    public void ResetPlayer()
    {
        botolCount = 0;
        donatCount = 0;
        UpdateBotolText();
        UpdateDonatText();
        // Tambahkan kode untuk mereset posisi player jika diperlukan
    }
    void HitObstacle()
    {

        currentLives--; // Mengurangi nyawa saat bertabrakan dengan obstacle
        uiManager.UpdateLives(currentLives); // Memperbarui UI nyawa
    }

}

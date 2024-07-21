using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject model;
    public float runSpeed = 10f;
    public float minSpeed = 10f;
    public float maxSpeed = 30f;
    public float laneChangeSpeed = 20f;
    public float jumpSpeed = 5f;
    public float jumpLength = 7.5f;
    public float slideLength = 10f;
    public float jumpHeight = 1f;
    public float invisibleTime = 5f;

    public float countdownTimer; // Timer hitung mundur

    private Animator animator;
    private Rigidbody rb;
    private UIManager uiManager;
    private BoxCollider boxCollider;
    private Vector3 targetPosition;
    private Vector3 boxColliderSize;
    private Vector2 startingTouch;
    private bool isSwiping = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private bool isInvisible = false;
    private float jumpStart;
    private float slideStart;
    private int currentLives = 5;
    private int coins;
    private bool isGameOver = false;


      public void AddCoin() {
        coins++; // Tambahkan satu koin ke jumlah koin yang dikumpulkan
        // Di sini kamu bisa menambahkan pembaruan UI untuk menampilkan jumlah koin yang dikumpulkan
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        animator.Play("runStart");

        countdownTimer = invisibleTime; // Atur timer awal
    }

    void Update() {
        if (!isGameOver) {
            MoveCharacter();
            HandleTimer(); // Panggil metode untuk mengatur timer
        }
    }

    void FixedUpdate() {
        rb.velocity = Vector3.forward * runSpeed;
    }

    void MoveCharacter() {
        HandleKeyboard();
        HandleTouch();
        HandleJump();
        HandleSlide();

        Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        transform.localPosition = Vector3.MoveTowards(transform.position, newPosition, laneChangeSpeed * Time.deltaTime);
    }

    void HandleKeyboard() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLane(-1);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeLane(1);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Slide(); 
    }

    void HandleTouch() {
        if (Input.touchCount == 1) {
            if (isSwiping) {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                
                if (diff.magnitude > 0.01f) {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x)) {
                        HandleVerticalSwiping(diff.y);
                    } else {
                        HandleHorizontalSwipping(diff.x);
                    }

                    isSwiping = false;
                }
            }
      
            validateSwiping();
        }
    }

    void HandleVerticalSwiping(float diffY) {
        if (diffY < 0) Slide();
        else Jump();
    }

    void HandleHorizontalSwipping(float diffX) {
        if (diffX < 0) ChangeLane(-1);
        else ChangeLane(1);
    }

    void validateSwiping() {
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            startingTouch = Input.GetTouch(0).position;
            isSwiping = true;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            isSwiping = false;
        }
    }

    void ChangeLane(int direction) {
        float targetLane = targetPosition.x + direction;

        if (targetLane >= -3f && targetLane <= 3f) {
            targetPosition.x = targetLane;
        }
    }

    void Jump() {
        if (!isJumping) {
            isJumping = true;
            jumpStart = transform.position.z;
            animator.SetBool("Jumping", true);
            animator.SetFloat("JumpSpeed", runSpeed / jumpLength);
        }
    }

    void HandleJump() {
        if (isJumping && !isSliding) {
            float ratio = (transform.position.z - jumpStart) / jumpLength;

            if (ratio >= 1) {
                isJumping = false;
                animator.SetBool("Jumping", false);
            } else {
                targetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
        } else {
            targetPosition.y = Mathf.MoveTowards(targetPosition.y, 0, jumpSpeed * Time.deltaTime);
        }
    }

    void Slide() {
        if (!isJumping && !isSliding) {
            isSliding = true;
            slideStart = transform.position.z;
            boxCollider.size /= 2;
            animator.SetBool("Sliding", true);
            animator.SetFloat("JumpSpeed", runSpeed / jumpLength);
        }
    }

    void HandleSlide() {
        if (isSliding) {
            float ratio = (transform.position.z - slideStart) / slideLength;

            if (ratio >= 1) {
                isSliding = false;
                boxCollider.size = boxColliderSize;
                animator.SetBool("Sliding", false);
            }
        } 
    }

    void OnTriggerEnter(Collider other) {
       if (other.CompareTag("Coin")) GetCoins(other);
       if (!isInvisible && other.CompareTag("Obstacle")) HitObstacles();
    }

    void GetCoins(Collider other) {
        coins++;
        uiManager.UpdateCoins(coins);
        other.gameObject.SetActive(false);
    }

    void HitObstacles() {
        currentLives--;
        uiManager.UpdateLives(currentLives);
        animator.SetTrigger("Hit");

        if (currentLives <= 0) {
            runSpeed = 0;
            animator.SetBool("Dead", true);
            // Call gameover
        } 
    }

    void HandleTimer() {
        if (countdownTimer > 0f) {
            countdownTimer -= Time.deltaTime;
            uiManager.UpdateTimer((int)countdownTimer); // Update UI dengan timer hitung mundur

            if (countdownTimer <= 0f) {
                EndGame(); // Panggil metode untuk mengakhiri permainan jika waktu habis
            }
        }
    }

  void EndGame() {
    isGameOver = true;
    runSpeed = 0f; // Menghentikan karakter agar tidak bergerak lagi
    animator.SetBool("Dead", true); // Set animasi Dead menjadi true saat permainan berakhir
    // Panggil fungsi untuk menampilkan layar game over atau lakukan tindakan sesuai kebutuhan

    // Menonaktifkan semua skrip yang terkait dengan permainan
    // Misalnya, jika ada skrip lain yang mengatur pergerakan karakter, UI, dll.
    // Anda bisa menonaktifkan skrip-skrip tersebut di sini.
    // Contoh: 
    // GetComponent<OtherScript>().enabled = false;
}



    public void IncreaseSpeed() {
        runSpeed *= 1.15f;
        runSpeed = (runSpeed >= maxSpeed) ? maxSpeed : runSpeed;
    }
}

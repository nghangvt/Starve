using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    private Rigidbody2D rb;
    public Vector3 moveInput;
    public Animator animator;
    public PlayerHealth playerHealth;
    
    public InventoryManager inventoryManager;
    private TileManager tileManager;

    public AudioClip backgroundAudioClip;
    public AudioClip runClip;
    public AudioClip takeItemClip;
    private AudioSource audioSource;

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset,
            Quaternion.identity);
        droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        tileManager = GameManager.instance.tileManager;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundAudioClip;
        audioSource.Play();
    }
    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        //xd huong di chuyen va dat trigger cho Animation
        if (moveInput.x > 0)
        {
            animator.SetTrigger("MoveRight");
            audioSource.PlayOneShot(runClip);
        }
        else if (moveInput.x < 0)
        {
            animator.SetTrigger("MoveLeft");
            audioSource.PlayOneShot(runClip);
        }
        else if (moveInput.y > 0)
        {
            animator.SetTrigger("MoveUp");
            audioSource.PlayOneShot(runClip);
        }
        else if (moveInput.y < 0)
        {
            animator.SetTrigger("MoveDown");
            audioSource.PlayOneShot(runClip);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (tileManager != null)
            {
                Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);

                string tileName = tileManager.GetTileName(position);

                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (tileName == "Interactable0" && inventoryManager.toolbar.selectedSlot.itemName == "CraftTable")
                    {
                        Inventory.Slot selectedSlot = inventoryManager.toolbar.selectedSlot;
                        if (selectedSlot.count > 0)
                        {
                            selectedSlot.RemoveItem();
                            if (selectedSlot.count < 0)
                            {
                                inventoryManager.toolbar.Remove(inventoryManager.toolbar.slots.IndexOf(selectedSlot));
                                
                            }
                        }
                        tileManager.SetInteracted(position);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleFoodConsumption();
        }
    }

    public void HandleFoodConsumption()
    {
        if (inventoryManager.toolbar.selectedSlot.itemName == "Apple" &&
        inventoryManager.toolbar.selectedSlot.count > 0)
        {
            playerHealth.currentHunger += 20;
            if (playerHealth.currentHunger > playerHealth.maxHunger)
            {
                playerHealth.currentHunger = playerHealth.maxHunger;
            }
            inventoryManager.toolbar.selectedSlot.RemoveItem();
        }
        playerHealth.Update();
    }

    public void TakeDame(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
}

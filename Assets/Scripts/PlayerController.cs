using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class PlayerController : MonoBehaviour
{
    
    public CharacterController mController;
    public Animator mAnimator;
    float Speed = 6f;
    float gravity = -9.10f;
    float jumpHeight = 5f;

    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;

    Vector3 velocity;
    bool isGrounded;

    private SliderUse healthBar;
    private SliderUse staminaBar;
    private SliderUse foodBar;
    private Canvas InstanceCanvasInGame;

    [SerializeField]int Health;
    [SerializeField]int Stamina;
    [SerializeField]int Food;

    float TimeToWait;
    float TimeToUseEndurance;
    float TimeToAddEndurance;
    float TimeToUseFood;
    public float EatingDistance = 1.0f;
    GameObject[] FoodsArray;
    SliderUse[] sliders;

    



    // Start is called before the first frame update
    void Start()
    {
        AiEnnemie.OnHit += Hit;
        Health = 100;
        Stamina = 100;
        Food = 100;
        TimeToWait = 0;
        TimeToUseEndurance = 0;
        TimeToAddEndurance = 0;
        TimeToUseFood = 0;

        InstanceCanvasInGame = InstanceGame.Instance.CanvasInstance;
        sliders = InstanceCanvasInGame.GetComponentsInChildren<SliderUse>();
        healthBar = sliders[0];
        staminaBar = sliders[1];
        foodBar = sliders[2];


        healthBar.SetMaxSlider(Health);
        foodBar.SetMaxSlider(Food);
        staminaBar.SetMaxSlider(Stamina);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetSlider(Health);
        staminaBar.SetSlider(Stamina);
        foodBar.SetSlider(Food);
        isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0)
        {
            Speed = 10f;
            TimeToUseEndurance += Time.deltaTime;
            if (TimeToUseEndurance > 0.1)
            {
                UseEndurance();
            }
            mAnimator.SetBool("running", true);
            Jump();
        }
        else
        {
            Speed = 5f;
            mAnimator.SetBool("running", false);
        }

        mAnimator.SetFloat("forward", verticalAxis);
        mAnimator.SetFloat("strafe", horizontalAxis);


        Vector3 move = transform.right * horizontalAxis + transform.forward * verticalAxis;

        mController.Move(move * Speed * Time.deltaTime);

        

        velocity.y += gravity * Time.deltaTime;

        mController.Move(velocity * Time.deltaTime);

        Jump();
        if(Health <= 0)
        {
            
            SceneManager.LoadScene("LosePanel");
            
        }
    }
    private void FixedUpdate()
    {

        FoodsArray = GameObject.FindGameObjectsWithTag("Food");
        TimeToUseFood += Time.deltaTime;
        if (TimeToUseFood > 0.8)
        {
            UseFood();
        }
        TimeToAddEndurance += Time.deltaTime;
        if (Stamina < 100 && Stamina > 5 && TimeToAddEndurance > 0.1 && !Input.GetKey(KeyCode.LeftShift))
        {
            AddEndurance();
            TimeToAddEndurance = 0;
        }
        else if (Stamina < 6)
        {
            
            CheckTime();
        }
        foreach (GameObject element in FoodsArray)
        {
            float Distance = Vector3.Distance(mController.transform.position, element.transform.position);
            if (Input.GetKey(KeyCode.E) && Distance <= EatingDistance)
            {
                AddFood(element);
            }
        }
    }
    void CheckTime()
    {
        TimeToWait += Time.deltaTime;
        if (TimeToWait >= 2)
        {
            Stamina += 10;
          
            TimeToWait = 0;
        }
    }

    void AddFood(GameObject food)
    {
        
        Food += 20;
      
        Destroy(food);
    }
    void UseFood()
    {
        
        Food -= 1;
      
        TimeToUseFood = 0;
        if (Food <= 0)
            SceneManager.LoadScene("LosePanel");
    }
    void AddEndurance()
    {
       
        if (Stamina <= 100)
            Stamina += 1;
        else
            Stamina = 100;
       
    }
    void UseEndurance()
    {
      
        if (Stamina > 0)
            Stamina -= 1;
        else
            Stamina = 0;
        
        TimeToUseEndurance = 0;
    }
   public void Hit()
    {
        Health -= 10;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }
        if (isGrounded)
        {
            mAnimator.SetBool("jump", false);
        }
        else
        {
            mAnimator.SetBool("jump", true);
        }
    }
}

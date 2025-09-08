using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    [Tooltip("Set 1 for right/forward, -1 for left/backward movement.")]
    public int direction = 1;

    [Header("Sliders")]
    public Slider dairySlider;
    public float dairyDecreaseRate = 2f;
    public Slider sugarSlider;
    public float sugarDecreaseRate = 1f;
    public Slider starchSlider;
    public float starchDecreaseRate = 0.5f;
    public Slider fruitAndVeggieSlider;
    public float fruitAndVeggieDecreaseRate = 3f;
    public Slider proteinSlider;
    public float proteinDecreaseRate = 1.5f;

    [Header("Visual Warning")]
    [Tooltip("Fraction of maxValue below which the slider will flash red.")]
    [Range(0f, 1f)]
    public float lowThreshold = 0.3f;
    public Color normalColor = Color.green;
    public Color warningColor = Color.red;
    public float flashSpeed = 5f; // how fast it flashes


    private bool isDead = false;

    void Start()
    {
        SetSliderMax(dairySlider);
        SetSliderMax(sugarSlider);
        SetSliderMax(starchSlider);
        SetSliderMax(fruitAndVeggieSlider);
        SetSliderMax(proteinSlider);

        SetSliderInteractable(dairySlider);
        SetSliderInteractable(sugarSlider);
        SetSliderInteractable(starchSlider);
        SetSliderInteractable(fruitAndVeggieSlider);
        SetSliderInteractable(proteinSlider);
    }

    void Update()
    {
        if (isDead) return;

        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        HandleSlider(dairySlider, dairyDecreaseRate, true);
        HandleSlider(sugarSlider, sugarDecreaseRate, true);
        HandleSlider(starchSlider, starchDecreaseRate, true);
        HandleSlider(fruitAndVeggieSlider, fruitAndVeggieDecreaseRate, true);
        HandleSlider(proteinSlider, proteinDecreaseRate, true);
    }

    void HandleSlider(Slider slider, float rate, bool critical)
    {
        if (slider == null || isDead) return;

        // Decrease slider
        slider.value -= rate * Time.deltaTime;
        slider.value = Mathf.Clamp(slider.value, 0f, slider.maxValue);

        // Kill player if critical and zero
        if (critical && Mathf.Approximately(slider.value, 0f))
            KillPlayer();

        // Flash red if near zero
        Image fill = slider.fillRect.GetComponent<Image>();
        if (slider.value <= slider.maxValue * 0.3f) // critical threshold = 20%
        {
            float lerp = Mathf.PingPong(Time.time * flashSpeed, 1f);
            fill.color = Color.Lerp(normalColor, warningColor, lerp);
        }
        else
        {
            fill.color = normalColor;
        }
    }

    void KillPlayer()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("💀 Player is dead!");
        moveSpeed = 0f;
        Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetSliderMax(Slider slider)
    {
        if (slider != null) slider.value = slider.maxValue;
    }

    void SetSliderInteractable(Slider slider)
    {
        if (slider != null) slider.interactable = true;
    }
}


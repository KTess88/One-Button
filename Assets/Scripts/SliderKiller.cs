using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiveSliderManager : MonoBehaviour
{
    [Header("Health Settings")]
    public Slider healthSlider;
    public float healthDecreaseRate = 2f;

    [Header("Hunger Settings")]
    public Slider hungerSlider;
    public float hungerDecreaseRate = 1f;

    [Header("Stamina Settings")]
    public Slider staminaSlider;
    public float staminaDecreaseRate = 0.5f;

    [Header("Oxygen Settings")]
    public Slider oxygenSlider;
    public float oxygenDecreaseRate = 3f;

    [Header("Energy Settings")]
    public Slider energySlider;
    public float energyDecreaseRate = 1.5f;

    private bool isDead = false;

    void Start()
    {
        // Start all sliders at maximum
        if (healthSlider != null) healthSlider.value = healthSlider.maxValue;
        if (hungerSlider != null) hungerSlider.value = hungerSlider.maxValue;
        if (staminaSlider != null) staminaSlider.value = staminaSlider.maxValue;
        if (oxygenSlider != null) oxygenSlider.value = oxygenSlider.maxValue;
        if (energySlider != null) energySlider.value = energySlider.maxValue;

        // Make sure sliders are draggable
        if (healthSlider != null) healthSlider.interactable = true;
        if (hungerSlider != null) hungerSlider.interactable = true;
        if (staminaSlider != null) staminaSlider.interactable = true;
        if (oxygenSlider != null) oxygenSlider.interactable = true;
        if (energySlider != null) energySlider.interactable = true;
    }

    void Update()
    {
        if (isDead) return;

        HandleSlider(healthSlider, healthDecreaseRate, true);
        HandleSlider(hungerSlider, hungerDecreaseRate, true);
        HandleSlider(staminaSlider, staminaDecreaseRate, false);
        HandleSlider(oxygenSlider, oxygenDecreaseRate, true);
        HandleSlider(energySlider, energyDecreaseRate, false);
    }

    void HandleSlider(Slider slider, float rate, bool critical)
    {
        if (slider == null) return;

        // If the player drags the slider to max, "refill" it
        if (Mathf.Approximately(slider.value, slider.maxValue))
        {
            return; // stay full until draining resumes
        }

        // Auto-decrease otherwise
        slider.value -= rate * Time.deltaTime;

        // Clamp at zero
        if (slider.value <= 0f)
        {
            slider.value = 0f;

            if (critical)
            {
                KillPlayer();
            }
        }
    }

    void KillPlayer()
    {
        isDead = true;
        Debug.Log("💀 Player is dead!");
    }
}

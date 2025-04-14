using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarManager : MonoBehaviour
{
    // ============================================================== Variables =====================================================
    // Object References
    public Slider energySlider;
    public RawImage xrayIcon;
    public RawImage recallIcon;
    public RawImage smokeIcon;

    // Color References
    public Color regularColor;
    public Color blockedColor;

    // ============================================================== Enery Functions =====================================================
    // Sets max energy and current energy
    public void SetMaxEnergy(int energy)
    {
        energySlider.maxValue = energy;
        energySlider.value = energy;
    }

    // Set current energy and update icons
    public void SetEnergy(int energy)
    {
        energySlider.value = energy;
        updateIcons(energy);
    }

    // ============================================================== Icon Functions =====================================================
    // Update Icons Based on Energy
    private void updateIcons(int energy)
    {
        xrayIcon.color = (energy < 2) ? blockedColor : regularColor;
        recallIcon.color = (energy < 5) ? blockedColor : regularColor;
        smokeIcon.color = (energy < 3) ? blockedColor : regularColor;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarManager : MonoBehaviour
{
    public Slider energySlider;
    public RawImage xrayIcon;
    public RawImage recallIcon;
    public RawImage smokeIcon;
    public Color regularColor;
    public Color blockedColor;

    public void SetMaxEnergy(int energy)
    {
        energySlider.maxValue = energy;
        energySlider.value = energy;
    }

    private void updateIcons(int energy)
    {
        xrayIcon.color = (energy < 2) ? blockedColor : regularColor;
        recallIcon.color = (energy < 5) ? blockedColor : regularColor;
        smokeIcon.color = (energy < 3) ? blockedColor : regularColor;
    }

    public void SetEnergy(int energy)
    {
        energySlider.value = energy;
        updateIcons(energy);
    }
}
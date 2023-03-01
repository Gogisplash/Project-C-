using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUse : MonoBehaviour
{
    public Slider mSlider;

    public Gradient mGradient;
    public Image mColor;

    private void Start()
    {
       
        mSlider = GetComponent<Slider>();
    }


    public void SetSlider(int current)
    {
        mSlider.value = current;
        mColor.color = mGradient.Evaluate(mSlider.normalizedValue);
    }

    public void SetMaxSlider(int max)
    {
        mSlider.maxValue = max;
        mSlider.value = max;
        mColor.color = mGradient.Evaluate(1f);
    }

   

}

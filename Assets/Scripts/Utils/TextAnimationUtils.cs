using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Utils
{
    // NOTE(vladimir): Supports integers only
    public static class TextAnimationUtils
    {
        public const float DURATION_CLAMP_TIME = 2.5f;

        public const string DefaultFormat = "{0}";
        
        public const float DEFAULT_TEXT_ANIMATION_SPEED = 0.0045f;
        public const float SLOWER_TEXT_ANIMATION_SPEED = 0.006f;
        
        // NOTE(vladimir): The lower the speed, the faster it animates (0.009f is rather slow)
        public static float AnimateTextAndGetDuration(TMP_Text text, int startValue, int endValue, 
            float speed, bool clampToDefault = true)
        {
            return AnimateTextFormatted(text, DefaultFormat, startValue, endValue, speed, clampToDefault).Duration;
        }
        
        public static Tween AnimateTextAndGetTween(TMP_Text text, int startValue, int endValue, float speed, 
            bool clampToDefault = true)
        {
            return AnimateTextFormatted(text, DefaultFormat, startValue, endValue, speed, clampToDefault).Tween;
        }
        
        public static float AnimateTextFormattedAndGetDuration(TMP_Text text, string format, int startValue, int endValue, 
            float speed, bool clampToDefault = true)
        {
            return AnimateTextFormatted(text, format, startValue, endValue, speed, clampToDefault).Duration;
        }
        
        public static Tween AnimateTextFormattedAndGetTween(TMP_Text text, string format, int startValue, int endValue, 
            float speed, bool clampToDefault = true)
        {
            return AnimateTextFormatted(text, format, startValue, endValue, speed, clampToDefault).Tween;
        }
        
        private static TweenAndDuration AnimateTextFormatted(TMP_Text text, string format, int startValue, int endValue, 
            float speed, bool clampToDefault = true)
        {
            text.text = startValue.ToString();
            int diff = Mathf.Abs(endValue - startValue);
            float duration = diff * speed;
            if (clampToDefault)
                duration = Mathf.Clamp(duration, 0f, DURATION_CLAMP_TIME);
            Tween animationTween = DOTween.To(v =>
            {
                text.text = string.Format(format, (int)v);
            }, startValue, endValue, duration);
            
            return new TweenAndDuration(duration, animationTween);

        }

        public static float AnimateTextFormattedWithDuration(TMP_Text text, string format, int startValue, int endValue, 
            float duration)
        {
            text.text = startValue.ToString();
            DOTween.To(v => text.text = string.Format(format, (int)v), startValue, endValue, duration).SetEase(Ease.Linear);

            return duration;
        }

    }
    
    public readonly struct TweenAndDuration
    {
        public readonly float Duration;
        public readonly Tween Tween;

        public TweenAndDuration(float duration, Tween tween)
        {
            Duration = duration;
            Tween = tween;
        }
    }
    
}
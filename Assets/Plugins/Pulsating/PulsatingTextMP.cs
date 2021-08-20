using TMPro;

public sealed class PulsatingTextMP : PulsatingUI<TextMeshProUGUI> 
{
    public override void AlphaApply()
    {
        Animated.alpha = Alpha;
    }
}

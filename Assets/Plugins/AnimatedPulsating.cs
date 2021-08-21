using Plugins.Pulsating;

namespace Plugins.Own.Animated
{
    public class AnimatedPulsating : AnimatedGeneric<Pulsating_Base>
    {
        public override bool Play()
        {
            if (Target) Target.Enable();
        
            return base.Play();
        }

        protected override void FillValue(float value)
        {
            Target.MaxA = value;
        }

        protected override void BeforePlay()
        {
            base.BeforePlay();
            Target.Enable();
        }

        protected override void InvokeOnFinish()
        {
            base.InvokeOnFinish();
            Target.Enable(false,finishAlpha:0f);
        }

        protected override void OnStop()
        {
            base.OnStop();
            Target.Enable(false);
        }
    }
}
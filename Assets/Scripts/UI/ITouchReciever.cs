namespace UI
{
    public interface ITouchReciever
    {
        public enum eTouchState
        {
            None,
            Enter,
            Down,
            Stay,
            Up,
            Exit,
        }
        void OnTouchDown();
        void OnTouchUp();
        void OnTouchStay();
        void OnTouchExit();
        void OnTouchEnter();
    }
}

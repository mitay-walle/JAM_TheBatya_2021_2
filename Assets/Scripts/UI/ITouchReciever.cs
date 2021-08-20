namespace UI
{
    public interface ITouchReciever
    {
        void OnTouchDown();
        void OnTouchUp();
        void OnTouchStay();
        void OnTouchExit();
        void OnTouchEntered();
    }
}

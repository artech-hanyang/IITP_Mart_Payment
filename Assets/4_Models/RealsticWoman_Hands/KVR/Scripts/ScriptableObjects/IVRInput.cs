namespace Kandooz.KVR
{
    internal interface IVRInput
    {
        float GetFingerValue(HandType hand, FingerName finger);
    }
}
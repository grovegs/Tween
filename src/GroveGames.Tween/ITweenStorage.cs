namespace GroveGames.Tween;

internal interface ITweenStorage
{
    public void Update(float[] deltaTimes);
    public bool IsActive(int slot, int version);
    public bool Cancel(int slot, int version);
}

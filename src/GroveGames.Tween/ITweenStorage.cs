namespace GroveGames.Tween;

internal interface ITweenStorage
{
    public int Count { get; }
    public void Update(float[] deltaTimes, int count);
    public bool IsActive(int slot, int version);
    public bool Cancel(int slot, int version);
}

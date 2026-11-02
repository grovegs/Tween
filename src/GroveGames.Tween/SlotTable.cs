using System;

namespace GroveGames.Tween;

internal sealed class SlotTable
{
    private int[] _values;
    private int[] _versions;
    private int[] _freeSlots;
    private int _count;
    private int _freeCount;

    public SlotTable(int capacity)
    {
        _values = new int[capacity];
        _versions = new int[capacity];
        _freeSlots = new int[capacity];
    }

    public int Capacity => _values.Length;

    public int Allocate(int value, out int version)
    {
        int slot;

        if (_freeCount > 0)
        {
            slot = _freeSlots[--_freeCount];
        }
        else
        {
            if (_count == _values.Length)
            {
                Array.Resize(ref _values, _count * 2);
                Array.Resize(ref _versions, _count * 2);
            }

            slot = _count++;
        }

        _values[slot] = value;
        version = _versions[slot];
        return slot;
    }

    public bool IsValid(int slot, int version)
    {
        return slot >= 0 && slot < _count && _versions[slot] == version;
    }

    public int GetValue(int slot)
    {
        return _values[slot];
    }

    public void SetValue(int slot, int value)
    {
        _values[slot] = value;
    }

    public void Release(int slot)
    {
        _versions[slot]++;

        if (_freeCount == _freeSlots.Length)
        {
            Array.Resize(ref _freeSlots, _freeCount * 2);
        }

        _freeSlots[_freeCount++] = slot;
    }
}

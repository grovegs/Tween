using System.Numerics;
using BenchmarkDotNet.Attributes;
using GroveGames.Scheduling;
using GroveGames.Tween;

namespace DotnetBenchmark;

[MemoryDiagnoser]
public class Benchmark
{
    private readonly struct Vector3Adapter : ITweenAdapter<Vector3>
    {
        public Vector3 Evaluate(in Vector3 from, in Vector3 to, float progress)
        {
            return Vector3.Lerp(from, to, progress);
        }
    }

    private const int Count = 1000;
    private const int SequenceCount = 300;
    private const float DeltaTime = 1f / 60f;
    private const float Flush = 1000f;

    private static readonly Action<float, float[]> s_setValue = static (value, target) => target[0] = value;
    private static readonly Action<Vector3, Vector3[]> s_setPosition = static (value, target) => target[0] = value;
    private static readonly Action s_completed = static () => { };

    private Tweener _createTweener = null!;
    private Tweener _updateTweener = null!;
    private float[] _floats = null!;
    private Vector3[] _vectors = null!;

    [GlobalSetup]
    public void Setup()
    {
        _createTweener = new Tweener(static exception => throw new InvalidOperationException("Tween callback failed", exception));
        _updateTweener = new Tweener(static exception => throw new InvalidOperationException("Tween callback failed", exception));
        _floats = new float[1];
        _vectors = new Vector3[1];

        for (var i = 0; i < Count; i++)
        {
            _updateTweener.Create(0f, 1f, 1f).WithEase(Ease.OutQuad).WithLoops(-1, LoopType.Yoyo).Bind(_floats, s_setValue);
        }

        CreateVector3sWithOptions();
        CreateSequences();
    }

    [Benchmark]
    public void Update()
    {
        _updateTweener.Update(DeltaTime, DeltaTime, DeltaTime);
    }

    [Benchmark]
    public void CreateFloats()
    {
        for (var i = 0; i < Count; i++)
        {
            _createTweener.Create(0f, 1f, 1f).Bind(_floats, s_setValue);
        }

        _createTweener.Update(Flush, Flush, Flush);
    }

    [Benchmark]
    public void CreateFloatsWithOptions()
    {
        for (var i = 0; i < Count; i++)
        {
            _createTweener.Create(0f, 1f, 1f).WithEase(Ease.OutQuad).WithDelay(0.1f).WithLoops(2, LoopType.Yoyo).WithTimeMode(TimeMode.Unscaled).WithCompletionCallback(s_completed).Bind(_floats, s_setValue);
        }

        _createTweener.Update(Flush, Flush, Flush);
    }

    [Benchmark]
    public void CreateVector3sWithOptions()
    {
        for (var i = 0; i < Count; i++)
        {
            _createTweener.Create<Vector3, Vector3Adapter>(Vector3.Zero, Vector3.One, 1f).WithEase(Ease.OutQuad).WithDelay(0.1f).WithLoops(2, LoopType.Yoyo).WithTimeMode(TimeMode.Unscaled).WithCompletionCallback(s_completed).Bind(_vectors, s_setPosition);
        }

        _createTweener.Update(Flush, Flush, Flush);
    }

    [Benchmark]
    public void CreateSequences()
    {
        for (var i = 0; i < SequenceCount; i++)
        {
            _createTweener.CreateSequence()
                .Append(_createTweener.Create(0f, 1f, 0.2f).WithEase(Ease.OutQuad), _floats, s_setValue)
                .Join(_createTweener.Create<Vector3, Vector3Adapter>(Vector3.Zero, Vector3.One, 0.2f).WithEase(Ease.OutBack), _vectors, s_setPosition)
                .Append(_createTweener.Create(1f, 0f, 0.2f).WithDelay(0.1f), _floats, s_setValue);
        }

        _createTweener.Update(Flush, Flush, Flush);
    }
}

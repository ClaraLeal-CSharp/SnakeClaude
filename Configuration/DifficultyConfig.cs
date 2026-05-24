using SnakeClaude.Enums;

namespace SnakeClaude.Configuration;

/// <summary>
/// Preset imutavel com todos os parametros que mudam por dificuldade.
/// </summary>
public sealed record DifficultyConfig
{
    public required DifficultyLevel Level { get; init; }
    public required string DisplayName { get; init; }
    public required string Description { get; init; }
    public required int GridWidth { get; init; }
    public required int GridHeight { get; init; }
    public required int InitialTickIntervalMs { get; init; }
    public required int AccelerationIntervalMs { get; init; }
    public required int SpeedIncrementMs { get; init; }
    public required int MinTickIntervalMs { get; init; }
    public required double PointMultiplier { get; init; }
    public required double ComboMultiplier { get; init; }
}

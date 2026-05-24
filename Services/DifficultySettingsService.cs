using SnakeClaude.Configuration;
using SnakeClaude.Enums;
using SnakeClaude.Interfaces;

namespace SnakeClaude.Services;

/// <summary>
/// Fonte unica de presets de dificuldade e balanceamento aplicado ao jogo.
/// </summary>
public sealed class DifficultySettingsService : IDifficultySettingsService
{
    private readonly IReadOnlyDictionary<DifficultyLevel, DifficultyConfig> _presets;

    public DifficultySettingsService()
    {
        var presets = new[]
        {
            new DifficultyConfig
            {
                Level = DifficultyLevel.Easy,
                DisplayName = "Fácil",
                Description = "Grid amplo, ritmo calmo e aceleração lenta.",
                GridWidth = 24,
                GridHeight = 24,
                InitialTickIntervalMs = 180,
                AccelerationIntervalMs = 10000,
                SpeedIncrementMs = 4,
                MinTickIntervalMs = 95,
                PointMultiplier = 0.85,
                ComboMultiplier = 0.85
            },
            new DifficultyConfig
            {
                Level = DifficultyLevel.Normal,
                DisplayName = "Normal",
                Description = "Balanceado para progressão clássica.",
                GridWidth = 20,
                GridHeight = 20,
                InitialTickIntervalMs = 150,
                AccelerationIntervalMs = 7000,
                SpeedIncrementMs = 6,
                MinTickIntervalMs = 60,
                PointMultiplier = 1,
                ComboMultiplier = 1
            },
            new DifficultyConfig
            {
                Level = DifficultyLevel.Hard,
                DisplayName = "Difícil",
                Description = "Grid compacto, começo rápido e pressão constante.",
                GridWidth = 16,
                GridHeight = 16,
                InitialTickIntervalMs = 115,
                AccelerationIntervalMs = 4200,
                SpeedIncrementMs = 8,
                MinTickIntervalMs = 38,
                PointMultiplier = 1.35,
                ComboMultiplier = 1.25
            }
        };

        _presets = presets.ToDictionary(preset => preset.Level);
        CurrentLevel = DifficultyLevel.Normal;
    }

    public DifficultyLevel CurrentLevel { get; private set; }

    public DifficultyConfig CurrentConfig => GetConfig(CurrentLevel);

    public IReadOnlyList<DifficultyConfig> GetAll() => _presets.Values
        .OrderBy(config => config.Level)
        .ToArray();

    public DifficultyConfig GetConfig(DifficultyLevel level)
        => _presets.TryGetValue(level, out var config) ? config : _presets[DifficultyLevel.Normal];

    public DifficultyConfig Select(DifficultyLevel level)
    {
        CurrentLevel = _presets.ContainsKey(level) ? level : DifficultyLevel.Normal;
        return CurrentConfig;
    }

    public void ApplyTo(GameSettings settings, DifficultyLevel level)
    {
        var config = Select(level);
        settings.GridWidth = config.GridWidth;
        settings.GridHeight = config.GridHeight;
        settings.TickIntervalMs = config.InitialTickIntervalMs;
        settings.AccelerationIntervalMs = config.AccelerationIntervalMs;
        settings.SpeedIncrementMs = config.SpeedIncrementMs;
        settings.MinTickIntervalMs = config.MinTickIntervalMs;
        settings.PointMultiplier = config.PointMultiplier;
        settings.ComboMultiplier = config.ComboMultiplier;
    }
}

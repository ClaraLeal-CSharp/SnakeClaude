using SnakeClaude.Configuration;
using SnakeClaude.Enums;

namespace SnakeClaude.Interfaces;

/// <summary>
/// Contrato central para presets e aplicacao de balanceamento por dificuldade.
/// </summary>
public interface IDifficultySettingsService
{
    DifficultyLevel CurrentLevel { get; }
    DifficultyConfig CurrentConfig { get; }
    IReadOnlyList<DifficultyConfig> GetAll();
    DifficultyConfig GetConfig(DifficultyLevel level);
    DifficultyConfig Select(DifficultyLevel level);
    void ApplyTo(GameSettings settings, DifficultyLevel level);
}

using System.Collections.ObjectModel;
using Tevux.Dashboards.Abstractions;

namespace Tech.Tevux.Dashboards.Controls;

/// <summary>
/// Concrete style of the rule.
/// </summary>
public class AppearanceRuleStyle : IAppearanceRuleStyle {
    static AppearanceRuleStyle() {
        Normal = new AppearanceRuleStyle { Name = AppearanceRuleType.Normal.ToString(), Foreground = 0xFF000000, Background = 0xFFD3D3D3 };
        Passive = new AppearanceRuleStyle { Name = AppearanceRuleType.Passive.ToString(), Foreground = 0xFF808080, Background = 0xFFD3D3D3 };
        Selected = new AppearanceRuleStyle { Name = AppearanceRuleType.Selected.ToString(), Foreground = 0xFFFFFFFF, Background = 0xFF008000 };
        Warning = new AppearanceRuleStyle { Name = AppearanceRuleType.Warning.ToString(), Foreground = 0xFF000000, Background = 0xFFFFA500 };
        Error = new AppearanceRuleStyle { Name = AppearanceRuleType.Error.ToString(), Foreground = 0xFFFFFFFF, Background = 0xFFFF0000 };
    }

    private AppearanceRuleStyle() { }

    /// <summary>
    /// A predefined style for error condition.
    /// </summary>
    public static AppearanceRuleStyle Error { get; }
    
    /// <summary>
    /// A predefined style for normal operation. 
    /// </summary>
    public static AppearanceRuleStyle Normal { get; }
    
    /// <summary>
    /// A predefined style for a condition which is not clear (disabled, missing, etc.).
    /// </summary>
    public static AppearanceRuleStyle Passive { get; }
    
    /// <summary>
    /// A predefined style for an elevated (slightly more important than normal) controls.
    /// </summary>
    public static AppearanceRuleStyle Selected { get; }
    
    /// <summary>
    /// A predefined style for warning condition.
    /// </summary>
    public static AppearanceRuleStyle Warning { get; }

    /// <summary>
    /// Background color value.
    /// </summary>
    public uint Background { get; private set; } = 0x00FFFFFF;
    
    /// <summary>
    /// Foreground color value.
    /// </summary>
    public uint Foreground { get; private set; } = 0x00FFFFFF;

    /// <inheritdoc/>
    public string Name { get; set; } = "Undefined";

    /// <summary>
    /// Maps predefined rule types to rule styles.
    /// </summary>
    public static AppearanceRuleStyle FromType(AppearanceRuleType type) {
        switch (type) {
            case AppearanceRuleType.Normal:
                return Normal;

            case AppearanceRuleType.Passive:
                return Passive;

            case AppearanceRuleType.Selected:
                return Selected;

            case AppearanceRuleType.Warning:
                return Warning;

            case AppearanceRuleType.Error:
                return Error;

            default:
                return Normal;
        }
    }
    
    /// <summary>
    /// Gets all possible rule styles.
    /// </summary>
    public static ReadOnlyCollection<IAppearanceRuleStyle> GetAllStyles() {
        return new ReadOnlyCollection<IAppearanceRuleStyle>(new List<IAppearanceRuleStyle> { Normal, Passive, Selected, Warning, Error }) ;
    }
}

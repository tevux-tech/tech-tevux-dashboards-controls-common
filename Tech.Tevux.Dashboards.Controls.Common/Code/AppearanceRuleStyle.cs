using Tevux.Dashboards.Abstractions;

namespace Tech.Tevux.Dashboards.Controls;

public class AppearanceRuleStyle : IAppearanceRuleStyle {
    static AppearanceRuleStyle() {
        Normal = new AppearanceRuleStyle() { Name = AppearanceRuleType.Normal.ToString(), Foreground = 0xFF000000, Background = 0xFFD3D3D3 };
        Passive = new AppearanceRuleStyle() { Name = AppearanceRuleType.Passive.ToString(), Foreground = 0xFF808080, Background = 0xFFD3D3D3 };
        Selected = new AppearanceRuleStyle() { Name = AppearanceRuleType.Selected.ToString(), Foreground = 0xFFFFFFFF, Background = 0xFF008000 };
        Warning = new AppearanceRuleStyle() { Name = AppearanceRuleType.Warning.ToString(), Foreground = 0xFF000000, Background = 0xFFFFA500 };
        Error = new AppearanceRuleStyle() { Name = AppearanceRuleType.Error.ToString(), Foreground = 0xFFFFFFFF, Background = 0xFFFF0000 };
    }

    private AppearanceRuleStyle() { }

    public static AppearanceRuleStyle Error { get; private set; }
    public static AppearanceRuleStyle Normal { get; private set; }
    public static AppearanceRuleStyle Passive { get; private set; }
    public static AppearanceRuleStyle Selected { get; private set; }
    public static AppearanceRuleStyle Warning { get; private set; }

    public uint Background { get; private set; } = 0x00FFFFFF;
    public uint Foreground { get; private set; } = 0x00FFFFFF;

    public string Name { get; set; } = "Undefined";

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
    public static List<IAppearanceRuleStyle> GetAllStyles() {
        return new List<IAppearanceRuleStyle>() { Normal, Passive, Selected, Warning, Error };
    }
}

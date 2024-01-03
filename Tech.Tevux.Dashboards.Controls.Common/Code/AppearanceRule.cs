using System.Globalization;
using Tevux.Dashboards.Abstractions;

namespace Tech.Tevux.Dashboards.Controls;

/// <summary>
/// Appearance rule that supports textual and numeric condition values.
/// </summary>
public class AppearanceRule : IAppearanceRule {
    private decimal? _decimalValue;
    private string _stringValue = "0";

    /// <summary>
    /// Creates a default instance.
    /// </summary>
    public AppearanceRule() : this(AppearanceRuleCondition.Undefined, "0") { }

    /// <summary>
    /// Creates a rule with specific parameters.
    /// </summary>
    public AppearanceRule(AppearanceRuleCondition condition, string value, AppearanceRuleType type = AppearanceRuleType.Warning, string textFormat = "") {
        Condition = condition;
        Value = value;
        TextFormat = textFormat;
        Style = AppearanceRuleStyle.FromType(type);
    }

    /// <inheritdoc/>
    public AppearanceRuleCondition Condition { get; set; }

    /// <inheritdoc/>
    public IAppearanceRuleStyle Style { get; set; }

    /// <inheritdoc/>
    public string TextFormat { get; set; }

    /// <inheritdoc/>
    public string Value {
        get {
            return _stringValue;
        }
        set {
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var number)) {
                _stringValue = value;
                _decimalValue = number;
            }
        }
    }

    /// <summary>
    /// Tries parsing a rule from a string.
    /// </summary>
    public static bool TryParse(string rawString, out AppearanceRule rule) {
        if (rawString is null) { goto error;}
        
        var ruleParts = rawString.Split('|');

        if (ruleParts.Length < 3) { goto error; }

        if (TryParseCondition(ruleParts[0], out var condition) == false) { goto error; }
        if (TryParseType(ruleParts[2], out var type) == false) { goto error; }

        var format = "";
        if (ruleParts.Length > 3) {
            format = ruleParts[3];
        }

        rule = new AppearanceRule(condition, ruleParts[1], type, format);

        return true;

    error:
        rule = new AppearanceRule();
        return false;
    }

    /// <summary>
    /// Checks if a value matches any conditions in the rule list.
    /// </summary>
    public bool Matches(string checkValue) {
        return Matches(checkValue, _stringValue);
    }

    /// <summary>
    /// Checks if a value matches any conditions in the rule list.
    /// </summary>
    public bool Matches(decimal checkValue) {
        return Matches(checkValue, _decimalValue);
    }

    /// <inheritdoc/>
    public override string ToString() {
        var returnString = $"{ShortenCondition(Condition)}|{Value}|{Style.Name}";

        if (string.IsNullOrEmpty(TextFormat) == false) { returnString += $"|{TextFormat}"; }

        return returnString;
    }
    private static string ShortenCondition(AppearanceRuleCondition condition) {
        switch (condition) {
            case AppearanceRuleCondition.Equal:
                return "==";

            case AppearanceRuleCondition.NotEqual:
                return "!=";

            case AppearanceRuleCondition.LessThan:
                return "<";

            case AppearanceRuleCondition.LessThanOrEqual:
                return "<=";

            case AppearanceRuleCondition.MoreThan:
                return ">";

            case AppearanceRuleCondition.MoreThanOrEqual:
                return ">=";

            case AppearanceRuleCondition.BitSet:
                return "BitSet";

            case AppearanceRuleCondition.BitNotSet:
                return "BitNotSet";

            default:
                return "==";
        }
    }

    private static bool TryParseCondition(string rawString, out AppearanceRuleCondition condition) {
        var returnValue = true;

        switch (rawString.Trim().ToUpperInvariant()) {
            case "EQUAL":
            case "==":
                condition = AppearanceRuleCondition.Equal;
                break;

            case "NOTEQUAL":
            case "!=":
                condition = AppearanceRuleCondition.NotEqual;
                break;

            case "LESSTHAN":
            case "<":
                condition = AppearanceRuleCondition.LessThan;
                break;

            case "MORETHAN":
            case ">":
                condition = AppearanceRuleCondition.MoreThan;
                break;

            case "LESSTHANOREQUAL":
            case "<=":
                condition = AppearanceRuleCondition.LessThanOrEqual;
                break;

            case "MORETHANOREQUAL":
            case ">=":
                condition = AppearanceRuleCondition.MoreThanOrEqual;
                break;

            case "BITSET":
                condition = AppearanceRuleCondition.BitSet;
                break;

            case "BITNOTSET":
                condition = AppearanceRuleCondition.BitNotSet;
                break;

            default:
                condition = AppearanceRuleCondition.Undefined;
                returnValue = false;
                break;
        }

        return returnValue;
    }

    private static bool TryParseType(string rawString, out AppearanceRuleType style) {
        var returnValue = true;

        switch (rawString.Trim().ToUpperInvariant()) {
            case "NORMAL":
            case "NORM":
            case "N":
                style = AppearanceRuleType.Normal;
                break;

            case "PASSIVE":
            case "PASS":
                style = AppearanceRuleType.Passive;
                break;

            case "SELECTED":
            case "SEL":
                style = AppearanceRuleType.Selected;
                break;

            case "WARNING":
            case "WARN":
                style = AppearanceRuleType.Warning;
                break;

            case "ERROR":
            case "ERR":
                style = AppearanceRuleType.Error;
                break;

            default:
                style = AppearanceRuleType.Undefined;
                returnValue = false;
                break;
        }

        return returnValue;
    }
    private bool Matches(string x, string y) {
        switch (Condition) {
            case AppearanceRuleCondition.Equal:
                return x == y;

            case AppearanceRuleCondition.NotEqual:
                return x != y;

            default:
                return false;
        }
    }

    private bool Matches(decimal x, decimal? y) {
        if (y.HasValue == false) { return false; }

        switch (Condition) {
            case AppearanceRuleCondition.Equal:
                return x == y;

            case AppearanceRuleCondition.NotEqual:
                return x != y;

            case AppearanceRuleCondition.LessThan:
                return x < y;

            case AppearanceRuleCondition.LessThanOrEqual:
                return x <= y;

            case AppearanceRuleCondition.MoreThan:
                return x > y;

            case AppearanceRuleCondition.MoreThanOrEqual:
                return x >= y;

            case AppearanceRuleCondition.BitSet:
                return ((((int)(x) >> (int)(y)) & 1) == 1);

            case AppearanceRuleCondition.BitNotSet:
                return ((((int)(x) >> (int)(y)) & 1) == 0);

            default:
                return false;
        }
    }
}

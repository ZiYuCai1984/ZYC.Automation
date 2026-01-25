using System.Web;

namespace ZYC.Automation.Modules.CLI;

internal sealed record CLIUriOptions(
    string? StartupCommandLineOverride,
    IReadOnlyList<string> ExecCommands,
    bool TypeOnly = false,
    string? TypeText = null,
    bool FocusOnLoaded = true)
{
    public bool ShouldExecute => !TypeOnly;

    public static CLIUriOptions Parse(Uri uri)
    {
        var q = HttpUtility.ParseQueryString(uri.Query);

        var exec = q.GetValues("exec")?.ToList() ?? new List<string>();
        var focus = q["focus"] is null || q["focus"] != "0";
        var typeOnly = q["type"] == "1";
        var typeText = q["text"];
        var startup = q["startup"];

        return new CLIUriOptions(startup, exec, typeOnly, typeText, focus);
    }
}
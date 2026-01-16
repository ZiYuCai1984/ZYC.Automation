namespace ZYC.Automation.Abstractions.State;

public class NavigationState
{
    public Uri? Focus { get; set; }

    public Uri[] TabItems { get; set; } = [];

    public HistoryItem[] History { get; set; } = [];

    public class HistoryItem
    {
        public DateTime Time { get; set; }

        /// <summary>
        ///     !WARNING In order to be compatible with the binding on the UI, type string is used instead of Uri.
        /// </summary>
        public string Uri { get; set; } = "";
    }
}
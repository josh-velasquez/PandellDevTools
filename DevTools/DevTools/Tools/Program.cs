using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DevTools.Tools
{
    enum Programs
    {
        [Description("slack")]
        Slack,
        [Description("outlook")]
        Outlook,
        [Description("chrome")]
        Chrome,
        [Description("spotify")]
        Spotify,
        [Description("wt")]
        WindowsTerminal
    }

    static class ProgramsTool
    {
        /// <summary>
        /// Gets the description of enum
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Description of enum</returns>
        public static string GetDescription(object e)
        {
            return e
            .GetType()
            .GetMember(e.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description
        ?? e.ToString();
        }
    }
}

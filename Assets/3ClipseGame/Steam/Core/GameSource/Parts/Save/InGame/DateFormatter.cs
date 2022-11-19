using System;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame
{
    public static class DateFormatter
    {
        public static string GetDateForSave()
        {
            var shortDate = DateTime.Now.ToShortDateString();
            var shortTime = DateTime.Now.ToShortTimeString();

            return string.Concat(shortDate, " ", shortTime);
        }
    }
}
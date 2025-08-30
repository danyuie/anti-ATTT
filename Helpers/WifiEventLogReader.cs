using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace TerminalSecurityTool.Helpers
{
    public class WifiEventLogReader
    {
        public List<(DateTime Time, string Message)> GetWifiEvents()
        {
            var results = new List<(DateTime, string)>();
            var query = new EventLogQuery("Microsoft-Windows-WLAN-AutoConfig/Operational", PathType.LogName);

            try
            {
                using (var reader = new EventLogReader(query))
                {
                    for (EventRecord evt = reader.ReadEvent(); evt != null; evt = reader.ReadEvent())
                    {
                        if (evt.LevelDisplayName == "Information" && evt.FormatDescription().Contains("connected"))
                        {
                            results.Add((evt.TimeCreated ?? DateTime.MinValue, evt.FormatDescription()));
                        }
                    }
                }
            }
            catch { }

            return results;
        }
    }
}

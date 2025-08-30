using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace TerminalSecurityTool.Helpers
{
    public class UsbEventLogReader
    {
        public List<(DateTime Time, string Message)> GetUsbEvents()
        {
            var list = new List<(DateTime, string)>();
            var query = new EventLogQuery("Microsoft-Windows-DriverFrameworks-UserMode/Operational", PathType.LogName,
                "*[System[(EventID=2003 or EventID=2100 or EventID=2102)]]");

            try
            {
                using var reader = new EventLogReader(query);
                EventRecord evt;

                while ((evt = reader.ReadEvent()) != null)
                {
                    string msg = evt.FormatDescription();
                    list.Add((evt.TimeCreated ?? DateTime.MinValue, msg));
                    evt.Dispose();
                }
            }
            catch
            {
                list.Add((DateTime.Now, "⚠ Không thể đọc Event Log (yêu cầu quyền admin)"));
            }

            return list;
        }
    }
}

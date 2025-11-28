using System;

namespace VendorConnector.Services
{
    public static class FirmwarePolicy
    {
        public static bool CanUpdate(string nowLocal, string window)
        {
            var parts = window.Split('-');
            var from = TimeSpan.Parse(parts[0]);
            var to   = TimeSpan.Parse(parts[1]);
            var now  = TimeSpan.Parse(nowLocal);
            if (from <= to) return now >= from && now <= to;
            return now >= from || now <= to;
        }

        public static int SemverCmp(string a, string b)
        {
            var pa = a.Split('.');
            var pb = b.Split('.');
            for (int i = 0; i < 3; i++)
            {
                int ai = int.Parse(pa[i]);
                int bi = int.Parse(pb[i]);
                if (ai != bi) return ai.CompareTo(bi);
            }
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using EnvDTE80;

namespace ssb.umfhook
{
    internal class DTEUtil
    {
        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);
        [DllImport("ole32.dll")]
        private static extern void GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        /// <summary>
        /// Gets the available Visual Studio DTE2
        /// </summary>
        public static List<DTE2> GetDTEs()
        {
            List<DTE2> dte2s = new List<DTE2>();

            IRunningObjectTable rot;
            GetRunningObjectTable(0, out rot);
            IEnumMoniker enumMoniker;
            rot.EnumRunning(out enumMoniker);
            enumMoniker.Reset();
            IntPtr fetched = IntPtr.Zero;
            IMoniker[] moniker = new IMoniker[1];
            while (enumMoniker.Next(1, moniker, fetched) == 0)
            {
                IBindCtx bindCtx;
                CreateBindCtx(0, out bindCtx);
                string displayName;
                moniker[0].GetDisplayName(bindCtx, null, out displayName);
                // add all VisualStudio ROT entries to list
                if (displayName.StartsWith("!VisualStudio"))
                {
                    object comObject;
                    rot.GetObject(moniker[0], out comObject);
                    dte2s.Add((DTE2)comObject);
                }
            }
            return dte2s;
        }
    }
}

﻿using System.Runtime.InteropServices;

namespace ssb.umfhook
{
    /// <summary>
    /// Class containing the IOleMessageFilter
    /// thread error-handling functions.
    /// </summary>
    public class MessageFilter : IOleMessageFilter
    {
        // Start the filter.
        public static void Register()
        {
            IOleMessageFilter newFilter = new MessageFilter();
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(newFilter, out oldFilter);
        }

        // Done with the filter, close it.
        public static void Revoke()
        {
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(null, out oldFilter);
        }

        //
        // IOleMessageFilter functions.
        // Handle incoming thread requests.
        int IOleMessageFilter.HandleInComingCall(int dwCallType, System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr lpInterfaceInfo)
        {
            return 0; //Return the flag SERVERCALL_ISHANDLED.
        }

        // Thread call was rejected, so try again.
        int IOleMessageFilter.RetryRejectedCall(System.IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
        {
            if (dwRejectType == 2)
                // flag = SERVERCALL_RETRYLATER.
            {
                return 99; // Retry the thread call immediately if return >=0 & <100.
            }
            return -1; // Too busy; cancel call.
        }

        int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
        {
            //Return the flag PENDINGMSG_WAITDEFPROCESS.
            return 2;
        }

        // Implement the IOleMessageFilter interface.
        [DllImport("Ole32.dll")]
        private static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out IOleMessageFilter oldFilter);
    }
}
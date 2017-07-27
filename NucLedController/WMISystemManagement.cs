using System;
using System.Management;

namespace NucLedController
{
    public sealed class WMISystemManagement
    {
        private static readonly string OBJECT_SEARCHER_SCOPE = "\\root\\WMI";
        private static readonly string OBJECT_SEARCHER_QUERY_STRING = "SELECT * FROM CISD_WMI";
        private static readonly string SET_LED_METHOD_NAME = "SetState";

        private static readonly WMISystemManagement INSTANCE = new WMISystemManagement();

        private EnumerationOptions enumerationOptions;
        private ManagementObjectSearcher objectSearcher;

        private WMISystemManagement()
        {
            enumerationOptions = new EnumerationOptions();
            enumerationOptions.ReturnImmediately = false;
            objectSearcher = new ManagementObjectSearcher(OBJECT_SEARCHER_SCOPE, OBJECT_SEARCHER_QUERY_STRING, enumerationOptions);
        }

        public static WMISystemManagement Instance
        {
            get { return INSTANCE; }
        }

        public void WriteData(byte[] data)
        {

            foreach (ManagementObject queryObj in objectSearcher.Get())
            {
                queryObj.InvokeMethod(SET_LED_METHOD_NAME, new object[] { BitConverter.ToInt32(data, 0) });
                queryObj.Dispose();
            }

        }

    }
}

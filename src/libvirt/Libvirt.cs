using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace libvirt
{
    public static class Libvirt
    {
        public const string Name = "libvirt";

        public const string LibCName = "libc";

        static Libvirt()
        {
            NativeLibrary.SetDllImportResolver(typeof(Libvirt).Assembly, ImportResolver);
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            IntPtr handle = IntPtr.Zero;

            if (libraryName == Libvirt.Name)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    NativeLibrary.TryLoad("libvirt.so.0", assembly, searchPath, out handle);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    NativeLibrary.TryLoad("libvirt-0.dll", assembly, searchPath, out handle);
                }
            }
            else if (libraryName == Libvirt.LibCName)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    NativeLibrary.TryLoad("libc.so.6", assembly, searchPath, out handle);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    NativeLibrary.TryLoad("msvcrt.dll", assembly, searchPath, out handle);
                }
            }


            return handle;
        }

        public const int VIR_UUID_STRING_BUFLEN = 36 + 1;

        public static Version Version
        {
            get
            {
                LibvirtHelper.ThrowExceptionOnError(virGetVersion(out ulong libVer, null, out _));

                int release = (int)(libVer % 1000);
                int minor = (int)((libVer % 1000000) / 1000);
                int major = (int)(libVer / 1000000);

                return new Version(major, minor, release);
            }
        }

        #region Library

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virGetVersion")]
        public static extern int virGetVersion([Out] out ulong libVer, [In] string type, [Out] out ulong typeVer);

        [DllImport(Libvirt.LibCName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "free")]
        public static extern void virFree(IntPtr ptr); //Todo: virFree in dll?

        #endregion

        #region Connect

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpen")]
        public static extern IntPtr virConnectOpen(string name);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpenReadOnly")]
        public static extern IntPtr virConnectOpenReadOnly(string name);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectClose")]
        public static extern int virConnectClose(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetCapabilities")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] 
        public static extern string virConnectGetCapabilities(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetHostname")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] 
        public static extern string virConnectGetHostname(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetType")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StaticStringMarshaler))]
        public static extern string virConnectGetType(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListAllDomains")]
        public static extern int virConnectListAllDomains(IntPtr conn, [Out] out IntPtr domains, virConnectListAllDomainsFlags flags);

        #endregion

        #region Domain

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetID")]
        public static extern int virDomainGetID(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StaticStringMarshaler))]
        public static extern string virDomainGetName(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetUUIDString")]
        public static extern int virDomainGetUUIDString(IntPtr domain, [Out] IntPtr uuid);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetOSType")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
        public static extern string virDomainGetOSType(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetInfo")]
        public static extern int virDomainGetInfo(IntPtr domain, [Out] virDomainInfo info);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetXMLDesc")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
        public static extern string virDomainGetXMLDesc(IntPtr domain, int flags = 0);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainCreateXML")]
        public static extern IntPtr virDomainCreateXML(IntPtr conn, string xmlDesc, uint flags = 0);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainDestroy")]
        public static extern int virDomainDestroy(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainFree")]
        public static extern int virDomainFree(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainShutdown")]
        public static extern int virDomainShutdown(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSuspend")]
        public static extern int virDomainSuspend(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainResume")]
        public static extern int virDomainResume(IntPtr domain);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainReboot")]
        public static extern int virDomainReboot(IntPtr domain, uint flags = 0);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSave")]
        public static extern int virDomainSave(IntPtr domain, string to);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainRestore")]
        public static extern int virDomainRestore(IntPtr conn, string from);

        #endregion

        #region Error

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virGetLastError")]
        public static extern IntPtr virGetLastError();

        #endregion
    }

    /// <summary>
    /// Marshals a char* string without freeing the memory
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/17667611/how-to-marshal-to-ansi-string-via-attribute"/>
    internal class StaticStringMarshaler : ICustomMarshaler
    {
        public static ICustomMarshaler GetInstance(string cookie)
        {
            var result = new StaticStringMarshaler();

            return result;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj) => default;

        public object MarshalNativeToManaged(IntPtr pNativeData) => Marshal.PtrToStringUTF8(pNativeData);

        public void CleanUpManagedData(object ManagedObj) { }

        public void CleanUpNativeData(IntPtr pNativeData) { }

        public int GetNativeDataSize() => -1;
    }

    /// <summary>
    /// Marshals a char* string and freeing the memory using libc
    /// </summary>
    internal class CStringMarshaler : ICustomMarshaler
    {
        public static ICustomMarshaler GetInstance(string cookie)
        {
            var result = new CStringMarshaler();

            return result;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj) => default;

        public object MarshalNativeToManaged(IntPtr pNativeData) => Marshal.PtrToStringUTF8(pNativeData);

        public void CleanUpManagedData(object ManagedObj) { }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Libvirt.virFree(pNativeData);
        }

        public int GetNativeDataSize() => -1;
    }
}
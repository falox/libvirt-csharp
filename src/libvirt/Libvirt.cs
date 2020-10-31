using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace libvirt
{
    public partial class Libvirt
    {
        public const string Name = "libvirt";

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

            return handle;
        }

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpen")]
        public static extern IntPtr virConnectOpen(string name);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint="virConnectClose")]
        public static extern int virConnectClose(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virGetLastError")]
        public static extern IntPtr virGetLastError();
    }
}
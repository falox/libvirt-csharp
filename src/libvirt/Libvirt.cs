using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace libvirt
{
    public class Libvirt
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

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpenReadOnly")]
        public static extern IntPtr virConnectOpenReadOnly(string name);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint="virConnectClose")]
        public static extern int virConnectClose(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetCapabilities")]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string virConnectGetCapabilities(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetHostname")]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string virConnectGetHostname(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetType")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StaticStringMarshaler))]
        public static extern string virConnectGetType(IntPtr conn);

        [DllImport(Libvirt.Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = "virGetLastError")]
        public static extern IntPtr virGetLastError();    
    }

    /// <summary>
    /// Marshals a char* string without freeing the memory
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/17667611/how-to-marshal-to-ansi-string-via-attribute"/>
    internal class StaticStringMarshaler : ICustomMarshaler
    {
        public static ICustomMarshaler GetInstance(string cookie)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException(nameof(cookie));
            }

            var result = new StaticStringMarshaler();

            return result;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj) =>  Marshal.StringToHGlobalAnsi((string) ManagedObj);

        public object MarshalNativeToManaged(IntPtr pNativeData) => Marshal.PtrToStringAnsi(pNativeData);

        public void CleanUpManagedData(object ManagedObj) { }

        public void CleanUpNativeData(IntPtr pNativeData) { }

        public int GetNativeDataSize() => -1;
    }
}
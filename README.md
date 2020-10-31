# libvirt-csharp

`libvirt-csharp` is a cross-platform library to access the [libvirt](https://libvirt.org/) virtualization API from C# and .NET Core.

The [official C# bindings](https://libvirt.org/csharp.html) of the libvirt API are pretty old and they are built on the .NET Framework (not Core) and Mono. This is an attempt to adhere to the new standard and patterns.

## Compiling the source code

```
https://github.com/falox/libvirt-csharp.git
cd libvirt-csharp
dotnet build
```

## Running the tests

```
dotnet test
```

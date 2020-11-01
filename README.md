[![.NET Core](https://github.com/falox/libvirt-csharp/workflows/.NET%20Core/badge.svg?branch=main)](https://github.com/falox/libvirt-csharp/actions?query=workflow%3A%22.NET+Core%22)
[![Coverage Status](https://coveralls.io/repos/github/falox/libvirt-csharp/badge.svg?branch=main)](https://coveralls.io/github/falox/libvirt-csharp?branch=main)

# libvirt-csharp

`libvirt-csharp` is a cross-platform library to access the [libvirt](https://libvirt.org/) virtualization API from C# and .NET Core.

The [official C# bindings](https://libvirt.org/csharp.html) of the libvirt API are pretty old and they are built on the .NET Framework (not Core) and Mono. This is an attempt to adhere to the new standard and patterns.

## Compiling the source code

```bash
git clone https://github.com/falox/libvirt-csharp.git
cd libvirt-csharp
dotnet build
```

You cannot run `dotnet run`, since there are no sample clients yet. You can find some examples in the `tests` directory, and you can run them with:

```bash
dotnet test
```

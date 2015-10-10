FluentUriBuilder
================

***A safer and more readable way to build URI's in .NET***

[![Build status](https://ci.appveyor.com/api/projects/status/pw16gfpk4y0e5eh0?svg=true)](https://ci.appveyor.com/project/balazsbotond/fluenturibuilder)

Quick Examples
--------------

To build this new URI:

ftp://user:password@example.com:888/path/to/file?param1=val1&param2=val2#fragment

Write:

```csharp
var uri = FluentUriBuilder.Create()
    .Scheme(UriScheme.Ftp)
    .Credentials("user", "password")
    .Host("example.com")
    .Port(888)
    .Path("path/to/file")
    .QueryParams(new {
        param1 = "val1",
        param2 = "val2"
    })
    .Fragment("fragment")
    .ToUri();
```

To modify an existing URI:

```csharp
var uri = FluentUriBuilder.From("http://example.com/somepath?foo=bar#baz")
    .Port(8080)
    .Path("/otherpath")
    .WithoutQueryParams()
    .WithoutFragment()
    .ToUri();
```

The result is:

http://example.com:8080/otherpath

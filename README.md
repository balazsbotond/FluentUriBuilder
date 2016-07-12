FluentUriBuilder
================

***A safer and more readable way to build URI's in .NET***

[![Build status](https://ci.appveyor.com/api/projects/status/pw16gfpk4y0e5eh0?svg=true)](https://ci.appveyor.com/project/balazsbotond/fluenturibuilder)
[![NuGet package](https://img.shields.io/nuget/v/FluentUriBuilder.svg)](https://www.nuget.org/packages/FluentUriBuilder/)

Quick example
-------------

To build this new URI:

```
ftp://user:password@example.com:888/path/to/file?param1=val1&param2=a%23value%26with%40weird%3fcharacters#fragment
```

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
        param2 = "a#value&with@weird?characters"
    })
    .Fragment("fragment")
    .ToString();
```

Rationale
---------

You often see code that creates URI's using simple string concatenation or format strings:

```csharp
var valueWithWeirdCharacters = "a#value&with@weird?characters";
var badUri1 = "http://example.com/path?param1=" + valueWithWeirdCharacters + "&param2=asdf";
var badUri2 = string.Format("http://example.com/path?param1={0}&param2=asdf", valueWithWeirdCharacters);
```

The result is an invalid URI because the value of `param1` is not escaped:

```csharp
"http://example.com/path?param1=a#value&with@weird?characters&param2=asdf"
```

One alternative is to escape each value using `Uri.EscapeDataString`:

```csharp
var correctUri = "http://example.com/path?param1=" +
    Uri.EscapeDataString(valueWithWeirdCharacters) +
	"&param2=asdf";
```

But this quickly gets ugly when you have a lot of arguments, and it is easy to forget.

Enter `System.UriBuilder`:

```csharp
var valueWithWeirdCharacters = "a#value&with@weird?characters";
var uriBuilder = new UriBuilder("http://example.com/path");
var parameters = HttpUtility.ParseQueryString(string.Empty);

parameters["param1"] = valueWithWeirdCharacters;
parameters["param2"] = "asdf";

uriBuilder.Query = parameters.ToString();

var correctUri = uriBuilder.AbsoluteUri;
```

This is correct, but also the most complicated and unreadable of all of the above examples.
This is why I created `FluentUriBuilder`: to allow creating URI's *in a safe and readable way*:

```csharp
var valueWithWeirdCharacters = "a#value&with@weird?characters";
var uri = FluentUriBuilder
    .From("http://example.com/path")
	.QueryParams(new {
	    param1 = valueWithWeirdCharacters,
		param2 = "asdf"
	});
```

More Examples
-------------

Query parameters can also be specified using an `IDictionary<TKey, TValue>`:

```csharp
var params = new Dictionary<string, string> {
	{ "user", "averagejoe236" },
	{ "apiKey", "af43af43rcfaf34xqf" }
};

var uri = FluentUriBuilder.Create()
    .Scheme(UriScheme.Http)
	.Host("facebook.com")
	.Path("posts")
	.QueryParams(params)
	.ToString();
```

Or one by one, by calling `.QueryParam("key", "value")` repeatedly:

```csharp
var uri = FluentUriBuilder.Create()
    .Scheme(UriScheme.Http)
	.Host("google.com")
	.Path("/")
	.QueryParam("q", "FluentUriBuilder")
	.QueryParam("source", "hp")
	.ToString();
```

The latter can be used to specify more query parameters with the same name.

To modify an existing URI:

```csharp
var uri = FluentUriBuilder.From("http://example.com/somepath?foo=bar#baz")
    .Port(8080)
    .Path("/otherpath")
    .RemoveQueryParams()
    .RemoveFragment()
    .ToUri();
```

**This example returns a `System.Uri` instance instead of a string because of the `.ToUri()` call at the end.**


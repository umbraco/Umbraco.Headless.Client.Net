<p align="center">
  <img src="img/logo.png" alt="Umbraco Heartcore Logo" />
</p>

<br>

# .NET Core Client Library for Umbraco Heartcore

Umbraco Heartcore is the headless cms version of Umbraco as a service.

This repository contains the .NET Core client library for the Umbraco Heartcore REST APIs.
The library is based on Netstandard2.0 to support application development including Xamarin/UWP applications.

## Download & Install

### Via NuGet

To get the binaries of this library as distributed by Umbraco, ready for use within your project you can install them using the .NET package manager.

Please note that the minimum NuGet client version requirement has been updated to 2.12 in order to support multiple .NET Standard targets in the NuGet package.

```
Install-Package Umbraco.Headless.Client.Net
```

### Via Git

To get the source code of the client library via git just type:

```bash
git clone https://github.com/umbraco/Umbraco.Headless.Client.Net.git
cd Umbraco.Headless.Client.Net
```

## Requirements

Given that this is a client library for use with Umbraco Headless you will need an Umbraco Headless project to utilize the library. A new project can be created through the Umbraco Cloud Portal and you can create a (14 day free) trial through [umbraco.com/headless](https://umbraco.com/headless).

## Solution setup

* There's a `sln` file in the root of this repository which references these projects:

* Umbraco.Headless.Client.Net - which is the class library built for .NET Standard (netstandard2.0) which is what you would use to access the Umbraco Headless APIs.
* Umbraco.Headless.Client.Net.Tests - this is the test project, which uses xunit for the various unit and integration tests. All API responses are saved as resources in the project, so all the calls from the library can be mocked.

## Code Samples

In the root of the git repository is a `/samples` folder, which contains two different .NET Core solutions with implementations showing how the client library can be used.

### Console sample

In `/samples/Umbraco.Headless.Client.Samples.Console/` is a .NET Core based Console implementation, which shows a few different approaches to using the client library to interact with both the Content Delivery and the Content Management APIs.

In order to use the sample you will need an Umbraco Headless project with content and media. If you don't have one you can use `demo-headless` as the project alias for the options, which doesn't require an API Key.

### .NET Core MVC website sample

In `/samples/Umbraco.Headless.Client.Samples.Web/` is a .NET Core 2.2 based MVC website implementation, which shows one possible approach to creating a website using Umbraco Headless for Content Delivery.

In order to use the sample you will need an Umbraco Headless project with content, media and document types that correspond to those setup in the Views and Models of the sample website. You can use `demo-headless` as the project alias to get started with the sample. The Project behind this alias has been used as the source of the sample, so its a good place to start.

## Dependencies

### Refit

The client library depend on refit for working with the various endpoints of the Umbraco Headless Content Delivery and Content Management APIs. What it does is essentially to turn the REST API into a live interface, and it supports UWP, Xamarin.Android, Xamarin.Mac, Xamarin.iOS, Desktop .NET 4.6.1 and .NET Core. So it should work with almost any type of project you can imagine.

- [Refit: The automatic type-safe REST library for .NET Core, Xamarin and .NET](https://github.com/reactiveui/refit/)

## Bugs, issues and Pull Requests

If you encounter a bug when using this client library you are welcome to open an issue in the issue tracker of this repository. We always welcome Pull Request and please feel free to open an issue before submitting a Pull Request to discuss what you want to submit.

Questions about usage should be posted to the Headless forum on [our.umbraco.com](https://our.umbraco.com).

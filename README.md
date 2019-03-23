# Newtonsoft.Json.Partial

[![GitHub Tag](https://img.shields.io/github/tag/FlorianRappl/Newtonsoft.Json.Partial.svg?style=flat-square)](https://github.com/FlorianRappl/Newtonsoft.Json.Partial/releases)
[![NuGet Count](https://img.shields.io/nuget/dt/Newtonsoft.Json.Partial.svg?style=flat-square)](https://www.nuget.org/packages/Newtonsoft.Json.Partial/)
[![Issues Open](https://img.shields.io/github/issues/FlorianRappl/Newtonsoft.Json.Partial.svg?style=flat-square)](https://github.comFlorianRappl/Newtonsoft.Json.Partial/issues)

A small helper library to allow deserialization (and serialization) of JSON fragments. Helpful for `PATCH` or partial `PUT` operations in web API projects.

## Installation

Install the library via NuGet. All you need to do is manage your NuGet packages via Visual Studio or run

```sh
nuget install Newtonsoft.Json.Partial
```

in the package manager command line. Alternatively, you can install the library via the command line using the `dotnet` tooling.

## Usage

The main class is called `Part<T>`. This class is a wrapper that should be used for deserializing objects with information about the used keys.

### Simple example

```c#
// The DTO
public class Employee
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "age")]
    public int Age { get; set; }
}

// The incoming JSON
var json = "{\"id\": 17, \"name\": \"Test\"}";

// Usually, we do this to obtain the deserialized object
var normalObj = JsonConvert.DeserializeObject<Employee>(json);

// Now we can also do this to obtain the deserialized object and more information
var partialObj = JsonConvert.DeserializeObject<Part<Employee>>(json);
```

Note that `partialObj.Data` is equal to `normalObj`, however, in `partialObj` we also have a `Keys` property.

### Using the information

Once we have a `Part<T>` object we can use it to get information to the used `Keys` or use one of the extension methods.

```c#
var partialObj = JsonConvert.DeserializeObject<Part<Employee>>(json);

if (partialObj.IsSet(m => m.Age))
{
    // Do something if Age has been set
}
```

Alternatively, we can also supply a callback action.

```c#
var partialObj = JsonConvert.DeserializeObject<Part<Employee>>(json);

partialObj.IsSet(m => m.Age, value =>
{
    // Do something with the supplied value of Age
});
```

It is also possible to perform some validation on the supplied JSON object to verify that no "illegal" properties have been specified:

```c#
var partialObj = JsonConvert.DeserializeObject<Part<Employee>>(json);

// Perform validation of the partial object
var isValid = partialObj.Validate();
```

### Decorating Data Transfer Objects

In addition to the usual `JsonProperty`, `JsonIgnore`, etc. attributes there is also the `JsonNoPartial` attribute. It specifies that in context of partial deserialization that property must not be set.

## License

MIT License (MIT). For more information see [LICENSE](./LICENSE) file.

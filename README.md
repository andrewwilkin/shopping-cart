# Shopping Cart
Very Simple Shopping Cart PoC Implementation Written In C# / .Net Core 2
This is <b>NOT meant for production</b> use as is - you have been warned!
<br />

## Target Frameworks

The WebApi and Client targets the following frameworks: 
	.Net Core 2.1 (and above)

<br />

## Solutions

ShoppingCart.Api	The Web Api (runs locally using in memory database)

ShoppingCart.Client	The API Client (connects to the Web Api above - which naturally needs to be running)


## How to use the server
Just give it a whirl and all being well it should launch on:
	https://localhost:44383

You may have to accept the installation of a self signed certificate

All being well if you navigate to this location in a browser it should open up Swagger (note only tested this on Windows).
If you want to change the port for development or want to launch a browser automatically then edit the launchSettings.json


Note: Have only implemented "Application/Json" as a content type, you could easily change this to also support XML if you need


## How to use the client

Add reference to the client namespace
```csharp
using ShoppingCart.Client;
```

Setup the client like so:
```csharp
var config = new ApiConfig();
var apiClientAsync = new ApiClientAsync(config);
```
The config is as much there for future environmental support, and has a reference to the API Urls.


<b>Before using this you need to kick off the server or you'll have a somewhat limited experience...</b>


## Get a product list (note there is no pagination implemented)

```csharp
var response = await apiClientAsync.ProductService.GetProductListAsync();
```

Where response is a Tuple composed of status (bool) and a Product List


## Create empty cart

```csharp
var response = await apiClientAsync.CartService.CreateCartAsync();
```

Where response is a Tuple composed of status (bool) and a Cart


## Add a product to the cart

```csharp
var response = await apiClientAsync.CartService.AddProductToCartAsync({product}, {quantity});
```

Where response is a Tuple composed of status (bool) and a Cart

There are a number of other methods implemented - check out the tests project in that solution.

## Async vs Sync
Only async methods are available at this time, it would be possible to simply wrap async calls with Wait() if you want to get up and running and have a proper use case for doing so.

Given it is over the old tinterweb async is pretty much the default these days.



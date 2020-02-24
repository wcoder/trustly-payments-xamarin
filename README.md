# Trustly for Xamarin

Trustly payments for Xamarin.Android and Xamarin.iOS

[Full documentation](https://trustly.com/en/developer/api/#/iosandroid)

## iOS

```cs
var userContentController = new WKUserContentController();
var configuration = new WKWebViewConfiguration();

configuration.UserContentController = userContentController;

// Use configuration when creating WKWebView and attach it to your view
_webView = new WKWebView(webViewContainer.Bounds, configuration);
webViewContainer.AddSubview(_webView);

// Add TrustlyWKScriptMessageHandler
userContentController.AddScriptMessageHandler(
    new TrustlyWKScriptOpenURLScheme(_webView),
    TrustlyWKScriptOpenURLScheme.NAME);
```

Also you can see [native README](native/trustly-framework-ios)

## Android

```cs
TODO
```

Also you can see [native README](native/trustly-library-android)


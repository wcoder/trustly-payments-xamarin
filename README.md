# Trustly for Xamarin

Trustly payments for Xamarin.Android and Xamarin.iOS

[Full documentation](https://trustly.com/en/developer/api/#/iosandroid)

## iOS

```cs
public override void ViewDidLoad()
{
    base.ViewDidLoad();
    
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
}
```

Also you can see [native README](native/trustly-framework-ios)

## Android

```cs
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);
    SetContentView(Resource.Layout.activity_main);

    // Get WebView & WebSettings
    WebView myWebView = FindViewById<WebView>(Resource.Id.webview);
    myWebView.SetWebViewClient(new WebViewClient());
    WebSettings webSettings = myWebView.Settings;

    // Enable javascript and DOM Storage
    webSettings.JavaScriptEnabled = true;
    webSettings.DomStorageEnabled = true;

    // Add TrustlyJavascriptInterface
    myWebView.AddJavascriptInterface(
        new TrustlyJavascriptInterface(this),
        TrustlyJavascriptInterface.NAME);
}
```

Also you can see [native README](native/trustly-library-android)


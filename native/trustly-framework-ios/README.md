Trustly Framwork iOS v1.0
=====================

This library has been created for iOS to simplify the process of
implementing the functionality needed when using Trustly within an iOS app.

For full documentation on the Trustly API internals visit our developer
website: http://trustly.com/developer .

Overview
========

CUSTOM URL SCHEME
We highly recommend implementing a custom URL scheme for your iOS apps. By doing so we can
redirect users back to your App after using external identification apps such as Mobilt BankID.
It's a very simple process; just follow any Tutorial on iOS/iPhone Custom URL Schemes.

The URL scheme can then be registred using one of the following methods:

1. Register URL Scheme directly in Trustly backend; please contact your integration manager.
2. Sent as an attribute "URLScheme" when making an API-call to Trustly.

WKSCRIPTMESSAGEHANDLER
When a Custom URL Scheme has been added we will communicate to your app via a WKScripScriptMessageHandler
in WKWebView. We do highly recommend using WKWebView instead of UIWebView since external identification apps
otherwise wont be able to be opened automatically, thereby defeating the main purpose of the iOS implementation.
By adding this framwork to you project you will have the necessary WKScripScriptMessageHandlers's needed.
Just drag the framework to you Project Navigator window in Xcode and it will be added to your project.
Also don't forget to add TrustlyFrameworkiOS to "Embedded Binaries" within your project target.

Example
--------------------

override func viewDidLoad() {
    super.viewDidLoad()

    let userContentController:WKUserContentController = WKUserContentController()
    let configuration:WKWebViewConfiguration = WKWebViewConfiguration()
    configuration.userContentController = userContentController

    // Use configuration when creating WKWebView and attach it to your view
    self.webView = WKWebView(frame: self.view.bounds, configuration: configuration)
    self.view = self.webView

    // Add TrustlyWKScriptMessageHandler
    userContentController.addScriptMessageHandler(
    TrustlyWKScriptOpenURLScheme(webView: webView), name: TrustlyWKScriptOpenURLScheme.NAME)
}

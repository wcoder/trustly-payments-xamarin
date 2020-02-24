//
// TrustlyWKScriptOpenURLScheme.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2020 Yauheni Pakala
//

using System;
using Foundation;
using UIKit;
using WebKit;

namespace Xam.Trustly.iOS
{
    /// <summary>
    /// Will try to open the URL, then return result in callback
    /// </summary>
    public class TrustlyWKScriptOpenURLScheme : NSObject, IWKScriptMessageHandler
    {
        public const string NAME = "trustlyOpenURLScheme";

        private readonly WKWebView _webView;

        public TrustlyWKScriptOpenURLScheme(WKWebView webView)
        {
            _webView = webView;
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            var parsed = getParsedJSON(message.Body);
            var callback = parsed.ObjectForKey(new NSString("callback")) as NSString;
            var urlscheme = parsed.ObjectForKey(new NSString("urlscheme")) as NSString;

            var success = UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(urlscheme));
            var successStr = success.ToString().ToLower(); // "Bool" to "bool"
            var js = new NSString($"{callback}({successStr}, \"{urlscheme}\")");
            _webView?.EvaluateJavaScript(js, null);
        }

        /// <summary>
        /// Helper function that will try to parse AnyObject to JSON and return as <see cref="NSDictionary"/>
        /// </summary>
        /// <param name="object">AnyObject</param>
        /// <returns>JSON object as <see cref="NSDictionary"/> if parsing is successful, otherwise null</returns>
        private NSDictionary getParsedJSON(NSObject @object)
        {
            try
            {
                var jsonString = @object as NSString;
                var jsonData = jsonString.Encode(NSStringEncoding.UTF8);

                var parsed = NSJsonSerialization.Deserialize(jsonData, NSJsonReadingOptions.FragmentsAllowed, out var error);
                if (error != null)
                {
                    throw new NSErrorException(error);
                }
                return parsed as NSDictionary;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A JSON parsing error occurred: {ex}");
            }
            return null;
        }
    }
}

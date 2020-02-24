/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016 Trustly Group AB
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */


import Foundation
import WebKit

/**
 Will try to open the URL, then return result in callback
 :param: JSON
 */
public class TrustlyWKScriptOpenURLScheme: NSObject, WKScriptMessageHandler {
    
    public static let NAME = "trustlyOpenURLScheme"
    var webView: WKWebView
    
    public init(webView: WKWebView) {
        self.webView = webView
    }
    
    public func userContentController(userContentController: WKUserContentController, didReceiveScriptMessage message: WKScriptMessage) {
        
        let parsed = getParsedJSON(message.body)!
        let callback:String = parsed.objectForKey("callback") as! String
        let urlscheme:String = parsed.objectForKey("urlscheme") as! String
        
        let success:Bool = UIApplication.sharedApplication().openURL(NSURL(string: urlscheme)!)
        let js:String = NSString(format: "%@(%@,\"%@\");", callback, success, urlscheme) as String
        webView.evaluateJavaScript(js, completionHandler: nil)
    }
    
    /**
     Helper function that will try to parse AnyObject to JSON and return as NSDictionary
     :param: AnyObject
     :returns: JSON object as NSDictionary if parsing is successful, otherwise nil
     */
    func getParsedJSON(object: AnyObject) -> NSDictionary? {
        do {
            let jsonString:String = object as! String
            let jsonData = jsonString.dataUsingEncoding(NSUTF8StringEncoding)!
            
            let parsed = try NSJSONSerialization.JSONObjectWithData(jsonData, options: NSJSONReadingOptions.AllowFragments) as! NSDictionary
            return parsed
        }
        catch let error as NSError {
            print("A JSON parsing error occurred:\n \(error)")
        }
        return nil
    }
}
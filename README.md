# Zhigly
![alt tag](http://i.imgur.com/VtRhu0z.png)

**Zhigly** - Listings for College Communities

## Requirements
 * Visual Studio 2010 Express or above.
 * SQL database management software of your choosing.

## Installation

 * Download the code and open with Visual Studio 2010 Express or above.
 * NuGet packages are included. If you have any problem please follow [these instructions](http://stackoverflow.com/questions/6876732/how-do-i-get-nuget-to-install-update-all-the-packages-in-the-packages-config).
 * Import the schema information via the database.sql file inside the SQL folder.
 * Configure the API files found inside the API folder. This includes Facebook, Twitter, Google+, Imgur, Recaptcha and Zoho.
 * Configuration for Google Analytics can be found inside the JS/GoogleAnalytics.js file. 
 
 ## Note
 **Zhigly** will still run without any additional configuration, however you will be losing out on the added functionality that these third-party services provide. 
 
## Technologies Used

* [Facebook SDK](https://developers.facebook.com/docs/javascript) - using Facebook plugins
* [Twitter SDK](https://dev.twitter.com/web/javascript) - using Twitter plugins
* [Google+ API](https://developers.google.com/+/web/api/rest/) - using various Google+ data
* [Imgur API](https://api.imgur.com/) - hosting images uploaded by users
* [jQuery](https://jquery.com/) - client-side JavaScript (CDN provided by [Google](https://developers.google.com/speed/libraries/))
* [reCAPTCHA](https://www.google.com/recaptcha) - spam and abuse protection
* [Google Analytics](https://www.google.com/analytics/) - various website analytics tools
* [Google Fonts](https://fonts.google.com/) - source of all the fonts

## Live Demo at Zhigly.com

**Zhigly** is live via hosting provided by GearHost. [Click here](http://zhigly.com/) to view.


## License

**Zhigly** is licensed under the [MIT license](LICENSE).

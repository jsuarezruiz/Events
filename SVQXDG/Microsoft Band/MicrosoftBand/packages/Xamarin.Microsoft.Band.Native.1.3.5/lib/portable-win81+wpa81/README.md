# Microsoft Band SDK (Preview)

The Microsoft Band SDK gives developers access to the sensors available on the band, as well as the ability to create and send notifications to tiles. Enhance and extend the experience of your applications to your customers' wrists.

## Amazing App Experiences
Extend the experience of your application to your users' wrists via a new dimension of interaction. Create an app that can send UI content to the band, keeping users engaged when they're in motion. Your app can also receive data directly from the band sensors, giving your users more reasons to interact with it. Create a personalized, data-rich, custom experience and enhanced scenarios that will engage users in ways only possible with Microsoft Band.

### Access Sensors
Use a range of sensors including heart rate, UV, accelerometer, gyroscope, and skin temperature, as well as fitness data, to design cutting-edge user experiences:

 - **Accelerometer**  
   Provides X, Y, and Z acceleration in meters per second squared (m/s²) units.
 - **Gyroscope**  
   Provides X, Y, and Z angular velocity in degrees per second (°/sec) units.
 - **Distance**  
   Provides the total distance in centimeters, current speed in centimeters per second (cm/s), current pace in milliseconds per meter (ms/m), and the current pedometer mode (such as walking or running).
 - **Heart Rate**  
   Provides the number of beats per minute, also indicates if the heart rate sensor is fully locked onto the wearer’s heart rate.
 - **Pedometer**  
   Provides the total number of steps the wearer has taken.
 - **Skin Temperature**  
   Provides the current skin temperature of the wearer in degrees Celsius.
 - **UV**  
   Provides the current ultra violet radiation exposure intensity.
 - **Device Contact**  
   Provides a way to let the developer know if someone is currently wearing the device.
 - **Calories**  
   Provides the total number of calories the wearer has burned.

### Create App Tiles
Keep users engaged and extend your app experience to Microsoft Band. Create tiles for the band that send glance-able data and notifications from your app to your users.

#### Tile Notifications

Each app tile is visually represented on the Start Strip by an icon, and when a new notification arrives, the icon is scaled down and a number badge appears on the tile. App notifications come in two flavors:

 - **Dialogs**  
   Dialog notifications are popups meant to quickly display information to the user. Once the user dismisses the dialog, the information contained therein does not persist on the Band.
 - **Messages**  
   Message notifications are sent and stored in a specific tile, and a tile can keep up to 8
messages at a time. Messages may display a dialog as well.
Both notifications types contain a title text and a body text.

#### Custom Tile Pages

Custom tiles have application defined layouts and custom content, which includes multiple icons, buttons, text blocks, and barcodes. With custom tiles, developers can define unique experiences for their applications. The developers control exactly how many pages to show inside of a tile as well as the content of individual pages. 

They can update the contents of a page that has been created using custom layout at any point, unlike messaging tiles where every new message results in the creation of a new page inside the tile. In addition, a developer can choose to add additional pages inside the tile. If the total number of pages goes past the maximum pages allowed inside the tile, the right most page is dropped out when a new page is added.

#### Tile Events

It is also possible to register for tile events. This allows a developer to know when the user has entered and exited their tile. In addition, they can receive events when a user taps on a button in one of their custom tiles.

### Personalize Device
Monetize your app by offering users ways to customize the band. Change the color theme, or bring the Me Tile to life by changing the wallpaper.

[1]:http://developer.microsoftband.com/
[2]:https://raw.githubusercontent.com/mattleibow/Microsoft-Band-SDK-Bindings/master/Images/capabilities.png
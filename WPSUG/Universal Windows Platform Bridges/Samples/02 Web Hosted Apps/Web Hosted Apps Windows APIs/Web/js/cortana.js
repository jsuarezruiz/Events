(function() {

// Handle Cortana activation adding the event listener before DOM Content Loaded
// parse out the command type and call the respective game APIs

  if (typeof Windows !== 'undefined') {

    Windows.UI.WebUI.WebUIApplication.addEventListener("activated", function (args) {

      var activation = Windows.ApplicationModel.Activation;
      
      if (args.kind === activation.ActivationKind.voiceCommand) {

        var speechRecognitionResult = args.result;
        var textSpoken = speechRecognitionResult.text;

        // Determine the command type {api} defined in vcd
        if (speechRecognitionResult.rulePath[0] === "API") {

          // Determine the API searched for
          if (textSpoken.search("camera") >= 0) {
            // Search for the camera api
            console.log("Search for the camera api");
          }
          else if (textSpoken.search("contacts") >= 0) {
            // Search for the contacts api
            console.log("Search for the contacts api");
          }
          else {
            // No stream specified by user
            console.log("No valid stream specified");
          }

        }
        else {
          // No valid command specified
          console.log("No valid command specified");
        }
      }
    });
  }
	
})();
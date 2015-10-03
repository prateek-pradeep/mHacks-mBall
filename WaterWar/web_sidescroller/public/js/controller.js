$(".playerButton").click(function() {
  var currentTIme = new Date($.now());
  myFirebaseRef.push({
    player: {
      player: this.innerText,
      time: currentTIme.toString()
    }
  });

  window.location = location.origin + '/' + this.innerText[7];

});

// var Firebase = require("firebase");
var myFirebaseRef = new Firebase("https://fiery-heat-7336.firebaseio.com/");

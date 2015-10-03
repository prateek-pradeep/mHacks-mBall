$("#start").click(function() {

  // window.location.href = "/"+connectionArray.length+1;
});

// var connectionArray =[];

var axisX = 0;
var axisY = 0;

var lastX = 0;
var lastY = 0;

var xDiv = 3,
  yDiv = 3;

$( document ).ready(function() {
  $("#start").width(window.innerWidth);
	$("#start").height(window.innerHeight);
	$("#start").html("Welcome Player "+location.pathname[1]+"<br> <br>"+'Touch and Play')
	$("#start").css("font-size",25);
	$("#start").css("background-color",parseInt(location.pathname[1])%2==0?'#0078e7':'#E7D300');

});

window.onload = function() {
	
  if (window.DeviceMotionEvent == undefined) {
    console.log('not');

  } else {
    window.ondevicemotion = function(event) {

      axisX = (event.accelerationIncludingGravity.x / xDiv).toFixed(1);
      axisX = Math.min(Math.max(axisX, -1), 1);

      axisY = (event.accelerationIncludingGravity.y / yDiv).toFixed(1);
      axisY = Math.min(Math.max(axisY, -1), 1);      

      if (Math.abs(axisX - lastX) >= 0.1 ||
        Math.abs(axisY - lastY) >= 0.1) {        

        var msg = {
          "type": "move_axis",
          "move_axis": true,
          "axisX": axisX,
          "axisY": axisY,
        };

        // $("#start").text(''conn.index);

        conn.sendMessage(msg, conn.index);
      }

      lastX = axisX;
      lastY = axisY;
    }
  }
};

/**
 * Created by JSalwitz on 8/29/2015.
 */

/*
 Manages the "Spike Evader button"...
 */

// constants...
var _penaltyText = ["Get Ready!", "Wait.", "Wait..", "Wait..."];
var _finishingText = ["", "First Place!", "Second Place", "Third Place", "Fourth Place"];
var _penaltyColor = ["#800080", "#FF0000", "#FF0000", "#FF0000"];
var fps = 1;

var BUTTON_UP = 0;
var BUTTON_DOWN = 1;

function InputAxis(axis) {

  this.handleClick = function(axis) {
    var msg = {
      "type": "move_axis",
      "move_axis": axis      
    };
    conn.sendMessage(msg, 0);
  }

}

function MoveDirectionButton(element) {

  var self = this;
  this.m_element = element;
  this.m_state = BUTTON_UP;

  $(element).on("touchstart", function() {

    //console.log("[TOUCHBEGIN] Entered");
    self.m_state = BUTTON_DOWN;
    var direction = parseInt($(this).data("direction"));
    self.handleClick(direction);
  });

  $(element).on("touchend", function() {
    //console.log("[TOUCHEND] Entered");
    self.m_state = BUTTON_UP;
    var direction = parseInt($(this).data("direction"));
    self.handleClick(direction);
  });

  this.handleClick = function(direction) {
    var msg = {
      "type": "move_direction",
      "move_direction": direction,
      "button_state": self.m_state
    };
    conn.sendMessage(msg, 0);
  }
}


function RaceButton(element) {

  var self = this;
  this.m_element = element;
  this.m_isRacing = false;
  this.m_finishedPlace = 0; // if > 0 then race is finished
  this.m_penaltyCounter = 0;

  // message handlers..
  this.onStartNewGame = function() {
    self.m_penaltyCounter = 0;
    self.m_isRacing = false;
    this.m_finishedPlace = 0;
    this.redraw();
  };

  this.onStartRace = function() {
    this.m_isRacing = true;
    this.redraw();
  };

  this.onFinishedRace = function(finishingPlace) {
    this.m_isRacing = false;
    this.m_finishedPlace = finishingPlace;
    this.redraw();
  };

  this.falseStart = function() {
    var oldCounter = self.m_penaltyCounter;
    self.m_penaltyCounter = _penaltyText.length;
    if (oldCounter == 0)
      self.penaltyAnimation();
    else
      self.redraw();
  };

  $(element).on("touchend", function() {
    self.handleClick();
  });

  this.handleClick = function() {
    var msg = {
      'move_direction': "0"
    };
    conn.sendMessage(msg, 0);
  }

  this.handleClick2 = function() {
    if (this.m_finishedPlace == 0) {
      if (!this.m_isRacing)
        this.falseStart();

      if (this.m_penaltyCounter == 0) {
        var msg = {
          'type': "go_action"
        };
        conn.sendMessage(msg, 0);
      }
    }
  };

  this.penaltyAnimation = function() {

    if (self.m_penaltyCounter > 0)
      self.m_penaltyCounter -= 1;

    self.redraw();

    if (self.m_penaltyCounter > 0) {
      setTimeout(function() {
        requestAnimationFrame(self.penaltyAnimation);
      }, 1000 / fps);
    }
  };

  this.redraw = function() {
    var color = 0;
    var text = "";

    if (self.m_finishedPlace > 0) {
      color = "#00105f";
      text = "Finished... " + _finishingText[self.m_finishedPlace];
    } else
    if (self.m_penaltyCounter > 0 || !self.m_isRacing) {
      color = _penaltyColor[self.m_penaltyCounter];
      text = _penaltyText[self.m_penaltyCounter];
    } else {
      color = "#23d12f";
      text = "GO!"
    }

    $(self.m_element).css("background", color);
    $(self.m_element).children(".text").html(text);
  }
}

//# sourceURL=spikeEvader_ButtonHandler.js

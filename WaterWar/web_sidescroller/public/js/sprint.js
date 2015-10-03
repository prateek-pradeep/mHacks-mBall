$(document).ready(function() {

  console.log("Document Loaded");

  // INIT..
  conn = new Connection();
  conn.sendMessage({
    "type": "connect"
  });

  var m_move_direction_left_button = new MoveDirectionButton($("#left_button"));
  var m_move_direction_right_button = new MoveDirectionButton($("#right_button"));

 
  // set player color...
  var _playerColors = ["#000000", "#d62d20", "#a200ff", "#f47835", "#ffc425"];
  $('#player_indicator').css({
    fill: _playerColors[conn.index]
  });

  // Process incoming game messages
  $(document).on("game_message", function(e, message) {
    console.log("Received Message: " + JSON.stringify(message));
    var payload = message.payload;
    switch (payload.type) {
      case "start_new_game":
        m_raceButton.onStartNewGame();
        break;
      case "start_race":
        m_raceButton.onStartRace();
        break;
      case "finished_race":
        m_raceButton.onFinishedRace(payload.place);
        break;
    }
  });
});

mergeInto(LibraryManager.library, {

  SavePlayerProgressDataExternal: function (data) {
    var newDataString = UTF8ToString(data);
    var dataObject = JSON.parse(newDataString);
    console.log("Save Data: " + dataObject);
    player.setData(dataObject);
  },

  LoadPlayerProgressDataExternal: function () {
    player.getData().then(function (_data) {
      const myJSON = JSON.stringify(_data);
      console.log("Load Data: " + myJSON);
      unityInstance.SendMessage('JsEventsReceiver', 'SetPlayerProgressData', myJSON);
    })
  },

  SetLeaderboardValueExternal: function (value) {
    ysdk.isAvailableMethod('leaderboards.setLeaderboardScore').then(
      function (result) {
        if (result) {
          //console.log('Set Leaderboard Score: ' + value)
          leaderboards.setLeaderboardScore('Score', value);
        }
      });
  },

});
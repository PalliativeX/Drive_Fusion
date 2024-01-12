mergeInto(LibraryManager.library, {

  ShowInterstitialAdExternal: function () {
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onOpen : function () {
          //console.log('Interstitial Open');
          unityInstance.SendMessage('AdService', 'OnInterstitialAdOpened');
        },
        onClose: function (wasShown) {
          //console.log('Interstitial Closed');
          unityInstance.SendMessage('AdService', 'OnInterstitialAdClosed');
          window.focus();
        },
        onError: function (error) {
          //console.log('Interstitial Error', error);
        }
      }
    })
  },

  ShowRewardedAdExternal: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: function() {
          console.log('Rewarded Open');
          unityInstance.SendMessage('AdService', 'OnRewardedAdOpened');
        },
        onRewarded: function() {
          console.log('Rewarded OnRewarded');
          unityInstance.SendMessage('AdService', 'OnRewardedAdGetReward');
        },
        onClose: function(){
          console.log('Rewarded OnClose');
          unityInstance.SendMessage('AdService', 'OnRewardedAdClosed');
          window.focus();
        },
        onError: function(error) {
          console.log('Rewarded Error', error);
          unityInstance.SendMessage('AdService', 'OnRewardedAdFail');
        }
      }
    })
  },

});
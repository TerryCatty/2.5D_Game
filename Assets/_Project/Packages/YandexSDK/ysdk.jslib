mergeInto(LibraryManager.library, {
	ShowHintExtern: function(){
		ysdk.adv.showRewardedVideo({
			callbacks:{
				onOpen: () => {
					console.log("Video ad open");
				},
				onRewarded: () => {
					console.log('Rewarded!');
					unityInstance.SendMessage('GameManager','AdvIsShowHint');
				},
				onClose: () => {
					unityInstance.SendMessage('GameManager','AdvIsShow');
				}, 
				onError: (e) => {
					console.log('Error while open video ad:', e);
				}
			}
		})
	},
	ShowAdvExtern: function(){
		ysdk.adv.showFullscreenAdv({
			callbacks: {
				onClose: function(wasShown) {
					unityInstance.SendMessage('GameManager','AdvIsShow');
				},
				onError: function(error) {
          // some action on error
				}
			}
		})
	},
	ShowAdvExternLoadLevel: function(){
					console.log("Adv");
		ysdk.adv.showFullscreenAdv({
			callbacks: {
				onClose: function(wasShown) {
					console.log("AdvShow");
					unityInstance.SendMessage('GameManager','AdvIsShowNextLevel');
				},
				onError: function(error) {
          // some action on error
				}
			}
		})
	},
	SaveExtern: function(date){
		var dateString = UTF8ToString(date);
		var myObj = JSON.parse(dateString);
		player.setData(myObj);
	},

	

	LoadExtern: function(){
		player.getData().then(_date =>{
			const myJSON = JSON.stringify(_date);
			console.log("Load in yandex");
			unityInstance.SendMessage("GameManager", "LoadData", myJSON);
		});
	},
	/*ShowStickyBanner: function(){
		ysdk.adv.showBannerAdv();
	},
	HideStickyBanner: function(){
		ysdk.adv.hideBannerAdv();
	}*/
	CheckMobile: function(){
		let isMob = ysdk.deviceInfo.isMobile();
    	console.log(isMob);
    	if(isMob == true){
    		unityInstance.SendMessage('GameManager', 'IsMobile');
    		console.log(isMob);
    	}else{

    		unityInstance.SendMessage('GameManager', 'IsDesktop');
    		console.log(isMob);
    	}
      	
	},
	OpenLinkInSameTab: function(link)
	{
		window.location.href = link;
	},
	LoadingAPIReady: function(){
		YaGames
        .init()
        .then(ysdk => {
          
			ysdk.features.LoadingAPI.ready();
    		unityInstance.SendMessage('GameManager', 'ReadyAPI');
        });
},

GameplayAPIStart: function(){
	YaGames
        .init()
        .then(ysdk => {
          
			ysdk.features.GameplayAPI.start();
        });
},

GameplayAPIStop: function(){
	YaGames
        .init()
        .then(ysdk => {
          
			ysdk.features.GameplayAPI.stop();
        });
},
AuthUser: function(){
      auth();
    },
CheckUserAuth: function(){
	initPlayer().then(_player => {
        if (_player.getMode() === 'lite') {
        	console.log("not auth");
    		unityInstance.SendMessage('GameManager', "SendMsgAuth","true");
	
        }else{
        	console.log("auth");
        	unityInstance.SendMessage('GameManager', "SendMsgAuth","false");
        }
    });
},
    
   	


});

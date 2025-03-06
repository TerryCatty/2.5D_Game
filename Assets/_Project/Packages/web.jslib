/*mergeInto(LibraryManager.library, {
	
	SaveExtern: function(date){
		var dateString = UTF8ToString(date);
		var myObj = JSON.parse(dateString);
		gp.player.set('gamedatajson', JSON.stringify(progress));
		
	},

	

	LoadExtern: function(){
		let data = "";
		try {
		    data = gp.player.get('gamedatajson');
			unityInstance.SendMessage("GameManager", "LoadData", data);
		} catch (err) {
		    
			unityInstance.SendMessage("GameManager", "LoadData", data);
		}
	},
});
*/
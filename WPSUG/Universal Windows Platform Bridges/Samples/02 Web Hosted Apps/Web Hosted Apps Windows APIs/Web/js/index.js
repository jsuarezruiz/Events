$(document).ready(function() {
	var btnReload = document.getElementById('Reload');
	btnReload.addEventListener('click', function(){window.location.reload(true);})
	// Load Plugin Data
	$.getJSON("data/plugins.json", function(data){
		p.init(data);
	});
});
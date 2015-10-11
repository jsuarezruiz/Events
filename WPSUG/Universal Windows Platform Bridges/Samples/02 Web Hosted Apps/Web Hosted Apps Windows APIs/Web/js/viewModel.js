function apis() {
	var self = this;

	self.plugins = ko.observableArray();

	self.init = function(data) {
		for(var plugin in data.plugins) {
			self.plugins.push(data.plugins[plugin]);
		}

		pluginsList = new List('plugins', {valueNames: [ 'title', 'namespace' ]});
		var gistScript = document.createElement('script');
		gistScript.setAttribute('src', '/js/lib/gist-embed.js');
		document.body.appendChild(gistScript);
	}
}

var p = new apis();

var pluginsList;

ko.applyBindings(p);
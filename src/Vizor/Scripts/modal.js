function viToggleModal(id) {
	var elem = document.getElementById(id);

	if (document.createEvent) {
		event = document.createEvent("MouseEvents");
		event.initMouseEvent("click", true, true, elem.ownerDocument.defaultView,
			0, 0, 0, 0, 0, false, false, false, false, 0, null);
		elem.dispatchEvent(event);
	} else if (elem.fireEvent) {
		elem.fireEvent("onclick");
	}
}
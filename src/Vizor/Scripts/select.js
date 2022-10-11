function viConvertSelect(id, objRef) {
	new TomSelect(id, {
		onChange: function (value) {
			objRef.invokeMethodAsync('SelectValueCallback', value);
		}
	});
}
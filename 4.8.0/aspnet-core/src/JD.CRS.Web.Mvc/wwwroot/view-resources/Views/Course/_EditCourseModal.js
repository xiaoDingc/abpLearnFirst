﻿(function ($) {
	var _courseService = abp.services.app.course;
	var _$modal = $('#CourseEditModal');
	var _$form = $('form[name=CourseEditForm]');
	var _$status = $("#Status");

	_$modal.on('shown.bs.modal', function () {
		debugger;
		var res = _$form.find('input[type=text]:first').focus();
		console.log(_$status.val());
		alert(_$status.val(status));
		//_$status.val(status);//Status value
		
		//_$status.val()=status;
	});

	function save() {

		if (!_$form.valid()) {
			return;
		}
		var course = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js   
		abp.ui.setBusy(_$form);
		_courseService.update(course).done(function () {
			_$modal.modal('hide');
			location.reload(true); //reload page to see edited course!
		}).always(function () {
			abp.ui.clearBusy(_$modal);
		});
	}


	//Handle save button click
	_$form.closest('div.modal-content').find(".save-button").click(function (e) {
		e.preventDefault();
		save();
	});

	//Handle enter key
	_$form.find('input').on('keypress', function (e) {

		if (e.which === 13) {
			e.preventDefault();
			save();
		}

	});

	$.AdminBSB.input.activate(_$form);

})(jQuery);
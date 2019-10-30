(function () {
	$(function () {
		var _courseService = abp.services.app.course;
		var _$modal = $('#CourseCreateModal');
		var _$form = _$modal.find('form');
		var _$status = $('#Status');

		var _$keyword = $("#KeyWord");
		var _$search = $("#Search");
		var _$dataTable = $('#dataTable');

		//绑定方法 - 显示CreateModal
		$.fn.dataTable.ext.buttons.alert = {
			className: 'buttons-alert',
			action: function (e, dt, node, config) {
				_$modal.modal('show');
			}
		};



		_$dataTable.DataTable({
			aLengthMenu: [[10, 25, 50, -1], [10, 25, 50, "全部"]], //PageSize
            pagingType: "full_numbers", //Type
            scrollY: 470, //Scroll
            //dom: 'Blfrtip', //Layout
            dom: "<'row'<'col-sm-8'B><'col-sm-4'lf>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-6'i><'col-sm-6'p>>", //Layout
			//Button
			buttons:
				[
					{// 自定义按钮
						extend: 'alert',
						text: '+'
						//text: '<i class="material-icons">add</i>'
					},
					{ //复制到剪贴板
						extend: 'copy',
						text: 'Copy'
					},
					{ //导出CSV
						extend: 'csv',
						text: 'CSV',
						bom: true
					},
					{ //导出Excel
						extend: 'excel',
						text: 'Excel'
					},
					{ //导出PDF
						extend: 'pdf',
						text: 'PDF'
					},
					{ //打印
						extend: 'print',
						text: 'Print'
						//text: '<i class="material-icons">print</i>'
					}
				],
			oLanguage: { //Language
				//"sUrl": "~/lib/dataTables/Language/zh-cn.json"
				"aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "全部"]], //每页显示记录数
				"sEmptyTable": "无数据",
				"sInfo": "显示第 _START_ 至 _END_ 项记录，共 _TOTAL_ 项",
				"sInfoEmpty": "显示第 0 至 0 项记录 / 共 0 项",
				"sInfoFiltered": "(从 _MAX_ 条记录过滤)",
				"sInfoPostFix": "",
				"sThousands": ",",
				"sLengthMenu": "_MENU_ 项/页",
				"sLoadingRecords": "载入中...",
				"sProcessing": "处理中...",
				"sSearch": "过滤:",
				"sZeroRecords": "没找到匹配的记录",
				"oPaginate": {
					"sFirst": "首页",
					"sPrevious": "上页",
					"sNext": "下页",
					"sLast": "末页"
				},
				"oAria": {
					"sSortAscending": ": 升序排序",
					"sSortDescending": ": 降序排序"
				},

			}
		});
		//pdfMake.fonts = {
		//	Roboto: {
		//		normal: 'Roboto-Regular.ttf',
		//		bold: 'Roboto-Medium.ttf',
		//		italics: 'Roboto-Italic.ttf',
		//		bolditalics: 'Roboto-Italic.ttf'
		//	},
		//	微软雅黑: {
		//		normal: 'msyh.ttf',
		//		bold: 'msyh.ttf',
		//		italics: 'msyh.ttf',
		//		bolditalics: 'msyh.ttf',
		//	}
		//};

		_$search.click(function () {
			location.href = '/Course?status=' + _$status.val() + '&keyword=' + _$keyword.val();
		});



		_$form.validate({
		});

		$('#RefreshButton').click(function () {
			refreshCourseList();
		});

		$('.delete-course').click(function () {
			var courseId = $(this).attr("data-course-id");
			var courseName = $(this).attr('data-course-name');
			deleteCourse(courseId, courseName);
		});


		_$status.change(function () {
			location.href = '/Course?status=' + _$status.val();
		});

		$('.edit-course').click(function (e) {
			var courseId = $(this).attr("data-course-id");

			e.preventDefault();
			$.ajax({
				url: abp.appPath + 'Course/EditCourseModal?courseId=' + courseId,

				type: 'POST',
				contentType: 'application/html',
				success: function (content) {
					$('#CourseEditModal div.modal-content').html(content);
				},

				error: function (e) { }
			});
		});

		_$form.find('button[type="submit"]').click(function (e) {
			e.preventDefault();

			if (!_$form.valid()) {
				return;
			}

			var course = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js         

			abp.ui.setBusy(_$modal);
			_courseService.create(course).done(function () {
				_$modal.modal('hide');
				location.reload(true); //reload page to see new user!

			}).always(function () {
				abp.ui.clearBusy(_$modal);
			});
		});


		_$modal.on('shown.bs.modal', function () {
			_$modal.find('input:not([type=hidden]):first').focus();

		});


		function refreshCourseList() {
			location.reload(true); //reload page to see new user!

		}

		function deleteCourse(courseId, courseName) {
			abp.message.confirm(
				abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'CRS'), courseName),

				function (isConfirmed) {
					if (isConfirmed) {
						_courseService.delete({
							id: courseId

						}).done(function () {
							refreshCourseList();

						});
					}
				}
			);
		}
	});
})();
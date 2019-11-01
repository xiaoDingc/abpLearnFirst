(function() {
    $(function() {

        var _EquipmentTypeService = abp.services.app.equipmentType;
        var _$modal = $('#EquipmentTypeCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshEquipmentTypeList();
        });

        $('.delete-EquipmentType').click(function () {
						debugger ;
            var EquipmentTypeId = $(this).attr("data-EquipmentType-id");
            var EquipmentTypeName = $(this).attr('data-EquipmentType-name');

            deleteEquipmentType(EquipmentTypeId, EquipmentTypeName);
        });

        $('.edit-EquipmentType').click(function (e) {
            var EquipmentTypeId = $(this).attr("data-EquipmentType-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'EquipmentType/EditEquipmentTypeModal?EquipmentTypeId=' + EquipmentTypeId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#EditEquipmentTypeModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var EquipmentType = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            //EquipmentType.roleNames = [];
            //var _$roleCheckboxes = $("input[name='role']:checked");
            //if (_$roleCheckboxes) {
            //    for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
            //        var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
            //        EquipmentType.roleNames.push(_$roleCheckbox.val());
            //    }
            //}

            abp.ui.setBusy(_$modal);
            _EquipmentTypeService.create(EquipmentType).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new EquipmentType!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshEquipmentTypeList() {
            location.reload(true); //reload page to see new EquipmentType!
        }

        function deleteEquipmentType(EquipmentTypeId, EquipmentTypeName) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'CRS'), EquipmentTypeName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _EquipmentTypeService.delete({
                            id: EquipmentTypeId
                        }).done(function () {
                            refreshEquipmentTypeList();
                        });
                    }
                }
            );
        }
    });
})();

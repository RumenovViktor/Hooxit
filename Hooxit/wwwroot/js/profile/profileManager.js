// TODO: Rewrite the whole damn thing.

(function () {
    var profileManager = function () {
        var hacks = {
            blurTriggeredCount: 0
        }

        function toggleUpdateField() {
            $('.toggle-update').on('click', function () {
                hacks.blurTriggeredCount = 0;
                var parent = $(this).parent().parent();
                parent.find('.toggle-update-display-value').hide();

                parent.find('.toggle-update-edit').show();
                parent.find('input').focus();
                parent.find('.toggle-update-edit').parent().addClass('position-edit-field');

                parent.find('.hideOnBlur').filter(':first').on('blur', function (e) {
                    hacks.blurTriggeredCount++;

                    var $self = $(this);
                    var parent = $self.parent().hide();
                    var displayValue = parent.parent().find('.toggle-update-display-value');
                    displayValue.show();

                    parent.parent().find('.toggle-update-edit').hide();
                    var updatedValueContainer = $self.parent().parent().find('.toggle-update-edit');
                    updatedValueContainer.parent().removeClass('position-edit-field');
                    var updatedUserInfoType = parent.parent().find('.positionType');

                    var antiforgeryToken = $('#userInfo_antiforgery_token').val();
                    var data = updatedValueContainer.find('.userInfoForUpdate').val();

                    if (displayValue[0].innerText === data || hacks.blurTriggeredCount > 1)
                        return;

                    switch (updatedUserInfoType[0].innerText) {
                        case 'position':
                            var url = urls.buildChangePositionUrl();
                            ajaxRequest.sendAjax(url, { 'Position': data }, 'POST', 'application/json', { 'RequestVerificationToken': antiforgeryToken },
                                function (response) {
                                    // show message
                                    toastr.success('Position was updated successfuly!');
                                    displayValue[0].innerText = response.position;
                                }, function (error) {
                                    toastr.error('An error accured, please try again later!');
                                    updatedValueContainer.find('.userInfoForUpdate').val(displayValue.val());
                                });
                            break;
                        case 'country':
                            var url = urls.buildChangeCountryUrl();
                            // ajaxRequest.sendAjax(url, { 'Country': data }, 'POST', 'application/json', { 'RequestVerificationToken': antiforgeryToken }, null, null);
                            break;
                        case 'email':
                            var url = urls.buildChangeEmailUrl();
                            ajaxRequest.sendAjax(url, { 'Email': data }, 'POST', 'application/json', { 'RequestVerificationToken': antiforgeryToken },
                                function (response) {
                                    toastr.success('Email was updated successfuly!');
                                    displayValue[0].innerText = response.email;
                                }, function (error) {
                                    toastr.error('An error accured, please try again later!');
                                    updatedValueContainer.find('.userInfoForUpdate').val(displayValue.val());
                                });
                            break;
                        default:
                            break;
                    }
                });
            });
        }

        function initFormToggleEvents() {
            $('.addExperience').on('click', function () {
                toggleForm($(this));
            });
        }

        function toggleForm(form) {
            $('#addExperienceForm').toggle();
        }

        function appendNewExperience(data) {
            if (!data)
                return;

            $('#experienceContainer').append(data);
        }

        return {
            toggleUpdateField: toggleUpdateField,
            initFormToggleEvents: initFormToggleEvents,
            appendNewExperience: appendNewExperience
        };
    }

    var manager = profileManager();

    manager.toggleUpdateField();
    manager.initFormToggleEvents();
    manager.appendNewExperience();
})();
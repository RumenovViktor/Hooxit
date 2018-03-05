var userInfoManager = (function () {
    var _requestTypes = {
        position: 'position',
        email: 'email',
        country: 'country' 
    };

    var _properties = {
        events: {
            click: 'click',
            blur: 'blur',
            mouseDown: 'mousedown'
        },
        attributes: {
            disabled: 'disabled'
        },
        dataTypes: {
            applicationJson: 'application/json'
        },
        penClass: '.toggle-update',
        editFieldWrapperClass: '.userInfoEditFieldWrapper',
        displayFieldWrapperClass: '.userInfodisplayValue',
        userInfoForUpdateClass: '.userInfoForUpdate',
        requestTypeClass: '.requestType',
        cardTextClass: '.card-text',
        countryListClass: '.countryList',
        listGroupItem: '.list-group-item',
        activeClassNoDot: 'active',
        activeClassWithDot: '.active'
    };

    var toggleField = function (wrapper) { wrapper.toggle(); }

    var sendAjax = function (url, displayField, postData, successMessage, failureMessage) {
        ajaxRequest.sendAjax(url, postData, 'POST', _properties.dataTypes.applicationJson, null,
            function (response) {
                toastr.success(successMessage);
                displayField.innerText = response.value;

            }, function (error) {
                toastr.error(failureMessage);
            });
    }

    var executeRequest = function (type, updatedFieldValue, displayField) {
        switch (type) {
            case _requestTypes.position:
                var postData = { 'Position': updatedFieldValue };
                sendAjax(urls.buildChangePositionUrl(), displayField, postData, 'Position was successfuly updated!', 'Something went wrong! Failed updating your position.');
                break;
            case _requestTypes.email:
                var postData = { 'Email': updatedFieldValue };
                sendAjax(urls.buildChangeEmailUrl(), displayField, postData, 'Email was successfuly updated!', 'Something went wrong! Failed updating your Email.')
                break;
            case _requestTypes.country:
                var selectedCountry = $(_properties.countryListClass + ' ' + _properties.listGroupItem + _properties.activeClassWithDot);
                var postData = { 'CountryId': selectedCountry.attr('data-value') };

                ajaxRequest.sendAjax(urls.buildChangeCountryUrl(), postData, 'POST', _properties.dataTypes.applicationJson, null,
                    function (response) {
                        toastr.success('Country was successfuly updated!');
                        displayField.innerText = selectedCountry[0].innerText;

                    }, function (error) {
                        toastr.error('Something went wrong! Failed updating the country.');
                    });
                break;
            default:
                throw new Error('Request type is not supported!');
                break;
        }
    }

    var preventDefaultsForCountry = function () {
        $(_properties.countryListClass + ' ' + _properties.listGroupItem).on(_properties.events.mouseDown, function (e) {
            e.preventDefault();

            $(_properties.countryListClass + ' ' + _properties.listGroupItem).removeClass(_properties.activeClassNoDot);
            $(this).addClass(_properties.activeClassNoDot);
            $(this).parentsUntil(_properties.cardTextClass).find(_properties.userInfoForUpdateClass).val($(this).text().trim());
        });
    }

    this.init = function () {
        preventDefaultsForCountry();
        toggleUpdateField();
    }

    var updateField = function () {
        this.userInfoForUpdate.find(_properties.userInfoForUpdateClass)
            .off(_properties.events.blur);

        $(_properties.displayFieldWrapperClass).show();
        $(_properties.editFieldWrapperClass).hide();

        if (this.displayFieldValue === this.updatedFieldValue)
        {
            return;
        }

        executeRequest(this.requestType, this.updatedFieldValue, this.displayField);
    }

    this.toggleUpdateField = function () {
        $(_properties.penClass).on(_properties.events.click, function () {
            var wrapper = $(this).parentsUntil(_properties.cardTextClass);
            var displayFieldWrapper = wrapper.find(_properties.displayFieldWrapperClass);

            displayFieldWrapper.hide();

            var editFieldWrapper = wrapper.find(_properties.editFieldWrapperClass);
            editFieldWrapper.show();

            editFieldWrapper.find(_properties.userInfoForUpdateClass).focus();

            var userInfoForUpdate = wrapper.find(_properties.editFieldWrapperClass);
            var requestType = wrapper.find(_properties.requestTypeClass)[0].innerHTML;
            
            userInfoForUpdate.find(_properties.userInfoForUpdateClass)
                .on(_properties.events.blur, function () {
                    updateField.call({
                        userInfoForUpdate: userInfoForUpdate,
                        displayField: displayFieldWrapper[0],
                        displayFieldValue: displayFieldWrapper[0].innerText.trim(),
                        updatedFieldValue: editFieldWrapper.find(_properties.userInfoForUpdateClass).val(),
                        requestType: requestType
                    });
                });
        });
    }

    return this;
})();

$(document).ready(function () {
    userInfoManager.init();
});
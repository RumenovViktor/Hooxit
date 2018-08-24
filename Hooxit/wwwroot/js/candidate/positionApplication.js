class PositionApplication {
    constructor() {
        this.registerEventListeners();
    }

    onApplySuccess() {
        $('#applyBtn').addClass('disabled');
        toastr.success("Successfuly applied for the position");
    }

    onApplyError() {
        toastr.error("Could not apply for the position. Please contact support!");
    }

    registerEventListeners() {
        $('#applyBtn').on('click', (event) => {
            event.preventDefault();

            const positionId = $('#positionId').val();
            const data = {
                'PositionId': positionId
            };
            
            ajaxRequest.sendAjax('/Candidate/Profile/Apply/', data, 'POST', 'application/json', null, this.onApplySuccess.bind(this), this.onApplyError.bind(this));
        });
    }
}

new PositionApplication();
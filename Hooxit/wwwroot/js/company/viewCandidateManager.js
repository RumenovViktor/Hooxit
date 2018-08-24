class ViewCandidateManager {
    constructor() {
        this.init();
    }

    onShowInterestSuccess() {
        $('.unshowInterest').show();
        $('.showInterest').hide();
    }

    onRemoveInterestSuccess() {
        $('.unshowInterest').hide();
        $('.showInterest').show();
    }

    init() {
        const interestedEnabled = $('#interested').val() === "True" ? true : false;

        if (interestedEnabled) {
            $('.unshowInterest').show();
            $('.showInterest').hide();
        } else {
            $('.unshowInterest').hide();
            $('.showInterest').show();
        }
    }
}

window.viewCandidateManager = new ViewCandidateManager();
mergeInto(LibraryManager.library, {

    OpenRateWindowExternal: function () {
        ysdk.feedback.canReview()
            .then(function (value, reason) {
                if (value) {
                    ysdk.feedback.requestReview()
                        .then(function (feedbackSent) {
                            if (feedbackSent) { // BUG: THIS IS ALWAYS TRUE 
                                window.focus();
                                //console.log('Feedback sent successfully !');
                            }
                        })
                } else {
                    //console.log(reason)
                }
            })
    },

    CanReviewExternal: function () {
        CanReviewHTML();
    },

});
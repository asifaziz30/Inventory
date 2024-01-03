


var accountDetails = (function () {


    function markApproved(tab) {
        switch (tab.toLowerCase()) {
            case 'applicationform':
                rivets.bind($('#nav-application-tab .approvals-tick ,#nav-application .approvals-tick'), { isFormApproved: true });
                break;
            case 'documents':
                rivets.bind($('#nav-documents-tab .approvals-tick ,#nav-documents .approvals-tick'), { isDocsApproved: true });
                break;
            case 'signature':
                rivets.bind($('#nav-signature-tab .approvals-tick , #nav-signature .approvals-tick'), { isSignatureApproved: true });
                break;
            case 'profilepicture':
                rivets.bind($('#nav-profile-tab .approvals-tick ,#nav-profile .approvals-tick'), { isProfilePicApproved: true });
                break;
            case 'screenshot':
                rivets.bind($('#nav-screenshot-tab .approvals-tick ,#nav-screenshot .approvals-tick'), { isScreenshotApproved: true });
                break;
            case 'location':
                rivets.bind($('#nav-location-tab .approvals-tick ,#nav-location  .approvals-tick'), { isLocationApproved: true });
                break;

        }
    }
    return {
        bindDocuments: function (url, type) {

           showLoader();
            $.get(url).done(function (data) {
                var documents = data.map(function (d) {
                    var date = new Date();
                    d.filePath = d.filePath.replace('~', app.imageHost);// + '?_ =' + date.getTime();
                    return d;
                });

                switch (type) {
                    case 'documents':
                        var otherDocuments = documents.filter(function (d) {
                            return (d.documentTypeId != 7 && d.documentTypeId != 8 && d.documentTypeId != 9);
                        });

                        if (otherDocuments.length > 0) {
                            $('#documentSection').show();
                            rivets.bind($('#documentSection'), { document: { items: otherDocuments } });
                        }
                       
                        break;
                    case 'signature':
                        var signature = documents.filter(function (d) {
                            return d.documentTypeId == 7;
                        });

                        if (signature.length > 0) {
                            rivets.bind($('#signatureSection'), { document: { items: signature } });
                            $('#signatureSection').show();
                        }
                       
                        break;
                    case 'profile':
                        var profilePic = documents.filter(function (d) {
                            return d.documentTypeId == 8;
                        });

                        if (profilePic.length > 0) {
                           rivets.bind($('#profileSection'), { document: { items: profilePic } });
                            $('#profileSection').show();
                        }
                                               
                        break;
                    case 'screenshot':
                        var screenshot = documents.filter(function (d) {
                            return d.documentTypeId == 9;
                        });

                        if (screenshot.length > 0) {
                            rivets.bind($('#screenshotSection'), { document: { items: screenshot } });
                            $('#screenshotSection').show();
                        }
                       
                        break;

                }


            }).fail(function (error) {

                console.log(error);
            })
                .always(function () {
                   hideLoader();
                });

        },
        bindLocation: function (url) {
           showLoader();
            var gMap = 'https://maps.google.com/maps?q=';
            $.get(url).done(function (data) {
                if (data) {
                    var locationPath = gMap + data.latitude + ',' + data.longitude + '&output=svembed';
                    rivets.bind($('#locationSection'), { locationPath });
                }

            }).fail(function (error) {

                console.log(error);
            }).always(function () {
               hideLoader();
            });
        },
        bindApplication: function (url) {
           showLoader();
            $.get(url).done(function (data) {
                if (data) {

                    rivets.bind($('#applicationSection'), { appInfo: data });
                    $('.form-floating-label .form-control').each(function () {
                        if ($(this).val() !== '') {
                            $(this).addClass('filled');
                        } else {
                            $(this).removeClass('filled');
                        }
                    });

                    rivets.bind($('.approvals-tick'), data.userDetails);
                    $('#applicationSection').show();
                }

            }).fail(function (error) {

                console.log(error);
            }).always(function () {
                hideLoader();
            });
        },
        approveTab: function (url, data) {
            showLoader();

            $.post(url, data).done(function () {
                markApproved(data.tab);

            }).fail(function (error) {
                console.log(error);
            }).always(function () {
                hideLoader();
            });
        }
    };
})();
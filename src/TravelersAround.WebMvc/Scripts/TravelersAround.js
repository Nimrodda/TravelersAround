//TravelersAround.com
//2011
//Nimrod Dayan

var tickModel;
var traveler = { "TravelerID": "", "Firstname": "", "Lastname": "", "Birthdate": "", "StatusMessage": "", "Gender": "", "": false, "Message": null, "Success": false, "Page": 0 };

/* Ticker functions
------------------------*/

function tick() {
    var data;
    $.post("ajax/tick", null, function (data) {
        tickCallback(data);
    });
    
}

function tickCallback(callbackModel) {
    tickModel = callbackModel;
    if (tickModel.Success) {
        newMessagesEvent(tickModel.NewMessagesCount);
    }
    else {
    
    }
}

function newMessagesEvent(msgCount) {
    if (msgCount > 0) {
        $('#newMessagesCounter').html(msgCount);
    }
    else {
        $('#newMessagesCounter').html('');
    }
}

function ticker() {
    //60 second pulse to keep user marked online
    var t = setInterval("tick()", 60 * 1000);
}

/* Ajax submit handlers
-----------------------*/
function printAsyncResponse(message, container) {
    $(container).find('div.submit-button')
                .append('<span>' + message.ResponseMessage + '</span>')
                .find('span')
                .fadeOut(2000, function () {
                    $(this).remove();
                });
                

}

function statusMessageAsyncSubmit(formData) {
    var data;
    traveler.StatusMessage = $(formData.StatusMessage).val();
    $.post('/profile/edit?async=true', traveler, function (data) {
        printAsyncResponse(data, $('#status-message-container'));
    });
    return false;
}


/* initializers
------------------*/

function getTravelerInfo() {
    var data;
    $.get("/profile/edit?async=true", null, function (data) {
        traveler = data;
    });
}

$(document).ready(function () {
    ticker();
    getTravelerInfo();
});  
    
    



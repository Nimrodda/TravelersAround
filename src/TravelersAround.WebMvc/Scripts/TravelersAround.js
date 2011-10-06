//TravelersAround.com
//2011
//Nimrod Dayan

var tickModel;
var traveler = { "TravelerID": "", "Firstname": "", "Lastname": "", "Birthdate": "", "StatusMessage": "", "Gender": "", "": false, "Message": null, "Success": false, "Page": 0 };

/* Ticker functions
------------------------*/

function tick() {
    var data;
    $.post("/ajax/tick", null, function (data) {
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
    $(container).find('.submit-button').parent()
                .append('<span>' + message.ResponseMessage + '</span>')
                .find('span')
                .fadeOut(2000, function () {
                    $(this).remove();
                });
                

}

function statusMessageAsyncSubmit(formData) {
    var data;
    traveler.StatusMessage = $(formData.StatusMessage).val();
    $.post('/profile/edit', $.postify(traveler), function (data) {
        printAsyncResponse(data, $('#status-message-container'));
    });
    return false;
}

function getTravelerInfoCallback() {
    updateStatusMessage();
    

}

/* UI manipulators
---------------------*/

function updateStatusMessage() {
    $('#status-message-container textarea').html(traveler.StatusMessage);
}



/* initializers
------------------*/

function getTravelerInfo() {
    var data;
    $.get("/profile/edit", null, function (data) {
        traveler = data;
        getTravelerInfoCallback();
    });
}

//page init
$(document).ready(function () {
    ticker();
    getTravelerInfo();
    
});  
    
    



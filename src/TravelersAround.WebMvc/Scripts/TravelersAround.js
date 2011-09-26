//TravelersAround.com
//2011
//Nimrod Dayan

var tickModel;

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

$(document).ready(function () {
    ticker();
});  
    
    



//TravelersAround.com
//2011
//

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

    }
    else {
    
    }
}

function ticker() {
    var t = setInterval("tick()", 10 * 1000);
}

$(document).ready(function () {
    ticker();
});  
    
    



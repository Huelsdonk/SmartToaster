
// variables

const tempDisplay = $('#tempLabel');
const tempSetting = $("#customRange1");
const timeDisplay = $('#timeLabel');
const timeSetting = $("#customRange2")
const onButton = $("#customSwitch1");
const onButtonIcon = $(".onButtonIcon");
let timeoutID;

// handler for temp display/put request

tempSetting.on('change', function () {
    tempDisplay.text("Heat setting " + tempSetting.val());
    const dbId = $(this).attr('data');
    updateToaster(dbId);
});

// time display handler/put request

timeSetting.on('change', function () {
    event.preventDefault();
    let currTimeSetting = $(this).val();
    const dbId = $(this).attr('data');
    timeDisplay.text(timeSetting.val() + ' seconds');

    updateToaster(dbId);

});

//handler for on/off button, icon/put request

onButton.on('change', function () {
    clearTimeout(timeoutID);
    const dbId = $(this).attr('data');
    onButtonIcon.toggle();
    updateToaster(dbId);
    
    function startTimer() {
        timeoutID = setTimeout(() => {
            onButtonIcon.toggle();
            onButton.prop("checked", false);
            updateToaster(dbId);
        }, timeSetting.val() * 1000);
    }

    //function stopTimer() {

    //    clearTimeout(timeoutID);
    //    console.log("stopped")

    //}


    if (onButton.prop("checked")) {
        startTimer();

    }
    
});

// get request from api, populate frontend

function getData() {
   

    $.getJSON("/api/toasters", function (response) {
        tempDisplay.text("Heat setting " + response[0].Heat);
        tempSetting.attr("data", response[0].Id);
        tempSetting.val(response[0].Heat);
        timeDisplay.text(response[0].Time + ' seconds');
        timeSetting.attr("data", response[0].Id);
        timeSetting.val(response[0].Time);
        onButton.attr("data", response[0].Id);
        console.log(response)
    }

    )
}

//ajax put call

function updateToaster(dbId) {

    const updateTimeObj = {

        On: onButton.is(":checked"),
        Heat: parseInt(tempSetting.val()),
        Time: parseInt(timeSetting.val())

    }
    $.ajax(`/api/toasters/${dbId}`, {
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(updateTimeObj)
    });
}

getData();
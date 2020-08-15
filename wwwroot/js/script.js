$(document).ready(function () {

    const tempDisplay = $('#tempLabel');
    const tempSetting = $("#customRange1")
    const timeDisplay = $('#timeLabel');
    const timeSetting = $("#customRange2")
    const onButton = $("#customSwitch1");
    const onButtonIcon = $(".onButtonIcon");

    tempDisplay.text("Heat setting " + tempSetting.val());

    tempSetting.on('change', function () {
        console.log($(this).val());
        tempDisplay.text("Heat setting " + tempSetting.val());

    });


    timeDisplay.text(timeSetting.val() + ' seconds');

    timeSetting.on('change', function () {
        console.log($(this).val());
        timeDisplay.text(timeSetting.val() + ' seconds');

    });

    onButton.on('change', function () {
        onButtonIcon.toggle();
    })

});


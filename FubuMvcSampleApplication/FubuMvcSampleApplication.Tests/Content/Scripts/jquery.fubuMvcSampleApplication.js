$(document).ready(function() {
    $("#Save").click(SaveUser);
    $("#DisplayUsingJson").click(Display);
});

function SaveUser() {
    var user = {};
    user.UserId = $("#UserId").val();
    user.LastName = $("#LastName").val();
    user.FirstName = $("#FirstName").val();
    user.DateOfBirth = $("#DateOfBirth").val();
    
    $.getJSON("Save", user, function(user) { $("#message").html(user.Message); });
};

function Display() {
    var user = {};
    user.UserId = $("#UserId").val();
    
    $.getJSON("Display.json", user, function(user) {    
        $("#LastName").val(user.LastName);
        $("#FirstName").val(user.FirstName);
        $("#message").html(user.Message); 
    });
};       
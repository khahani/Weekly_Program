// JavaScript Document

$(document).ready(function(){
    $("#LiMenuTeacher").click(function(){
        $("#DivContentTeacher").slideToggle("slow");
		$("#LiMenuTeacher").css({"background-color" : "#4E4D4D" , "color" : "#FFFFFF"});
		$("#DivContentTimes").hide("");
		$("#LiMenuTeacher").css({"background-color" : "#EDEDED" , "color" : "#000000"});
    });
    $("#LiMenuTime").click(function(){
        $("#DivContentTimes").slideToggle("slow");
		$("#LiMenuTime").css({"background-color" : "#4E4D4D" , "color" : "#FFFFFF"});
		$("#DivContentTeacher").hide("");
		$("#LiMenuTeacher").css({"background-color" : "#EDEDED" , "color" : "#000000"});
    });
});

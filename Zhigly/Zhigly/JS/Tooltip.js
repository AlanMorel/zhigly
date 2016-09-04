/*
 * Image preview script 
 * powered by jQuery (http://www.jquery.com)
 * 
 * written by Alen Grakalic (http://cssglobe.com)
 * 
 * bug fix and modifications by Alan Morel (http://alanmorel.com)
 *
 * for more info visit http://cssglobe.com/post/1695/easiest-tooltip-and-image-preview-using-jquery
 *
 */

this.preview = function () {

    var xOffset = 35;
    var yOffset = 25;

    $("a.preview").hover(function (e) {
        this.t = this.title;
        this.title = "";
        var c = (this.t !== "") ? "<br/>" + this.t : "";
        $("body").append("<p id='preview'><img src='" + this.href + "' class='preview-image' />" + c + "</p>");
        $("#preview")
			.css("top", (e.pageY - yOffset) + "px")
			.css("left", (e.pageX + xOffset) + "px")
			.fadeIn("fast");
    },
	function () {
	    this.title = this.t;
	    $("#preview").remove();
	});

    $("a.preview").mousemove(function (e) {
        $("#preview")
			.css("top", (e.pageY - yOffset) + "px")
			.css("left", (e.pageX + xOffset) + "px");
    });

    $("a.preview").click(function () { return false; });
};

$(document).ready(function () {
    preview();
});
/*
* Copyright 2011 Sean McCleary
* 
* This file is part of capt.
*
* capt is free software: you can redistribute it and/or modify
* it under the terms of the GNU Affero General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
* 
* capt is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License
* along with capt.  If not, see <http://www.gnu.org/licenses/>.
*/
var Capt = {};

Capt.Hints = [];

/*
 * This is called when a field, with a default, pre-filled hint, is focused.
 * It'll get that hint out of there so's it don't get in the user's way.
 */ 
Capt.FocusFieldWithDefaultHint = function (el, hintText) {
	if (el.value == hintText) {
		$(el).removeClass('prefilled_hint');
		el.value = '';
	}
};

/*
 * This is called when a user has put focus on a field with a hint,
 * but then left without typing anything there.  It'll put the hint back.
 */
Capt.BlurFieldWithDefaulthint = function (el, hintText) {
	if (jQuery.trim(el.value) == '') {
		$(el).addClass('prefilled_hint');
		el.value = hintText;
	}
};

/*
 * This is called when the user clicks into the "Your Caption" textarea, 
 * and the rest of the caption-related controls should be displayed.
 */
Capt.ExpandCaptionControls = function () {
	$("#CaptionText").animate({ height: '150px' });
	$("#secondary_caption_controls_container").slideDown();
};

/*
 * This updates the 140-character-limit counter.
 */
Capt.UpdateCounter = function () {

	var $textarea = $(this);

	if ($textarea.val().length > 140) {
		$textarea.val($textarea.val().substr(0, 140));
	}

	$("#" + $textarea.attr('id') + "_counter").html(140 - this.value.length);
};

/*
 * This toggles the arrows when a user votes something up or down.
 */
Capt.ToggleArrowHighlights = function (clicked_arrow, other_arrow) {

	if (!$(other_arrow).hasClass('highlighted')) {
		$(clicked_arrow).addClass('highlighted');
	}
	$(other_arrow).removeClass('highlighted');
};

/*
 * This is called when the user has submitted his AJAX comment.
 */
Capt.CommentPosted = function (containerName) {

	$('#' + containerName).find('.comment_save_button').removeAttr('disabled');
	$('#' + containerName).find('textarea').removeAttr('disabled').val('');
};

/*
* This is called when there was an error saving the comment
*/
Capt.CommentNotPosted = function (containerName) {

	$('#' + containerName).find('.comment_save_button').removeAttr('disabled');
	$('#' + containerName).find('textarea').removeAttr('disabled');
	alert("Oh, uh, there was a problem saving your comment.  Sorry about that.");
};

Capt.CommentPostBegin = function (containerName) {
	$('#' + containerName).find('.comment_save_button').attr('disabled', 'disabled');
	$('#' + containerName).find('textarea').attr('disabled', 'disabled');
}

/*
* Stuff to do when the DOM is ready.
*/
$(document).ready(function () {

	// Re-size the caption table cell to better fit the image
	$("#main_image").load(function () {

		var imgWidth = $(this).width();
		var imgHeight = $(this).height() + 40;
		$("#picture_captions_image_cell")
			.css("width", imgWidth + "px")
			.css("height", imgHeight + "px");


	});


	// Get the pre-filled hints & set the events on fields with pre-filled hints
	$(".prefilled_hint")
		.each(function () {
			Capt.Hints[this.id] = this.value;
		})
		.focus(function () {
			Capt.FocusFieldWithDefaultHint(this, Capt.Hints[this.id]);
		})
		.blur(function () {
			Capt.BlurFieldWithDefaulthint(this, Capt.Hints[this.id]);
		});

	// Set up the event to send them to the login page if they try to
	// enter a caption or vote on a caption without being logged in.
	$(".requires_login").click(function () {
		location.href = '/Account/LogOn?ReturnUrl=' + location.href;
		return false;
	});

	// Set it up so the other controls are visible when clicking in the caption
	$("#CaptionText:not(.requires_login)")
		.focus(Capt.ExpandCaptionControls)
		.keyup(Capt.UpdateCounter)
		.click(Capt.UpdateCounter)
		.change(Capt.UpdateCounter);

	$(".caption_comment_text")
		.keyup(Capt.UpdateCounter)
		.click(Capt.UpdateCounter)
		.change(Capt.UpdateCounter);


	// Disable the "name" box when the user clicks the "Anonymous" checkbox
	$("#IsAnonymous").click(function () {

		var user_name = $("#UserName");

		if (this.checked) {
			Capt.oldNameValue = user_name.val();
			user_name.val('');
			user_name.attr('disabled', 'disabled');
		}
		else {
			if (Capt.oldNameValue) {
				user_name.val(Capt.oldNameValue);
			}
			user_name.removeAttr('disabled');
		}

	});

	$(".below_threshold").click(function () {

		alert("You can't vote until you get your score back up above zero!\nWrite some captions! (And make 'em good!)");
		return false;

	});

	// Wire up the voting arrows.  Set it up so that if: 
	// A: One is highlighted and the other is clicked, they both go grey, or
	// B: If neither is highlited and one is clicked, it becomes highlighted
	$(".vote_control_container").each(function () {
		var up_arrow = $(this).find(".up_arrow").not(".below_threshold")[0];
		var down_arrow = $(this).find(".down_arrow").not(".below_threshold")[0];

		$(up_arrow).click(function () {
			Capt.ToggleArrowHighlights(up_arrow, down_arrow);
		});

		$(down_arrow).click(function () {
			Capt.ToggleArrowHighlights(down_arrow, up_arrow);
		});

	});


});



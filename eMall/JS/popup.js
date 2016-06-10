// Checks if the browsers is IE or another.
// document.all will return true or false depending if its IE
// If its not IE then it adds the mouse event
if (!document.all)
    document.captureEvents(Event.MOUSEMOVE)

// On the move of the mouse, it will call the function getPosition
document.onmousemove = getPosition;

// These varibles will be used to store the position of the mouse
var X = 0
var Y = 0

// This is the function that will set the position in the above varibles 
function getPosition(args) {
    // Gets IE browser position
    if (document.all) {
        X = event.clientX + document.body.scrollLeft
        Y = event.clientY + document.body.scrollTop
    }

    // Gets position for other browsers
    else {
        X = args.pageX
        Y = args.pageY
    }
}
function backgroundFilter() {
    var div;
    if (document.getElementById)
    // Standard way to get element
        div = document.getElementById('backgroundFilter');
    else if (document.all)
    // Get the element in old IE's 
        div = document.all['backgroundFilter'];

    // if the style.display value is blank we try to check it out here 
    if (div.style.display == '' && div.offsetWidth != undefined && div.offsetHeight != undefined) {
        div.style.display = (div.offsetWidth != 0 && div.offsetHeight != 0) ? 'block' : 'none';
    }
    // If the background is hidden ('none') then it will display it ('block').
    // If the background is displayed ('block') then it will hide it ('none').
    div.style.display = (div.style.display == '' || div.style.display == 'block') ? 'none' : 'block';    
}

function popUp() {
    var div;
    if (document.getElementById)
    // Standard way to get element
        div = document.getElementById('popupWindow');
    else if (document.all)
    // Get the element in old IE's
        div = document.all['popupWindow'];

    // if the style.display value is blank we try to check it out here 
    if (div.style.display == '' && div.offsetWidth != undefined && div.offsetHeight != undefined) {
        div.style.display = (div.offsetWidth != 0 && elem.offsetHeight != 0) ? 'block' : 'none';
    }
    // If the PopUp is hidden ('none') then it will display it ('block').
    // If the PopUp is displayed ('block') then it will hide it ('none').
    div.style.display = (div.style.display == '' || div.style.display == 'block') ? 'none' : 'block';

    // Off-sets the X position by 15px
    X = X + 15;

    // Sets the position of the DIV
    div.style.left = X + 'px';
    div.style.top = Y + 'px';
}

function positionPopUp() {
    var div;
    if (document.getElementById)
    // Standard way to get element
        div = document.getElementById('popupWindow');
    else if (document.all)
    // Get the element in old IE's
        div = document.all['popupWindow'];
    // Off-sets the X position by 15px
    X = X + 15;

    div.style.display = 'block';
    // Sets the position of the DIV
    div.style.left = X + 'px';
    div.style.top = Y + 'px';
    var hdPopUp = document.getElementById('ctl00_ContentPlaceHolder1_hdPopUp');
    hdPopUp.value = X + ',' + Y;
}

function setPositionPopUp() {
    var hdPopUp;
    if (document.getElementById)
    // Standard way to get element
        hdPopUp = document.getElementById('ctl00_ContentPlaceHolder1_hdPopUp');
    else if (document.all)
    // Get the element in old IE's
        hdPopUp = document.all['ctl00_ContentPlaceHolder1_hdPopUp'];

    var hdValue = hdPopUp.value;
    var pos = hdValue.split(",");
    var Xpos = pos[0];
    var Ypos = pos[1];
    var div;

    if (document.getElementById)
    // Standard way to get element
        div = document.getElementById('popupWindow');
    else if (document.all)
    // Get the element in old IE's
        div = document.all['popupWindow'];

    div.style.display = 'block';
    // Sets the position of the DIV
    div.style.left = Xpos + 'px';
    div.style.top = Ypos + 'px';
}

function hidediv(id) {
    //safe function to hide an element with a specified id
    if (document.getElementById) { // DOM3 = IE5, NS6
        document.getElementById(id).style.display = 'none';
    }
    else {
        if (document.layers) { // Netscape 4
            document.id.display = 'none';
        }
        else { // IE 4
            document.all.id.style.display = 'none';
        }
    }
}

function showPopupDiv(id) {
    //safe function to hide an element with a specified id
    if (document.getElementById) { // DOM3 = IE5, NS6
        document.getElementById(id).style.display = 'none';
    }
    else {
        if (document.layers) { // Netscape 4
            document.id.display = 'none';
        }
        else { // IE 4
            document.all.id.style.display = 'none';
        }
    }
}
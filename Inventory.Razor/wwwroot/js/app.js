//Active the anchor on redirecting
$(function () {
  var url = window.location.href;
  $(".sidebar .nav-collapse li  a").each(function () {
    if (url == (this.href)) {
      $(this).addClass('activeLink');
      $(this).parent('li').siblings('li').find('a').removeClass('activeLink');
      $(this).parents('.nav-item').addClass("submenu");
      $(this).parents('.collapse').addClass("show");
      $(this).parents('.nav-item ').children('a').attr('aria-expanded', "true");
      $(this).parents('.nav-item ').siblings().children('a').attr('aria-expanded', "false");

      $(this).parent().addClass('submenu');
      $(this).parent().siblings().removeClass('submenu active');
      $(this).parent().siblings().find('.collapse').removeClass("show");
      $(this).attr('aria-expanded', "false");
      $(this).parent().siblings().children('a').attr('aria-expanded', "false");
    }
  });
});
// addClass if nav-item click and has subnav

//$(".sidebar .nav-item a").on('click', (function () {
//  if ($(this).parent().find('.collapse').hasClass("show")) {
//    $(this).parent().removeClass('submenu');
//    $(this).attr('aria-expanded', "true");
//    $(this).parent().siblings().children('a').attr('aria-expanded', "false");
//  } else {
//    $(this).parent().addClass('submenu');
//    $(this).parent().siblings().removeClass('submenu active');
//    $(this).parent().siblings().find('.collapse').removeClass("show");
//    $(this).attr('aria-expanded', "false");
//    $(this).parent().siblings().children('a').attr('aria-expanded', "false");
//  }
//}));
//vsteps
$('.vsteps').on('click', 'li', function (e) {
  e.preventDefault();
  $(this).addClass('toggle-vsteps').siblings('li').removeClass('toggle-vsteps')
  var curLi = $(this).data('href');
  $(curLi).fadeIn().siblings().fadeOut();
});
//hsteps
//$('.hsteps li').on('click', function () {
//    $(this).addClass('currentvstep').siblings('li').removeClass('currentvstep');
//});
//Edit steps
$(document).on('click', '.btn-editPreview', function (e) {
  e.preventDefault();
  var curEdit = $(this).data('edit');
  $(curEdit).click();
});
//
$('.sidebar .nav').addClass('px-4');
$('.btn-minimize').on('click', function () {
  $('.sidebar .nav').toggleClass('px-4')
});

$(document).on('change', '.custom-file-input', function () {
  let fileName = $(this).val().split('\\').pop();
  $(this).next('.custom-file-label').addClass("selected").html(fileName);
});

//Add row list
var dataId = 0;
$('.table-add').on('click', '.btn-addrow', function (e) {
  e.preventDefault();
  var rowClone = $(this).parents('tr').clone();
  $(rowClone).find('input[type=text],input[type=date],select').val("");
  $('.table-list').find('tbody').append(rowClone);
  var trID = $(rowClone).attr('data-id', dataId++);
  $(trID).data(id)
});

//Delete table row on confirmation
$('.table-list').on('click', '.btn-removerow', function (e) {
  e.preventDefault();
  $('#modalDelate').modal('show');
  var curRowId = $(this).parents('tr').data('id');
  $('#modalDelate').data('id', curRowId);
})
$('#modalDelate').on('click', '.confirmDeleteBtn', function (e) {
  e.preventDefault();
  var id = $('#modalDelate').data('id');
  $('[data-id=' + id + ']').remove();
  $('#modalDelate').modal('hide');
})

//Collapesable container
$('.btn-buycurrency').on('click', function (e) {
  e.preventDefault();
  var curLi = $(this).data('href');
  $(curLi).slideToggle().siblings('.collapsed-container').slideUp();
});
//Toggle the steplist
//$('.steplist h2').on('click', function () {
//    $(this).parent('.card').find('.card-body').slideToggle();
//    $(this).parent('.card').siblings('.card').find('.card-body').slideUp();
//    $(this).parent('.card').find('.caret').toggleClass('toggle-stepcaret');
//    $('.steplist .card:first-child .card-body')
//    $(this).parent('.card').siblings('.card').find('.caret').removeClass('toggle-stepcaret');
//});
// new hsteps
$('.hsteps').on('click', 'li', function (e) {
  e.preventDefault();
  $(this).addClass('toggle-hsteps').siblings('li').removeClass('toggle-hsteps')
  var curLi = $(this).data('href');
  $(curLi).slideDown().siblings().slideUp();
});
/*
filedrag.js - HTML5 File Drag & Drop demonstration
Featured on SitePoint.com
Developed by Craig Buckler (@craigbuckler) of OptimalWorks.net
*/
(function () {

  // getElementById
  function $id(id) {
    return document.getElementById(id);
  }


  // output information
  function Output(msg) {
    var m = $id("messages");
    m.innerHTML = msg + m.innerHTML;
  }


  // file drag hover
  function FileDragHover(e) {
    e.stopPropagation();
    e.preventDefault();
    e.target.className = (e.type == "dragover" ? "hover" : "");
  }


  // file selection
  function FileSelectHandler(e) {

    //if (!e || !e.target || !e.target.files || e.target.files.length === 0) {
    //  return;
    //}
    // cancel event and hover styling
    FileDragHover(e);

    // fetch FileList object
    var files = e.target.files || e.dataTransfer.files;

    // process all File objects
    for (var i = 0, f; f = files[i]; i++) {
      ParseFile(f);
      UploadFile(f);
    }

  }

  var index = 0;

  // output file information
  function ParseFile(file) {
    index++;

    Output(
      '<div class="upload-list" id="uploadFile-' + index + '">' +
      "<p><strong class='d-block'>" + file.name + ":</strong>" +
      "File info: <strong>" + file.name +
      "</strong> type: <strong>" + file.type +
      "</strong> size: <strong>" + file.size +
      "</strong> bytes<i class='fas fa-times btn-closelist'></i></p>" +
      "</div>"
    );

    // display an image
    if (file.type.indexOf("image") == 0) {
      var reader = new FileReader();
      reader.onload = function (e) {

        var d = $id("uploadFile-" + index);
        d.innerHTML = '<img src="' + e.target.result + '" />' + d.innerHTML;
      }

      reader.readAsDataURL(file);
    }

    // display text
    if (file.type.indexOf("text") == 0) {
      var reader = new FileReader();
      reader.onload = function (e) {
        Output(
          "<p><strong>" + file.name + ":</strong></p><pre>" +
          e.target.result.replace(/</g, "&lt;").replace(/>/g, "&gt;") +
          "</pre>"
        );
      }
      reader.readAsText(file);
    }

  }

  // upload JPEG files
  function UploadFile(file) {
    //// following line is not necessary: prevents running on SitePoint servers
    //if (location.host.indexOf("sitepointstatic") >= 0) return

    var xhr = new XMLHttpRequest();
    var formData = new FormData()
    if (xhr.upload && file.type == "image/jpeg" && file.size <= $id("MAX_FILE_SIZE").value) {

      //// create progress bar
      //var o = $id("progress");
      //var progress = o.appendChild(document.createElement("p"));
      //progress.appendChild(document.createTextNode("upload " + file.name));


      //// progress bar
      //xhr.upload.addEventListener("progress", function (e) {
      //  var pc = parseInt(100 - (e.loaded / e.total * 100));
      //  progress.style.backgroundPosition = pc + "% 0";
      //}, false);

      //// file received/failed
      //xhr.onreadystatechange = function (e) {
      //  if (xhr.readyState == 4) {
      //    progress.className = (xhr.status == 200 ? "success" : "failure");
      //  }
      //};

      // start upload
      xhr.open("POST", $id("upload").action, true);
      xhr.setRequestHeader("X_FILENAME", file.name);
      formData.append('file', file)
      xhr.send(formData);
    }
  }

  // initialize
  function Init() {

    var fileselect = $id("fileselect"),
      filedrag = $id("filedrag"),
      submitbutton = $id("submitbutton");
    if (fileselect == null) {
      return;
    }
    // file select
    fileselect.addEventListener("change", FileSelectHandler, false);

    // is XHR2 available?
    var xhr = new XMLHttpRequest();
    if (xhr.upload) {

      // file drop
      filedrag.addEventListener("dragover", FileDragHover, false);
      filedrag.addEventListener("dragleave", FileDragHover, false);
      filedrag.addEventListener("drop", FileSelectHandler, false);
      filedrag.style.display = "block";

      // remove submit button
      submitbutton.style.display = "none";
    }

  }

  // call initialization file
  $(document).ready(function () {
    if (window.File && window.FileList && window.FileReader) {
      Init();
    }
  });


})();

//Delete the upload list
$(document).on('click', '.upload-list .btn-closelist', function () {
  $(this).parents('.upload-list').remove();
});
//File drag and drop ends here
//Auto  complete
function autocomplete(inp, arr) {
  if (inp == undefined || inp == null) {
    return;
  }

  var currentFocus;
  inp.addEventListener("input", function (e) {
    var a, b, i, val = this.value;
    var parent;
    closeAllLists();
    if (!val) { return false; }
    currentFocus = -1;
    parent = document.getElementById("autocompleteWrapper");
    a = document.createElement("DIV");
    a.setAttribute("id", "autocompleteList");
    a.setAttribute("class", "autocomplete-items");
    parent.appendChild(a);
    for (i = 0; i < arr.length; i++) {
      if ((arr[i].name.substr(0, val.length).toUpperCase() == val.toUpperCase())
        || (arr[i].phone.substr(0, val.length).toUpperCase() == val.toUpperCase())
        || (arr[i].email.substr(0, val.length).toUpperCase() == val.toUpperCase())) {
        b = document.createElement("DIV");
        b.setAttribute("class", "selected-item");
        b.innerHTML = "<span>" + "Name:" + arr[i].name.substr(0, val.length) + "</span>";
        b.innerHTML += arr[i].name.substr(val.length);
        b.innerHTML += "<span>" + " Phone:" + arr[i].phone.substr(0, val.length) + "</span>";
        b.innerHTML += arr[i].phone.substr(val.length);
        b.innerHTML += "<span>" + " Email:" + arr[i].email.substr(0, val.length) + "</span>";
        b.innerHTML += arr[i].email.substr(val.length);
        b.innerHTML += "<input type='hidden' value='" + arr[i].name + "'>";
        b.addEventListener("click", function (e) {
          inp.value = this.getElementsByTagName("input")[0].value;
          closeAllLists();
        });
        a.appendChild(b);
      }
    }
    //Dragable search list
    Sortable.create(autocompleteList, {
      group: {
        name: 'shared',
        pull: 'clone',
        put: false,// Do not allow items to be put into this list
        sort: false
      },
      selectedClass: "selected",
      animation: 150
    });
    Sortable.create(selectedCustomer, {
      group: 'shared',
      selectedClass: "selected",
      animation: 150,
      sort: false
    });

  });
  inp.addEventListener("keydown", function (e) {
    var x = document.getElementById("autocompleteList");
    if (x) x = x.getElementsByTagName("div");
    if (e.keyCode == 40) {
      currentFocus++;
      addActive(x);
    } else if (e.keyCode == 38) { //up
      currentFocus--;
      addActive(x);
    } else if (e.keyCode == 13) {
      e.preventDefault();
      if (currentFocus > -1) {
        if (x) x[currentFocus].click();
      }
    }
  });
  function addActive(x) {
    if (!x) return false;
    removeActive(x);
    if (currentFocus >= x.length) currentFocus = 0;
    if (currentFocus < 0) currentFocus = (x.length - 1);
    x[currentFocus].classList.add("autocomplete-active");
  }
  function removeActive(x) {
    for (var i = 0; i < x.length; i++) {
      x[i].classList.remove("autocomplete-active");
    }
  }
  function closeAllLists(elmnt) {
    var x = document.getElementsByClassName("autocomplete-items");
    for (var i = 0; i < x.length; i++) {
      if (elmnt != x[i] && elmnt != inp) {
        x[i].parentNode.removeChild(x[i]);
      }
    }
  }
  document.addEventListener("click", function (e) {
    closeAllLists(e.target);
  });
}

/*An array containing all the country names in the world:*/
//var countries = ["Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua & Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia & Herzegovina", "Botswana", "Brazil", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central Arfrican Republic", "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica", "Cote D Ivoire", "Croatia", "Cuba", "Curacao", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "French Polynesia", "French West Indies", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kosovo", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauro", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Korea", "Norway", "Oman", "Pakistan", "Palau", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre & Miquelon", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Korea", "South Sudan", "Spain", "Sri Lanka", "St Kitts & Nevis", "St Lucia", "St Vincent", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga", "Trinidad & Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks & Caicos", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States of America", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe"];
var countries = [
  { name: "Sender1", phone: "9641234566", email: "donor1@abc.com" },
  { name: "Sender2", phone: "9711234567", email: "donor2@abc.com" },
  { name: "Sender3", phone: "9111234567", email: "donor3@abc.com" },
  { name: "Receiver1", phone: "9641234568", email: "beneficiary1@abc.com" },
  { name: "Receiver2", phone: "9711234569", email: "beneficiary2@abc.com" },
  { name: "Receiver3", phone: "9111234569", email: "beneficiary3@abc.com" }
];

/*initiate the autocomplete function on the "myInput" element, and pass along the countries array as possible autocomplete values:*/
autocomplete(document.getElementById("myInput"), countries);

//Sidebar toggle on click and hover events
$('.btn-minimize').on('click', function () {
  $('html').toggleClass('sidebar_minimize');
});


$(".sidebar").hover(function () {
  if ($('html').hasClass('sidebar_minimize')) {
    $('html').toggleClass('sidebar_minimize_hover');
  }
});
//Currency Viewer
$('.selectcurrency-dropdown .dropdown-menu .dropdown-item').on('click', function (e) {
  var countryCurrency = $(this).html();
  $('.selectcurrency-dropdown .btn-outline-secondary').html(countryCurrency);
  $('.currency-media').find('img').attr('src', $('.selectcurrency-dropdown .btn-outline-secondary').find('img').attr('src'));
  $('.currency-media').find('h5').text($('.selectcurrency-dropdown .btn-outline-secondary').find('.Currency-code').text());
  $('.currency-media').find('h4').text($('.selectcurrency-dropdown .btn-outline-secondary').find('.country-name').text());
  var curLi = $(this).data('currency');
  $(curLi).slideDown().siblings('.currency-list,.currencybox').slideUp();
});
$('.currency-list').on('click', 'li.dropdown', function () {
  $(this).find('.menudropdown').slideDown();
});
$('.currency-list').on('click', 'li:not(li.dropdown)', function () {
  $('.menudropdown').slideUp();
  var curLi = $(this).data('note');
  $(curLi).slideDown().siblings().slideUp();
});
$('.menudropdown').on('click', '[data-note]', function () {
  var curLi = $(this).data('note');
  $(curLi).slideDown().siblings().slideUp();
});
//Currency Viewer ends here






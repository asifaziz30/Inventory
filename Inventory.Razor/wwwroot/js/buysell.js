(function ($) {

  'use strict';
  /*
  Wizard #4
  */
  var $w4finish = $('#w4').find('ul.pager li.finish'),
    $w4validator = $("#w4 form").validate({
      highlight: function (element) {
        $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
      },
      success: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
        $(element).remove();
      },
      errorPlacement: function (error, element) {
        element.parent().append(error);
      }
    });

  $w4finish.on('click', function (ev) {
    ev.preventDefault();
    $('#w4 #confirmationModal').show();
  });

  $('#w4').bootstrapWizard({
    tabClass: 'wizard-steps',
    nextSelector: 'ul.pager li.next',
    previousSelector: 'ul.pager li.previous',
    firstSelector: null,
    lastSelector: null,
    onNext: function (tab, navigation, index, newindex) {

      var formData = new FormData();
      var files = $('input[type=file]');
      for (var i = 0; i < files.length; i++) {
        if (files[i].value == "" || files[i].value == null) {
          //return false;
        }
        else {
          formData.append(files[i].name, files[i].files[0]);
        }
      }
      var formSerializeArray = $('#w4 form').serializeArray();
      for (var i = 0; i < formSerializeArray.length; i++) {
        formData.append(formSerializeArray[i].name, formSerializeArray[i].value)
      }
      $.ajax({
        type: "POST",
        url: window.buysellUrl,
        enctype: 'multipart/form-data',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
          $('#diverror').html('');
          let step = result?.Result?.step;
          if (result?.redirect !== undefined) {
            window.location.href = result.redirect;
          }
          $('#Step').val(step);
          if (step == undefined) {
            $('#preview').html(result);
          }
        },
        error: function (response) {
          loadErrors(response.responseJSON);
          $w4validator.focusInvalid();
          return false;
        }
      });
    },
    onTabChange: function (tab, navigation, index, newindex) {
      var $total = navigation.find('li').length - 1;
      $w4finish[newindex != $total ? 'addClass' : 'removeClass']('hidden');
      $('#w4').find(this.nextSelector)[newindex == $total ? 'addClass' : 'removeClass']('hidden');
    },
    onTabShow: function (tab, navigation, index) {
      var $total = navigation.find('li').length - 1;
      var $current = index;
      var $percent = Math.floor(($current / $total) * 100);
      $('#Step').val(index + 1);

      navigation.find('li').removeClass('active');
      navigation.find('li').eq($current).addClass('active');

      $('#w4').find('.progress-indicator').css({ 'width': $percent + '%' });
      tab.prevAll().addClass('completed');
      tab.nextAll().removeClass('completed');
    }
  });

}).apply(this, [jQuery]);

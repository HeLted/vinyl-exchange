import $ from "jquery"

export default function hideModal() {
  $(".modal").hide();
  $(".modal-backdrop").hide();
  $("body").removeClass("modal-open");
  $("body").css("padding","0px");
}

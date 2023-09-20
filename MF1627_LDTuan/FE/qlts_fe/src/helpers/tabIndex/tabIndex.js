/**
 * Nhấn tab để di chuyển giữa các component
 * @param {*} event
 * @param {int} index số index của các component
 * Author: LDTUAN (20/08/2023)
 */
export function checkTabIndex(event, index) {
  var charCode = event.which ? event.which : event.keyCode;
  if (index == "islast" && charCode == this.$_MISAEnum.KEYCODE.TAB &&
  this.toast_content_warning) {
    event.preventDefault();
    this.$refs[this.buttonFocus].focusButton();
  }else if(index == "islast" && charCode == this.$_MISAEnum.KEYCODE.TAB &&
  !this.toast_content_warning){
    event.preventDefault();
    this.$refs[this.inputFocus].focusButton();
  }
}

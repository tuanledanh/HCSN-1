<template>
  <div class="header" :class="{'collapsed-width': collapsed}">
    <div class="header-left">Danh sách tài sản</div>
    <div class="header-right">
      <div class="header-right-text">Sở tài chính</div>
      <div class="header-right-year-option border-radius">
        <span>Năm</span>
        <span class="year-option">2021</span>
        <span class="year-option-icon"></span>
      </div>
      <div class="header-right-icons">
        <div class="icon-container">
          <div class="bell-icon"></div>
          <div class="tooltips-text">Thông báo</div>
        </div>
        <div class="icon-container">
          <div class="window-icon"></div>
          <div class="tooltips-text">Menu</div>
        </div>
        <div class="icon-container">
          <div class="question-mark-icon"></div>
          <div class="tooltips-text">Hỏi đáp</div>
        </div>
        <div class="icon-container">
          <div class="user-chevd-container">
            <div class="user-icon"></div>
            <div class="chevd-icon"></div>
          </div>
          <div class="tooltips-text">Tài khoản</div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["collapsed"],
};
</script>

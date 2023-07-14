<template>
  <div class="combo-toast">
    <div class="toast toast--unSelect">
      <div class="toast-container">
        <div class="toast__item toast__item--error">
          <div class="toast__item-icon">
            <i class="fas fa-exclamation-triangle"></i>
          </div>
          <div class="toast__item-text">Đã bỏ chọn 1 bản ghi</div>
          <div class="toast__item-close"><i class="fas fa-times"></i></div>
        </div>
      </div>
    </div>
    <div class="toast toast--select">
      <div class="toast-container">
        <div class="toast__item toast__item--success">
          <div class="toast__item-icon">
            <i class="fas fa-check-circle"></i>
          </div>
          <div class="toast__item-text">Đã chọn 1 bản ghi</div>
          <div class="toast__item-close"><i class="fas fa-times"></i></div>
        </div>
      </div>
    </div>
  </div>
</template>

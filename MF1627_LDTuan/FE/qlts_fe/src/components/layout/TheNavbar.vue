<template>
  <div class="navbar">
    <div class="nav-left">Danh sách tài sản</div>
    <div class="nav-right">
      <span class="nav__span">Sở tài chính</span>
      <!-- <div class="input--year">
        <span class="input--span">Năm</span>
        <input type="text" value="2021" />
        <div class="icon--add-sub">
          <div class="icon--pull-up"></div>
          <div class="icon--drop-down"></div>
        </div>
      </div> -->
      <MISAInputYear></MISAInputYear>
      <div class="nav__icon-wrapper">
        <MISAIcon notification tooltip content="Thông báo" position="bot"></MISAIcon>
        <MISAIcon utilities tooltip content="Menu" position="bot"></MISAIcon>
        <MISAIcon question tooltip content="Hỏi đáp" position="bot"></MISAIcon>
        <MISAIcon user tooltip content="Profile" position="bot"></MISAIcon>
        <MISAIcon drop_down tooltip content="Tùy chọn" position="left-300"></MISAIcon>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheNavbar",
};
</script>

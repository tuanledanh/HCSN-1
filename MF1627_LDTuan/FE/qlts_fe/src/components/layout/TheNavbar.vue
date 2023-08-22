<template>
  <div class="navbar">
    <div v-if="contentDisplay" class="nav-left">{{ contentDisplayText }}</div>
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
        <div class="left">
          <MISATooltip bottom content="Thông báo">
            <MISAIcon notification></MISAIcon>
          </MISATooltip>
          <MISATooltip bottom content="Tiện ích">
            <MISAIcon utilities></MISAIcon
          ></MISATooltip>
          <MISATooltip bottom content="Hỏi đáp">
            <MISAIcon question></MISAIcon
          ></MISATooltip>
          <MISATooltip bottom content="Thông tin">
            <MISAIcon user></MISAIcon
          ></MISATooltip>
        </div>
        <div class="right">
          <MISATooltip bottom_end content="Mở rộng">
            <MISAIcon drop_down navbar></MISAIcon
          ></MISATooltip>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheNavbar",
  props: {
    contentDisplay: {
      type: Boolean,
      default: false,
    },
    contentDisplayText: {
      type: String,
      default: null,
    },
  },
};
</script>

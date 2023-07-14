<template>
  <div class="navbar">
    <div class="nav-left">Danh sách tài sản</div>
    <div class="nav-right">
      <span class="nav__span">Sở tài chính</span>
      <div class="input--year">
        <span class="input--span">Năm</span>
        <input type="text" value="2021" />
        <div class="icon--add-sub">
          <div class="icon--pull-up"></div>
          <div class="icon--drop-down"></div>
        </div>
      </div>
      <div class="nav__icon-wrapper">
        <MISAIcon
          nav
          iconName="icon--notification"
          tooltipText="Notification"
        ></MISAIcon>
        <MISAIcon
          nav
          iconName="icon--utilities"
          tooltipText="Menu"
        ></MISAIcon>
        <MISAIcon
          nav
          iconName="icon--question"
          tooltipText="Question"
        ></MISAIcon>
        <!-- <div class="icon-nav">
          <div class="icon-nav--combo">
            <div class="icon--user"></div>
            <div class="icon--drop-down">
              <select name="" id="" @change="onChangeLanguage($event)">
                  <option value="VN">Tiếng Việt</option>
                  <option value="EN">Tiếng Anh</option>
                </select>
            </div>
          </div>
        </div> -->
        <MISAIcon
          nav
          iconName="icon--user"
          tooltipText="Profile"
        ></MISAIcon>
        <MISAIcon
          nav
          iconName="icon--drop-down"
          tooltipText="Expand"
        ></MISAIcon>
      </div>
    </div>
  </div>
</template>
<script>
import app from "@/main.js";
export default {
  name: "TheNavbar",
  mounted() {},
  methods: {
    /**
     * Thay đổi ngôn ngữ theo loại đã chọn
     * @param {*} event bắt sự kiện onchange của dropdown
     * Author: LDTUAN (03/07/2023)
     */
    onChangeLanguage(event) {
      const langCode = event.currentTarget.value;
      app.config.globalProperties.$_LANG_CODE = langCode;
    },
  },
};
</script>

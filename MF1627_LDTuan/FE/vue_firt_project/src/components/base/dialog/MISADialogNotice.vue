<template>
  <div class="dialog" id="dialog-notice">
    <div class="dialog-wrapper">
      <div class="dialog__header">
        <div class="header__text">Dữ liệu không hợp lệ</div>
        <button class="button button--sub button--sub-non-border button--empty icon-content-top"
          @click="btnCloseOnClick()">
          <div class="icon--exit"></div>
        </button>
      </div>
      <div class="dialog__content">
        <div class="dialog-content__icon">
          <div class="icon--warning"></div>
        </div>
        <ul class="dialog-content__message">
          <li v-for="(item, index) in errors" :key="index">{{ item }}</li>
        </ul>
      </div>
      <div class="dialog__footer">
        <button class="button button--main">Đồng ý</button>
      </div>
    </div>
  </div>
  </template>
  <script>
  export default {
    name: "MISADialogNotice",
    props: ["errors"],
    methods: {
      /**
       * Thưc hiện đóng dialog khi click icon exit
       * Author: LDTUAN (04/07/2023)
       */
      btnCloseOnClick(){
        this.$emit("onCloseDialog");
      }
    },
    data() {
      return {};
    },
  };
  </script>
  
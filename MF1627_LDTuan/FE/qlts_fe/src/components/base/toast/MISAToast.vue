<template>
  <div
    :class="[
      'toast__message',
      { 'toast--center': typeToast === 'warning' || typeToast === 'export' },
      { 'toast--bottom': typeToast === 'success' || typeToast === 'update' },
    ]"
  >
    <section class="t-toast br-8 flex-column">
      <main class="t-toast__content center-y">
        <section
          class="t-toast--icon-warning center"
          v-if="typeToast === 'warning'"
        >
          <MISAIcon warning></MISAIcon>
        </section>
        <section
          class="t-toast--icon-success center"
          v-if="typeToast === 'success'"
        >
          <MISAIcon success></MISAIcon>
        </section>
        <section
          class="t-toast--icon-success center"
          v-if="typeToast === 'update'"
        >
          <MISAIcon success></MISAIcon>
        </section>
        <section
          class="t-toast--icon-warning center"
          v-if="typeToast === 'export'"
        >
          <MISAIcon export_toast></MISAIcon>
        </section>
        <div class="message">
          <p
            :class="[
              {
                't-toast--title-16px':
                  typeToast === 'warning' || typeToast === 'export',
              },
              { 't-toast--title-14px': typeToast === 'success' || typeToast === 'update'},
            ]"
          >
            <span v-html="content"></span>
          </p>
          <div v-if="moreInfo" class="moreInfo">
            <div
              v-if="displayInfo"
              @click="btnViewInfo"
              class="t-toast--title-16px view"
            >
              Ẩn chi tiết phát sinh
            </div>
            <div v-else @click="btnViewInfo" class="t-toast--title-16px view">
              Xem chi tiết phát sinh
            </div>
            <div v-if="displayInfo" class="moreInfo-text">
              <span
                v-for="mess in moreInfoList"
                :key="mess"
                class="t-toast--title-16px"
                v-html="mess"
              ></span>
            </div>
          </div>
        </div>
      </main>
      <footer
        class="t-toast__footer center-y"
        v-if="typeToast === 'warning' || typeToast === 'export'"
      >
        <slot> </slot>
      </footer>
    </section>
  </div>
</template>
<script>
export default {
  name: "MISAToast",
  props: {
    // Loại toast hiển thị
    typeToast: {
      type: String,
      default: "warning",
      validator: function (value) {
        return ["success", "warning", "export", "update"].indexOf(value) !== -1;
      },
    },

    // Nội dung của thông báo
    content: {
      type: String,
      default: "",
    },
    // Nội dung phụ
    moreInfo: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      // Xem thông tin chi tiết phát sinh hay không
      displayInfo: false,
      // moreInfo sau khi tách
      moreInfoList: [],
    };
  },
  created() {
    if (this.moreInfo) {
      this.moreInfoList = this.moreInfo.split(",").map((item) => item.trim());
    }
  },

  methods: {
    btnViewInfo() {
      this.displayInfo = !this.displayInfo;
    },
  },
};
</script>
<style scoped>
.t-toast {
  background-color: #fff;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
  width: 500px;
  padding: 14px 30px 14px 40px;
  row-gap: 30px;
}

.t-toast__footer {
  height: 50px;
  text-align: right;
  justify-content: right;
  column-gap: 16px;
}
.t-toast__content {
  column-gap: 30px;
}

.t-toast--icon-warning {
  border-radius: 50%;
  height: 30px;
  aspect-ratio: 1;
}
.warning {
  background-position: -416px -66px;
  width: 24px;
  height: 20px;
  transform: scale(1.5);
}

.t-toast--icon-success {
  background-color: #1ac871;
  border-radius: 50%;
  height: 30px;
  aspect-ratio: 1;
  box-shadow: 0 0 0 5px #baeed4;
}

.t-toast--icon-update {
  background-color: #c8821a;
  border-radius: 50%;
  height: 30px;
  aspect-ratio: 1;
  box-shadow: 0 0 0 5px #eedaba;
}

.success {
  background-position: -71px -512px;
  width: 11px;
  height: 8px;
}

.update {
  background-position: -71px -512px;
  width: 11px;
  height: 8px;
}

.t-toast--title-16px {
  font-size: 16px;
  word-wrap: break-word;
  word-break: keep-all;
}

.t-toast--title-14px {
  font-size: 14px;
  word-wrap: break-word;
  word-break: keep-all;
}

.bold {
  font-weight: 700;
}

.toast__message {
  position: absolute;
  box-sizing: border-box;
}
.toast--center {
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.toast--bottom {
  right: 30px;
  bottom: 80px;
}

.toast--bottom .t-toast {
  padding: 6px 30px;
}

.message {
  display: flex;
  flex-direction: column;
  column-gap: 20px;
}

.view {
  color: #00b7e5;
  cursor: pointer;
}

.moreInfo {
  display: flex;
  flex-direction: column;
  row-gap: 6px;
}

.moreInfo-text {
  display: flex;
  flex-direction: column;
  row-gap: 6px;
  max-height: 45px;
  overflow-y: auto;
}
</style>

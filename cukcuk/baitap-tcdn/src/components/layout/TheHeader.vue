<template>
  <div class="header-wrapper">
    <div class="header">
      <div
        class="header-icon mi mi-24"
        :class="{ disabled: disabled }"
        @click="toggleMenu()"
      ></div>
      <div class="header-branch pointer">
        <div class="header-branch-name">
          <span>CÔNG TY CỔ PHẦN MISA</span>
        </div>
        <div class="header-branch-option mi mi-14"></div>
      </div>
      <!-- <div class="header-tooltip">
            <div class="header-current-db">
                <div class="header-tooltip-onl"></div>
                <div class="header-tooltip-name">
                    <span>DC-01</span>
                </div>
            </div>
        </div>
        <div class="header-space"></div>
        <div class="header-download"></div>
        <div class="header-search">
            <Input 
                :placeholder="'Nhập từ khóa tìm kiếm'" 
                :timer="1000" 
                :searchClass="'search-container'"
            />
        </div>
        <div class="header-icon-btn d-flex alignt-center">
            <div class="header-icon mi mi-24 option-icon"></div>
        </div>
        
        <div class="header-icon-btn d-flex alignt-center">
            <div class="header-icon mi mi-24 setting-icon"></div>
        </div>
        <div class="header-icon-btn d-flex alignt-center">
            <div class="header-icon mi mi-24 message-icon"></div>
            
        </div>
        <div class="header-icon-btn d-flex alignt-center">
            <div class="header-icon mi mi-24 help-icon"></div>

        </div> -->
      <div class="header-space"></div>
      <div class="header-icon-btn d-flex alignt-center">
        <div class="header-icon mi mi-24 notify-icon"></div>
      </div>
      <div class="info-container">
        <button class="info-button">
          <div class="info-icon mi mi-32"></div>
          <div class="info">
            <span>Nguyễn Quang Vinh</span>
          </div>
          <div class="info-option mi mi-14"></div>
        </button>
      </div>
    </div>
  </div>
</template>
<script>
/**
 * Mô tả: Header
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 16/04/2022
 */
// import Input from '../base/BaseInput.vue'
export default {
  // components:{
  //     Input
  // },
  data() {
    return {
      disabled: false,
    };
  },
  props: {
    isDisabled: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    isDisabled: function () {
      this.disabled = this.isDisabled;
      console.log(this.isDisabled);
    },
  },
  methods: {
    //Thu nhỏ menu
    toggleMenu() {
      this.disabled = true;
      this.$emit("toggle", true);
    },
  },
};
</script>
<style scoped>
.header-wrapper {
  position: absolute;
  top: 0;
  width: 100%;
  z-index: 8;
}
.header {
  background-color: fff;
  height: 48px;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: #fff;
}
.header-search {
  margin-right: 20px;
}
.header-icon-btn {
  position: relative;
  height: 100%;
}
.header-icon-btn::after {
  content: "";
  position: absolute;
  width: 0;
  height: 0;
  border-left: 23px solid transparent;
  border-right: 23px solid transparent;

  top: 100%;
  left: 0;
}
.header-icon-btn:hover {
  background-color: #4ea344;
}
.header-icon-btn:hover::after {
  border-top: 10px solid #4ea344;
}
.header-icon {
  background-position: -843px -30px;
  margin: 0 10px;
  cursor: pointer;
}
.header-icon-btn:hover > .header-icon {
  background-position: -568px -76px;
}
.help-icon {
  background-position: -731px -30px;
}
.header-icon-btn:hover > .help-icon {
  background-position: -201px -89px;
}
.option-icon {
  background-position: -568px -30px;
}
.header-icon-btn:hover > .option-icon {
  background-position: -568px -76px;
}
.setting-icon {
  background-position: -675px -30px;
}
.header-icon-btn:hover > .setting-icon {
  background-position: -144px -88px;
}
.message-icon {
  background-position: -899px -30px;
}
.header-icon-btn:hover > .message-icon {
  background-position: -368px -88px;
}
.notify-icon {
  background-position: -788px -30px;
}
.header-icon-btn:hover > .notify-icon {
  background-position: -257px -88px;
}
.header-branch {
  display: flex;
  align-items: center;
  font-family: MISA notosans bold;
  font-size: 14px;
  padding-left: 10px;
}
.header-branch-name {
  white-space: nowrap;
  overflow: hidden;
}
.header-branch-option {
  background-position: -80px -360px;
  transform: rotate(90deg);
  margin: 0 10px;
}
.header-current-db {
  display: flex;
  margin-left: 15px;
  padding: 0 12px;
  border: 1px solid var(--border-color);
  align-items: center;
  border-radius: 40px;
  font-size: 13px;
  height: 30px;
}
.header-tooltip-onl {
  background-color: #4ea344;
  width: 6px;
  height: 6px;
  border-radius: 50%;
  margin-right: 10px;
}
.header-space {
  flex: 1;
  height: 100%;
}

.info-container {
  font-family: MISA notosans bold;
  font-size: 14px;
  padding-right: 20px;
}
.info-button {
  display: flex;
  align-items: center;
  background: #fff;
  border: unset;
  outline: unset;
  padding: 0 12px;
  height: 100%;
}
.info-icon {
  border: 1px solid var(--border-color);
  border-radius: 50%;
  margin-right: 10px;
}
.info {
  margin-right: 10px;
}

.info-option {
  background-position: -1076px -32px;
}
.disabled {
  display: none;
}
@media screen and (max-width: 1280px) {
  .header-tooltip {
    margin-right: 10px;
  }
  .header-tooltip span {
    white-space: nowrap;
    display: block;
  }
  .header-search {
    min-width: 50px;
  }
}
</style>

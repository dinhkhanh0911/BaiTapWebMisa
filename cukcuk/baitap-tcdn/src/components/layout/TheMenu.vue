<template>
  <div class="menu-container" :class="{ 'zoom-out': !isZoomOut }">
    <div class="logo-container">
      <div class="header-icon mi mi-24" @click="zoomInMenu()"></div>
      <a class="menu-container-option"> </a>
      <a class="logo">
        <img
          src="@/assets/img/Logo_Module_TiengViet_White.66947422.svg"
          alt=""
          height="32"
        />
      </a>
    </div>
    <TheNavbar :toggle="isZoomOut" />
  </div>
</template>

<script>
import TheNavbar from "@/components/layout/TheNavbar.vue";

/**
 * Mô tả: Menu
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 18/04/2022
 */
export default {
  components: { TheNavbar },
  data() {
    return {
      isZoomOut: true,
    };
  },
  props: {
    toggle: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    //
    toggle: function () {
      if (this.toggle) {
        this.isZoomOut = !this.toggle;
        console.log(this.isZoomOut);
      }
    },
  },
  methods: {
    //Mở rộng menu
    zoomInMenu() {
      this.isZoomOut = true;
      console.log(this.isZoomOut);
      this.$emit("zoomOut", false);
    },
  },
};
</script>
<style scoped>
.menu-container {
  height: 100vh;
  width: var(--menu-width);
  background-color: #393a3d;
}

.logo-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 48px;
  cursor: pointer;
}
.logo {
  height: 32px;
  cursor: pointer;
}
.menu-container-option {
  display: block;
  width: 24px;
  height: 24px;
  margin-right: 10px;
  background: url("@/assets/img/Sprites.64af8f61.svg") no-repeat -424px -86px;
}
.header-icon {
  background-position: -313px -32px;
  margin: 0 10px;
  display: none;
}
.zoom-out {
  width: 52px;
}
.zoom-out .logo-container {
  height: 48px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.zoom-out .menu-container-option,
.zoom-out .logo {
  display: none !important;
}
.zoom-out .header-icon {
  display: block;
}
</style>

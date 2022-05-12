<template>
  <div class="toast" :class="typeToast[type].class">
    <div class="mi mi-48 toast-icon"></div>
    <div class="toast-content">
      <span>{{ message }}</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      typeToast: {
        success: {
          class: "toast-success",
        },
        error: {
          class: "toast-error",
        },
        warning: {
          class: "toast-warning",
        },
        info: {
          class: "toast-info",
        },
      },
    };
  },
  props: {
    message: {
      type: String,
      default: "Thành công",
    },
    type: {
      type: String,
      default: "success",
    },
    show: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style>
@keyframes moveRightToLeft {
  from {
    transform: translateX(100%);
  }
  to {
    transform: translateX(0);
  }
}
@keyframes fadeOut {
  to {
    opacity: 0;
  }
}

.toast {
  height: 80px;
  min-width: 300px;
  border-radius: 4px;
  background-color: #fff;
  display: flex;
  align-items: center;
  padding-left: 16px;
  padding-right: 10px;
  box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.16);
  border: 1px solid #2ca01c;
  justify-items: left;
  animation: moveRightToLeft linear 0.3s, fadeOut 0.2s 2s forwards;
}
.toast + .toast {
  margin-top: 24px;
}
.toast-success .toast-icon {
  background-position: -984px -456px;
}
.toast-error .toast-icon {
  background-position: -752px -456px;
}
.toast-content {
  flex: 1;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: left;
  font-size: 13px;
  padding: 20px;
}
.toast-close button {
  border: none;
  background-position: -1189px -515px;
  background-color: #fff;
  margin-top: 10px;
}
</style>

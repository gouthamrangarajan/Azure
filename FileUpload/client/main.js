import "./style.css";

const app = {
  setup() {
    return { msg: "Welcome" };
  },
};
const vueApp = Vue.createApp(app);
vueApp.component("app-nav", {
  template: "#nav",
});
vueApp.component("file-upload", {
  template: "#upload",
  setup() {
    const uploadRef = Vue.ref(null);
    const fileName = Vue.ref("");
    const fileChanged = () => {
      if (uploadRef.value.files.length > 0)
        fileName.value = uploadRef.value.files[0].name;
      else fileName.value = "";
    };
    return { uploadRef, fileName, fileChanged };
  },
});
vueApp.mount("#app");
